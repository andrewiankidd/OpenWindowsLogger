using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenWindowsLogger
{
  public class logger
  {
    StreamWriter logFilew = new StreamWriter(OpenWindowsLogger.OWLmain.logPath);
    public void rwlog(string rw, string contents, bool HTMLout)
    {
      string timestamp = "[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + "]";

      if (rw == "raw")
      {
        logFilew.WriteLine(contents);
        logFilew.Flush();
      }

      if (rw == "w")
      {
        if (HTMLout)
        {
          contents = contents.Replace("/", "</td><td>");
          contents = "<tr><td>"+timestamp+"</td><td>" + contents + "</td></tr>";
        }
        else
        {
          contents = timestamp + " - " + contents;
        }
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
