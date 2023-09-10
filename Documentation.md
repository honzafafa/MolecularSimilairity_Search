# DESCRIPTION OF THE PROJECT

I am excited to present to you my end-of-year project for the programming class, which focuses on developing a Chemical Structure Similarity Analysis Tool using Functional-Class Fingerprints (FCFP) and the Tanimoto coefficient as the similarity measure. The motivation behind this project is to create a tool that can be integrated into a virtual screening pipeline to help identify potential hits from a large compound library based on their similarity to known active compounds. This project aligns well with my interests in computational drug design.

**The primary goal of the project is to create a robust and user-friendly tool for performing chemical structure similarity analysis, of already approved drugs. So virtual screeing experiments centered about exploring similarity bewtween those drugs and theri groups can be conducted**

### The main objectives of the project are:

- Develop a class to represent molecules, with properties and methods for storing and manipulating atomic information. Implement functionality for parsing input molecular structures from supported chemical file formats (e.g., SMILES or SDF).

- Implement atom typing functionality to assign each atom in the molecule to a specific functional class based on its chemical properties and local environment.

- Implement a method to generate a unique hash for each atom and its local environment within a specified radius, using a suitable hash function.

- Develop functionality to construct the FCFP for each input molecule using the generated environment hashes.

- Implement the Tanimoto coefficient as a simple similarity measure to compare the generated FCFP fingerprints.

- Design a simple user interface to accept input molecular structures and FCFP parameters, initiate the analysis, and display the results in a clear and concise format.

**Upon completion of the project, the developed tool will be capable of processing and comparing molecular structures in standard chemical file formats, generating FCFP fingerprints for the input molecules, and calculating the similarity between them using the Tanimoto coefficient. The results will be presented in a clear and concise format, such as a similarity matrix or a list of similarity scores.**

In the context of a virtual screening pipeline, this tool could be used to identify potential hits from a large compound library by comparing their FCFP fingerprints to those of known active compounds. Compounds with high similarity scores can then be prioritized for experimental validation, potentially saving time and resources in the drug discovery process.

# USER DOCUMENTATION
(this documentation assumes basic background knowledge in cheminformatics)

 To run the app in you command line (bash) is to run the following commands:
```
cd /YOUR LOACTION/MolecularSimilairity_Search/MolSearch/MolSearch/bin/Debug/net6.0/
dotnet MolSearch.dll
```
The console app will then gude you throut the process, it will ask you to input necessary parameters for the fingerprint generation and that it will ask you for the path to the screened molecule, the screening library.

### All of the parameters imputed
- radius of enviroment of each atom, used to generated is't type
- length (nuber of bits) of the fingerprints calculated
- directory path to the target molecule
- directory path to the screening library (**WARNING** PLEASE make sure that each of the molecules in the screenign library has a unique identifies on the first row in the sdf file)
- directory path to where you want to save results from you screening (should be not yet created txt file)

You can of course you your own molecules and screening libraries (in the stadart sdf format) but PLEASE be sure you are comparing the same types of SDF files (for example, some sdf files include structurally uninportand hydrogens and some don't).\
The app has some testing data included in its desing in the case you would want to access those here are the paths to some of them:\
\
**THE TEST TARGET MOLECULES:**
```
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Fentanyl.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Morphine.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Oxycodone.sdf
```
**THE TEST SCREENING LIBRARIES:** 
```
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/20200113-L1300-FDA-approved-Drug-Library.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/20230828-L1300-FDA-approved-Drug-Library.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/Approveddrugslibrary.sdf
```
You shoul be able to determine your own path of where to save the full results of the screen. The should be saved in a TEXT FILE\
\
**!! WARNING !!** \
the app doesn't check if the file you are writing to already exists so if it does, expect that it will completely rewrite it or at least alter it

### Form of the results 
The results from the screen, will be printed to your resutls file (.txt) in following format:\
\
each of the molecuels in the screening library will have its unique row pritned to the results fiele\
the row will be structured as:\
\
*Key: "uniques identified of the screened molecule", Value: "Similary of target and screeaned molecules measured"*

**That should be everything necessary for standart usage of the program :)** 

# Developer documentation 
(this documentation assumes that you have background knowledge in cheminformatics)

**Please note that the algoritms implemented were choosen to accomodate the standarts in chemoinfomratics but parts of them we simplified for the possiblity of implemtning the program in timely manner**

Here is a list of the parts that were simplified in comparision with the cheminformatics standart:
- the basic atom typing functionality:
   - In Standart FCFP generation programmes, atom typing capabilites include charge, bond hybridization, functional groups and etc... I have choosed to no include those in my atom typing algoritm for following reason: These atomy typing cabalities are apply to many specifi case and many edge cases becouse of that they could be implemnted as extemetly lage switch so implememnting them would really be coding challenge but a date entry challenge, and so I don't think implementing them would be meaningful for the purpose of this project.
   - I have implemeted the atom typing capability in a simpleifed manned where I take into account only the type of element and the types and count of bonds connected to it. This cactures mostly the structuraly similarities between molecules which is sufficuent for this project.
 
 Now I will proceed in domcumenting each of the modules present in this app, here is the list of them with decription which should be sufficient for uderstanding of the expert user.\
 \
The program has 2 parts, main part and accomdading libries, in this list I don't distinguish in betweend them and list all of the modules on individual file level of abstaction:
## 1. Program.cs
### Documentation for `Program.cs`

---

#### Overview:

This file is the entry point for the virtual screening application. It drives the interaction between the user and the software, enabling the user to screen a target molecule against a library of molecules to find similarities. The results are presented in the console and written to a file.

---

#### Classes:

1. **Program**: The primary class that contains the `Main` method, driving the flow of the application.

#### Methods:

1. **Main**: 
   - Initializes the application interface.
   - Reads the target molecule and the molecular library.
   - Calculates the Tanimoto similarity coefficients between the target molecule and each molecule in the library.
   - Writes the similarity measurements to an output file.
   - Displays the top 10 molecules with the highest similarity in a table format.

2. **DisplayTable**: 
   - Input: Lists of molecule names and their respective similarity scores.
   - Purpose: Displays a table format of the top 10 most similar molecules on the console.

3. **GetLongestStringLength**:
   - Input: An `IEnumerable` of strings.
   - Purpose: Determines the length of the longest string in the collection.
   - Returns: The length of the longest string.

---

#### Dependencies:

1. **System**: Provides essential data types, events, event handlers, interfaces, attributes, and processing exceptions.
2. **System.Collections**: Contains interfaces and classes that define various collections of objects.
3. **Test**: Presumably a namespace for debugging purposes (not detailed in the provided code).
4. **MolMesure**: A namespace for measuring molecular similarities.
5. **LibraryProcessor**: A namespace dedicated to reading and processing molecular libraries.
6. **Interace**: Contains the user interface elements for the application.
7. **System.Data**: Provides access to classes representing the ADO.NET architecture.

---

#### Usage:

Run the program, and follow the on-screen prompts to provide the necessary inputs such as paths to the target molecule, the molecular library, and the desired output directory. The program will process the inputs, calculate similarities between the target molecule and the library, and provide results in both the console and a specified output file.

## 2. Inteface.cs

### Programmer Documentation for the `ApplicationInterface` class in `Interace` namespace

---

#### Overview:

This class serves as the main user interface for a virtual screening tool. It gathers user inputs for the parameters required to screen a molecule against a library of molecules. The tool identifies potential similarities and patterns in molecular structures.

---

#### Classes:

**1. ApplicationInterface:**

- **Properties:**
  - `Radius`: Represents the radius for Functional Class FingerPrints (FCFP) generation.
  - `Length`: Specifies the length of the generated fingerprints.
  - `TargetPath`: Holds the path to the target molecule's file.
  - `LibraryPath`: Holds the path to the library of molecules against which the target molecule will be screened.
  - `ResultsPath`: Specifies the directory path where the results will be saved.

- **Methods:**
  - `Start()`: This method initializes the interface, displays introductory messages, and guides the user through the process of providing necessary inputs for the virtual screening. It validates the user's inputs and sets the properties of the class based on them.

---

#### Dependencies:

- `System`: Provides fundamental classes for base datatypes.
- `System.Diagnostics`: Provides classes to interact with system processes, event logs, and performance counters.
- `System.IO`: Contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.

---

#### Usage:

To use the `ApplicationInterface` class:

1. Create an instance of the class.
2. Call the `Start()` method to initialize the interface and gather the necessary inputs from the user.

## 3. ConvertToType.cs
### Programmer Documentation for `ConvertToHash` namespace

---

### Overview:

This namespace, `ConvertToHash`, provides functionality to represent and manipulate molecular structures. It allows the parsing of molecular data from SDF (Structure-Data File) format into a more structured `Molecule` representation. Additionally, it offers a mechanism to generate Functional Class FingerPrints (FCFP) for a given molecule, which can be useful for molecular similarity comparisons.

---

### Dependencies:

- System.Text
- System.Security.Cryptography
- System.Collections

---

### Classes:

#### 1. Atom

- **Purpose**: Represents an atomic element and its neighbors in a molecular structure.
- **Properties**:
  - `ElementType`: The type or symbol of the atom.
  - `Neighbors`: List of neighboring atoms.
- **Methods**:
  - `AddNeighbour(Atom atom)`: Adds a neighboring atom to the current atom's list of neighbors.

#### 2. Bond

- **Purpose**: Represents a bond between two atoms.
- **Properties**:
  - `Atom1`: The first atom in the bond.
  - `Atom2`: The second atom in the bond.
  - `BondType`: Integer representing the type of the bond.
- **Constructor**:
  - `Bond(Atom atom1, Atom atom2, int bondType)`: Initializes the bond with two atoms and a bond type.

#### 3. Molecule

- **Purpose**: Represents a molecular structure comprising of atoms and bonds.
- **Properties**:
  - `Atoms`: List of atoms in the molecule.
  - `Bonds`: List of bonds in the molecule.
- **Methods**:
  - `AddAtom(Atom atom)`: Adds an atom to the molecule.
  - `AddBond(Bond bond)`: Adds a bond to the molecule.

#### 4. SdfParser

- **Purpose**: Provides functionality to parse SDF (Structure-Data File) formatted data into a `Molecule` object.
- **Methods**:
  - `Parse(string[] lines)`: Parses an array of strings (representing lines from an SDF file) into a `Molecule` object.

#### 5. FCFPGenerator

- **Purpose**: Generates Functional Class FingerPrints (FCFP) for a given molecule.
- **Methods**:
  - `GenerateFcfp(Molecule molecule, int radius, int length)`: Generates the FCFP for a given molecule based on the specified radius and length.
  - `GetType(Atom atom, Molecule molecule)`: Determines the type of an atom based on its atomic symbol and bond types.
  - `GetEnvironment(Atom atom, int radius)`: Retrieves the environment of an atom up to a specified radius.
  - `GetEnvironmentDescriptor(List<Atom> environment, Molecule molecule)`: Creates a string descriptor for the atoms in a given environment.
  - `GetHash(string input)`: Computes a hash value for a given input string using the SHA256 hashing algorithm.

---

### Usage Notes:

1. To read SDF formatted data and convert it into a `Molecule` object, instantiate the `SdfParser` class and use the `Parse` method.
2. To generate Functional Class FingerPrints (FCFP) for a molecule, instantiate the `FCFPGenerator` class and use the `GenerateFcfp` method with the desired radius and length.
3. The `BondType` in the `Bond` class can be used to differentiate between single, double, and triple bonds. Adjustments can be made to accommodate other bond types if necessary.

## 4. Measure.cs
### Programmer Documentation for `MolMesure` namespace

---

### Overview:

The `MolMesure` namespace houses the `SimilarityCalculator` class which offers functionality to compute the similarity between two molecular fingerprints. The primary similarity metric employed is the Tanimoto coefficient (also known as the Jaccard similarity coefficient). This coefficient provides a value between 0 (completely dissimilar) and 1 (completely similar) to represent the degree of similarity between two binary fingerprints.

---

### Dependencies:

- System.Collections

---

### Classes:

#### 1. SimilarityCalculator

- **Purpose**: Provides functionality to compute the Tanimoto coefficient between two molecular fingerprints.
  
- **Methods**:

  - `Tanimoto(BitArray fingerprint1, BitArray fingerprint2)`: 
    - **Purpose**: Calculates the Tanimoto coefficient between two fingerprints.
    - **Parameters**: 
      - `fingerprint1`: The first molecular fingerprint as a `BitArray`.
      - `fingerprint2`: The second molecular fingerprint as a `BitArray`.
    - **Return**: A `decimal` value representing the Tanimoto coefficient. A value close to 1 indicates high similarity, and a value close to 0 indicates low similarity.
    - **Exceptions**: Throws an `ArgumentException` if the two fingerprints are not of the same length.
    - **Notes**: The method computes the Tanimoto coefficient using the formula: 
      
      \[
      \text{Tanimoto} = \frac{\text{intersection}}{\text{union}}
      \]
      
      where "intersection" is the count of bits set in both fingerprints, and "union" is the count of bits set in either fingerprint.

---

### Usage Notes:

1. The `SimilarityCalculator` class is designed to work with binary molecular fingerprints represented as `BitArray` objects. Ensure that the fingerprints being compared are of the same length.
2. The Tanimoto coefficient is a commonly used metric in cheminformatics to measure the similarity between molecular fingerprints. It provides an intuitive measure, where a value of 1 indicates that the fingerprints are identical, and a value of 0 indicates that they have no bits in common.
3. Before calling the `Tanimoto` method, it's crucial to ensure that the fingerprints are of the same length to avoid `ArgumentException`.

---

## 5. ParseLibrary.cs
### LibraryProcessor Namespace Documentation

---

#### LibraryReader Class

The `LibraryReader` class is responsible for reading molecular library files, parsing them, and generating fingerprints for each molecule using the Functional Class FingerPrints (FCFP) method.

##### Properties:

- **Generator**: An instance of the `FCFPGenerator` class responsible for creating fingerprints for molecules.
- **Parser**: An instance of the `SdfParser` class that parses molecules from the Structure-Data File (SDF) format.

##### Methods:

- **Read(string filePath, int radius, int length)**:
    - **Description**: Reads a molecular library file and generates fingerprints for each molecule in the library.
    - **Parameters**:
        - **filePath**: Path to the molecular library file.
        - **radius**: The radius used for FCFP generation.
        - **length**: The length of the generated fingerprint.
    - **Returns**: A dictionary where keys are molecule names or unique identifiers, and values are their corresponding fingerprints (as `BitArray`).
    - **Remarks**: For molecules that lack a unique identifier, the filename will be used as the molecule's name.

- **SplitByDelimiter(string[] lines, string delimiter)**:
    - **Description**: Splits the given array of lines into separate sections based on the specified delimiter.
    - **Parameters**:
        - **lines**: An array of strings representing the content to be split.
        - **delimiter**: The delimiter string used to identify section breaks.
    - **Returns**: A list of string arrays, where each array represents a separate section split by the delimiter.
    - **Remarks**: This method is particularly useful for parsing files where individual entries are separated by a specific delimiter (e.g., `$$$$` in SDF format).

## 6. Testing.cs
### Programmer Documentation for the `Test` namespace

---

### Overview:
The `Test` namespace contains classes that are specifically designed for debugging purposes. This file is not required for the core functionality of the system but may be useful for future modifications and troubleshooting. The namespace primarily aids in reading and displaying molecular data.

---

### Dependencies:
- `ConvertToHash` namespace: For parsing molecules using the `SdfParser` class and representing molecular structures using the `Molecule`, `Atom`, and `Bond` classes.

---

### Classes:

#### 1. `ReadFile`
- **Description**: A utility class that provides functionality to read and print the contents of a file to the console.
- **Methods**:
  - `Print(string filepath)`: Reads the contents of the specified file and prints each line to the console.

#### 2. `PrintMolecule`
- **Description**: A utility class designed to print specific details of a parsed molecule object to the console. This class is particularly useful for debugging and verifying the correctness of parsed molecules.
- **Properties**:
  - `numAtoms`: Stores the number of atoms in the molecule after parsing.
  - `numBonds`: Stores the number of bonds in the molecule after parsing.
- **Methods**:
  - `PrintMol(string filepath)`: This method parses a molecule from the given file, prints out the symbols of all atoms and types of all bonds in the molecule, and then returns an integer array containing the number of atoms and bonds.

---

### Usage:
These classes are primarily intended for debugging and testing. To use them, instantiate the desired class and call the appropriate method, passing the file path of the molecule to be parsed or printed. 


# FINAL COMMENTS
Overall I found this this project quite interesting and learned a lot about cheminfomratic methods. The project might be slightly more algrothmically simple that usual, but I think that the topic of the project is iteresting enougth to justifi this change. The development of the console app went quite well and I think I am presenting here a complete solution ready to use in the ways documented here. 

Jan Faflik, 10.9.2023
