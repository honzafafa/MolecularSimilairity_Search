using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ConvertToHash;

public class Atom
// this class exists to store data about given atom
{
    public string ElementType { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Atom(string elementType, double x, double y, double z)
    {
        ElementType = elementType;
        X = x;
        Y = y;
        Z = z;
    }
}

public class Bond
// this class exists to store data about given bond
{
    public Atom Atom1 { get; set; }
    public Atom Atom2 { get; set; }
    public int BondType { get; set; }

    public Bond(Atom atom1, Atom atom2, int bondType)
    {
        Atom1 = atom1;
        Atom2 = atom2;
        BondType = bondType;
    }
}

public class Molecule
// this class exists to store data about given molecule
{
    public List<Atom> Atoms { get; set; }
    public List<Bond> Bonds { get; set; }

    public Molecule()
    {
        Atoms = new List<Atom>();
        Bonds = new List<Bond>();
    }

    public void AddAtom(Atom atom)
    {
        Atoms.Add(atom);
    }

    public void AddBond(Bond bond)
    {
        Bonds.Add(bond);
    }
}

public class SdfParser
// this class exist to parse sdf file into a class molecule
{
    public Molecule Parse(string filePath)
    // this method exist to parse sdf file into a class molecule
    // it returns the molecule object creating acording to the sdf file
    {
        var molecule = new Molecule();

        // Read the file line by line
        string[] lines = File.ReadAllLines(filePath);

        // Extract the number of atoms and bonds from the counts line
        int numAtoms = int.Parse(lines[3].Substring(0, 3).Trim());
        int numBonds = int.Parse(lines[3].Substring(3, 3).Trim());

        // Parse atoms
        for (int i = 0; i < numAtoms; i++)
        {
            var line = lines[4 + i];  // atom lines start after the counts line
            var x = double.Parse(line.Substring(0, 10).Trim());
            var y = double.Parse(line.Substring(10, 10).Trim());
            var z = double.Parse(line.Substring(20, 10).Trim());
            var symbol = line.Substring(31, 3).Trim();

            var atom = new Atom(symbol, x, y, z);
            molecule.AddAtom(atom);
        }

        // Parse bonds
        for (int i = 0; i < numBonds; i++)
        {
            var line = lines[4 + numAtoms + i];  // bond lines start after the atom lines
            var atom1Index = int.Parse(line.Substring(0, 3).Trim()) - 1; // -1 because indices are 1-based in SDF
            var atom2Index = int.Parse(line.Substring(3, 3).Trim()) - 1;
            var bondType = int.Parse(line.Substring(6, 3).Trim());

            var bond = new Bond(molecule.Atoms[atom1Index], molecule.Atoms[atom2Index], bondType);
            molecule.AddBond(bond);
        }

        return molecule;
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

public class AtomTypingEngine
// Purpose of this class is to create the types of atoms necessary to generate the FCFP foingerpints
{ 
    private string BasicAtomType(Atom atom, Molecule molecule)
    // Determine basic atom type based on atomic symbol and bond types
    // returs the atom type descriptor
    {
        int singleBondCount = molecule.Bonds.Count(b => (b.Atom1 == atom || b.Atom2 == atom) && b.BondType == 1);
        int doubleBondCount = molecule.Bonds.Count(b => (b.Atom1 == atom || b.Atom2 == atom) && b.BondType == 2);
        int tripleBondCount = molecule.Bonds.Count(b => (b.Atom1 == atom || b.Atom2 == atom) && b.BondType == 3);

        string descriptor = atom.ElementType;

        if (singleBondCount > 0)
            descriptor += $"-{singleBondCount}";
        if (doubleBondCount > 0)
            descriptor += $"={doubleBondCount}";
        if (tripleBondCount > 0)
            descriptor += $"≡{tripleBondCount}";

        return descriptor;
    }

    public string GetCircularTypeForAtom(Atom atom, Molecule molecule, int radius)
    // Get the circular type for an atom at a given radius
    {
        if (radius == 0)
        {
            return BasicAtomType(atom, molecule);
        }

        List<Atom> neighbors = GetNeighboringAtoms(atom, molecule);
        var types = neighbors.Select(a => GetCircularTypeForAtom(a, molecule, radius - 1));

        return string.Join(",", types.OrderBy(t => t));
    }

    public Dictionary<Atom, List<string>> GetCircularAtomTypes(Molecule molecule, int maxRadius)
    // Assign atom types to a molecule
    {
        Dictionary<Atom, List<string>> circularTypes = new Dictionary<Atom, List<string>>();

        foreach (var atom in molecule.Atoms)
        {
            List<string> atomCircularTypes = new List<string>();
            for (int radius = 0; radius <= maxRadius; radius++)
            {
                atomCircularTypes.Add(GetCircularTypeForAtom(atom, molecule, radius));
            }

            circularTypes[atom] = atomCircularTypes;
        }

        return circularTypes;
    }

    private List<Atom> GetNeighboringAtoms(Atom atom, Molecule molecule)
    // Get neighboring atoms for a given atom in a molecule
    {
        List<Atom> neighbors = new List<Atom>();
        foreach (Bond bond in molecule.Bonds)
        {
            if (bond.Atom1 == atom)
            {
                neighbors.Add(bond.Atom2);
            }
            else if (bond.Atom2 == atom)
            {
                neighbors.Add(bond.Atom1);
            }
        }
        return neighbors;
    }
}

public class FCFPGenerator
//This class generates thš FCFP fingerint of give molecue
{
    //initalize new atomtyping engine
    private AtomTypingEngine typingEngine = new AtomTypingEngine();

    public List<int> GenerateFCFP(Molecule molecule, int maxRadius)
    //Generate the final hast from the molecuel and use fingerpints with given radius
    {
        Dictionary<Atom, List<string>> circularTypes = typingEngine.GetCircularAtomTypes(molecule, maxRadius);
        HashSet<int> hashes = new HashSet<int>();

        int counter = 0;

        foreach (List<string> typesForAtom in circularTypes.Values)
        {
            foreach (string type in typesForAtom)
            {
                int hashValue = ComputeHash(type);
                Console.WriteLine(hashValue);
                hashes.Add(hashValue);
                counter += 1;
            }
        }
        Console.WriteLine(counter);
        return hashes.ToList();
    }

    private int ComputeHash(string type)
    //This method uses the SHA256 cryptographic method to Hast the atom types
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the input string to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(type);

            // Compute the hash
            byte[] hash = sha256.ComputeHash(bytes);

            // Convert the first 4 bytes of the hash to an integer
            int integer = BitConverter.ToInt32(hash, 0);

            // Make sure the integer is positive
            if (integer < 0) integer = -integer;

            return integer;
        }
    }
}