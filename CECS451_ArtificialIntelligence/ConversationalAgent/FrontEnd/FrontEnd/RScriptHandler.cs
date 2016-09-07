using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd
{
    public class RScriptHandler
    {

        public static string RunCommand(string rCodeFilePath, string rScriptExecutable, string args)
        {

            string file = rCodeFilePath;
            string result = string.Empty;

            try
            {

                ProcessStartInfo info = new ProcessStartInfo(rScriptExecutable);
                info.WorkingDirectory = Path.GetDirectoryName(rScriptExecutable);
                info.Arguments = $"{rCodeFilePath} {args}";

                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;

                using (Process process = new Process())
                {

                    process.StartInfo = info;
                    process.Start();
                    result = process.StandardOutput.ReadToEnd();

                }

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception("R Script failed: " + result, ex);

            }


        }

    }
}
