using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public sealed class Log : ILog
    {
        private Log()
        {

        }
        private static readonly Lazy<Log> _log = new Lazy<Log>(() => new Log());
        public static Log GetInstance
        {
            get
            {
                return _log.Value;
            }
        }
        public void LogException(string message)
        {
            string filename = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToString("yyMMdd"));
            string logfilepath = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, filename);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--------------------------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (StreamWriter sw = new StreamWriter(logfilepath))
            {
                sw.Write(sb.ToString());
                sw.Flush();
            }
        }
    }
}
