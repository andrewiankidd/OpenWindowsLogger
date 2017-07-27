using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWindowsLogger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int i = 0;
            foreach (string arg in args)
            {
              string command = arg.ToUpper();
              string value = (i+1) >= args.Length ? null : args[i + 1];
              switch (command)
              {     
                case "/T":  //timeout
                  OWLmain.timeoutLimit = Convert.ToInt32(value) ;
                  break;
                case "/L":  //logpath
                  OWLmain.logPath = value;
                  break;
                case "/K":  //logkeys?
                  OWLmain.logKeys = Convert.ToBoolean(value);
                  break;
                case "/H":  //HTMLout?
                  OWLmain.HTMLout = Convert.ToBoolean(value);
                  break;
                case "/F":  //filterExisting?
                  OWLmain.filterExisting = Convert.ToBoolean(value);
                  break;
                case "/E":  //filterExternal?
                  OWLmain.filterExternal = Convert.ToBoolean(value);
                  break;
                case "/A":  //autoopenlog?
                  OWLmain.autoOpenLog = Convert.ToBoolean(value);
                  break;
                case "/S":  //suppressdupes?
                  OWLmain.suppressDupes = Convert.ToBoolean(value);
                  break;
                default:
                  break;
              }
              i++;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OWLmain());

            
        }
    }
}
