using PrintingApp.Models.ImpositionFormAttributes;
using PrintingApp.Presenters;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrintingApp.Views {
    public enum PrintingStyle { SheetFed, HeatSet, UV, Digital }
    public interface IImpositionFormView : ISheetSize, ISides, IOptions, IFinalImposition, IEvents {
        ImpositionCalculatorPresenter Presenter { get; set; }
        Form ImpositionForm { get; }
        int PageCount { get; set; }
        string PagesPerSignature { get; set; }
        float CutOff { get; set; }
        float RollSize { get; set;  }
        float Gripper { get; set; }
        float TailMargin { get; set; }
        float SideMargin { get; set; }
        float Bleeds { get; set; }
        int PageSizeWidth { get; set; }
        int PageSizeLength { get; set; }
        float SignatureSizeWidth { get; set; }
        float SignatureSizeLength { get; set; }
        string ErrorMessage { get; set; }
        bool PrintingDesignFromBtnVisibility { get; set; }
        bool SheetFedCheckedValue { get; set; }
        PrintingStyle PrintingStyleChecked { get; set; }
        bool ErrorBoxShown { set; }
        List<float> CutOffDataSource { get; set; }
        List<float> RollSizeDataSource { get; set; }
    }
}
