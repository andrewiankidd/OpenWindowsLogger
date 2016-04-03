using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWindowsLogger
{
  public class logger
  {
    StreamWriter logFilew = new StreamWriter(OpenWindowsLogger.OWLmain.logPath);
    public void rwlog(string rw, string contents)
    {
      if (rw == "w")
      {
        logFilew.WriteLine(contents);
        logFilew.Flush();
      }

      if (rw == "r")
      {
        //scrapped feature, live log, will redo
      }
    }

  }
}
