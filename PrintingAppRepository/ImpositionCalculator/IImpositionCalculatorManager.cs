using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingAppRepository.SignatureSize.Model;

namespace PrintingAppRepository.ImpositionCalculator {
    public interface IImpositionCalculatorManager {
        float ReturnSignatureSizeWidth(SignatureSizeWidth widthParameters);
        float ReturnSignatureSizeLength(SignatureSizeLength lengthParameters);
        Option ReturnFinalOptionOne(PropertiesToCalculateOptionsWith props);
        Option ReturnFinalOptionTwo(PropertiesToCalculateOptionsWith props);
        ComboBoxItem[] ReturnInkDataSource();
        ComboBoxItem[] ReturnCoatingDataSource();
    }
}
