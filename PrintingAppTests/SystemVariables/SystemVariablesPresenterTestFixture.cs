using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintingApp.Views;
using Moq;
using PrintingAppRepository.SystemVariables;
using PrintingApp.Presenters;
using PrintingApp.Models.CustomEventArgs;
using System.Collections.Generic;

namespace PrintingAppTests.SystemVariables {
    [TestClass]
    public class SystemVariablesPresenterTestFixture {
        private readonly Mock<ISystemVariablesView> _mockView;
        private readonly Mock<ISystemVariablesManager> _mockSystemVariablesManager;
        private readonly SystemVariablesPresenter _presenter;

        public SystemVariablesPresenterTestFixture() {
            _mockView = new Mock<ISystemVariablesView>();
            _mockSystemVariablesManager = new Mock<ISystemVariablesManager>();
            _presenter = new SystemVariablesPresenter(_mockView.Object, _mockSystemVariablesManager.Object);
        }

        [TestMethod]
        public void DisplayNudVariables_SystemVariablesReturnedFromJSONFile_BindingLipHeadTrimAndFootTrimAllEqualReturnedValuesFromManagerMethod() {
            _mockView.SetupProperty(r => r.BindingLip);
            _mockView.SetupProperty(r => r.FootTrim);
            _mockView.SetupProperty(r => r.HeadTrim);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables(3, 4, 5));

            _mockView.Raise(r => r.OnFormLoad += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.FootTrim == 5 && _mockView.Object.HeadTrim == 4 &&
                _mockView.Object.BindingLip == 3, "FootTrim, HeadTrim, BindingLip have not correctly assigned based on their retrospective values returned from the manager method");

        }

        [TestMethod]
        public void LogErrorToView_ExceptionHasBeenThrown_ErrorMessageIsEqualToPassedInMessage() {
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables(3, 4, 5));

            string message = "Test error thrown";

            _mockView.Raise(r => r.Error += null, new ErrorEventArgs(message));

            Assert.AreEqual(message, _mockView.Object.ErrorMessage);

        }

        [TestMethod]
        public void ValidatePotentialCutOff_UserHasAttemptedToAddARepeatedValue_ValidationMethodWillThrowAnExceptionWhichWillBeLogged() {
            _mockView.SetupProperty(r => r.CurrentCutOffValues, new List<float>() { 1, 6, 8, 9.5F });
            _mockView.SetupProperty(r => r.CutOffValueToAdd, 9.5F);
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.AddCutOffValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("You cannot add a cut off value which is already present in the list."));

        }

        [TestMethod]
        public void ValidatePotentialRollSize_UserHasAttemptedToAddAUniqueValue_ValidationMethodWillNotThrowAnExceptionAndLogIt() {
            _mockView.SetupProperty(r => r.CurrentRollSizeValues, new List<float>() { 1, 6, 8, 9.5F });
            _mockView.SetupProperty(r => r.RollSizeValueToAdd, 7.5F);
            _mockView.SetupProperty(r => r.ErrorMessage);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("RollSize"))
                .Returns(new List<object>() { 5.5, 6, 7 });

            _mockView.Raise(r => r.AddRollSizeValue += null, new EventArgs());

            Assert.IsNull(_mockView.Object.ErrorMessage);

        }

        [TestMethod]
        public void ValidatePotentialCutOff_UserHasAttemptedToAddAUniqueValue_ValidationMethodWillNotThrowAnExceptionAndLogIt() {
            _mockView.SetupProperty(r => r.CurrentCutOffValues, new List<float>() { 1, 6, 8, 9.5F });
            _mockView.SetupProperty(r => r.CutOffValueToAdd, 7.5F);
            _mockView.SetupProperty(r => r.ErrorMessage);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("CutOff"))
                .Returns(new List<object>() { 5.5, 6, 7 });

            _mockView.Raise(r => r.AddCutOffValue += null, new EventArgs());

            Assert.IsNull(_mockView.Object.ErrorMessage);

        }

        [TestMethod]
        public void ValidatePotentialRollSize_UserHasAttemptedToAddARepeatedValue_ValidationMethodWillThrowAnExceptionWhichWillBeLogged() {
            _mockView.SetupProperty(r => r.CurrentRollSizeValues, new List<float>() { 1, 6, 8, 9.5F });
            _mockView.SetupProperty(r => r.RollSizeValueToAdd, 9.5F);
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.AddRollSizeValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("You cannot add a roll size value which is already present in the list."));

        }

        [TestMethod]
        public void ValidatePotentialSheetSize_UserHasAttemptedToAddARepeatedValue_ValidationMethodWillThrowAnExceptionWhichWillBeLogged() {
            _mockView.SetupProperty(r => r.CurrentSheetSizeValues, new List<string>() { "2 x 8", "4 x 12", "6 x 14" });
            _mockView.SetupProperty(r => r.SheetSizeToAdd, "2 x 8");
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.AddSheetSizeValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("You cannot add a sheet size value which is already present in the list."));

        }

        [TestMethod]
        public void ValidatePotentialSheetSize_SheetSizeToAddIsNotFormattedCorrectly_ValidationMethodWillThrowAnExceptionWhichWillBeLogged() {
            _mockView.SetupProperty(r => r.CurrentSheetSizeValues, new List<string>() { "2 x 8", "4 x 12", "6 x 14" });
            _mockView.SetupProperty(r => r.SheetSizeToAdd, "2x9");
            _mockView.SetupProperty(r => r.ErrorMessage);

            _mockView.Raise(r => r.AddSheetSizeValue += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.ErrorMessage.Contains("The potential sheet size must have a space both before the x and after it."));

        }

        [TestMethod]
        public void ValidatePotentialSheetSize_UserHasAttemptedToAddAUniqueValue_ValidationMethodWillNotThrowAnExceptionAndLogIt() {
            _mockView.SetupProperty(r => r.CurrentSheetSizeValues, new List<string>() { "2 x 8", "4 x 12", "6 x 14" });
            _mockView.SetupProperty(r => r.SheetSizeToAdd, "2 x 4");
            _mockView.SetupProperty(r => r.ErrorMessage);
            _mockSystemVariablesManager.Setup(r => r.ReturnListBoxValues("SheetSize"))
                .Returns(new List<object>() { "2 x 6", "4 x 8"});

            _mockView.Raise(r => r.AddSheetSizeValue += null, new EventArgs());

            Assert.IsNull(_mockView.Object.ErrorMessage);

        }
    }
}
