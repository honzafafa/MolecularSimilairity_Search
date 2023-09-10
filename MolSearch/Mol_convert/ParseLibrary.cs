using System.Collections;
using ConvertToHash;

namespace LibraryProcessor;

// Provides functionality to read molecular library files and extract fingerprints.
public class LibraryReader
{
    // Instance of the FCFP generator for creating fingerprints
    FCFPGenerator Generator = new FCFPGenerator();

    // Instance of the SdfParser for parsing molecules from SDF format
    SdfParser Parser = new SdfParser();

    // Reads the molecular library from a file and extracts fingerprints for each molecule.
    // Returns a dictionary mapping molecule names/identifiers to their fingerprints
    public Dictionary<string, BitArray> Read(string filePath, int radius, int length)
    {
        Dictionary<string, BitArray> moleculeHashes = new Dictionary<string, BitArray>();

        // Read the entire file into memory
        string[] fileLines = File.ReadAllLines(filePath);

        // Split the file into individual molecule sections
        List<string[]> moleculesAsLines = SplitByDelimiter(fileLines, "$$$$");
        bool statementActive = true;

        foreach (string[] moleculeLines in moleculesAsLines)
        {
            Molecule molecule = Parser.Parse(moleculeLines);

            // Use the first line in each SDF entry as the molecule's unique identifier
            // Assuming the first line in each SDF entry is the molecule's name or at least unique id.
            string moleculeName = moleculeLines[0].Trim();

            // Handle missing or empty molecule names
            if (String.IsNullOrEmpty(moleculeName))
            {
                if (statementActive)
                {
                    Console.WriteLine("One or more entries in this file lack unique identidifier (which is fine for individual molecules)");
                    statementActive = false;
                }

                // Use the file name as the molecule name if missing
                moleculeName = Path.GetFileName(filePath);
            }

            // Generate the fingerprint for the molecule
            BitArray hash = Generator.GenerateFcfp(molecule, radius, length);

            moleculeHashes[moleculeName] = hash;
        }

        return moleculeHashes;
    }

    // Splits the lines of the file by a given delimiter into separate sections.
    // Returns a list of string arrays, each representing a section.
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