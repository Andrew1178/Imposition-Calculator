using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrintingApp.Presenters;
using PrintingAppRepository.ImpositionCalculator;
using PrintingApp.Views;
using Moq;
using PrintingAppRepository.PrintingDesign;
using PrintingAppRepository.SystemVariables;
using PrintingAppRepository.ImpositionCalculator.Implementation;

namespace PrintingAppTests.ImpositionCalculator {
    [TestClass]
    public class ImpositionCalculatorManagerTestFixture {
        private ImpositionCalculatorPresenter _impositionCalculatorPresenter;
        private SystemVariablesPresenter _systemVariablesPresenter;
        private IImpositionCalculatorManager _impositionCalculatorManager;
        private Mock<ISystemVariablesView> _mockSystemVariablesView;
        private Mock<IPrintingDesignManager> _mockPrintingDesignManager;
        private Mock<ISystemVariablesManager> _mockSystemVariablesManager;
        private Mock<IImpositionCalculatorRepository> _mockImpositionCalculatorRepo;
        private Mock<IImpositionFormView> _mockView;

        public ImpositionCalculatorManagerTestFixture() {
            _mockImpositionCalculatorRepo = new Mock<IImpositionCalculatorRepository>();

            IImpositionCalculatorManager impoCalculatorManager = new ImpositionCalculatorManager(_mockImpositionCalculatorRepo.Object);
            _mockSystemVariablesView = new Mock<ISystemVariablesView>();
            _mockPrintingDesignManager = new Mock<IPrintingDesignManager>();
            _mockSystemVariablesManager = new Mock<ISystemVariablesManager>();
            _systemVariablesPresenter = new SystemVariablesPresenter(_mockSystemVariablesView.Object, _mockSystemVariablesManager.Object);
            _impositionCalculatorManager = impoCalculatorManager;
            _mockView = new Mock<IImpositionFormView>();
            _impositionCalculatorPresenter = new ImpositionCalculatorPresenter(_mockView.Object,
            _impositionCalculatorManager, _mockSystemVariablesManager.Object, _systemVariablesPresenter,
            _mockPrintingDesignManager.Object);
        }

        [TestMethod]
        public void ReturnSignatureSizeLength_PagesPerSignatureEquals32PgRightAngle_SignatureSizeLengthEquals30Point9375() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "32pg right angle");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeLength, 30.9375);
        }

        [TestMethod]
        public void ReturnSignatureSizeLength_PagesPerSignatureEquals16PgRightAngle_SignatureSizeLengthEquals20Point625() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "16pg right angle");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeLength, 20.625);
        }

        [TestMethod]
        public void ReturnSignatureSizeLength_PagesPerSignatureEquals24PgRightAngle_SignatureSizeLengthEquals20Point625() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "24pg right angle");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeLength, 20.625);
        }

        [TestMethod]
        public void ReturnSignatureSizeLength_PagesPerSignatureEquals12PgRightAngle_SignatureSizeLengthEquals30Point9375() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "12pg right angle");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeLength, 30.9375);
        }

        [TestMethod]
        public void ReturnSignatureSizeLength_PagesPerSignatureEquals16PgRightAngleAlbum_SignatureSizeLengthEquals30Point9375() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "16pg right angle album");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeLength, 41.25);
        }

        [TestMethod]
        public void ReturnSignatureSizeLength_PagesPerSignatureEquals8pg2UptTearApart_SignatureSizeLengthEquals20Point625() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "8pg 2up tear apart");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeLength);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeLength, 20.625);
        }

        [TestMethod]
        public void ReturnSignatureSizeWidth_PagesPerSignatureEquals2Pg_SignatureSizeWidthEquals4Point25() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "2pg");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeWidth, 4.25);
        }

        [TestMethod]
        public void ReturnSignatureSizeWidth_PagesPerSignatureEquals8PgRightAngle_SignatureSizeWidthEquals8Point25() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "8pg right angle");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeWidth, 8.25);
        }

        [TestMethod]
        public void ReturnSignatureSizeWidth_PagesPerSignatureEquals24PgRightAngle_SignatureSizeWidthEquals16Point5() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "24pg right angle");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeWidth, 16.5);
        }

        [TestMethod]
        public void ReturnSignatureSizeWidth_PagesPerSignatureEquals16PgParallel_SignatureSizeWidthEquals16Point5() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "16pg parallel");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeWidth, 33);
        }

        [TestMethod]
        public void ReturnSignatureSizeWidth_PagesPerSignatureEquals2UpTearApart_SignatureSizeWidthEquals16Point5() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "2up tear apart");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeWidth, 16.5);
        }

        [TestMethod]
        public void ReturnSignatureSizeWidth_PagesPerSignatureEquals4pg_SignatureSizeWidthEquals8Point25() {
            _mockView.SetupProperty(r => r.PageCount, 2);
            _mockView.SetupProperty(r => r.PagesPerSignature, "4pg");
            _mockView.SetupProperty(r => r.PageSizeLength, 10);
            _mockView.SetupProperty(r => r.PageSizeWidth, 4);
            _mockView.SetupProperty(r => r.SignatureSizeWidth);

            _mockSystemVariablesManager.Setup(r => r.ReturnNudVariables())
                .Returns(new PrintingAppRepository.SystemVariables.Models.SystemVariables());

            _mockView.Raise(r => r.CalculateSignatureSize += null, new EventArgs());

            Assert.AreEqual(_mockView.Object.SignatureSizeWidth, 8.25);
        }

        [TestMethod]
        public void ReturnFinalOptionOne_CalculatePossibleOptionsCalled_OptionOneCorrectlyCalculated() {
            _mockView.SetupProperty(r => r.PageSizeWidth, 5);
            _mockView.SetupProperty(r => r.PageSizeLength, 15);
            _mockView.SetupProperty(r => r.SignatureSizeWidth, 5.5F);
            _mockView.SetupProperty(r => r.SignatureSizeLength, 15.3125F);
            _mockView.SetupProperty(r => r.SheetSizeAround, 23);
            _mockView.SetupProperty(r => r.SheetSizeAcross, 29);
            _mockView.SetupProperty(r => r.SideMargin, 1);
            _mockView.SetupProperty(r => r.Bleeds, 0.125F);
            _mockView.SetupProperty(r => r.TailMargin, 0.5F);
            _mockView.SetupProperty(r => r.Gripper, 0.5F);
            _mockView.SetupProperty(r => r.OptionOneAcross);
            _mockView.SetupProperty(r => r.OptionOneArea);
            _mockView.SetupProperty(r => r.OptionOneAround);
            _mockView.SetupProperty(r => r.OptionOneOut);
            _mockView.SetupProperty(r => r.OptionOneWasteage);

            _mockView.Raise(r => r.CalculatePossibleOptions += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.OptionOneAcross == 1 && _mockView.Object.OptionOneArea == 336.88F &&
                _mockView.Object.OptionOneAround == 4 && _mockView.Object.OptionOneOut == 4 &&
                _mockView.Object.OptionOneWasteage == 49);
        }

        [TestMethod]
        public void ReturnFinalOptionTwo_CalculatePossibleOptionsCalled_OptionTwoCorrectlyCalculated() {
            _mockView.SetupProperty(r => r.PageSizeWidth, 5);
            _mockView.SetupProperty(r => r.PageSizeLength, 15);
            _mockView.SetupProperty(r => r.SignatureSizeWidth, 5.5F);
            _mockView.SetupProperty(r => r.SignatureSizeLength, 15.3125F);
            _mockView.SetupProperty(r => r.SheetSizeAround, 23);
            _mockView.SetupProperty(r => r.SheetSizeAcross, 29);
            _mockView.SetupProperty(r => r.SideMargin, 1);
            _mockView.SetupProperty(r => r.Bleeds, 0.125F);
            _mockView.SetupProperty(r => r.TailMargin, 0.5F);
            _mockView.SetupProperty(r => r.Gripper, 0.5F);
            _mockView.SetupProperty(r => r.OptionTwoAcross);
            _mockView.SetupProperty(r => r.OptionTwoArea);
            _mockView.SetupProperty(r => r.OptionTwoAround);
            _mockView.SetupProperty(r => r.OptionTwoOut);
            _mockView.SetupProperty(r => r.OptionTwoWasteage);

            _mockView.Raise(r => r.CalculatePossibleOptions += null, new EventArgs());

            Assert.IsTrue(_mockView.Object.OptionTwoAcross == 5 && _mockView.Object.OptionTwoArea == 421.09F &&
                _mockView.Object.OptionTwoAround == 1 && _mockView.Object.OptionTwoOut == 5 &&
                _mockView.Object.OptionTwoWasteage == 37);

        }
    }
}
