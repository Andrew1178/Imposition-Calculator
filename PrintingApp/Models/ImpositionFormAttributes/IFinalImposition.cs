namespace PrintingApp.Models.ImpositionFormAttributes {
    public interface IFinalImposition {
        int FinalImpositionOut { get; set; }
        string CboImposition { get; set; }
        int ColourSideOne { get; set; }
        int ColourSideTwo { get; set; }
        int Plates { get; set; }
        int OverridePlates { get; set; }
    }
}
