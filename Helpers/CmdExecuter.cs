using System;
using System.Diagnostics;
using System.IO;
namespace CargoMailParser{

    public static class CmdExecutor{
        public static string run_cmd(string cmd, string args=null)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "classifier/classifier.py";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using(Process process = Process.Start(start))
            {
                using(StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    //Console.Write(result);
                    return result;
                }
            }
        }
    }
}
