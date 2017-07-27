using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Timers;

namespace OpenWindowsLogger
{
  public partial class OWLmain : Form
  {
    
    private static GlobalKeyboardHook keyListener = new GlobalKeyboardHook();
    public static List<string> existingWindows = new List<string>();
    public static List<string> filteredWindows = new List<string>();
    public static bool logEnabled = false;
    public static logger log;
    public static int timeoutLimit = 0;
    public static string logPath;
    public static bool filterExisting = true;
    public static bool filterExternal = true;
    public static bool autoOpenLog = true;
    public static bool suppressDupes = true;
    public static bool HTMLout = true;
    public static bool logKeys = true;

    public OWLmain()
    {
      InitializeComponent();

      //existing
      chkFilterExisting.Checked = filterExisting;
      //external
      chkExtFilterList.Checked = filterExternal;
      //autoopen
      chkAutoOpenLog.Checked = autoOpenLog;
      //suppressdupes
      chkSuppressDupes.Checked = suppressDupes;
      //htmlout
      chkHTMLOut.Checked = HTMLout;
      //logkeys
      chkLogKeys.Checked = logKeys;

      //logging
      if (String.IsNullOrEmpty(logPath))
      {
        if (HTMLout)
        {
          logPath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "output.html";
        }
        else { logPath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "output.txt"; }
      }
      log = new logger();

    }

    private void OWLmain_Load(object sender, EventArgs e)
    {
      //set logpath and create log file if it doesnt exist
      txtLogPath.Text = logPath;
      if (!File.Exists(logPath)) { File.CreateText(logPath); }

      //check for filter list and read into filteredWindows list
      if (File.Exists(Environment.CurrentDirectory + "\\filter.txt"))
      {
         filteredWindows = new List<string>(File.ReadAllLines(Environment.CurrentDirectory + "\\filter.txt"));
      }

      //start a thread for watching mouseclicks
      new Thread(() =>
      {
        mouseListener.mouseListenerMain();
      }).Start();

      //start keylogging
      keyListener.keyListenerMain();

      if (timeoutLimit > 0)
      {
        System.Timers.Timer timer = new System.Timers.Timer(timeoutLimit * 1000);
        timer.Elapsed += Timer_Elapsed;
        toggleLogging();
        timer.Start();
      }

    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      log.rwlog("w", "TIMER/COMPLETE/" + timeoutLimit + " seconds.", HTMLout);
      Application.Exit();
    }

    private void logWindows()
    {
      //Thread will log open windows
      new Thread(() =>
      {
        //set as background thread
        Thread.CurrentThread.IsBackground = true;
        //run OpenWindowGetter and iterate through results
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          string windowKey = handle + "/" + title;

          //if user has chosen to filter the existing windows, check current iteration result against list of known windows to ignore
          if (filterExisting && existingWindows.Contains(windowKey))
          {
            //it's on the existing window list, so ignore it
          }
          else if (filterExternal && filteredWindows.Contains(title) || filterExternal && filteredWindows.Contains(windowKey))
          {
            //it's on the filter list, so ignore it
          }
          //it's not on the filter list, so print it to log
          else {
            
            //if we wanna suppress dupes then add this entry to the filter before printing it out, it'll be ignored next time
            if (suppressDupes)
            {
              filteredWindows.Add(windowKey);
            }

            log.rwlog("w", "WINDOW/"+windowKey, HTMLout);
          }
        }
        //check if logging is still enabled, if not then close off, if so then run logger again
        if (!logEnabled) { return; } else { logWindows(); }
      }).Start();

    }

    private void getExistingWindows()
    {
      //Thread will log open windows
      new Thread(() =>
      {
        //set as background thread
        Thread.CurrentThread.IsBackground = true;
        //run OpenWindowGetter and iterate through results
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
          IntPtr handle = window.Key;
          string title = window.Value;
          string windowKey = handle + "/" + title;
          //add window key and title to list of existing windows
          existingWindows.Add(windowKey);
        }
      }).Start();

    }

    private void toggleLogging()
    {
      //if user wants to filter existing windows, run getExistingWindows, which will create list of running windows we can compare against later
      if (filterExisting) { getExistingWindows(); }
      if (HTMLout) { HTMLout = true; }

      //if logging is already enabled then disable
      if (logEnabled)
      {
        logEnabled = false;
        btnEnableLogging.Text = "Logging Disabled";
        btnEnableLogging.ForeColor = Color.Red;
        //write to log that logging has been disabled, and the datetime it was disabled
        log.rwlog("w", "LOGGING/DISABLED", HTMLout);
        if (HTMLout)
        {
          log.rwlog("raw", "</table>", HTMLout);
        }     
        //if user has chosen to automatically open the log file, open it in default text editor now
        if (autoOpenLog) { System.Diagnostics.Process.Start(logPath); }
      }
      //otherwise, enable
      else if (!logEnabled)
      {
        logEnabled = true;
        btnEnableLogging.Text = "Logging Enabled";
        btnEnableLogging.ForeColor = Color.Green;
        //write to log that logging has been enabled, and the datetime it was enabled
        if (HTMLout)
        {
          log.rwlog("raw", "<table>", HTMLout);
          log.rwlog("raw", "<tr><td>Timestamp</td><td>Action</td><td>Event</td><td>Details</td></tr>", HTMLout);
        }
        if (timeoutLimit > 0)
        {
          log.rwlog("w", "TIMER/STARTED/" + timeoutLimit + " seconds.", HTMLout);
        }
        log.rwlog("w", "LOGGING/ENABLED", HTMLout);
        logWindows();
      }
    }

    private void btnEnableLogging_Click(object sender, EventArgs e)
    {
      toggleLogging();
    }

    private void OWLmain_FormClosed(object sender, FormClosedEventArgs e)
    {
      //when the form closes stop all threads and exit app
      Application.Exit();
    }

    private void updateLogger(string path)
    {
      logPath = path;
      txtLogPath.Text = path;
      log = new logger();
    }

    private void btnLogPath_Click(object sender, EventArgs e)
    {
      if (logEnabled) { toggleLogging(); }
      if (HTMLout) {
        saveFileDialog.FileName = "OWL.html";
        saveFileDialog.DefaultExt = "html";
        saveFileDialog.Filter = "HTML | *.html";
      }
      else {
        saveFileDialog.FileName = "OWL.txt";
        saveFileDialog.DefaultExt = "txt";       
        saveFileDialog.Filter = "Text File | *.txt";
      }
      saveFileDialog.AddExtension = true;
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        updateLogger(saveFileDialog.FileName);
      }
    }

    private void chkHTMLOut_CheckedChanged(object sender, EventArgs e)
    {
      if (logEnabled) { toggleLogging(); }
      updateLogger(saveFileDialog.FileName);
    }

    private void chkLogKeys_CheckedChanged(object sender, EventArgs e)
    {
      logKeys = chkLogKeys.Checked;
    }

    private void chkFilterExisting_CheckedChanged(object sender, EventArgs e)
    {
      filterExisting = chkFilterExisting.Checked;
    }

    private void chkExtFilterList_CheckedChanged(object sender, EventArgs e)
    {
      filterExternal = chkExtFilterList.Checked;
    }

    private void chkAutoOpenLog_CheckedChanged(object sender, EventArgs e)
    {
      autoOpenLog = chkAutoOpenLog.Checked;
    }

    private void chkSuppressDupes_CheckedChanged(object sender, EventArgs e)
    {
      suppressDupes = chkSuppressDupes.Checked;
    }
  }
}
