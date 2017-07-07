using InitialSetup.Models;
using Newtonsoft.Json;
using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingAppRepository.SystemVariables.Models;
using System;
using System.IO;

namespace InitialSetup {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Starting setup...");
#if DEBUG
            string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "InitialSetup" }, StringSplitOptions.None)[0]}SetupFiles\\PrintingApp.txt";
#else
            string expectedFilePath = $"{Environment.CurrentDirectory}\\SetupFiles\\PrintingApp.txt";
#endif

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
                //new ComboBoxItem( "4cp", 4),
                    //new ComboBoxItem( "Black", 1),
                    //new ComboBoxItem( "2 - PMS", 2),
                    //new ComboBoxItem( "3 - PMS", 3),
                    //new ComboBoxItem( "4 - PMS", 4),
                    //new ComboBoxItem( "5 - PMS", 5),
                    //new ComboBoxItem( "6 - PMS", 6),
                    //new ComboBoxItem( "7 - PMS", 7),
                    //new ComboBoxItem( "8 - PMS", 8),
                    //new ComboBoxItem( "Metallic", 1),
                    //new ComboBoxItem( "K + PMS", 2),
                    //new ComboBoxItem( "K + 2 - PMS", 3),
                    //new ComboBoxItem( "K + 3 - PMS", 4),
                    //new ComboBoxItem( "K + 4 - PMS", 5),
                    //new ComboBoxItem( "4cp + PMS", 5),
                    //new ComboBoxItem( "4cp + 2 - PMS", 6),
                    //new ComboBoxItem( "4cp + 3 - PMS", 7),
                    //new ComboBoxItem( "4cp + 4 - PMS", 8),
                    //new ComboBoxItem( "Fluorescent", 1),
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
                var json = JsonConvert.SerializeObject(new RootJsonObject(sideOptions, systemVariables));

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
