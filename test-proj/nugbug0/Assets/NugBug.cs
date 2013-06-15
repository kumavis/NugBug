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

    public static void Main()
    {
      //
      // Setup the process with the ProcessStartInfo class.
      //
      ProcessStartInfo start = new ProcessStartInfo();
      //start.FileName = @"NuGet.exe"; // Specify exe name.
      start.FileName = @"/usr/local/bin/NuGet.exe"; // Specify exe name.
      start.UseShellExecute = false;
      start.RedirectStandardOutput = true;
      //
      // Start the process.
      //
      using (Process process = Process.Start(start))
      {
        //
        // Read in all the text from the process with the StreamReader.
        //
        using (StreamReader reader = process.StandardOutput)
        {
          string result = reader.ReadToEnd();
          UnityEngine.Debug.Log(result);
        }
      }
    }

    public static void Help() {
      int timeout = 3000;

      using (Process process = new Process())
      {
        process.StartInfo.FileName = "mono";
        process.StartInfo.Arguments = "--runtime=v4.0 /usr/local/bin/NuGet.exe help";
        process.StartInfo.UseShellExecute = true;
        //process.StartInfo.RedirectStandardOutput = true;
        //process.StartInfo.RedirectStandardError = true;

//        StringBuilder output = new StringBuilder();
//        StringBuilder error = new StringBuilder();
//
//        using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
//        using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
//        {
//          process.OutputDataReceived += (sender, e) => {
//            if (e.Data == null)
//            {
//                outputWaitHandle.Set();
//            }
//            else
//            {
//                output.AppendLine(e.Data);
//            }
//          };
//          process.ErrorDataReceived += (sender, e) =>
//          {
//              if (e.Data == null)
//              {
//                  errorWaitHandle.Set();
//              }
//              else
//              {
//                  error.AppendLine(e.Data);
//              }
//          };
//
          process.Start();
//
//          process.BeginOutputReadLine();
//          process.BeginErrorReadLine();
//
//          if (process.WaitForExit(timeout) &&
//              outputWaitHandle.WaitOne(timeout) &&
//              errorWaitHandle.WaitOne(timeout))
//          {
//              // Process completed. Check process.ExitCode here.
              UnityEngine.Debug.Log("done");
//          }
//          else
//          {
//              // Timed out.
//              UnityEngine.Debug.Log("timeout");
//              UnityEngine.Debug.Log(output);
//              UnityEngine.Debug.Log(error);
//
//
//          }
//        }
      }
    }

    public static void Help2() {
      // Start the child process.
      Process p = new Process();
      // Redirect the output stream of the child process.
      p.StartInfo.CreateNoWindow = true;
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.RedirectStandardInput = true;
      p.StartInfo.RedirectStandardError = true;
      p.StartInfo.FileName = "mono";
      p.StartInfo.Arguments = "--runtime=v4.0 /usr/local/bin/NuGet.exe help";


      p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

      p.Start();
      // Do not wait for the child process to exit before
      // reading to the end of its redirected stream.
      // p.WaitForExit();
      // Read the output stream first and then wait.
      //string output = p.StandardOutput.ReadToEnd();

      

      //string output = p.StandardOutput.ReadToEnd();

      //UnityEngine.Debug.Log( output );
      UnityEngine.Debug.Log("done");
    }

    public static void Ls() {
      // Start the child process.
      Process p = new Process();
      // Redirect the output stream of the child process.
      //p.StartInfo.Arguments = "help";
      p.StartInfo.CreateNoWindow = true;
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.RedirectStandardInput = true;
      p.StartInfo.RedirectStandardError = true;
      p.StartInfo.FileName = "ls";
      p.Start();
      // Do not wait for the child process to exit before
      // reading to the end of its redirected stream.
      // p.WaitForExit();
      // Read the output stream first and then wait.
      string output = p.StandardOutput.ReadToEnd();
      p.WaitForExit();

      UnityEngine.Debug.Log( output );
    }

    private static StringBuilder sortOutput = new StringBuilder("");
    private static int numOutputLines = 0;

    private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
      // Collect the sort command output. 
      if (!String.IsNullOrEmpty(outLine.Data))
      {
        numOutputLines++;

        // Add the text to the collected output.
        sortOutput.Append(Environment.NewLine + 
            "[" + numOutputLines.ToString() + "] - " + outLine.Data);
      }

      UnityEngine.Debug.Log( "sortOutput" );
    }

  }
}