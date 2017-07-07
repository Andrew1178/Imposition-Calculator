using PrintingAppRepository.SystemVariables.Models;
using System.Collections.Generic;

namespace PrintingAppRepository.SystemVariables {
    public interface ISystemVariablesRepository {
        void SetNudVariables(Models.SystemVariables systemVariables);
        Models.SystemVariables ReturnNudVariables();
        void AddListBoxValue(string propertyName, object valueToAdd);
        void DeleteListBoxValues(string propertyName, string dataType, List<object> valuesToRemove);
        List<object> ReturnListBoxValues(string propertyName);
        PrintingStyleClass ReturnPrintingStyleValuesBasedOnPassedInStyle(string printingStyle);
        void ModifyPrintingStyleValues(string printingSyle, PrintingStyleClass valuesToChange);
        List<string> ReturnAllPrintingStyles();
    }
}
