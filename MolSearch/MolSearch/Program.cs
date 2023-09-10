using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConvertToHash;
using Test;
using MolMesure;
using LibraryProcessor;
using Interace;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Data;
using System.Diagnostics.Metrics;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {

            ApplicationInterface appInterface = new ApplicationInterface();
            //appInterface.Start();
            Console.WriteLine("\nProcessing... Please wait.");
            Console.WriteLine("------------------------------------------------");

            LibraryReader Reader = new LibraryReader();

            //Dictionary<string, BitArray> target = Reader.Read(appInterface.TargetPath, appInterface.Radius, appInterface.Length);
            Dictionary<string, BitArray> target = Reader.Read("/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI.sdf", 2, 1024);
            string targetKey = target.Keys.First();

            //Dictionary<string, BitArray> library = Reader.Read(appInterface.LibraryPath, appInterface.Radius, appInterface.Length);
            Dictionary<string, BitArray> library = Reader.Read("/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/20200113-L1300-FDA-approved-Drug-Library.sdf", 2, 1024);
            //Dictionary<string, BitArray> library = Reader.Read("/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI.sdf", 2, 1024);
            string targetPath = "/Users/faflik/Desktop/Results1.txt";

            //Visuals
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            SimilarityCalculator Measure = new SimilarityCalculator();

            Dictionary<string, decimal> measurments = new Dictionary<string, decimal>();

            foreach (string currentKey in library.Keys)
            {
                decimal similarity = Measure.Tanimoto(target[targetKey], library[currentKey]);
                measurments[currentKey] = similarity;
            }

            int numMolecules = measurments.Keys.Count;
            List<string> namesToPresent = new List<string>();
            List<decimal> measurmetsToPresent = new List<decimal>();

            using (StreamWriter writer = new StreamWriter(targetPath))
            {
                foreach (KeyValuePair<string, decimal> measurment in measurments.OrderBy(key => key.Value))
                {
                    string result = "Key: " + measurment.Key + ", Value: " + measurment.Value;

                    writer.WriteLine(result);
                    numMolecules -= 1;

                    if (numMolecules < 10)
                    {
                        namesToPresent.Add(measurment.Key);
                        measurmetsToPresent.Add(measurment.Value);
                    }
                }
            }

            //Present the results

            //for (int i = (measurmetsToPresent.Count - 1); i >= 0; i--)
            //{
            //    Console.WriteLine("Key: {0}, Value: {1}", namesToPresent[i], measurmetsToPresent[i]);
            //}

            DisplayTable(namesToPresent, measurmetsToPresent);
        }

        static void DisplayTable(List<string> names, List<decimal> similarities)
        {

            // Calculate column widths
            int nameColWidth = Math.Max("Molecule Name".Length, GetLongestStringLength(names));
            int similarityColWidth = 30;  // Beacuse of the maximal decimal presicision is 28-29 digits

            // Print headers
            Console.WriteLine(new string('-', nameColWidth + similarityColWidth + 7));
            Console.WriteLine($"| {"Molecule Name".PadRight(nameColWidth)} | {"Similarity Score".PadRight(similarityColWidth)} |");
            Console.WriteLine(new string('-', nameColWidth + similarityColWidth + 7));

            // Print rows
            for (int i = (names.Count - 1); i >= 0; i--)
            {
                Console.WriteLine($"| {names[i].PadRight(nameColWidth)} | {similarities[i].ToString("F12").PadRight(similarityColWidth)} |");
            }

            Console.WriteLine(new string('-', nameColWidth + similarityColWidth + 7));
        }

        static int GetLongestStringLength(IEnumerable<string> strings)
        {
            int maxLength = 0;
            foreach (var str in strings)
            {
                if (str.Length > maxLength)
                {
                    maxLength = str.Length;
                }
            }
            return maxLength;
        }
    }
}
