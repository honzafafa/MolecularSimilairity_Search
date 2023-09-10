using ConvertToHash;

namespace Test;

// !! THIS NAMESPACE IS USED ONLY FOR DEBUGGING PURPOSES !!
// This fiele is redundant but could be useful for futher modification

// Provides functionality to print the contents of a file to the console.
class ReadFile
{
    // Prints the contents of the specified file to the console.
    public void Print(string filepath)
    {
        string[] lines = File.ReadAllLines(filepath);

        foreach (string line in lines)
            Console.WriteLine(line);
    }
}

// Provides functionality to print details of a parsed molecule object to the console. 
public class PrintMolecule
{
    public int numAtoms { get; set; }
    public int numBonds { get; set; }

    //Parses the molecule from a file, prints the atom symbols and bond types, and returns the counts of atoms and bonds.
    public int[] PrintMol(string filepath)
    {
        SdfParser Parser = new SdfParser();
        Molecule PrintMol = Parser.Parse(File.ReadAllLines(filepath));

        int Acounter = 0;
        foreach (Atom i in PrintMol.Atoms)
        {
            Console.WriteLine(i.ElementType);
            Acounter += 1;
        }

        int Bcounter = 0;
        foreach (Bond i in PrintMol.Bonds)
        {
            Console.WriteLine(i.BondType);
            Bcounter += 1;
        }
        int[] retrieve = { Acounter, Bcounter };
        return retrieve;

    }
}