using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConvertToHash;
using Test;
using MolMesure;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filepath1 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/PG5_2D.sdf";
            //string filepath2 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI_2D.sdf";
            //string filepath3 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI_3D.sdf";
            //string filepath4 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Morphine_3D.sdf";
            string filepath5 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Meperidine_3D.sdf";
            string filepath6 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Hydromorphone_3D.sdf";

            //for testing purposes
            //ReadFile Test = new ReadFile();
            // Test.Print(filepath);

            //for testing purposes
            //PrintMolecule Print = new PrintMolecule();
            //int[] retrive = Print.PrintMol(filepath);
            //foreach(int i in retrive) Console.WriteLine(i);

            SdfParser Parser = new SdfParser();
            Molecule Molecule1 = Parser.Parse(filepath5);
            Molecule Molecule2 = Parser.Parse(filepath6);


            FCFPGenerator Generator = new FCFPGenerator();
            BitArray Hash1 = Generator.GenerateFcfp(Molecule1, 3, 4096);
            BitArray Hash2 = Generator.GenerateFcfp(Molecule2, 3, 4096);

            SimilarityCalculator Measure = new SimilarityCalculator();
            double similarity = Measure.Tanimoto(Hash1, Hash2);
            Console.WriteLine(similarity);
        }
    }
}
