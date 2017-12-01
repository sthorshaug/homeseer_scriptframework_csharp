using System;
using System.IO;
using System.Collections.Generic;

namespace ExtractScripts
{
    public class ScriptGrabber
    {
        private string inputPath = "";
        private string outputPath = "";

        public ScriptGrabber()
        {
        }

        public bool Run(string[] args) {
            if(!ParseArguments(args) || !Verifypaths()) 
            {
                return false;
            }
            string[] files = Directory.GetFiles(inputPath);
            foreach (var file in files)
            {
                try 
                {
                    ExtractScript(file);
                }
                catch(Exception ex) {
                    Console.WriteLine("Failed to extract from " + file + ": " + ex.Message);
                }
            }
            return true;
        }

        private void ExtractScript(string file) 
        {
            bool outputData = false;
            var outfilename = outputPath + "/" + Path.GetFileName(file);
            string[] input = File.ReadAllLines(file);
            List<string> output = new List<string>();

            foreach(var line in input) 
            {
                if(outputData && line.Contains("#endregion"))
                {
                    outputData = false;
                    continue;
                }
                else if(!outputData && (line.Contains("#region Using") || line.Contains("#region Scripts")))
                {
                    outputData = true;
                    continue;
                }
                if(outputData) 
                {
                    var outLine = line;
                    if(outLine.StartsWith("        "))
                    {
                        outLine = outLine.Substring(8);
                    }
                    output.Add(outLine);
                }
            }
            File.WriteAllLines(outfilename, output);
            Console.WriteLine("Created " + outfilename);
        }

        private bool Verifypaths()
        {
            if (inputPath.Equals("") || outputPath.Equals(""))
            {
                Console.WriteLine("Not all paths given");
                return false;
            }

            if(!Directory.Exists(inputPath))
            {
                Console.WriteLine("Input path does not exists");
                return false;
            }

            if(!Directory.Exists(outputPath))
            {
                try
                {
                    Directory.CreateDirectory(outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to create directory " + outputPath + ": " + ex.Message);
                    return false;
                }

            }

            return true;
        }

        private bool ParseArguments(string[] args) 
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: -i <inputpath> -o <outputpath>");
                return false;
            }
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals("-i"))
                {
                    inputPath = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-o"))
                {
                    outputPath = args[i + 1];
                    i++;
                }
            }
            return true;
        }
    }
}
