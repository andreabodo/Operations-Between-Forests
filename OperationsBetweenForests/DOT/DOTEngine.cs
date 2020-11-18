using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.DOT
{
    public static class DOTEngine
    {

        public static void Run(String dotFilePath, int mode = 0)
        {
            //string executable = @"..\..\external\sfdp.exe";
            string executable = @"external\dot.exe";
            Console.WriteLine(executable);
            //string output = @"C:\Users\andre\Desktop\tempgraph";
            //File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = executable;
            switch (mode)
            {
                case (int)ModeEnum.PNG: process.StartInfo.Arguments = string.Format(@"{0} -Tpng -O", dotFilePath);
                    break;
                case (int)ModeEnum.SVG: process.StartInfo.Arguments = string.Format(@"{0} -Tsvg -O", dotFilePath);
                    break;
                case (int)ModeEnum.JPEG: process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", dotFilePath);
                    break;
                case (int)ModeEnum.PDF: process.StartInfo.Arguments = string.Format(@"{0} -Tpdf -O", dotFilePath);
                    break;
            }
            

            // Go
            process.Start();
            // and wait dot.exe to complete and exit
            process.WaitForExit();
        }
    }
}
