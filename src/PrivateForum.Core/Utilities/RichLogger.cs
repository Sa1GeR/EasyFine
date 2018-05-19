using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{
    public class RichLogger
    {
        private StringBuilder sb;
        private Stopwatch sw;

        public RichLogger()
        {
            this.sb = new StringBuilder();
            this.sw = new Stopwatch();
            this.sw.Start();
        }

        public void Log(string message)
        {
            this.sb.AppendLine(string.Format("[{0}] [{1}] {2}", DateTime.Now, this.sw.Elapsed, message));
            this.sw.Restart();
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public void Flush()
        {
            File.AppendAllText(@"c:\temp\log.txt", sb.ToString());
        }
    }
}
