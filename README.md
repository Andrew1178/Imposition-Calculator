# Imposition-Calculator
Application used to create scaled mock-ups of printing designs

This application is built using C#, MVP and WinForms.

The application is currently in beta while being tested upon by an acquaintance of mine. I will continue to update the application as new feature requests are requested and also whenever any bugs are discovered.

# Author
Andrew Kilburn

# Prerequisites
To work upon the application you'll need .NET 4.5 installed

# Installing

To run the application when debugging you will simply need to run the InitalSetup project, it will create the JSON file needed for the PrintingApp project.

To install the application for release, you will need to build both projects (Inital Setup and PrintingApp) in release mode. You can then navigate to the bin folders of the application and copy out the contents into your chosen location. Once you have down this, run the InitalSetup.exe file. This will create a JSON file which will be used to store some variables. You are then free to play with the application. Run the PrintingApp.exe file to run the application.
