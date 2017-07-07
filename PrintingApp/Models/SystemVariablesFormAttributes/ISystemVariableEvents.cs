using PrintingApp.Models.CustomEventArgs;
using System;

namespace PrintingApp.Models.SystemVariablesFormAttributes {
    public interface ISystemVariableEvents {
        event EventHandler<EventArgs> AddSheetSizeValue;
        event EventHandler<EventArgs> RemoveSheetSizeValue;
        event EventHandler<EventArgs> AddCutOffValue;
        event EventHandler<EventArgs> RemoveCutOffValues;
        event EventHandler<EventArgs> AddRollSizeValue;
        event EventHandler<EventArgs> RemoveRollSizeValues;
        event EventHandler<ErrorEventArgs> Error;
        event EventHandler<EventArgs> SetSystemVariables;
        event EventHandler<EventArgs> OnFormLoad;
        event EventHandler<EventArgs> ImpositionFormActivated;
        event EventHandler<EventArgs> CboPrintingStyleChanged;
        event EventHandler<EventArgs> ModifyPrintingStyle;
    }
}
