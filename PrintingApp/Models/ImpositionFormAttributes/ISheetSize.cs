using System.Collections.Generic;

namespace PrintingApp.Models.ImpositionFormAttributes {
    /// <summary>
    /// This is a section of the ImpositionForm. I have opted to split it out into different interfaces
    /// </summary>
    public interface ISheetSize { 
        List<string> SheetSizeDataSource { get; set; }
        float SheetSizeAround { get; set; }
        float SheetSizeAcross { get; set; }
        float OverrideSheetSizeAround { get; set; }
        float OverrideSheetSizeAcross { get; set; }
        string CboSheetSize { get; set; }
    }
}
