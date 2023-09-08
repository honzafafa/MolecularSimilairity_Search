using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ConvertToHash;
using Test;
using MolMesure;
using LibraryProcessor;

namespace Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            ////string filepath1 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/PG5_2D.sdf";
            string filepath2 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI.sdf";
            //string filepath3 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Oxycodone.sdf";
            //string filepath4 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Morphine.sdf";
            //string filepath5 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Meperidine.sdf";
            //string filepath6 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Hydromorphone.sdf";
            //string filepath7 = "/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Fentanyl.sdf";

            ////for testing purposes
            ////ReadFile Test = new ReadFile();
            //// Test.Print(filepath);

            ////for testing purposes
            ////PrintMolecule Print = new PrintMolecule();
            ////int[] retrive = Print.PrintMol(filepath);
            ////foreach(int i in retrive) Console.WriteLine(i);

            ////Create new sdf parser
            //SdfParser Parser = new SdfParser();

            ////Read 2 molecules into a molecule object
            //Molecule Molecule1 = Parser.Parse(File.ReadAllLines(filepath3));
            //Molecule Molecule2 = Parser.Parse(File.ReadAllLines(filepath4));
            //Molecule Molecule3 = Parser.Parse(File.ReadAllLines(filepath5));
            //Molecule Molecule4 = Parser.Parse(File.ReadAllLines(filepath6));
            //Molecule Molecule5 = Parser.Parse(File.ReadAllLines(filepath7));

            ////Create a new Generator
            //FCFPGenerator Generator = new FCFPGenerator();

            ////Convert both of the molecules into the BitArrays
            //BitArray Hash1 = Generator.GenerateFcfp(Molecule1, 3, 1024);
            //BitArray Hash2 = Generator.GenerateFcfp(Molecule2, 3, 1024);
            //BitArray Hash3 = Generator.GenerateFcfp(Molecule3, 3, 1024);
            //BitArray Hash4 = Generator.GenerateFcfp(Molecule4, 3, 1024);
            //BitArray Hash5 = Generator.GenerateFcfp(Molecule5, 3, 1024);


            ////Creare a new Similiarity Calculator
            //SimilarityCalculator Measure = new SimilarityCalculator();

            ////measure the similarity of the molecules
            //double similarity1 = Measure.Tanimoto(Hash1, Hash1);
            //Console.WriteLine("");
            //Console.WriteLine(similarity1);

            //double similarity2 = Measure.Tanimoto(Hash1, Hash2);
            //Console.WriteLine("");
            //Console.WriteLine(similarity2);

            //double similarity3 = Measure.Tanimoto(Hash1, Hash3);
            //Console.WriteLine("");
            //Console.WriteLine(similarity3);

            //double similarity4 = Measure.Tanimoto(Hash1, Hash4);
            //Console.WriteLine("");
            //Console.WriteLine(similarity4);

            //double similarity5 = Measure.Tanimoto(Hash1, Hash5);
            //Console.WriteLine("");
            //Console.WriteLine(similarity5);

            LibraryReader Reader = new LibraryReader();
            Dictionary<string, BitArray> Results = Reader.Read("/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/Approveddrugslibrary.sdf", 2, 4096);


            foreach(var key in Results.Keys) Console.WriteLine(key);

            //var results = Reader.Read(filepath2, 2, 20);

            //foreach (bool i in results["one"]) Console.WriteLine(i);

            //string fileContent = File.ReadAllText("/Users/faflik/Projects/C#/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/Approveddrugslibrary.sdf");
            //Console.WriteLine(fileContent);
        }
    }
}
