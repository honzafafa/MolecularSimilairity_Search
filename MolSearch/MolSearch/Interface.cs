using System;
using System.Collections.Generic;

namespace Interace;

public class ApplicationInterface
{
    public int Radius { get; set; }
    public int Length { get; set; }
    public string TargetPath { get; set; }
    public string LibraryPath { get; set; }
    public string ResultsPath { get; set; }

    public void Start()
    {
        Console.WriteLine("WELCOME TO THIS VIRTUAL SCREENING TOOL!");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("The goal of this projec twas to create a simple but functional virtual screenig tool");
        Console.WriteLine("that could handle the screening of small molecules (potential drugs) againts libraries other small molecules (apporved drugs)");
        Console.WriteLine("With the purpose of petentially finding petentially usefull similarities and patterns in the moleculer");
        Console.WriteLine();
        Console.WriteLine("ENJOY!");
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Follow the prompts to screen a molecule against a molecular library.");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("This program can deal with molecules and libraries in the .sdf file format.");
        Console.WriteLine("The molecules in the Screened Libraires shoul be separated by '$$$$' and be in a stadart sdf format");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine();

        try
        {
            // Get radius necessary of creation fo the fingerpints
            Console.Write("Enter radius for FCFP generation (e.g. 2); (sensible range: 2-4): ");
            if (!int.TryParse(Console.ReadLine(), out int radius))
            {
                Console.WriteLine("Error: Invalid value for radius. Please enter a valid integer.");
                return;
            }

            Radius = radius;

            // Get length of the fingerprint vectors created
            Console.Write("Enter length for FCFP generation (usually 1024 or 4096); (sensible range: 100-6000): ");
            if (!int.TryParse(Console.ReadLine(), out int length))
            {
                Console.WriteLine("Error: Invalid value for length. Please enter a valid integer.");
                return;
            }

            Length = length;

            // Get File Paths
            Console.Write("Enter the path of the molecule you want to screen against: ");
            string targetPath = Console.ReadLine();
            if (!File.Exists(targetPath))
            {
                Console.WriteLine($"Error: File not found at path '{targetPath}'.");
                return;
            }

            TargetPath = targetPath;


            Console.Write("Enter the path to the library you want to screen: ");
            string libraryPath = Console.ReadLine();
            if (!File.Exists(libraryPath))
            {
                Console.WriteLine($"Error: File not found at path '{libraryPath}'.");
                return;
            }

            LibraryPath = libraryPath;

            Console.Write("Enter the path to the directory where you want to save your text file: ");
            string resultsPath = Console.ReadLine();

            if (String.IsNullOrEmpty(resultsPath))
            {
                Console.WriteLine($"Error: Please enter the path where you want to place your results'.");
                return;
            }

            ResultsPath = resultsPath;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nThank you for using the tool!");
            Console.WriteLine("\nYou will recieve the reults in a few moments");

        }
    }
}