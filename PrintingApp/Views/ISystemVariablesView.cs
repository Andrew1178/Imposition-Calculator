using PrintingApp.Models.SystemVariablesFormAttributes;
using System.Collections.Generic;

namespace PrintingApp.Views {
    public interface ISystemVariablesView: ISystemVariableEvents {
        float BindingLip { get; set; }
        float HeadTrim { get; set; }
        float FootTrim { get; set; }
        string ErrorMessage { get; set; }
        float CutOffValueToAdd { get; set; }
        float RollSizeValueToAdd { get; set; }
        string SheetSizeToAdd { get; set; }
        string CurrentPrintingStyle { get; set; }
        List<string> SheetSizeValuesToRemove { get; set; }
        List<string> CurrentSheetSizeValues { get; set; }
        List<float> CutOffValuesToRemove { get; set; }
        List<float> CurrentCutOffValues { get; set; }
        List<float> RollSizeValuesToRemove { get; set; }
        List<float> CurrentRollSizeValues { get; set; }
        List<string> CboPrintingStyleDataSource { get; set; }
        PrintingAppRepository.SystemVariables.Models.PrintingStyleClass CurrentPrintingStyleValues { get; set; }
        bool IsErrorPanelShown { get; set; }

    
    }
}
