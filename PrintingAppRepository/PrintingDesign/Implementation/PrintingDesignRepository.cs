using System;
using PrintingAppRepository.PrintingDesign.Models;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PrintingAppRepository.PrintingDesign.Implementation {
    public class PrintingDesignRepository : IPrintingDesignRepository {
        //If the program is working in debug the file will be stored in a seperate place 
#if DEBUG
        private readonly string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "Projects" }, StringSplitOptions.None)[0]}Projects\\PrintingApp\\SetupFiles\\PrintingApp.txt";
#else
            private readonly string expectedFilePath = $"{Environment.CurrentDirectory}\\SetupFiles\\PrintingApp.txt";
#endif

        /// <summary>
        /// Read the sheet parameters from the data source and return them
        /// </summary>
        /// <param name="maxClientViewHeight"></param>
        /// <param name="maxClientViewWidth"></param>
        /// <returns></returns>
        public PagePrintingDesignParameters ReturnPagePrintingDesignParams(int maxClientViewHeight, int maxClientViewWidth) {
            if (File.Exists(expectedFilePath)) {
                JToken json = JObject.Parse(File.ReadAllText(expectedFilePath))["PageParameters"];

                PagePrintingDesignParameters existingParams = JsonConvert.DeserializeObject<PagePrintingDesignParameters>(json.ToString());

                //I have to do this because otherwise the existingPrintingParams would be returned with the same size design and you need to pass in the
                //new view height and width to get the new printing design.
                PagePrintingDesignParameters newPrintingDesignParametes = 
                    new PagePrintingDesignParameters(existingParams.OriginalSheetWidth,
                    existingParams.OriginalSheetHeight, existingParams.PagesUp,
                    existingParams.PagesAcross, existingParams.PageSizeWidth,
                    existingParams.PageSizeHeight, existingParams.IsOptionOneChecked,
                    existingParams.Bleeds, maxClientViewHeight, maxClientViewWidth);

                return newPrintingDesignParametes;
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

        /// <summary>
        /// Save any printing design parameters created for later use
        /// </summary>
        /// <param name="pageParameters"></param>
        public void SavePagePrintingDesignParams(PagePrintingDesignParameters pageParameters) {
            if (File.Exists(expectedFilePath)) {
                string pageParametersAsJson = JsonConvert.SerializeObject(pageParameters);

                var currentJsonInFile = JObject.Parse(File.ReadAllText(expectedFilePath));
                currentJsonInFile["PageParameters"] = pageParametersAsJson;
                File.WriteAllText(expectedFilePath, currentJsonInFile.ToString());
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }
    }
}
