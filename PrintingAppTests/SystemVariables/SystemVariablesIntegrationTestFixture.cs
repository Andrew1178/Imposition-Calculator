using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintingApp.Presenters;
using Moq;
using PrintingApp.Views;
using PrintingAppRepository.SystemVariables;
using PrintingAppRepository.SystemVariables.Implementation;
using Newtonsoft.Json;
using System.Collections.Generic;
using PrintingAppRepository.SystemVariables.Models;
using System.Linq;

namespace PrintingAppTests.SystemVariables {
    [TestClass]
    public class SystemVariablesIntegrationTestFixture {
        private readonly Mock<ISystemVariablesView> _mockView;
        private readonly ISystemVariablesManager _manager;
        private readonly SystemVariablesPresenter _presenter;
        private readonly ISystemVariablesRepository _repo;

        public SystemVariablesIntegrationTestFixture() {
            _repo = new SystemVariablesRepository();
            _manager = new SystemVariablesManager(_repo);
            _mockView = new Mock<ISystemVariablesView>();
            _presenter = new SystemVariablesPresenter(_mockView.Object, _manager);
        }

        //TODO:
        //Check that file is created
        //Check that after changing a printing style that it has been updated
        //Check that the correct values is returned when a printing size is selected

        [TestMethod]
        public void AddCutOffValue_UserHasAdded999ToCutOff_999HasBeenAddedToCutOffInFile() {
            _mockView.SetupProperty(r => r.CurrentCutOffValues);
            _mockView.SetupProperty(r => r.CutOffValueToAdd, 999);

            _mockView.Raise(r => r.AddCutOffValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.CurrentCutOffValues.Contains(999));

        }

        [TestMethod]
        public void RemoveCutOffValue_UserHasRemoved999ToCutOff_999HasBeenRemovedFromCutOffInFile() {
            _mockView.SetupProperty(r => r.CurrentCutOffValues);
            _mockView.SetupProperty(r => r.CutOffValuesToRemove, new List<float>() { 999 });

            _mockView.Raise(r => r.RemoveCutOffValues += null, new EventArgs());

            Assert.IsTrue(!_mockView.Object.CurrentCutOffValues.Contains(999));

        }

        [TestMethod]
        public void AddRollSizeValue_UserHasAdded999ToRollSize_999HasBeenAddedToRollSizeInFile() {
            _mockView.SetupProperty(r => r.CurrentRollSizeValues);
            _mockView.SetupProperty(r => r.RollSizeValueToAdd, 999);

            _mockView.Raise(r => r.AddRollSizeValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.CurrentRollSizeValues.Contains(999));

        }

        [TestMethod]
        public void RemoveRollSizeValue_UserHasRemoved999ToRollSize_999HasBeenRemovedFromRollSizeInFile() {
            _mockView.SetupProperty(r => r.CurrentRollSizeValues);
            _mockView.SetupProperty(r => r.RollSizeValuesToRemove, new System.Collections.Generic.List<float>() { 999 });

            _mockView.Raise(r => r.RemoveRollSizeValues += null, new EventArgs());

            Assert.IsTrue(!_mockView.Object.CurrentRollSizeValues.Contains(999));

        }

        [TestMethod]
        public void AddSheetSizeValue_UserHasAdded999x999ToSheetSize_999x999HasBeenAddedToSheetSizeInFile() {
            _mockView.SetupProperty(r => r.CurrentSheetSizeValues);
            _mockView.SetupProperty(r => r.SheetSizeToAdd, "999 x 999");

            _mockView.Raise(r => r.AddSheetSizeValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.CurrentSheetSizeValues.Contains("999 x 999"));

        }

        [TestMethod]
        public void RemoveSheetSizeValue_UserHasRemoved999x999FromSheetSize_99x9999HasBeenRemovedFromSheetSizeInFile() {
            _mockView.SetupProperty(r => r.CurrentSheetSizeValues);
            _mockView.SetupProperty(r => r.SheetSizeValuesToRemove, new System.Collections.Generic.List<string>() { "999 x 999" });

            _mockView.Raise(r => r.RemoveSheetSizeValue += null, new EventArgs());

            Assert.IsTrue(!_mockView.Object.CurrentSheetSizeValues.Contains("999 x 999"));

        }

        [TestMethod]
        public void ReturnNudVariables_UserHasActivatedEventToViewNudVariables_ConstructorErrorDoesNotThrow() {
            _mockView.SetupProperty(r => r.FootTrim);
            _mockView.SetupProperty(r => r.HeadTrim);
            _mockView.SetupProperty(r => r.BindingLip);

            try {
                _mockView.Raise(r => r.OnFormLoad += null, new EventArgs());
            }
            catch (JsonSerializationException e) {
                Assert.Fail("Error: You have tried using the constructor to deserialise the JSON. This will error on collections such as CutOff." + e.Message);
            }
        }

        [TestMethod]
        public void SetNudVariables_UserHasPopulatedNudVariablesWithValues_FileContainsUpdatedValues() {
            _mockView.SetupProperty(r => r.FootTrim, 0.125F);
            _mockView.SetupProperty(r => r.HeadTrim, 0.1875F);
            _mockView.SetupProperty(r => r.BindingLip, 0.125F);

            _mockView.Raise(r => r.SetSystemVariables += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.FootTrim == 0.125F && _mockView.Object.HeadTrim == 0.1875F
                && _mockView.Object.BindingLip == 0.125F);

        }

        [TestMethod]
        public void ModifyPrintingValues_UserHasModifiedDigital_CurrentPrintingStyleKeyAndValueToChangeEqualsSetValue() {
            PrintingStyleClass expected = new PrintingStyleClass(1, 1.5F, 6.5F, 2);
            _mockView.SetupProperty(r => r.CurrentPrintingStyle, "Digital");
            _mockView.SetupProperty(r => r.CurrentPrintingStyleValues, expected);
            var comparer = new PrintingStyleEqualityComparer();

            _mockView.Raise(r => r.ModifyPrintingStyle += null, new EventArgs());

            //This works because in the ModifyPrintingStyle event, it calls two methods, 
            //one to change the value in the file and one to refresh the data. 
            //So therefore it modified the CurrentPrintingStyleKeyAndValueToChange property
            Assert.IsTrue(_mockView.Object.CurrentPrintingStyle == "Digital" && comparer.Equals(expected, _mockView.Object.CurrentPrintingStyleValues));
        }

        [TestMethod]
        public void PopulatePrintingKeysDataSource_UserHasInitiatedMethodToPopulate_All4DefaultPrintingKeysExists() {
            _mockView.SetupProperty(r => r.CboPrintingStyleDataSource);

            _mockView.Raise(r => r.OnFormLoad += null, new EventArgs());

            //This works because in the ModifyPrintingStyle event, it calls two methods, 
            //one to change the value in the file and one to refresh the data. 
            //So therefore it modified the CurrentPrintingStyleKeyAndValueToChange property
            Assert.IsTrue(_mockView.Object.CboPrintingStyleDataSource.SequenceEqual(new List<string>() { "Sheet Fed", "Heat Set","UV", "Digital" }));
        }

        [TestMethod]
        public void DisplayCurrentPrintingValues_UserHasPassedInAnEmptyString_ExceptionWillBeThrownAndLogged() {
            _mockView.SetupProperty(r => r.CurrentPrintingStyle, "");
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.CboPrintingStyleChanged += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("Printing Style cannot be null or empty."));
        }

        [TestMethod]
        public void DisplayCurrentPrintingValues_UserHasPassedInANull_ExceptionWillBeThrownAndLogged() {
            _mockView.SetupProperty(r => r.CurrentPrintingStyle, null);
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.CboPrintingStyleChanged += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("Printing Style cannot be null or empty."));
        }
    }
}
