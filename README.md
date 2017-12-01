# HomeSeer scripts
## Intention
The intention of this project is to make it possible to develop scripts for
HomeSeer within the Visual Studio IDE.  

This project includes a custom HomeSeer-wrapper, which may be configured to
behave as wanted. 

## Development
The script-files are placed within the HomeseerScripts/Scripts folder.  
In the class-file: 
 - Surround the wanted using-statements with "#region Using" and end with "#endregion"
 - Surround the wanted functions with "#region Scripts" and end with "#endregion"

Use the NUnit-framework to simulate the HomeSeer-framework by instrumenting the HomeseerWrapper.  

You do proably no want all the scripts in the folder, since some of them are
specially designed to work on my system (e.g. name of devices or device numbers).
Feel free to use whatever you like of the framework or the scripts.

## Extracting the scripts
In the root-folder there is a script called "generatescripts.sh". This script will
extract the parts surrounded by the #region tags and place the resulting files in 
a folder called hs_scripts.  
These files should be placed within the script-folder on the HomeSeer-system.