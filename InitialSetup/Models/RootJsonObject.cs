using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingAppRepository.PrintingDesign.Models;
using PrintingAppRepository.SystemVariables.Models;

namespace InitialSetup.Models {
    /// <summary>
    /// Used as a container to store JSON in a readable format
    /// </summary>
    public class RootJsonObject {
        public RootJsonObject(SideOptions sideOptions, SystemVariables systemVariables) {
            SideOptions = sideOptions;
            SystemVariables = systemVariables;
            PageParameters = null;
        }
        public SideOptions SideOptions { get; set; }
        public SystemVariables SystemVariables { get; set; }
        public PagePrintingDesignParameters PageParameters { get; set; }
    }
}
