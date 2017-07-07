using PrintingApp.Models.CustomEventArgs;
using System;
using System.Windows.Forms;

namespace PrintingApp.Views {
    public interface IPrintingDesignView {
        string Scale { set; }
        Form Form { get; }
        string ErrorMessage { get; set; }

        event EventHandler<EventArgs> ResizeForm;
        event EventHandler<EventArgs> ClearPaintedItems;
        event EventHandler<EventArgs> OnFormLoad;
        event EventHandler<ErrorEventArgs> LogErrorToView;
        event EventHandler<EventArgs> SetImpositionFormAsActive;
    }
}
