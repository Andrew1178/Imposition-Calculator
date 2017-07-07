using PrintingAppRepository.ImpositionCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintingAppRepository.ImpositionCalculator.Model;

namespace PrintingAppTests.ImpositionCalculator.Fakes {
    class FakeImpositionCalculatorRepository : IImpositionCalculatorRepository {
        private readonly string expectedFilePath = $"{Environment.CurrentDirectory.Split(new string[] { "Projects" }, StringSplitOptions.None)[0]}Projects\\PrintingApp\\SetupFiles\\TestPrintingApp.txt";

        public ComboBoxItem[] ReturnCoatingDataSource() {
            throw new NotImplementedException();
        }

        public ComboBoxItem[] ReturnInkDataSource() {
            throw new NotImplementedException();
        }

        
    }
}
