namespace PrintingApp.Models.ImpositionFormAttributes {
    public interface IOptions {
        int OptionOneAround { get; set; }
        int OptionOneAcross { get; set; }
        int OptionOneOut { get; set; }
        float OptionOneArea { get; set; }
        int OptionOneWasteage { get; set; }
        string OptionOneMessage { get; set; }

        int OptionTwoAround { get; set; }
        int OptionTwoAcross { get; set; }
        int OptionTwoOut { get; set; }
        float OptionTwoArea { get; set; }
        int OptionTwoWasteage { get; set; }
        string OptionTwoMessage { get; set; }

        bool IsOptionOneChecked { get; set; }
    }
}
