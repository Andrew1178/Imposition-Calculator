using System;
using System.Collections.Generic;
using PrintingAppRepository.SystemVariables.Models;

namespace PrintingAppRepository.SystemVariables.Implementation {
    public class SystemVariablesManager : ISystemVariablesManager {
        private ISystemVariablesRepository _systemVariablesRepo;
        public SystemVariablesManager(ISystemVariablesRepository systemVariablesRepo) {
            _systemVariablesRepo = systemVariablesRepo;
        }

        public void AddListBoxValue(string propertyName, object sheetSizeValue) {
            _systemVariablesRepo.AddListBoxValue(propertyName, sheetSizeValue);
        }

        public void DeleteListBoxValues(string propertyName, string dataType, List<object> valuesToRemove) {
            _systemVariablesRepo.DeleteListBoxValues(propertyName, dataType, valuesToRemove);
        }

        public void ModifyPrintingStyleValues(string printingSyle, PrintingStyleClass valuesToChange) {
            _systemVariablesRepo.ModifyPrintingStyleValues(printingSyle, valuesToChange);
        }

        public List<string> ReturnAllPrintingStyles() {
            return _systemVariablesRepo.ReturnAllPrintingStyles();
        }

        public List<object> ReturnListBoxValues(string propertyName) {
            return _systemVariablesRepo.ReturnListBoxValues(propertyName);
        }

        public Models.SystemVariables ReturnNudVariables() {
            return _systemVariablesRepo.ReturnNudVariables();
        }

        public PrintingStyleClass ReturnPrintingStyleValuesBasedOnPassedInStyle(string printingStyle ) {
            if (string.IsNullOrEmpty(printingStyle))
                throw new Exception("Printing Style cannot be null or empty.");

            return _systemVariablesRepo.ReturnPrintingStyleValuesBasedOnPassedInStyle(printingStyle);
        }

        public void SetNudVariables(Models.SystemVariables systemVariables) {
            _systemVariablesRepo.SetNudVariables(systemVariables);
        }
    }
}
