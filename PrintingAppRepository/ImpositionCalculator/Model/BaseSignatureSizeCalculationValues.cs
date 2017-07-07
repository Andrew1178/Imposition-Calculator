namespace PrintingAppRepository.SignatureSize.Model {
    public abstract class BaseSignatureSizeCalculationValues {
        public string PagesPerSignature { get; set; }
        public int ValueOneMultiplier { get; set; }
        public int ValueTwoMultiplier { get; set; }
        public int ValueThreeMultipler { get; set; }
    }
}
