namespace PrintingAppRepository.SignatureSize.Model {
    public class Option {
        public Option(int around = 0, int across = 0, int _out = 0, float area = 0, int wasteage = 0) {
            Around = around;
            Across = across;
            Out = _out;
            Area = area;
            Wasteage = wasteage;
        }
        public int Around { get; set; }
        public int Across { get; set; }
        public int Out { get; set; }
        public float Area { get; set; }
        public int Wasteage { get; set; }
    }
}
