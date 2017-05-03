using System;
using System.IO;



namespace IPS_temperature_extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string userHomefolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string theReport = "/report.txt";
                string line;
                StreamReader file = new StreamReader(userHomefolder + theReport);
                string outputFile = userHomefolder + "/outputfile.csv";
                File.WriteAllText(outputFile, "Date/Time," + "GPU No.," + "Model," + "Bus-ID," + "Temp (C)," + 
                    "Pwr:Usage (W)," + "Pwr: CAP (W)," + "Memory usage (MiB)," + "Memory CAP (MiB)," + "GPU-Util %");
                File.AppendAllText(outputFile, Environment.NewLine);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("2017"))
                    {
                        File.AppendAllText(outputFile, line.Substring(0,20) + ","); // Date field
                    }
                    if (line.Contains("Tes"))
                    {
                        File.AppendAllText(outputFile, line.Substring(4, 1) + ","); // No. of GPU field
                        File.AppendAllText(outputFile, line.Substring(7, 15) + ","); // Model field
                        File.AppendAllText(outputFile, line.Substring(34, 12) + ","); // Bus-ID field
                    }
                    if (line.Contains("| N/A"))
                    {
                        File.AppendAllText(outputFile, line.Substring(8, 2) + ","); // Temperature field
                        File.AppendAllText(outputFile, line.Substring(21, 2) + ","); // Power usage field
                        File.AppendAllText(outputFile, line.Substring(28, 2) + ","); // Power CAP field
                        File.AppendAllText(outputFile, line.Substring(36, 4) + ","); // Memory current usage field
                        File.AppendAllText(outputFile, line.Substring(47, 4) + ","); // Memory CAP field
                        File.AppendAllText(outputFile, line.Substring(60, 3) + ","); // GPU Util field
                        File.AppendAllText(outputFile, Environment.NewLine);
                    }
                };
                file.Close();

            }
            catch (IOException ioe)
            {
                Console.WriteLine("IO Exception: check file path/name");
            }
        }
    }
}
