using System;
using System.Collections.Generic;
using System.IO;
using ConvertToHash;
using MolMesure;

namespace Solution
{
    class ReadFile
    //Class exists for dedugging purposes
    {
        public void Print(string filepath)
        // this method prints out given file
        {
            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
                Console.WriteLine(line);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //string filepath = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Structure2D_COMPOUND_CID_156974238.sdf";

            string filepath = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/ChEBI_4466.sdf";

            //for testing purposes
            //ReadFile Test = new ReadFile();
            // Test.Print(filepath);

            //for testing purposes
            //PrintMolecule Print = new PrintMolecule();
            //int[] retrive = Print.PrintMol(filepath);
            //foreach(int i in retrive) Console.WriteLine(i);

            SdfParser Parser = new SdfParser();
            Molecule Molecule1 = Parser.Parse(filepath);

            FCFPGenerator Generator = new FCFPGenerator();
            List<int> Hash = Generator.GenerateFCFP(Molecule1, 2);

            int count = 0;
            foreach (int i in Hash)
            {
                Console.WriteLine(i);
                count += 1;
            }
            Console.WriteLine();
            Console.WriteLine(count);
        }
    }
}
