using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HexZeta.PlantTools.RvmViewer
{
    class Program
    {
        static void Main(string[] args)
        {

            // Ab4d.SharpEngine Trial License can be used for testing the Ab4d.SharpEngine and is valid until July 19, 2025.
            Ab4d.SharpEngine.Licensing.SetLicense(licenseOwner: "none",
                                                  licenseType: "TrialLicense",
                                                  platforms: "All",
                                                  license: "CD5A-C518-7CB0-6C23-D399-9255-3CDE-F36E-E78A-8D6F-5AFC-572D-42CA-33CD-09A3-2142-8A6B");


            foreach (FileInfo file in (new DirectoryInfo("D:\\RVM")).GetFiles("*"))
            {
                ConvertFile(file.FullName);
            }

            //System.Diagnostics.Process.Start("d:\\test.off");
        }

        private static void ConvertFile(string file)
        {
            using (StreamReader r = new StreamReader(file, Encoding.ASCII))
            {
                List<string> cont = new List<string>();
                while (!r.EndOfStream)
                {
                    var line = $"{r.ReadLine()}";
                    cont.Add(line);
                }

                RvmParser rvm = new RvmParser(cont);

                List<string> outCont = rvm.ConvertToObj();
                if (file.IndexOf('.') < 0)
                    file += ".obj";
                using (StreamWriter w = new StreamWriter(file.Replace(".rvm", ".obj"), false, Encoding.ASCII))
                {
                    foreach (string line in outCont)
                    {
                        w.WriteLine(line);
                    }
                }
            }
        }
    }
}
