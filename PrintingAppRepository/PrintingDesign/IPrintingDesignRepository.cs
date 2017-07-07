using PrintingAppRepository.PrintingDesign.Models;

namespace PrintingAppRepository.PrintingDesign {
   public interface IPrintingDesignRepository {
        void SavePagePrintingDesignParams(PagePrintingDesignParameters pageParameters);
        PagePrintingDesignParameters ReturnPagePrintingDesignParams(int maxClientViewHeight, int maxClientViewWidth);
    }
}
