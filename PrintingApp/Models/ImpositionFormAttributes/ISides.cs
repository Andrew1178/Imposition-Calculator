using PrintingAppRepository.ImpositionCalculator.Model;

namespace PrintingApp.Models.ImpositionFormAttributes {
    /// <summary>
    /// This is a section of the ImpositionForm. I have opted to split it out into different interfaces
    /// </summary>
    public interface ISides {
        int SideOneInkValue { get; set; }
        int SideTwoInkValue { get; set; }
        int SideOneCoatingValue { get; set; }
        int SideTwoCoatingValue { get; set; }
        ComboBoxItem[] InkSideOneDataSource { get; set; }
        ComboBoxItem[] CoatingSideOneDataSource { get;  set; }
        ComboBoxItem[] InkSideTwoDataSource { get; set; }
        ComboBoxItem[] CoatingSideTwoDataSource { get; set; }
    }
}