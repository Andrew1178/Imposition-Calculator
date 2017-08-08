namespace PrintingApp.Models.ImpositionFormAttributes {
    /// <summary>
    /// This is a section of the ImpositionForm. I have opted to split it out into different interfaces
    /// </summary>
    public interface IFinalImposition {
        int FinalImpositionOut { get; set; }
        string CboImposition { get; set; }
        int ColourSideOne { get; set; }
        int ColourSideTwo { get; set; }
        int Plates { get; set; }
        int OverridePlates { get; set; }
    }
}
