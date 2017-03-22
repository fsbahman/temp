using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLRepositoryHack.Logic
{
    public class ReportReader
    {
        readonly string _filepath;
        public ReportReader(string reportPath)
        {
            _filepath = reportPath;
        }

        public IEnumerable<ReportModel> ReadFile()
        {
            using (StreamReader fileStream = new StreamReader(_filepath))
            {
                while (!fileStream.EndOfStream)
                {
                    var oneLine = fileStream.ReadLine();
                    var details = oneLine.Split(',');
                    var model = new ReportModel
                    {
                        Identifier = details[0],
                        EventTime = new DateTime(long.Parse(details[1])),
                        EventName = details[2],
                        BCName = details[3],
                        Metadata = details.Length == 5 ? details[4] : ""
                    };
                    yield return model;
                }
                yield break;
            }
        }

    }
}
