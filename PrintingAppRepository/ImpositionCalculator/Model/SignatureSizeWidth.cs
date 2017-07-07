namespace PrintingAppRepository.SignatureSize.Model {
    public class SignatureSizeWidth : BaseSignatureSizeCalculationValues {
        public SignatureSizeWidth(string pagesPerSignature, float width, float bleeds, float bindingLip, int? multiplierOne = 1, int? multiplierTwo = 2, int? multiplierThree = 1) {
            Width = width;
            ValueOneMultiplier = (int)multiplierOne;
            Bleeds = bleeds;
            ValueTwoMultiplier = (int)multiplierTwo;
            BindingLip = bindingLip;
            ValueThreeMultipler = (int)multiplierThree;
            PagesPerSignature = pagesPerSignature;
        }
        public float Width { get; set; }
        public float Bleeds { get; set; }
        public float BindingLip { get; set; }
    }
}
