using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace ConvertToHash;

public class Atom
// this class exists to store data about given atom
{
    public string ElementType { get; set; }
    public List<Atom> Neighbors { get; set; } = new List<Atom>();

    public Atom(string elementType)
    {
        ElementType = elementType;

    }

    //this method is useful for adding infomration about given neighnout to an atom
    public void AddNeighbour(Atom atom)
    {
        Neighbors.Add(atom);
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
    public Molecule Parse(string[] lines)
    // this method exist to parse sdf file into a class molecule
    // it returns the molecule object creating acording to the sdf file
    {
        Molecule molecule = new Molecule();

        //try
        //{
        //    // Extract the number of atoms and bonds from the counts line
        //    int numAtoms = int.Parse(lines[3].Substring(0, 3).Trim());
        //    int numBonds = int.Parse(lines[3].Substring(3, 3).Trim());

        //    // Parse atoms
        //    for (int i = 0; i < numAtoms; i++)
        //    {
        //        var line = lines[4 + i];  // atom lines start after the counts line

        //        var symbol = line.Substring(31, 3).Trim();

        //        var atom = new Atom(symbol);
        //        molecule.AddAtom(atom);
        //    }

        //    // Parse bonds
        //    for (int i = 0; i < numBonds; i++)
        //    {
        //        var line = lines[4 + numAtoms + i];  // bond lines start after the atom lines
        //        var atom1Index = int.Parse(line.Substring(0, 3).Trim()) - 1; // -1 because indices are 1-based in SDF
        //        var atom2Index = int.Parse(line.Substring(3, 3).Trim()) - 1;
        //        var bondType = int.Parse(line.Substring(6, 3).Trim());

        //        var bond = new Bond(molecule.Atoms[atom1Index], molecule.Atoms[atom2Index], bondType);

        //        molecule.Atoms[atom1Index].AddNeighbour(molecule.Atoms[atom2Index]);
        //        molecule.Atoms[atom2Index].AddNeighbour(molecule.Atoms[atom1Index]);

        //        molecule.AddBond(bond);
        //    }

        //}

        //catch
        //{
        //    Console.WriteLine(lines[3]);
        //    Console.WriteLine("Retarded data");

        //}

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

public class FCFPGenerator
{
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

    private string GetType(Atom atom, Molecule molecule)
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

    private List<Atom> GetEnvironment(Atom atom, int radius)
    {
        List<Atom> environment = new List<Atom>();
        HashSet<Atom> visited = new HashSet<Atom>();
        Queue<Atom> queue = new Queue<Atom>();
        int currentRadius = 0;

        queue.Enqueue(atom);
        visited.Add(atom);

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

    private string GetEnvironmentDescriptor(List<Atom> environment, Molecule molecule)
    //This method creates a sorted descriptor of all the atoms in the atoms enviroment
    {
        // Sort the atoms by their types
        environment.Sort((a, b) => GetType(a, molecule).CompareTo(GetType(b, molecule)));

        // Concatenate the atom types into a string
        StringBuilder descriptor = new StringBuilder();
        foreach (Atom atom in environment)
        {
            descriptor.Append(atom.GetType());
            descriptor.Append('-');
        }

        return descriptor.ToString();
    }

    private int GetHash(string input)
    //This method uses the SHA256 cryptographic method to Hast the atom types
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