namespace PrintingAppRepository.SignatureSize.Model {
    public class SignatureSizeLength : BaseSignatureSizeCalculationValues{
        public SignatureSizeLength(string pagesPerSignature, float length, float headTrim, float footTrim, int? multiplierOne = 1, int? multiplierTwo = 1, int? multiplierThree = 1) {
            PagesPerSignature = pagesPerSignature;
            Length = length;
            ValueOneMultiplier = (int)multiplierOne;
            HeadTrim = headTrim;
            ValueTwoMultiplier = (int)multiplierTwo;
            FootTrim = footTrim;
            ValueThreeMultipler = (int)multiplierThree;
        }
        public float Length { get; set; }
        public float HeadTrim { get; set; }
        public float FootTrim { get; set; }
    }
}
