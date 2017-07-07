using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrintingAppRepository.SystemVariables;
using PrintingApp.Views;
using PrintingApp.Presenters;
using PrintingAppRepository.ImpositionCalculator;
using PrintingAppRepository.PrintingDesign;
using PrintingApp.Models.CustomEventArgs;
using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingAppRepository.SignatureSize.Model;
using System.Linq;
using System.Collections.Generic;
using PrintingAppRepository.SystemVariables.Models;

namespace PrintingAppTests.ImpositionCalculator {
    //Please note that I observe the following name convention for unit tests: [MethodName_StateUnderTest_ExpectedBehavior]
    [TestClass]
    public class ImpositionCalculatorPresenterTestFixture {
        private readonly ImpositionCalculatorPresenter _impositionCalculatorPresenter;
        private readonly SystemVariablesPresenter _systemVariablesPresenter;
        private readonly Mock<IImpositionCalculatorManager> _mockImpositionCalculatorManager;
        private readonly Mock<ISystemVariablesView> _mockSystemVariablesView;
        private readonly Mock<IPrintingDesignManager> _mockPrintingDesignManager;
        private readonly Mock<ISystemVariablesManager> _mockSystemVariablesManager;
        private readonly Mock<IImpositionCalculatorRepository> _mockImpositionCalculatorRepo;
        private readonly Mock<IImpositionFormView> _mockView;
        /* Ideas on what to test:
         * Correct outcome
         * Error being logged to view
         * 
         * Individual methods
         * Methods working together so check to see if other events get raised
         * CalculatePossibleOptions - Test the manager and maybe mock the repository so that we can see
         * if the values are calculated correctly.
         * */

        public ImpositionCalculatorPresenterTestFixture() {
            _mockSystemVariablesView = new Mock<ISystemVariablesView>();
            _mockPrintingDesignManager = new Mock<IPrintingDesignManager>();
            _mockSystemVariablesManager = new Mock<ISystemVariablesManager>();
            _systemVariablesPresenter = new SystemVariablesPresenter(_mockSystemVariablesView.Object, _mockSystemVariablesManager.Object);
            _mockImpositionCalculatorRepo = new Mock<IImpositionCalculatorRepository>();
            _mockImpositionCalculatorManager = new Mock<IImpositionCalculatorManager>();
            _mockView = new Mock<IImpositionFormView>();
            _impositionCalculatorPresenter = new ImpositionCalculatorPresenter(_mockView.Object,
            _mockImpositionCalculatorManager.Object, _mockSystemVariablesManager.Object, _systemVariablesPresenter,
            _mockPrintingDesignManager.Object);
        }

        [TestMethod]
        public void PopulateInkDataSources_ApplicationOnLoad_InkDataSourcesPopulatedWithDataFromJSON() {
            //Arrange
            _mockView.SetupProperty(r => r.InkSideOneDataSource);
            _mockView.SetupProperty(r => r.InkSideTwoDataSource);
            _mockImpositionCalculatorManager.Setup(r => r.ReturnInkDataSource())
                .Returns
                (() => new ComboBoxItem[] {
                    new ComboBoxItem("Test", 1 ),
                    new ComboBoxItem("Test 2", 2)
                });

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            bool isEqual = true;

            //TODO: Override a method such as SequenceEquals to find out why it doesn't find both
            //arrays as equal.
            for (int i = 0; i < _mockView.Object.InkSideOneDataSource.Length; i++) {
                if (_mockView.Object.InkSideOneDataSource[i].Text
                    != _mockImpositionCalculatorManager.Object.ReturnInkDataSource()[i].Text
                    && _mockView.Object.InkSideTwoDataSource[i].Text
                    != _mockImpositionCalculatorManager.Object.ReturnInkDataSource()[i].Text) {
                    isEqual = false;
                }
                if (_mockView.Object.InkSideOneDataSource[i].Value
                    != _mockImpositionCalculatorManager.Object.ReturnInkDataSource()[i].Value
                    && _mockView.Object.InkSideTwoDataSource[i].Value
                    != _mockImpositionCalculatorManager.Object.ReturnInkDataSource()[i].Value) {
                    isEqual = false;
                }
            }

            //Assert
            Assert.IsTrue(isEqual, "Ink Side One or two is not equal to the ReturnCoatingDataSource method.");
        }

        [TestMethod]
        public void PopulateInkDataSources_ApplicationOnLoad_InkDataSourcesWillNotReferenceSameList() {
            //Arrange
            _mockView.SetupProperty(r => r.InkSideOneDataSource);
            _mockView.SetupProperty(r => r.InkSideTwoDataSource);
            _mockImpositionCalculatorManager.Setup(r => r.ReturnInkDataSource())
                .Returns
                (() => new ComboBoxItem[] {
                    new ComboBoxItem("Test", 1 ),
                    new ComboBoxItem("Test 2", 2)
                });

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsFalse(_mockView.Object.InkSideOneDataSource == _mockView.Object.InkSideTwoDataSource,
                "InkSideOne and InkSideTwo cannot have the same data source because if they do, when a user changes" +
                "the selected index for one of the boxes, it will change the others too as they reference the same" +
                "list.");
        }

        [TestMethod]
        public void PopulateCoatingDataSources_ApplicationOnLoad_InkDataSourcesPopulatedWithDataFromJSON() {
            //Arrange
            _mockView.SetupProperty(r => r.CoatingSideOneDataSource);
            _mockView.SetupProperty(r => r.CoatingSideTwoDataSource);
            _mockImpositionCalculatorManager.Setup(r => r.ReturnCoatingDataSource())
                .Returns
                (() => new ComboBoxItem[] {
                        new ComboBoxItem("Test 3", 5),
                        new ComboBoxItem("Test 4", 7)
                });

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            bool isEqual = true;

            //TODO: Override a method such as SequenceEquals to find out why it doesn't find both
            //arrays as equal.
            for (int i = 0; i < _mockView.Object.CoatingSideOneDataSource.Length; i++) {
                if (_mockView.Object.CoatingSideOneDataSource[i].Text
                    != _mockImpositionCalculatorManager.Object.ReturnCoatingDataSource()[i].Text
                    && _mockView.Object.CoatingSideTwoDataSource[i].Text
                    != _mockImpositionCalculatorManager.Object.ReturnCoatingDataSource()[i].Text) {
                    isEqual = false;
                }
                if (_mockView.Object.CoatingSideOneDataSource[i].Value
                    != _mockImpositionCalculatorManager.Object.ReturnCoatingDataSource()[i].Value
                    && _mockView.Object.CoatingSideTwoDataSource[i].Value
                    != _mockImpositionCalculatorManager.Object.ReturnCoatingDataSource()[i].Value) {
                    isEqual = false;
                }
            }

            //Assert
            Assert.IsTrue(isEqual, "Coating Side One or two is not equal to the ReturnCoatingDataSource method.");
        }

        [TestMethod]
        public void PopulateCoatingDataSources_ApplicationOnLoad_CoatingDataSourcesCannotReferenceSameLists() {
            //Arrange
            _mockView.SetupProperty(r => r.CoatingSideOneDataSource);
            _mockView.SetupProperty(r => r.CoatingSideTwoDataSource);
            _mockImpositionCalculatorManager.Setup(r => r.ReturnCoatingDataSource())
                .Returns
                (() => new ComboBoxItem[] {
                        new ComboBoxItem("Test 3", 5),
                        new ComboBoxItem("Test 4", 7)
                });

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsFalse(_mockView.Object.CoatingSideOneDataSource == _mockView.Object.CoatingSideTwoDataSource,
                "CoatingSideOne and CoatinSideTwo cannot have the same data source because if they do, when a user changes" +
                "the selected index for one of the boxes, it will change the others too as they reference the same" +
                "list.");
        }

        [TestMethod]
        public void PopulateCutOffDataSources_ApplicationOnLoad_CboCutOffDataSourceEqualsDataReturnedFromMethod() {
            //Arrange
            List<object> expected = new List<object>() { 14.5F, 11, 1.3F };
            _mockView.SetupProperty(r => r.CutOffDataSource);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("CutOff"))
                .Returns(expected);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsTrue(_mockView.Object.CutOffDataSource.SequenceEqual(expected.ConvertAll(r => Convert.ToSingle(r)).OrderBy(r => r).ToList()),
                "CutOffDataSource needs to equal the return from the manager method");
        }

        [TestMethod]
        public void PopulateRollSizeDataSources_ApplicationOnLoad_CboRollSizeDataSourceEqualsDataReturnedFromMethod() {
            //Arrange
            List<object> expected = new List<object>() { 14.5F, 11, 1.3F };
            _mockView.SetupProperty(r => r.RollSizeDataSource);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("RollSize"))
                .Returns(expected);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsTrue(_mockView.Object.RollSizeDataSource.SequenceEqual(expected.ConvertAll(r => Convert.ToSingle(r)).OrderBy(r => r).ToList()),
                "RollSizeDataSource needs to equal the return from the manager method");
        }

        [TestMethod]
        public void PopulateCutOffDataSources_ApplicationOnLoad_CboCutOffDataSourceDoesNotEqualExpectedAsItIsNotOrdered() {
            //Arrange
            List<object> expected = new List<object>() { 14.5F, 11, 1.3F };
            _mockView.SetupProperty(r => r.CutOffDataSource);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("CutOff"))
                .Returns(expected);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsFalse(_mockView.Object.CutOffDataSource.SequenceEqual(expected.ConvertAll(r => Convert.ToSingle(r))),
                "CutOffDataSource needs to be ordered by asc");
        }

        [TestMethod]
        public void PopulateRollSizeDataSources_ApplicationOnLoad_CboRollSizeDataSourceDoesNotEqualExpectedAsItIsNotOrdered() {
            //Arrange
            List<object> expected = new List<object>() { 14.5F, 11, 1.3F };
            _mockView.SetupProperty(r => r.RollSizeDataSource);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("RollSize"))
                .Returns(expected);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsFalse(_mockView.Object.RollSizeDataSource.SequenceEqual(expected.ConvertAll(r => Convert.ToSingle(r))),
                "RollSizeDataSource needs to be ordered by asc");
        }

        [TestMethod]
        public void PopulateSheetSizeDataSources_ApplicationOnLoad_CboSheetSizeDataSourceEqualsDataReturnedFromMethod() {
            //Arrange
            _mockView.SetupProperty(r => r.SheetSizeDataSource);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("SheetSize"))
                .Returns(new System.Collections.Generic.List<object>() { "8 x 8", "10 x 4" });

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsTrue(_mockView.Object.SheetSizeDataSource.SequenceEqual(_mockSystemVariablesManager.Object.ReturnListBoxValues("SheetSize").Select(r => r.ToString()).ToList()),
                "SheetSizeDataSource needs to equal the return from the manager method");
        }


        [TestMethod]
        public void SetDefaultValues_FormOnLoad_PagesPerSigantureIsSetAs2pg() {
            //Arrange
            _mockView.SetupProperty(r => r.PagesPerSignature);
            _mockView.SetupProperty(r => r.SheetFedCheckedValue);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.AreEqual(_mockView.Object.PagesPerSignature, "2pg");
        }

        [TestMethod]
        public void SetDefaultValues_FormOnLoad_SheetFedChecked() {
            //Arrange
            _mockView.SetupProperty(r => r.PagesPerSignature);
            _mockView.SetupProperty(r => r.SheetFedCheckedValue);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsTrue(_mockView.Object.SheetFedCheckedValue);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_FinalImposition0_PlatesEqual0() {
            //Arrange
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.FinalImpositionOut, 0);

            //Act
            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            //Assert
            Assert.AreEqual(_mockView.Object.Plates, 0);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_OverridePlatesNot0_PlatesEqualOverridePlates() {
            //Arrange
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.OverridePlates, 5);

            //Act
            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            //Assert
            Assert.AreEqual(_mockView.Object.OverridePlates, _mockView.Object.Plates);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_PrintingStyleIsSheetFedAndImpositionIsSheetWise_PlatesEqualColourSideOne() {
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.CboImposition, "Sheetwise");
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.ColourSideOne, _mockView.Object.Plates);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_PrintingStyleIsSheetFedAndImpositionIsIrregularSWise_PlatesEqualColourSideOne() {
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.CboImposition, "Irregular S\\Wise");
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.ColourSideOne, _mockView.Object.Plates);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_PrintingStyleIsSheetFedAndImpositionIsBadger_PlatesEqualColourSideOne() {
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.CboImposition, "Badger");
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            Assert.AreEqual(0, _mockView.Object.Plates);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_PrintingStyleIsUVAndImpositionIsSheetwise_PlatesEqualColourSideOne() {
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.CboImposition, "Sheetwise");
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.UV);

            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            Assert.AreEqual(0, _mockView.Object.Plates);
        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_PrintingStyleIsUVAndColourSideOneAndTwoNot0_PlatesEqualSumOfColours() {
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.ColourSideOne, 1);
            _mockView.SetupProperty(r => r.ColourSideTwo, 6);
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.UV);

            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.ColourSideOne + _mockView.Object.ColourSideTwo, _mockView.Object.Plates);

        }

        [TestMethod]
        public void CalculateFinalImpositionPlates_PrintingStyleIsSheetFedAndColourSideOneAndTwoNot0_PlatesEqualColourSideOne() {
            _mockView.SetupProperty(r => r.Plates);
            _mockView.SetupProperty(r => r.ColourSideOne, 1);
            _mockView.SetupProperty(r => r.ColourSideTwo, 6);
            _mockView.SetupProperty(r => r.FinalImpositionOut, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.CalculateFinalImpositionPlates += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.ColourSideOne, _mockView.Object.Plates);
        }

        [TestMethod]
        public void LogErrorToView_ExceptionHasBeenThrown_ErrorMessageIsEqualToPassedInMessage() {
            _mockView.SetupProperty(r => r.ErrorMessage);

            string message = "Test error thrown";

            _mockView.Raise(r => r.Error += null, new ErrorEventArgs(message));

            Assert.AreEqual(message, _mockView.Object.ErrorMessage);

        }

        [TestMethod]
        public void SetSideOneColours_SideOneInkIs6AndSideOneCoatingIs5_SideOneColourIsEqualToSumOfInkAndCoating() {
            _mockView.SetupProperty(r => r.ColourSideOne);
            _mockView.SetupProperty(r => r.SideOneInkValue, 6);
            _mockView.SetupProperty(r => r.SideOneCoatingValue, 5);

            _mockView.Raise(r => r.CalculateSideOneColourValue += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SideOneInkValue + _mockView.Object.SideOneCoatingValue, _mockView.Object.ColourSideOne);

        }

        [TestMethod]
        public void SetSideTwoColours_SideTwoInkIs6AndSideTwoCoatingIs5_SideTwoColourIsEqualToSumOfInkAndCoating() {
            _mockView.SetupProperty(r => r.ColourSideTwo);
            _mockView.SetupProperty(r => r.SideTwoInkValue, 6);
            _mockView.SetupProperty(r => r.SideTwoCoatingValue, 5);

            _mockView.Raise(r => r.CalculateSideTwoColourValue += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SideTwoInkValue + _mockView.Object.SideTwoCoatingValue, _mockView.Object.ColourSideTwo);
        }

        [TestMethod]
        public void CalculateSheetSizeAround_OverrideSheetSizeAroundIs1_SheetSizeAroundEquals1() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.PrintingStyleChecked);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAround, 1);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.OverrideSheetSizeAround, _mockView.Object.SheetSizeAround);
        }

        [TestMethod]
        public void CalculateSheetSizeAround_OverrideSheetSizeAroundIs0AndPrintingStyleIsSheetFed_SheetSizeAroundEqualsCboSheetSize() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAround, 0);
            _mockView.SetupProperty(r => r.CboSheetSize, "23 x 24");
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            Assert.AreEqual(23, _mockView.Object.SheetSizeAround);
        }

        [TestMethod]
        public void CalculateSheetSizeAround_CboSheetSizeNotInCorrectFormat_ExceptionThrownAndLoggedWhenAssigningSheetSizeAround() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAround, 0);
            _mockView.SetupProperty(r => r.CboSheetSize, "23  24");
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            bool hasThrownError = _mockView.Object.ErrorMessage.Contains("Sheet size must also be in the format of: 'number x number'");
            Assert.IsTrue(hasThrownError);
        }

        [TestMethod]
        public void CalculateSheetSizeAcross_CboSheetSizeNotInCorrectFormat_ExceptionThrownAndLoggedWhenAssigningSheetSizeAcross() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAcross, 0);
            _mockView.SetupProperty(r => r.CboSheetSize, "23  24");
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            bool hasThrownError = _mockView.Object.ErrorMessage.Contains("Sheet size must also be in the format of: 'number x number'");
            Assert.IsTrue(hasThrownError);
        }

        [TestMethod]
        public void CalculateSheetSizeAround_OverrideSheetSizeAroundIs0AndPrintingStyleIsUV_SheetSizeAroundEqualsCutOff() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAround, 0);
            _mockView.SetupProperty(r => r.CutOff, 12.5F);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.UV);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.CutOff, _mockView.Object.SheetSizeAround);
        }

        [TestMethod]
        public void CalculateSheetSizeAcross_OverrideSheetSizeAcrossIs1_SheetSizeAcrossEquals1() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAcross, 1);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.OverrideSheetSizeAcross, _mockView.Object.SheetSizeAcross);
        }

        [TestMethod]
        public void CalculateSheetSizeAcross_OverrideSheetSizeAcrossIs0AndPrintingStyleIsSheetFed_SheetSizeAcrossEqualsCboSheetSize() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.CboSheetSize, "23 x 24");
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            Assert.AreEqual(24, _mockView.Object.SheetSizeAcross);
        }

        [TestMethod]
        public void CalculateSheetSizeAcross_OverrideSheetSizeAcrossIs0AndPrintingStyleIsUV_SheetSizeAcrossEqualsRollSize() {
            _mockView.SetupProperty(r => r.SheetSizeAround);
            _mockView.SetupProperty(r => r.SheetSizeAcross);
            _mockView.SetupProperty(r => r.OverrideSheetSizeAcross, 0);
            _mockView.SetupProperty(r => r.RollSize, 12.5F);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.UV);

            _mockView.Raise(r => r.CalculateSheetSizes += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.RollSize, _mockView.Object.SheetSizeAcross);
        }

        [TestMethod]
        public void CalculateSignatureSizeValue_IFArgumentSatisfied_SignatureSizeLengthAndWidthEqualReturnValuesFromManager() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "2pg");
            _mockView.SetupProperty(r => r.PageSizeLength, 5);
            _mockView.SetupProperty(r => r.PageSizeWidth, 10);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);
            _mockView.SetupProperty(r => r.Bleeds);
            _mockView.SetupProperty(r => r.SignatureSizeLength); ;
            SignatureSizeLength sigLength = new SignatureSizeLength("2pg", 4, 5, 5);
            SignatureSizeWidth sigWidth = new SignatureSizeWidth("2pg", 4, 5, 5);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockImpositionCalculatorManager.Setup(r => r.ReturnSignatureSizeLength(It.IsAny<SignatureSizeLength>()))
                .Returns(5);

            _mockImpositionCalculatorManager.Setup(r => r.ReturnSignatureSizeWidth(It.IsAny<SignatureSizeWidth>()))
                .Returns(15.2F);

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.SignatureSizeLength == _mockImpositionCalculatorManager.Object.ReturnSignatureSizeLength(sigLength)
                && _mockView.Object.SignatureSizeWidth == _mockImpositionCalculatorManager.Object.ReturnSignatureSizeWidth(sigWidth));

        }

        [TestMethod]
        public void CalculateSignatureSizeValue_IFArgumentNotSatisfied_SignatureSizeLengthAndWidthEqual0() {
            _mockView.SetupProperty(r => r.PageCount, 0);
            _mockView.SetupProperty(r => r.PageSizeLength, 0);
            _mockView.SetupProperty(r => r.PagesPerSignature);
            _mockView.SetupProperty(r => r.PageSizeWidth, 0);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.SignatureSizeLength == 0
                && _mockView.Object.SignatureSizeWidth == 0);

        }

        [TestMethod]
        public void CalculateOptionOneValues_IFArgumentSatisfied_OptionOneValuesAssignedCorrectly() {
            _mockView.SetupProperty(r => r.PageSizeLength, 1);
            _mockView.SetupProperty(r => r.PageSizeWidth, 1);
            _mockView.SetupProperty(r => r.SignatureSizeWidth, 1);
            _mockView.SetupProperty(r => r.SignatureSizeLength, 1);
            _mockView.SetupProperty(r => r.SheetSizeAround, 1);
            _mockView.SetupProperty(r => r.SheetSizeAcross, 1);
            _mockView.SetupProperty(r => r.OptionOneAcross);
            _mockView.SetupProperty(r => r.OptionOneArea);
            _mockView.SetupProperty(r => r.OptionOneAround);
            _mockView.SetupProperty(r => r.OptionOneWasteage);
            _mockView.SetupProperty(r => r.OptionOneOut);

            Option option = new Option(5, 6, 7, 8, 9);

            _mockImpositionCalculatorManager.Setup(r => r.ReturnFinalOptionOne(It.IsAny<PropertiesToCalculateOptionsWith>()))
                .Returns<PropertiesToCalculateOptionsWith>(X => option);

            _mockImpositionCalculatorManager.Setup(r => r.ReturnFinalOptionTwo(It.IsAny<PropertiesToCalculateOptionsWith>()))
                .Returns<PropertiesToCalculateOptionsWith>(X => new Option());

            _mockView.Raise(r => r.CalculatePossibleOptions += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.OptionOneAround == option.Around
                && _mockView.Object.OptionOneAcross == option.Across
                && _mockView.Object.OptionOneOut == option.Out
                && _mockView.Object.OptionOneArea == option.Area
                && _mockView.Object.OptionOneWasteage == option.Wasteage
                );
        }

        [TestMethod]
        public void CalculateOptionTwoValues_IFArgumentSatisfied_OptionTwoValuesAssignedCorrectly() {
            _mockView.SetupProperty(r => r.PageSizeLength, 1);
            _mockView.SetupProperty(r => r.PageSizeWidth, 1);
            _mockView.SetupProperty(r => r.SignatureSizeWidth, 1);
            _mockView.SetupProperty(r => r.SignatureSizeLength, 1);
            _mockView.SetupProperty(r => r.SheetSizeAround, 1);
            _mockView.SetupProperty(r => r.SheetSizeAcross, 1);
            _mockView.SetupProperty(r => r.OptionTwoAcross);
            _mockView.SetupProperty(r => r.OptionTwoArea);
            _mockView.SetupProperty(r => r.OptionTwoAround);
            _mockView.SetupProperty(r => r.OptionTwoWasteage);
            _mockView.SetupProperty(r => r.OptionTwoOut);

            Option option = new Option(1, 2, 3, 4, 5);

            _mockImpositionCalculatorManager.Setup(r => r.ReturnFinalOptionOne(It.IsAny<PropertiesToCalculateOptionsWith>()))
                .Returns<PropertiesToCalculateOptionsWith>(X => new Option());

            _mockImpositionCalculatorManager.Setup(r => r.ReturnFinalOptionTwo(It.IsAny<PropertiesToCalculateOptionsWith>()))
                .Returns<PropertiesToCalculateOptionsWith>(X => option);

            _mockView.Raise(r => r.CalculatePossibleOptions += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.OptionTwoAround == option.Around
                && _mockView.Object.OptionTwoAcross == option.Across
                && _mockView.Object.OptionTwoOut == option.Out
                && _mockView.Object.OptionTwoArea == option.Area
                && _mockView.Object.OptionTwoWasteage == option.Wasteage
                );
        }

        [TestMethod]
        public void SetOptionOneLabel_PrintingStyleIsSheetFed_OptionOneMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionOneLabel_SumofColourSideOneAndTwoEqual0_OptionOneMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.ColourSideOne, 0);
            _mockView.SetupProperty(r => r.ColourSideTwo, 0);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionOneLabel_PageSizeLengthEquals0_OptionOneMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.ColourSideOne, 0);
            _mockView.SetupProperty(r => r.ColourSideTwo, 0);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionOneLabel_PrintingStyleIsUVAndOptionOneAroundIs0_OptionOneMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.OptionOneAround, 0);
            _mockView.SetupProperty(r => r.ColourSideOne, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.UV);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionOneLabel_ColourSideOneIs0AndColourSideTwoIsGreaterThan0_OptionOneMessageEqualsIrregular() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.OptionOneAround, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 0);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);
            _mockView.SetupProperty(r => r.PageSizeLength, 1);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("Irregular 1 Side", _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionOneLabel_ColourSideOneIsGreaterThan0AndColourSideTwoEquals0_OptionOneMessageEqualsIrregular() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.OptionOneAround, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 0);
            _mockView.SetupProperty(r => r.PageSizeLength, 1);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("Irregular 1 Side", _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionOneLabel_OptiononeAcrossDivideByTwoRemainderEquals0_OptionOneMessageEqualsTurn() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.OptionOneAround, 1);
            _mockView.SetupProperty(r => r.OptionOneAcross, 4);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);
            _mockView.SetupProperty(r => r.PageSizeLength, 1);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("W/Turn", _mockView.Object.OptionOneMessage);
        }

        public void SetOptionOneLabel_OptiononeAroundDivideByTwoRemainderEquals0_OptionOneMessageEqualsTurn() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.OptionOneAround, 2);
            _mockView.SetupProperty(r => r.OptionOneAcross, 3);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);
            _mockView.SetupProperty(r => r.PageSizeLength, 1);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("W/Turn", _mockView.Object.OptionOneMessage);
        }

        public void SetOptionOneLabel_NoArgumentsSatisfied_OptionOneMessageEqualsSheetwise() {
            _mockView.SetupProperty(r => r.OptionOneMessage);
            _mockView.SetupProperty(r => r.OptionOneAround, 3);
            _mockView.SetupProperty(r => r.OptionOneAcross, 3);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);
            _mockView.SetupProperty(r => r.PageSizeLength, 1);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("Sheetwise", _mockView.Object.OptionOneMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_PrintingStyleIsSheetFed_OptionTwoMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_SumofColourSideTwoAndTwoEqual0_OptionTwoMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.ColourSideOne, 0);
            _mockView.SetupProperty(r => r.ColourSideTwo, 0);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_PrintingStyleIsUVAndOptionTwoAroundIs0_OptionTwoMessageIsEmpty() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.OptionTwoAround, 0);
            _mockView.SetupProperty(r => r.ColourSideTwo, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.UV);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual(string.Empty, _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_ColourSideTwoIs0AndColourSideTwoIsGreaterThan0_OptionTwoMessageEqualsIrregular() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.OptionTwoAround, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 0);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("Irregular 1 Side", _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_ColourSideTwoIsGreaterThan0AndColourSideTwoEquals0_OptionTwoMessageEqualsIrregular() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.OptionTwoAround, 1);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 0);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("Irregular 1 Side", _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_OptionTwoAcrossDivideByTwoRemainderEquals0_OptionTwoMessageEqualsTurn() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.OptionTwoAround, 1);
            _mockView.SetupProperty(r => r.OptionTwoAcross, 4);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("W/Turn", _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_OptionTwoAroundDivideByTwoRemainderEquals0_OptionTwoMessageEqualsTurn() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.OptionTwoAround, 3);
            _mockView.SetupProperty(r => r.OptionTwoAcross, 2);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);
            _mockView.SetupProperty(r => r.PageSizeLength, 2);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("W/Turn", _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetOptionTwoLabel_NoArgumentsSatisfied_OptionTwoMessageEqualsSheetwise() {
            _mockView.SetupProperty(r => r.OptionTwoMessage);
            _mockView.SetupProperty(r => r.OptionTwoAround, 3);
            _mockView.SetupProperty(r => r.OptionTwoAcross, 3);
            _mockView.SetupProperty(r => r.PrintingStyleChecked, PrintingStyle.SheetFed);
            _mockView.SetupProperty(r => r.ColourSideOne, 2);
            _mockView.SetupProperty(r => r.ColourSideTwo, 2);
            _mockView.SetupProperty(r => r.PageSizeLength, 1);

            _mockView.Raise(r => r.PopulateOptionLabels += null, new EventArgs());

            Assert.AreEqual("Sheetwise", _mockView.Object.OptionTwoMessage);
        }

        [TestMethod]
        public void SetCboImpositionValues_OptionOneCheckedAndOptionOneMessageNotNullOrEmpty_CboImpositionEqualsOptionOneMessage() {
            _mockView.SetupProperty(r => r.CboImposition);
            _mockView.SetupProperty(r => r.OptionOneMessage, "Sheetwise");
            _mockView.SetupProperty(r => r.IsOptionOneChecked, true);

            _mockView.Raise(r => r.SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked += null, new EventArgs());

            Assert.AreEqual("Sheetwise", _mockView.Object.CboImposition);
        }

        [TestMethod]
        public void SetCboImpositionValues_OptionOneIsNotCheckedAndOptionOneMessageNotNullOrEmpty_CboImpositionEqualsOptionTwoMessage() {
            _mockView.SetupProperty(r => r.CboImposition);
            _mockView.SetupProperty(r => r.OptionTwoMessage, "W/Turn");
            _mockView.SetupProperty(r => r.IsOptionOneChecked, false);

            _mockView.Raise(r => r.SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked += null, new EventArgs());

            Assert.AreEqual("W/Turn", _mockView.Object.CboImposition);
        }

        [TestMethod]
        public void SetFinalImpositionOutValue_OptionOneOutNotEqualTo0_FinalImpositionOutEqualsOptionOneOut() {
            _mockView.SetupProperty(r => r.FinalImpositionOut);
            _mockView.SetupProperty(r => r.OptionOneOut, 3);

            _mockView.Raise(r => r.SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.OptionOneOut, _mockView.Object.FinalImpositionOut);
        }

        [TestMethod]
        public void SetFinalImpositionOutValue_OptionTwoOutEqualTo0AndOptionTwoEqualNotEqualTo0_FinalImpositionOutEqualsOptionTwoOut() {
            _mockView.SetupProperty(r => r.FinalImpositionOut);
            _mockView.SetupProperty(r => r.OptionOneOut, 0);
            _mockView.SetupProperty(r => r.OptionTwoOut, 3);

            _mockView.Raise(r => r.SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.OptionTwoOut, _mockView.Object.FinalImpositionOut);
        }

        [TestMethod]
        public void SetDefaultValues_ClearPageInitiated_PagesPerSigantureIsSetAs2pg() {
            //Arrange
            _mockView.SetupProperty(r => r.PagesPerSignature);
            _mockView.SetupProperty(r => r.SheetFedCheckedValue);

            //Act
            _mockView.Raise(r => r.ClearPage += null, new EventArgs());

            //Assert
            Assert.AreEqual(_mockView.Object.PagesPerSignature, "2pg");
        }

        [TestMethod]
        public void SetDefaultValues_ClearPageInitiated_SheetFedPrintingStyleShouldBeChecked() {
            //Arrange
            _mockView.SetupProperty(r => r.PagesPerSignature);
            _mockView.SetupProperty(r => r.SheetFedCheckedValue);

            //Act
            _mockView.Raise(r => r.FormOnLoad += null, new EventArgs());

            //Assert
            Assert.IsTrue(_mockView.Object.SheetFedCheckedValue);
        }

        [TestMethod]
        public void HideShowPrintingDesignButton_ClearPageInitiated_ShowPrintingDesignButtonShouldBeHidden() {
            _mockView.SetupProperty(r => r.PrintingDesignFromBtnVisibility);

            _mockView.Raise(r => r.ClearPage += null, new EventArgs());

            Assert.IsFalse(_mockView.Object.PrintingDesignFromBtnVisibility, "Show printing design button should be hidden on page reset");
        }

        //TODO: Find a way to stop this from throwing an exception occasionally because it tries to show a form
        //[TestMethod]
        //public void MakeShowPrintingDesignBtnVisible_VariablesSetupToCreatePrintingDesign_PrintingDesignFormShouldBeVisible() {
        //    _mockView.SetupProperty(r => r.Bleeds, 0.125F);
        //    _mockView.SetupProperty(r => r.SheetSizeAcross, 10);
        //    _mockView.SetupProperty(r => r.SheetSizeAround, 15);
        //    _mockView.SetupProperty(r => r.PageSizeLength, 30);
        //    _mockView.SetupProperty(r => r.PageSizeWidth, 20);
        //    _mockView.SetupProperty(r => r.OptionOneAround, 15);
        //    _mockView.SetupProperty(r => r.OptionOneAcross, 20);
        //    _mockView.SetupProperty(r => r.IsOptionOneChecked, true);

        //    _mockView.SetupProperty(r => r.PrintingDesignFromBtnVisibility);

        //    _mockView.Raise(r => r.CreatePrintingDesign += null, new EventArgs());

        //    Assert.IsTrue(_mockView.Object.PrintingDesignFromBtnVisibility, "Show printing design button should be visible");
        //}

        [TestMethod]
        public void MakeShowPrintingDesignBtnVisible_NoPropertiesNeededForDesignPopulated_ExceptionThrown() {
            _mockView.SetupProperty(r => r.PrintingDesignFromBtnVisibility);
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.CreatePrintingDesign += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("Specified argument was out of the range of valid values."));

        }

        /*
                     _view.RefreshData += PopulateSheetSizeDataSource;
            _view.RefreshData += PopulatePrintingStyleValues;
         */
        [TestMethod]
        public void PopulateCutOffDataSource_UserInitiatedDataRefresh_CutOffDataUpdated() {
            List<object> expected = new List<object>() { 5.5F, 6, 7 };

            _mockView.SetupProperty(r => r.CutOffDataSource, new List<float>() { 1, 3.5F, 6 });
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("CutOff"))
             .Returns(expected);
            _mockSystemVariablesManager.Setup(r => r.ReturnPrintingStyleValuesBasedOnPassedInStyle("Sheet Fed"))
                .Returns(new PrintingAppRepository.SystemVariables.Models.PrintingStyleClass());

            _mockView.Raise(r => r.RefreshData += null, new EventArgs());

            Assert.IsTrue(expected.ConvertAll(r => Convert.ToSingle(r)).ToList().SequenceEqual(_mockView.Object.CutOffDataSource));

        }

        [TestMethod]
        public void PopulateRollSizeDataSource_UserInitiatedDataRefresh_RollOffDataUpdated() {
            List<object> expected = new List<object>() { 5.5F, 6, 7 };

            _mockView.SetupProperty(r => r.RollSizeDataSource, new List<float>() { 1, 3.5F, 6 });
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("RollSize"))
             .Returns(expected);
            _mockSystemVariablesManager.Setup(r => r.ReturnPrintingStyleValuesBasedOnPassedInStyle("Sheet Fed"))
                .Returns(new PrintingAppRepository.SystemVariables.Models.PrintingStyleClass());

            _mockView.Raise(r => r.RefreshData += null, new EventArgs());

            Assert.IsTrue(expected.ConvertAll(r => Convert.ToSingle(r)).ToList().SequenceEqual(_mockView.Object.RollSizeDataSource));

        }

        [TestMethod]
        public void PopulatePrintingStyleValues_UserInitiatedDataRefresh_PrintingStyleValuesDataUpdated() {
            PrintingStyleClass expected = new PrintingStyleClass(1, 2.5F, 6.5F, 6.8F);

            _mockView.SetupProperty(r => r.Gripper, 1);
            _mockView.SetupProperty(r => r.TailMargin, 2);
            _mockView.SetupProperty(r => r.SideMargin, 3);
            _mockView.SetupProperty(r => r.Bleeds, 4);
            _mockSystemVariablesManager.Setup(r => r.ReturnPrintingStyleValuesBasedOnPassedInStyle("Sheet Fed"))
             .Returns(expected);

            _mockView.Raise(r => r.RefreshData += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.Gripper == expected.Gripper && _mockView.Object.SideMargin == expected.SideMargin
                && _mockView.Object.TailMargin == expected.TailMargin && _mockView.Object.Bleeds == expected.Bleeds);

        }
    }
}
