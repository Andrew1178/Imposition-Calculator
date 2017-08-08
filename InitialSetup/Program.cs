using InitialSetup.Models;
using Newtonsoft.Json;
using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingAppRepository.SystemVariables.Models;
using System;
using System.IO;

namespace InitialSetup {
    class Program {
        /// <summary>
        /// Create a JSON file if it does not exist already. I have opted to use a JSON file instead 
        /// of a DB because there isn't much data to store so I thought it would be more efficient to
        /// use a JSON file.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            Console.WriteLine("Starting setup...");
            //If debug store a file in a seperate location, else store in another
#if DEBUG
            string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "InitialSetup" }, StringSplitOptions.None)[0]}SetupFiles\\PrintingApp.txt";
#else
            string expectedFilePath = $"{Environment.CurrentDirectory}\\SetupFiles\\PrintingApp.txt";
#endif
            //If file doesnt exist, create the file
            if (!File.Exists(expectedFilePath)) {
                Console.WriteLine("Setup file does not exist..");
                SystemVariables systemVariables = new SystemVariables();

                ComboBoxItem[] ink = new ComboBoxItem[]
                {
                    new ComboBoxItem( "1", 1),
                    new ComboBoxItem( "2", 2),
                    new ComboBoxItem( "3", 3),
                    new ComboBoxItem( "4", 4),
                    new ComboBoxItem( "5", 5),
                    new ComboBoxItem( "6", 6),
                    new ComboBoxItem( "7", 7),
                    new ComboBoxItem( "8", 8),
                    new ComboBoxItem( "None", 0)
                };

                ComboBoxItem[] coating = new ComboBoxItem[]
                {
                   new ComboBoxItem("None", 0),
                   new ComboBoxItem("Gloss varnish", 1),
                   new ComboBoxItem("Dull varnish", 1),
                   new ComboBoxItem("Satin varnish", 1),
                   new ComboBoxItem("Gloss AQ", 0),
                   new ComboBoxItem("Satin AQ", 0),
                };

                SideOptions sideOptions = new SideOptions(ink, coating);

                //Serialise object
                var json = JsonConvert.SerializeObject(new RootJsonObject(sideOptions, systemVariables));

                //Create file and write data to file
                FileInfo file = new FileInfo(expectedFilePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, json);

                Console.WriteLine("Setup file created...");

            }

            Console.WriteLine("Finished setup...");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
