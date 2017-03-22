using EOLRepoEventLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLogger
{
    class Program
    {
        static void Main()
        {
            var logger = EventLogger.Instance;
            logger.BookStartTransaction(Guid.NewGuid());
            logger.PersistPerExecutaionDay();
        }
    }
}
