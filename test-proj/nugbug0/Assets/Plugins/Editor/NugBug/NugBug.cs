using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text;
using UnityEngine;

namespace NugBug
{

  class NugBug
  {

    public static void Help()
    {
      NuGetProcess("help");
    }

    public static void NuGetProcess(string arguments)
    {
      NugBugWindow.WriteToConsole("");
      NugBugWindow.WriteToConsole("$ nuget "+arguments);
      RunProcess("mono","--runtime=v4.0 ./Bin/NugBug/NuGet.exe "+arguments);
    }

    public static void RunProcess(string fileName, string arguments, int timeout = 3000)
    {
      using (Process process = new Process())
      {
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        
        StringBuilder output = new StringBuilder();
        StringBuilder error = new StringBuilder();

        using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
        using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
        {
          process.OutputDataReceived += (sender, e) =>
          {
            if (e.Data == null)
            {
                outputWaitHandle.Set();
            }
            else
            {
                output.AppendLine(e.Data);
            }
          };
          process.ErrorDataReceived += (sender, e) =>
          {
              if (e.Data == null)
              {
                  errorWaitHandle.Set();
              }
              else
              {
                  error.AppendLine(e.Data);
              }
          };
        
          process.Start();
          process.BeginOutputReadLine();
          process.BeginErrorReadLine();

          if (process.WaitForExit(timeout) &&
              outputWaitHandle.WaitOne(timeout) &&
              errorWaitHandle.WaitOne(timeout))
          {
              // Process completed. Check process.ExitCode here.
              NugBugWindow.WriteToConsole(error.ToString());
              NugBugWindow.WriteToConsole(output.ToString());
          }
          else
          {
              // Timed out.
              process.Kill();
              NugBugWindow.WriteToConsole(error.ToString());
              NugBugWindow.WriteToConsole(output.ToString());
              NugBugWindow.WriteToConsole("process timed out");
          }
        }
      }
    }

  }
}