using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Biodex_Console_test_programm
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = "C:/Users/jgtha/OneDrive/BBE/Biodex/Biodex Client/csv data to read for load(test)/Armin_Messung";

            int [][] data= readCSV(path);

            for(int i=0;i<data.Length;i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    Console.Write(data[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

        }

        public static int[][] readCSV(string path)
        {

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path, Encoding.Default);
                string[][] dataString = new string[lines.Length][];
                int[][] data= new int[lines.Length][];

                //Split all lines with a ','
                for (int i = 0; i < lines.Length; i++)
                {
                    dataString[i] = lines[i].Split(',');
                    int[] temp = new int[dataString[i].Length];
                    for (int j=0; j<dataString[i].Length; j++)
                    {                       
                        temp[j]= Convert.ToInt32(dataString[i][j]);                    
                    }
                    data[i] = temp;
                }
                
                return data;
            }
            else
            {
                throw new FileNotFoundException();
            }

        }
    }
}
