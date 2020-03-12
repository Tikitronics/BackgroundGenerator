using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackgroundGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> fonts = new List<string>();
            List<string> batchLines = new List<string>();
            string outputText = "Stop Starting & Start Finishing";
            string outputFolder = "output";
            string bgColor = "transparent";
            string fontColor = "white";
            int fontSize = 72;
            

            string path = Directory.GetCurrentDirectory();
            string[] installedFonts = Directory.GetFiles(path, "*.ttf");

            for (int i = 0; i < installedFonts.Length; i++)
            {
                fonts.Add(Path.GetFileName(installedFonts[i]));
            }

            batchLines.Add("mkdir " + outputFolder);
            foreach (string font in fonts)
            {
                Console.WriteLine(font);
                string line = String.Format("magick -size 1024x768 xc:{0} -font \"{1}\" -pointsize {2} -fill {3} -gravity center -draw \"text 0,0 '{4}'\" -trim \"{5}.png\"", bgColor, font, fontSize, fontColor, outputText, Path.Combine(outputFolder, font.Remove(font.Length - 4)));
                batchLines.Add(line);
                //Console.WriteLine(line);
            }
            batchLines.Add("pause");

            File.WriteAllLines("generate.bat", batchLines);
        }
    }
}
