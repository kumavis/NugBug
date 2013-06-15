using System;
using System.Diagnostics;

namespace NugBug
{
  
  class HelloWorld
  {

    public static void Main() {
      // Start the child process.
      Process p = new Process();
      // Redirect the output stream of the child process.
      p.StartInfo.Arguments = "This is NugBug. Welcome to the nug life. Can I nuget you something?";
      p.StartInfo.CreateNoWindow = true;
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.RedirectStandardOutput = true;
      p.StartInfo.RedirectStandardInput = true;
      p.StartInfo.RedirectStandardError = true;
      p.StartInfo.FileName = "say";
      p.Start();
      // Do not wait for the child process to exit before
      // reading to the end of its redirected stream.
      // p.WaitForExit();
      // Read the output stream first and then wait.
      //string output = p.StandardOutput.ReadToEnd();
      //p.WaitForExit();

      //Console.Write( output );
    }
  }

}