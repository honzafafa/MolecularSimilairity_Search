# Molecular Similairity Search
This is a Molecular Similarity search engine, the I created for as my 2nd Semester University project

## How to run this console application?

The easies way is to go to run the app in you command line (bash) is to run the following commands:
```
cd /YOUR LOACTION/MolecularSimilairity_Search/MolSearch/MolSearch/bin/Debug/net6.0/
dotnet MolSearch.dll
```
The console app will then gude you throut the process, it will ask you to input necessary parameters for the fingerprint generation and that it will ask you for the path to the screened molecule, the screening library.\
\
You can of course you your own molecules and screening libraries (in the stadart sdf format) but please be sure you are comparing the same types of SDF file (for example some sdf file includes structurally uninportand hydrogens and some don't).\
The app has some testing data included in its desing in the case you would want to access those here are the paths to some of them:\
\
THE TEST TARGET MOLECULES:
```
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/IGALMI.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Fentanyl.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Morphine.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/MOLECULES/Opioids/Oxycodone.sdf
```
THE TEST SCREENING LIBRARIES:
```
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/20200113-L1300-FDA-approved-Drug-Library.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/20230828-L1300-FDA-approved-Drug-Library.sdf
/YOUR LOCATION/MolecularSimilairity_Search/TEST_DATA/LIBRARIES/Approveddrugslibrary.sdf
```
You shoul be able to determine your own path of where to save the full results of the screen. The should be saved in a TEXT FILE\
\
!! WARNING !! \
the app doesn't check if the file you are writing to already exists so if it does, expect that it will completely rewrite it or at least alter it\
\
## Genral description of the project that was followed during its development

I am excited to propose my end-of-year project for the programming class, which focuses on developing a Chemical Structure Similarity Analysis Tool using Functional-Class Fingerprints (FCFP) and the Tanimoto coefficient as the similarity measure. The motivation behind this project is to create a tool that can be integrated into a virtual screening pipeline to help identify potential hits from a large compound library based on their similarity to known active compounds. This project aligns well with my interests in computational drug design.

The primary goal of the project is to create a robust and user-friendly tool for performing chemical structure similarity analysis. The tool should have the following input and output specifications:

Expected Input:
1. A list of molecular structures in a supported chemical file format (e.g., SMILES or SDF).
2. (Optional) Parameters for FCFP generation, such as the radius.

Expected Output:
1. A similarity matrix or a list of pairwise similarity scores between the input molecules, calculated using the Tanimoto coefficient.

The main objectives of the project are:

1. Develop a class to represent molecules, with properties and methods for storing and manipulating atomic information. Implement functionality for parsing input molecular structures from supported chemical file formats (e.g., SMILES or SDF).

2. Implement atom typing functionality to assign each atom in the molecule to a specific functional class based on its chemical properties and local environment.

3. Implement a method to generate a unique hash for each atom and its local environment within a specified radius, using a suitable hash function.

4. Develop functionality to construct the FCFP for each input molecule using the generated environment hashes.

5. Implement the Tanimoto coefficient as a simple similarity measure to compare the generated FCFP fingerprints.

6. Design a simple user interface to accept input molecular structures and (optionally) FCFP parameters, initiate the analysis, and display the results in a clear and concise format.

Upon completion of the project, the developed tool will be capable of processing and comparing molecular structures in standard chemical file formats, generating FCFP fingerprints for the input molecules, and calculating the similarity between them using the Tanimoto coefficient. The results will be presented in a clear and concise format, such as a similarity matrix or a list of similarity scores.

In the context of a virtual screening pipeline, this tool could be used to identify potential hits from a large compound library by comparing their FCFP fingerprints to those of known active compounds. Compounds with high similarity scores can then be prioritized for experimental validation, potentially saving time and resources in the drug discovery process.



