using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using PrintingAppRepository.SystemVariables.Models;

namespace PrintingAppRepository.SystemVariables.Implementation {
    public class SystemVariablesRepository : ISystemVariablesRepository {
        //Change the file location based on whether we are running in debug or releases
#if DEBUG
        private readonly string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "Projects" }, StringSplitOptions.None)[0]}Projects\\PrintingApp\\SetupFiles\\PrintingApp.txt";
#else
            private readonly string expectedFilePath = $"{Environment.CurrentDirectory}\\SetupFiles\\PrintingApp.txt";
#endif

        /// <summary>
        /// This method is a generic method which will store any data which comes from a ListBox, you
        /// just need to pass in the properties name and the value to add
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="valueToAdd"></param>
        public void AddListBoxValue(string propertyName, object valueToAdd) {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));

                List<object> list = JsonConvert.DeserializeObject<List<object>>(
                    currentJsonInFile["SystemVariables"][propertyName].ToString());

                list.Add(valueToAdd);

                currentJsonInFile["SystemVariables"][propertyName] = JsonConvert.SerializeObject(list);

                File.WriteAllText(expectedFilePath, currentJsonInFile.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        /// <summary>
        /// This is a generic method which will delete any list box value(s) you provide
        /// You must provide the property name and the data type in order to remove the values you wish
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="dataType"></param>
        /// <param name="valuesToRemove"></param>
        public void DeleteListBoxValues(string propertyName, string dataType, List<object> valuesToRemove) {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));
                List<object> list = JsonConvert.DeserializeObject<List<object>>(currentJsonInFile["SystemVariables"][propertyName].ToString());

                switch (dataType) {
                    case "float":
                        List<float> floatList = list.ConvertAll(r => Convert.ToSingle(r));
                        List<float> valuesToRemoveAsFloats = valuesToRemove.ConvertAll(r => Convert.ToSingle(r));
                        floatList.RemoveAll(r => valuesToRemoveAsFloats.Contains(r));
                        list = floatList.Cast<object>().ToList();
                        break;
                    default:
                        List<string> stringList = list.ConvertAll(r => r.ToString());
                        List<string> valuesToRemoveAsStrings = valuesToRemove.ConvertAll(r => r.ToString());
                        stringList.RemoveAll(r => valuesToRemoveAsStrings.Contains(r));
                        list = stringList.Cast<object>().ToList();
                        break;
                }


                if (list.Count == 0) {
                    currentJsonInFile["SystemVariables"][propertyName] = JsonConvert.SerializeObject(new List<float>());
                }
                else {
                    currentJsonInFile["SystemVariables"][propertyName] = JsonConvert.SerializeObject(list);
                }
                File.WriteAllText(expectedFilePath, currentJsonInFile.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        /// <summary>
        /// Update any of the printing style attribles
        /// </summary>
        /// <param name="printingStyle"></param>
        /// <param name="valuesToChange"></param>
        public void ModifyPrintingStyleValues(string printingStyle, PrintingStyleClass valuesToChange) {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));
                currentJsonInFile["SystemVariables"]["PrintingStyles"][printingStyle]["Bleeds"] = valuesToChange.Bleeds;
                currentJsonInFile["SystemVariables"]["PrintingStyles"][printingStyle]["SideMargin"] = valuesToChange.SideMargin;
                currentJsonInFile["SystemVariables"]["PrintingStyles"][printingStyle]["TailMargin"] = valuesToChange.TailMargin;
                currentJsonInFile["SystemVariables"]["PrintingStyles"][printingStyle]["Gripper"] = valuesToChange.Gripper;

                File.WriteAllText(expectedFilePath, currentJsonInFile.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        public List<string> ReturnAllPrintingStyles() {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));
                var printingStyleKeysAndValues = JsonConvert.DeserializeObject<Dictionary<string, PrintingStyleClass>>(currentJsonInFile["SystemVariables"]["PrintingStyles"].ToString());
                return printingStyleKeysAndValues.Select(r => r.Key).ToList();
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        /// <summary>
        /// This is a generic method which can be used to return any values for a WinForms ListBox control
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public List<object> ReturnListBoxValues(string propertyName) {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));
                return JsonConvert.DeserializeObject<List<object>>(currentJsonInFile["SystemVariables"][propertyName].ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        /// <summary>
        /// Return all WinForms numeric up down control values.
        /// These are BindingLip, HeadTrim and FootTrim
        /// </summary>
        /// <returns></returns>
        public Models.SystemVariables ReturnNudVariables() {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath))["SystemVariables"];

                Models.SystemVariables model = new Models.SystemVariables();
                model.BindingLip = float.Parse(currentJsonInFile["BindingLip"].ToString());
                model.HeadTrim = float.Parse(currentJsonInFile["HeadTrim"].ToString());
                model.FootTrim = float.Parse(currentJsonInFile["FootTrim"].ToString());

                return model;
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        public PrintingStyleClass ReturnPrintingStyleValuesBasedOnPassedInStyle(string printingStyle) {
            if (File.Exists(expectedFilePath)) {
                var printingStylesAsJson = JObject.Parse(File.ReadAllText(expectedFilePath))["SystemVariables"]["PrintingStyles"][printingStyle];
                return JsonConvert.DeserializeObject<PrintingStyleClass>(printingStylesAsJson.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        /// <summary>
        /// Parse out json in file
        /// Serialise systemVariables into json
        /// Replace section of json which is now classed as old
        /// Write to file
        /// </summary>
        /// <param name="systemVariables"></param>
        public void SetNudVariables(Models.SystemVariables systemVariables) {
            if (File.Exists(expectedFilePath)) {
                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));
                currentJsonInFile["SystemVariables"]["BindingLip"] = systemVariables.BindingLip;
                currentJsonInFile["SystemVariables"]["HeadTrim"] = systemVariables.HeadTrim;
                currentJsonInFile["SystemVariables"]["FootTrim"] = systemVariables.FootTrim;

                File.WriteAllText(expectedFilePath, currentJsonInFile.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }
    }
}