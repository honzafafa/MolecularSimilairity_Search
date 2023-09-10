# USER DOCUMENTATION
(this documentation assumes basic background knowledge in cheminformatics)

 To run the app in you command line (bash) is to run the following commands:
```
cd /YOUR LOACTION/MolecularSimilairity_Search/MolSearch/MolSearch/bin/Debug/net6.0/
dotnet MolSearch.dll
```
The console app will then gude you throut the process, it will ask you to input necessary parameters for the fingerprint generation and that it will ask you for the path to the screened molecule, the screening library.\

### All of the parameters imputed** 
- radius of enviroment of each atom, used to generated is't type
- length (nuber of bits) of the fingerprints calculated
- directory path to the target molecule
- directory path to the screening library (WARNING PLEASE make sure that each of the molecules in the screenign library has a unique identifies on the first row in the sdf file)
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
!! WARNING !! \
the app doesn't check if the file you are writing to already exists so if it does, expect that it will completely rewrite it or at least alter it\

### Form of the results 
The results from the screen, will be printed to your resutls file (.txt) in following format:\
\
each of the molecuels in the screening library will have its unique row pritned to the results fiele\
the row will be structured as:\
Key: "uniques identified of the screened molecule", Value: "Similary of target and screeaned molecules measured"\

**That should be everything necessary for standart usage of the program :)** 

# Developer documentation 
(this documentation assumes that you have background knowledge in cheminformatics)


