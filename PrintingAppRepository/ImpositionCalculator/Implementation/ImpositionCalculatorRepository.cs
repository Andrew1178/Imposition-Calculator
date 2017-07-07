using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrintingAppRepository.ImpositionCalculator.Model;
using System;
using System.IO;

namespace PrintingAppRepository.ImpositionCalculator.Implementation {
    public class ImpositionCalculatorRepository : IImpositionCalculatorRepository {
#if DEBUG
        private readonly string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "Projects" }, StringSplitOptions.None)[0]}Projects\\PrintingApp\\SetupFiles\\PrintingApp.txt";
#else
            private readonly string expectedFilePath = $"{Environment.CurrentDirectory}\\SetupFiles\\PrintingApp.txt";
#endif
        public ComboBoxItem[] ReturnCoatingDataSource() {
            //try retrieve values. If they do not exist, throw error asking user to run inital set up
            if (File.Exists(expectedFilePath)) {
                var json = JObject.Parse(File.ReadAllText(expectedFilePath))["SideOptions"]["Coating"];
                return JsonConvert.DeserializeObject<ComboBoxItem[]>(json.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }
        public ComboBoxItem[] ReturnInkDataSource() {
            //try retrieve values. If they do not exist, throw error asking user to run inital set up
            if (File.Exists(expectedFilePath)) {
                var json = JObject.Parse(File.ReadAllText(expectedFilePath))["SideOptions"]["Ink"];
                return JsonConvert.DeserializeObject<ComboBoxItem[]>(json.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }
    }
}
