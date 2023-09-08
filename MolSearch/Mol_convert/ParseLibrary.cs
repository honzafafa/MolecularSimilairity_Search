using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using ConvertToHash;

namespace LibraryProcessor;

public class LibraryReader
{
    FCFPGenerator Generator = new FCFPGenerator();
    SdfParser Parser = new SdfParser();

    public Dictionary<string, BitArray> Read(string filePath, int radius, int length)
    {
        Dictionary<string, BitArray> moleculeHashes = new Dictionary<string, BitArray>();

        string[] fileLines = File.ReadAllLines(filePath);
        List<string[]> moleculesAsLines = SplitByDelimiter(fileLines, "$$$$");

        //string[] individualMolecules = fileContent.Split("$$$$");

        foreach (string[] moleculeLines in moleculesAsLines)
        {
            Molecule molecule = Parser.Parse(moleculeLines);

            // Assuming the first line in each SDF entry is the molecule's name.
            string moleculeName = moleculeLines[0].Trim();

            BitArray hash = Generator.GenerateFcfp(molecule, radius, length);

            moleculeHashes[moleculeName] = hash;
        }

        return moleculeHashes;
    }

    private List<string[]> SplitByDelimiter(string[] lines, string delimiter)
    {
        List<string[]> sections = new List<string[]>();
        List<string> currentSection = new List<string>();

        foreach (string line in lines)
        {
            if (line.Trim() == delimiter)
            {
                sections.Add(currentSection.ToArray());
                currentSection.Clear();
            }
            else
            {
                currentSection.Add(line);
            }
        }

        // Adding the last section if the file doesn't end with the delimiter
        if (currentSection.Count > 0)
        {
            sections.Add(currentSection.ToArray());
        }

        return sections;
    }
}