using PrintingApp.Models.CustomEventArgs;
using System;

namespace PrintingApp.Models.ImpositionFormAttributes {
    public interface IEvents {
        event EventHandler<EventArgs> CalculateSheetSizes;
        event EventHandler<ErrorEventArgs> Error;
        event EventHandler<EventArgs> ClearPage;
        event EventHandler<EventArgs> CalculateSignatureSize;
        event EventHandler<EventArgs> OpenSystemVariablesForm;
        event EventHandler<EventArgs> FormOnLoad;
        event EventHandler<EventArgs> CalculatePossibleOptions;
        event EventHandler<EventArgs> CalculateSideOneColourValue;
        event EventHandler<EventArgs> CalculateSideTwoColourValue;
        event EventHandler<EventArgs> CalculateFinalImpositionPlates;
        event EventHandler<EventArgs> SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged;
        event EventHandler<EventArgs> PopulateOptionLabels;
        event EventHandler<EventArgs> SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked;
        event EventHandler<EventArgs> CreatePrintingDesign;
        event EventHandler<EventArgs> SetPrintingDesignFormAsActive;
        event EventHandler<EventArgs> RefreshData;

    }
}
