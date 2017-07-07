using PrintingAppRepository.ImpositionCalculator.Model;

namespace PrintingApp.Models.ImpositionFormAttributes {
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