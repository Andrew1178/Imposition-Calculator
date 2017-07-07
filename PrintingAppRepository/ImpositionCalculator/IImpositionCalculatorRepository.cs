using PrintingAppRepository.ImpositionCalculator.Model;

namespace PrintingAppRepository.ImpositionCalculator {
    public interface IImpositionCalculatorRepository {
        ComboBoxItem[] ReturnInkDataSource();
        ComboBoxItem[] ReturnCoatingDataSource();
    }
}
