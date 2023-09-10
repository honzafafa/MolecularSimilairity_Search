// Molecular Similarity Search
// Jan Faflík
// Summer Semeter 2022/23
// Programming 2 NPRG031

using System;
using System.Collections;
using Test;
using MolMesure;
using LibraryProcessor;
using Interace;
using System.Data;

namespace Solution
{
    class Program
    {
        // Entry point of the application.
        // This method initializes the application interface, reads molecular libraries, calculates similarities, and outputs the results.
        static void Main(string[] args)
        {
            // Initialize and start the application interface
            ApplicationInterface appInterface = new ApplicationInterface();
            appInterface.Start();
            Console.WriteLine("\nProcessing... Please wait.");
            Console.WriteLine("------------------------------------------------");

            // Read target molecule and molecular library
            LibraryReader Reader = new LibraryReader();

            Dictionary<string, BitArray> target = Reader.Read(appInterface.TargetPath, appInterface.Radius, appInterface.Length);
            string targetKey = target.Keys.First();

            Dictionary<string, BitArray> library = Reader.Read(appInterface.LibraryPath, appInterface.Radius, appInterface.Length);

            //Visuals
            Console.WriteLine();

            // Calculate similarities between target molecule and each molecule in the library
            SimilarityCalculator Measure = new SimilarityCalculator();

            Dictionary<string, decimal> measurments = new Dictionary<string, decimal>();

            foreach (string currentKey in library.Keys)
            {
                decimal similarity = Measure.Tanimoto(target[targetKey], library[currentKey]);
                measurments[currentKey] = similarity;
            }

            // Write the similarity measurements to a file and prepare data for table display
            int numMolecules = measurments.Keys.Count;
            List<string> namesToPresent = new List<string>();
            List<decimal> measurmetsToPresent = new List<decimal>();

            using (StreamWriter writer = new StreamWriter(appInterface.ResultsPath))
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

            DisplayTable(namesToPresent, measurmetsToPresent);
        }

        // Display a table with the top 10 most similar molecules
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

        // Determines the length of the longest string in a collection of strings.
        // Returns the length of the longest string.
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
