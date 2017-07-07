namespace PrintingAppRepository.SignatureSize.Model {
    public class PropertiesToCalculateOptionsWith {
        public PropertiesToCalculateOptionsWith(float pageSizeWidth, float pageSizeLength, float signatureSizeWidth, float signatureSizeLength,
            float sheetSizeAround, float sheetSizeAcross, float gripper, float tailMargin, float bleeds, float sideMargin) {
            PageSizeWidth = pageSizeWidth;
            PageSizeLength = pageSizeLength;
            SignatureSizeWidth = signatureSizeWidth;
            SignatureSizeLength = signatureSizeLength;
            SheetSizeAround = sheetSizeAround;
            SheetSizeAcross = sheetSizeAcross;
            Gripper = gripper;
            TailMargin = tailMargin;
            Bleeds = bleeds;
            SideMargin = sideMargin;
        }

        internal float PageSizeWidth { get; set; }
        internal float PageSizeLength { get; set; }
        internal float SignatureSizeWidth { get; set; }
        internal float SignatureSizeLength { get; set; }
        internal float SheetSizeAround { get; set; }
        internal float SheetSizeAcross { get; set; }
        internal float Gripper { get; set; }
        internal float TailMargin { get; set; }
        internal float Bleeds { get; set; }
        internal float SideMargin { get; set; }
    }
}
