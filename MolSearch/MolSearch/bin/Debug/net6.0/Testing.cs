using ConvertToHash;

namespace Test;

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

public class PrintMolecule
//Class exists for dedugging purposes
// it exists to print out infomration about created molecule objects
{
    public int numAtoms { get; set; }
    public int numBonds { get; set; }


    public int[] PrintMol(string filepath)
    // this method returns int[] with numeber of atoms and bonds in the molecules,
    // it also pritns out the symbols of all of the atoms and types of all the bonds
    // It exists only for debugging purposes
    {
        SdfParser Parser = new SdfParser();
        Molecule PrintMol = Parser.Parse(filepath);

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
