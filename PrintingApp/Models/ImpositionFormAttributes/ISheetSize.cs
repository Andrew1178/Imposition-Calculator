using System.Collections.Generic;

namespace PrintingApp.Models.ImpositionFormAttributes {
    public interface ISheetSize { 
        List<string> SheetSizeDataSource { get; set; }
        float SheetSizeAround { get; set; }
        float SheetSizeAcross { get; set; }
        float OverrideSheetSizeAround { get; set; }
        float OverrideSheetSizeAcross { get; set; }
        string CboSheetSize { get; set; }
    }
}
