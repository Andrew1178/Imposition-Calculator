using PrintingAppRepository.PrintingDesign.Models;
using System.Collections.Generic;
using System.Drawing;

namespace PrintingAppRepository.PrintingDesign {
    public interface IPrintingDesignManager {
        Rectangle ReturnSheet(SheetPrintingDesignParameters sheetParameters);
        IList<Rectangle> ReturnPages(PagePrintingDesignParameters pageParameters);
        void SavePagePrintingDesignParams(PagePrintingDesignParameters pageParameters);
        PagePrintingDesignParameters ReturnPagePrintingDesignParams(int maxClientViewHeight, int maxClientViewWidth);
    }
}
