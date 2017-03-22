using System;
using System.Windows.Forms;

namespace EOLRepositoryHack
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tabControl1.MouseUp += Tab_MouseUp;
        }

        private void openReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenNewReport();
                toolStripStatusLabelInfo.ForeColor = System.Drawing.Color.Green;
                toolStripStatusLabelInfo.Text = "Report is ready";
            }
            catch (Logic.EventGenerator.TempoEx ex)
            {
                toolStripStatusLabelInfo.ForeColor = System.Drawing.Color.Red;
                toolStripStatusLabelInfo.Text = $"Error happened: {ex.Message}";
            }
        }

        private void OpenNewReport()
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                var fileName = System.IO.Path.GetFileName(filePath);
                var control = new RepositoryEventVisualizer(filePath);
                control.Dock = DockStyle.Fill;
                var newPage = new TabPage(fileName);
                newPage.ToolTipText = "Midddle Click to close!";
                newPage.Controls.Add(control);
                newPage.Tag = filePath;
                tabControl1.Controls.Add(newPage);
                tabControl1.SelectedTab = newPage;
            }
        }

        private void Tab_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                tabControl1.Controls.Remove(((TabControl)sender).SelectedTab);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                var fileName = System.IO.Path.GetFileName(filePath);
                var control = new RepositoryEventVisualizerFromJson(filePath);
                control.Dock = DockStyle.Fill;
                var newPage = new TabPage(fileName);
                newPage.ToolTipText = "Midddle Click to close!";
                newPage.Controls.Add(control);
                newPage.Tag = filePath;
                tabControl1.Controls.Add(newPage);
                tabControl1.SelectedTab = newPage;
            }
        }
    }
}
