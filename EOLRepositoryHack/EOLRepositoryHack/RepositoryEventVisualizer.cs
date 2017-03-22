using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EOLRepositoryHack.Logic;
using System.Collections;

namespace EOLRepositoryHack
{
    public partial class RepositoryEventVisualizer : UserControl
    {
        string _filePath;
        IEnumerable<ReportModel> _data;
        //List<EventModel> _tree;

        ListViewColumnSorter lvwColumnSorter;

        public RepositoryEventVisualizer(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;
            var reporter = new ReportReader(filePath);
            _data = reporter.ReadFile();

            var generator = new EventGenerator();

            //_tree = generator.GetEvents(_data);

            TreeNode root = new TreeNode("Events");
            treeViewEvents.Nodes.Add(root);

            //EventModel rootModel = new Logic.EventModel();
            //rootModel.ChildEvents.AddRange(_tree);

            var rootModel = generator.GetEvents2(_data);

            MakeTree(rootModel, root);
            CalculateSummary(rootModel);

            lvwColumnSorter = new ListViewColumnSorter();
            this.listViewSummary.ListViewItemSorter = lvwColumnSorter;
        }

        private void MakeTree(EventModel model, TreeNode parent)
        {
            foreach (var eventModel in model.ChildEvents)
            {
                TreeNode tn;
                if (!eventModel.ChildEvents.Any() || eventModel.ChildEvents.FirstOrDefault().Name == Logic.Events.Update)
                {
                    tn = new TreeNode(eventModel.Name);
                }
                else
                {
                    tn = new TreeNode($"{eventModel.Metadata} {eventModel.BCName}");
                }
                tn.Tag = eventModel;
                parent.Nodes.Add(tn);
                MakeTree(eventModel, tn);
            }
        }

        private void treeViewEvents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var eventModel = e.Node.Tag as EventModel;
            listViewDetails.Columns.Clear();
            listViewDetails.Items.Clear();
            listViewDetails.ShowGroups = true;

            listViewDetails.Columns.Add("-", 10);
            listViewDetails.Columns.Add("Name", 300);
            listViewDetails.Columns.Add("Deatils", 300);
            if (eventModel == null) return;

            ListViewItem item1 = new ListViewItem();
            item1.SubItems.Add("Event Name");
            item1.SubItems.Add(eventModel.Name);

            ListViewItem item2 = new ListViewItem();
            item2.SubItems.Add("Duration");
            item2.SubItems.Add(eventModel.Duration.ToString());

            ListViewItem item3 = new ListViewItem();
            item3.SubItems.Add("BC Name");
            item3.SubItems.Add(eventModel.BCName);

            ListViewItem item4 = new ListViewItem();
            item4.SubItems.Add("Extra Data");
            item4.SubItems.Add(eventModel.Metadata);

            listViewDetails.Items.Add(item1);
            listViewDetails.Items.Add(item2);
            listViewDetails.Items.Add(item3);
            listViewDetails.Items.Add(item4);
        }

        private void CalculateSummary(EventModel rootModel)
        {
            /*
             * Longest operation
             * Longest in average
             * Longest per action like:
             *    Set Default
             *    Pre Process
             *    Transformation
             *    PostProcess
             */

            var longestEvent = GetLongestEvent(rootModel);

            labelLongestEventName.Text = $"{longestEvent.Name}: {longestEvent.Metadata}";
            labelLongestEventDuration.Text = longestEvent.Duration.ToString();

            GetLongestInAveragePerAction(rootModel);
        }

        private void GetLongestInAveragePerAction(EventModel rootModel)
        {
            listViewSummary.Columns.Clear();
            listViewSummary.Items.Clear();
            listViewSummary.ShowGroups = true;

            listViewSummary.Columns.Add("-", 10);
            listViewSummary.Columns.Add("Event Name", 150);
            listViewSummary.Columns.Add("Total Duration", 150);
            listViewSummary.Columns.Add("Occurences", 150);
            listViewSummary.Columns.Add("Average", 150);

            var eventsTime = new Dictionary<string, Tuple<int, TimeSpan>>();
            GetSumPerEventType(rootModel, eventsTime);

            foreach (KeyValuePair<string, Tuple<int, TimeSpan>> item in eventsTime)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.SubItems.Add(item.Key);
                lvItem.SubItems.Add(item.Value.Item2.ToString());
                lvItem.SubItems.Add(item.Value.Item1.ToString());
                lvItem.SubItems.Add((item.Value.Item2.TotalMilliseconds / item.Value.Item1).ToString());

                listViewSummary.Items.Add(lvItem);
            }
        }

        private void GetSumPerEventType(EventModel rootModel, Dictionary<string, Tuple<int, TimeSpan>> eventsTime)
        {
            if (!rootModel.ChildEvents.Any())
            {
                if (eventsTime.ContainsKey(rootModel.Name))
                {
                    eventsTime[rootModel.Name] = Tuple.Create(1 + eventsTime[rootModel.Name].Item1, rootModel.Duration + eventsTime[rootModel.Name].Item2);
                }
                else
                {
                    eventsTime.Add(rootModel.Name, Tuple.Create(1, rootModel.Duration));
                }
                return;
            }

            foreach (var model in rootModel.ChildEvents)
            {
                if (eventsTime.ContainsKey(model.Name))
                {
                    eventsTime[model.Name] = Tuple.Create(1 + eventsTime[model.Name].Item1, model.Duration + eventsTime[model.Name].Item2);
                }
                else
                {
                    eventsTime.Add(model.Name, Tuple.Create(1, model.Duration));
                }
                GetSumPerEventType(model, eventsTime);
            }
        }

        private EventModel GetLongestEvent(EventModel rootModel)
        {
            if (!rootModel.ChildEvents.Any()) return rootModel;
            var maxEvent = rootModel.ChildEvents.FirstOrDefault();
            foreach (var model in rootModel.ChildEvents)
            {
                if (model.Duration > maxEvent.Duration)
                {
                    maxEvent = model;
                }
                var maxEventChilds = GetLongestEvent(model);
                maxEvent = maxEvent.Duration > maxEventChilds.Duration ? maxEvent : maxEventChilds;
            }
            return maxEvent;
        }

        private void listViewSummary_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listViewSummary.Sort();
        }

        private void listViewSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSummary.SelectedItems.Count > 0)
            {
                var eventName = listViewSummary.SelectedItems[0].SubItems[1].Text;
                treeViewEvents.Nodes[0].Expand();
                foreach (var node in treeViewEvents.Nodes[0].Nodes)
                {
                    HighlightEvents((TreeNode)node, eventName);
                }
                treeViewEvents.Nodes[0].EnsureVisible();
            }

        }

        private void HighlightEvents(TreeNode root, string eventname)
        {
            root.Expand();
            var rootEventModel = (EventModel)root.Tag;
            if (rootEventModel.Name == eventname)
            {
                root.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                root.BackColor = System.Drawing.Color.White;
            }
            if (!rootEventModel.ChildEvents.Any())
            {
                return;
            }
            foreach (var child in root.Nodes)
            {
                HighlightEvents((TreeNode)child, eventname);
            }
        }
    }
}
