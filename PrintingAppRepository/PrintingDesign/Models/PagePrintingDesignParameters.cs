using System;

namespace PrintingAppRepository.PrintingDesign.Models {
    public class PagePrintingDesignParameters : SheetPrintingDesignParameters {
        public PagePrintingDesignParameters(float originalSheetWidth, float originalSheetHeight, int pagesUp, int pagesAcross,float pageSizeWidth, float pageSizeHeight,
            bool isOptionOneChecked, float bleeds, int maxClientViewHeight, int maxClientViewWidth) : base(originalSheetWidth, originalSheetHeight, isOptionOneChecked, maxClientViewHeight, maxClientViewWidth) {
          
            if(pageSizeWidth < 1 || pageSizeWidth > 50) {
                throw new ArgumentOutOfRangeException("Invalid Page Width, it must be greater than 1 inch and less than 50 inches.");
            }
            else if(pageSizeHeight < 1 || pageSizeHeight > 50) {
                throw new ArgumentOutOfRangeException("Invalid Page Length, it must be greater than 1 inch and less than 50 inches.");
            }
            else if(pagesUp < 1 || pagesUp > 50) {
                throw new ArgumentOutOfRangeException("Invalid Pages Up, it must be greater than 1 inch and less than 50 inches.");
            }
            else if(pagesAcross < 1 || pagesAcross > 50) {
                throw new ArgumentOutOfRangeException("Invalid Pages Across, it must be greater than 1 inch and less than 50 inches.");
            }
         
            PageSizeWidth = pageSizeWidth;
            PageSizeHeight = pageSizeHeight;
            PagesUp = pagesUp;
            PagesAcross = pagesAcross;
            Bleeds = bleeds;         
        }
     
        public int PagesUp { get; private set; }
        public int PagesAcross { get; private set; }
        public float PageSizeWidth { get; private set; }
        public float PageSizeHeight { get; private set; }     
        public float Bleeds { get; private set; }

    }
}
