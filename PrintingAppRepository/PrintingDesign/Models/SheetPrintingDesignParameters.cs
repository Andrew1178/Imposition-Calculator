using System;

namespace PrintingAppRepository.PrintingDesign.Models {
    public class SheetPrintingDesignParameters {
        public SheetPrintingDesignParameters(float originalSheetWidth, float originalSheetHeight, bool isOptionOneChecked, int maxClientViewHeight, int maxClientViewWidth) {
            if (originalSheetWidth < 10 || originalSheetWidth > 50) {
                throw new ArgumentOutOfRangeException("Invalid Sheet Width, it must be greater than 10 inches and less than 50.");
            }
            else if (originalSheetHeight < 10 || originalSheetHeight > 50) {
                throw new ArgumentOutOfRangeException("Invalid Sheet Length, it must be greater than 10 inches and less than 50 inches.");
            }
            PointsPerInch = 72;
            SheetXPosition = 2;
            SheetYPosition = 2;
            OriginalSheetWidth = originalSheetWidth;
            OriginalSheetHeight = originalSheetHeight;
            UpScaledSheetWidth = originalSheetWidth * PointsPerInch - SheetYPosition;
            UpScaledSheetHeight = originalSheetHeight * PointsPerInch - SheetXPosition;
            IsOptionOneChecked = isOptionOneChecked;
            MaxClientViewHeight = maxClientViewHeight;
            MaxClientViewWidth = maxClientViewWidth;
        }
        public int PointsPerInch { get; private set; }
        public int SheetXPosition { get; private set; }
        public int SheetYPosition { get; private set; }
        public bool IsOptionOneChecked { get; private set; }
        public int MaxClientViewHeight { get; set; }
        public int MaxClientViewWidth { get; set; }
        public float OriginalSheetWidth { get; private set; }
        public float OriginalSheetHeight { get; private set; }
        public float UpScaledSheetWidth { get; set; }
        public float UpScaledSheetHeight { get; set; }
        public float ScalingRatio { get; set; }
        public string Scale { get; set; }

    }
}
