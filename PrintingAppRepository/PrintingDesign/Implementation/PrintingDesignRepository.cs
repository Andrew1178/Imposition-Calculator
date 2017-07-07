using System;
using PrintingAppRepository.PrintingDesign.Models;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PrintingAppRepository.PrintingDesign.Implementation {
    public class PrintingDesignRepository : IPrintingDesignRepository {
#if DEBUG
        private readonly string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "Projects" }, StringSplitOptions.None)[0]}Projects\\PrintingApp\\SetupFiles\\PrintingApp.txt";
#else
            private readonly string expectedFilePath = $"{Environment.CurrentDirectory}\\SetupFiles\\PrintingApp.txt";
#endif
        public PagePrintingDesignParameters ReturnPagePrintingDesignParams(int maxClientViewHeight, int maxClientViewWidth) {
            if (File.Exists(expectedFilePath)) {
                JToken json = JObject.Parse(File.ReadAllText(expectedFilePath))["PageParameters"];

                PagePrintingDesignParameters existingPrintingParams = JsonConvert.DeserializeObject<PagePrintingDesignParameters>(json.ToString());

                //I have to do this because otherwise the existingPrintingParams would be returned with the same size design and you need to pass in the
                //new view height and width to get the new printing design.
                PagePrintingDesignParameters newPrintingDesignParametes = new PagePrintingDesignParameters(existingPrintingParams.OriginalSheetWidth, existingPrintingParams.OriginalSheetHeight,
                    existingPrintingParams.PagesUp, existingPrintingParams.PagesAcross, existingPrintingParams.PageSizeWidth, existingPrintingParams.PageSizeHeight,
                    existingPrintingParams.IsOptionOneChecked, existingPrintingParams.Bleeds, maxClientViewHeight, maxClientViewWidth);

                return newPrintingDesignParametes;
            }
            else {
                throw new Exception("Setup file not located. Please run the Inital Set up application. Please ask Andrew for more information.");
            }
        }

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
