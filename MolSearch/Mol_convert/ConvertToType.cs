using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace ConvertToHash;

// Represents an atomic element with its neighbors.
public class Atom
{
    public string ElementType { get; set; }
    public List<Atom> Neighbors { get; set; } = new List<Atom>();

    // Initializes a new instance of the Atom class with the specified element type.
    public Atom(string elementType)
    {
        ElementType = elementType;

    }

    // Adds a neighboring atom to the current atom's list of neighbors.
    public void AddNeighbour(Atom atom)
    {
        Neighbors.Add(atom);
    }
}

// Represents a bond between two atoms.
public class Bond
{
    public Atom Atom1 { get; set; }
    public Atom Atom2 { get; set; }
    public int BondType { get; set; }

    // Initializes a new instance of the Bond class with specified atoms and bond type.
    public Bond(Atom atom1, Atom atom2, int bondType)
    {
        Atom1 = atom1;
        Atom2 = atom2;
        BondType = bondType;
    }
}

// Represents a molecular structure consisting of atoms and bonds.
public class Molecule
{
    public List<Atom> Atoms { get; set; }
    public List<Bond> Bonds { get; set; }

    // Initializes a new instance of the Molecule class.
    public Molecule()
    { 
        Atoms = new List<Atom>();
        Bonds = new List<Bond>();
    }

    // Adds an atom to the molecule.
    public void AddAtom(Atom atom)
    {
        Atoms.Add(atom);
    }

    // Adds a bond to the molecule.
    public void AddBond(Bond bond)
    {
        Bonds.Add(bond);
    }
}

// Provides functionality to parse SDF (Structure-Data File) format to Molecule objects.
public class SdfParser
{

    // Parses the provided SDF lines into a Molecule object.
    // It returns the parsed Molecule object.
    public Molecule Parse(string[] lines)

    {
        Molecule molecule = new Molecule();

        // Extract the number of atoms and bonds from the counts line
        int numAtoms = int.Parse(lines[3].Substring(0, 3).Trim());
        int numBonds = int.Parse(lines[3].Substring(3, 3).Trim());

        // Parse atoms
        for (int i = 0; i < numAtoms; i++)
        {
            var line = lines[4 + i];  // atom lines start after the counts line

            var symbol = line.Substring(31, 3).Trim();

            var atom = new Atom(symbol);
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

            molecule.Atoms[atom1Index].AddNeighbour(molecule.Atoms[atom2Index]);
            molecule.Atoms[atom2Index].AddNeighbour(molecule.Atoms[atom1Index]);

            molecule.AddBond(bond);
        }

        return molecule;
    }
}

// Provides functionality to generate FCFP (Functional Class FingerPrints) from a Molecule object.
public class FCFPGenerator
{
    //  Generates the FCFP for a given molecule.
    public BitArray GenerateFcfp(Molecule molecule, int radius, int length)
    {
        BitArray fcfp = new BitArray(length);

        foreach (Atom atom in molecule.Atoms)
        {
            string atomType = GetType(atom, molecule);
            List<Atom> environment = GetEnvironment(atom, radius);
            string environmentDescriptor = GetEnvironmentDescriptor(environment, molecule);

            string hashInput = atomType + environmentDescriptor;
            int hash = GetHash(hashInput);

            int index = hash % length;
            fcfp.Set(index, true);
        }

        return fcfp;
    }

    // Determines the type of the atom based on its atomic symbol and bond types.
    // returs the atom type descriptor string
    private string GetType(Atom atom, Molecule molecule)
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

    // Retrieves the environment of the atom up to a specified radius.
    private List<Atom> GetEnvironment(Atom atom, int radius)
    {
        List<Atom> environment = new List<Atom>();
        HashSet<Atom> visited = new HashSet<Atom>();
        Queue<Atom> queue = new Queue<Atom>();
        int currentRadius = 0;

        queue.Enqueue(atom);
        visited.Add(atom);

        // Breadth-first search to explore the atom's environment
        while (queue.Count > 0 && currentRadius < radius)
        {
            int currentSize = queue.Count;

            for (int i = 0; i < currentSize; i++)
            {
                Atom currentAtom = queue.Dequeue();

                foreach (Atom neighbor in currentAtom.Neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                        environment.Add(neighbor);
                    }
                }
            }

            currentRadius++;
        }

        return environment;
    }

    // Creates a string descriptor of the atoms in the environment.
    private string GetEnvironmentDescriptor(List<Atom> environment, Molecule molecule)
    {
        // Sort the atoms by their types
        environment.Sort((a, b) => GetType(a, molecule).CompareTo(GetType(b, molecule)));

        // Concatenate the atom types into a basic descriptor string
        StringBuilder descriptor = new StringBuilder();
        foreach (Atom atom in environment)
        {
            descriptor.Append(GetType(atom, molecule));
            descriptor.Append('-');
        }

        return descriptor.ToString();
    }

    // Computes a hash for a given input string using SHA256.
    private int GetHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the input string to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(input);

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