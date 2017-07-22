# Imposition-Calculator
The purpose of this application is to create scaled mock-ups of printing designs that can be used for companies whom use web offset printing designs.

This application is built using C# and WinForms.

The application is currently in beta while being tested upon by an acquaintance of mine. I will continue to update the application as new feature requests are requested and also whenever any bugs are discovered.

# Author
Andrew Kilburn

# Prerequisites
To work upon the application you'll need  at least .NET 4.5 installed.

# Installing

To run the application when debugging you will simply need to run the InitalSetup project, it will create the JSON file needed for the PrintingApp project. 

It should look like this once the application has created the neccessary setup files:

![Inital Setup success](http://imgur.com/5Y2B3UO)

Once you've done this you are free to run the PrintingApp project. It will look like this if everything has been done correctly:

![Imposition Calculator load success](http://imgur.com/0Hd9gxe)

To install the application for release, you will need to build both projects (Inital Setup and PrintingApp) in release mode. You can then navigate to the bin folders of the application and copy out the contents into your chosen location. Once you have down this, run the InitalSetup.exe file. This will create a JSON file which will be used to store some variables. You are then free to play with the application. Run the PrintingApp.exe file to run the application. See below for more information:

![Imposition Calculator release files](http://imgur.com/bGmN3kI)

Once you filled out your options you should have a printing design which looks something like this:

![Imposition Calculator printing design example](http://imgur.com/aqc73Cl)
