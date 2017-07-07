using PrintingApp.Forms;
using PrintingApp.Models.CustomEventArgs;
using PrintingApp.Views;
using PrintingAppRepository.ImpositionCalculator;
using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingAppRepository.PrintingDesign;
using PrintingAppRepository.PrintingDesign.Models;
using PrintingAppRepository.SignatureSize.Model;
using PrintingAppRepository.SystemVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PrintingApp.Presenters {
    public class ImpositionCalculatorPresenter {
        private readonly IImpositionCalculatorManager _impostionCalculatorManager;
        private readonly ISystemVariablesManager _systemVariablesManager;
        private readonly SystemVariablesPresenter _systemVariablesFormPresenter;
        private readonly IPrintingDesignManager _printingDesignManager;
        private readonly string _pathToAppSettings = $"{AppDomain.CurrentDomain.BaseDirectory}/PrintAppSettings.txt";
        private PagePrintingDesignParameters _printingAppParameters;
        private readonly IImpositionFormView _view;

        public ImpositionCalculatorPresenter(IImpositionFormView View, IImpositionCalculatorManager impositionCalculatorManager,
            ISystemVariablesManager systemVariablesManager, SystemVariablesPresenter systemVariablesFormPresenter, IPrintingDesignManager printingDesignManager) {
            _view = View;
            _impostionCalculatorManager = impositionCalculatorManager;
            _systemVariablesManager = systemVariablesManager;
            _systemVariablesFormPresenter = systemVariablesFormPresenter;
            _printingDesignManager = printingDesignManager;
            InitialiseEvents();
        }
        private void InitialiseEvents() {
            _view.FormOnLoad += PopulateInkDataSources;
            _view.FormOnLoad += PopulateCoatingDataSources;
            _view.FormOnLoad += PopulateCutOffDataSource;
            _view.FormOnLoad += PopulateRollSizeDataSource;
            _view.FormOnLoad += PopulateSheetSizeDataSource;
            _view.FormOnLoad += SetDefaultValues;

            _view.CalculateSheetSizes += CalculateSheetSizeAcross;
            _view.CalculateSheetSizes += CalculateSheetSizeAround;

            _view.CalculateSignatureSize += CalculateSignatureSizeValues;

            _view.CalculatePossibleOptions += CalculateOptionOneValues;
            _view.CalculatePossibleOptions += CalculateOptionTwoValues;

            _view.PopulateOptionLabels += SetOptionOneLabel;
            _view.PopulateOptionLabels += SetOptionTwoLabel;

            _view.OpenSystemVariablesForm += OpenSystemVariablesForm;

            _view.SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked += SetCboImpositionValues;
            _view.SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked += SetFinalImpositionOutValue;

            _view.CalculateSideOneColourValue += SetSideOneColours;
            _view.CalculateSideTwoColourValue += SetSideTwoColours;

            _view.Error += LogErrorToView;

            _view.CalculateFinalImpositionPlates += CalculateFinalImpositionPlates;

            _view.SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged += PopulatePrintingStyleValues;

            _view.ClearPage += ResetForm;
            _view.ClearPage += SetDefaultValues;
            _view.ClearPage += HideShowPrintingDesignBtn;
            _view.ClearPage += HideErrorPanel;

            _view.CreatePrintingDesign += SetUpPrintingDiagram;

            _view.SetPrintingDesignFormAsActive += SetPrintingDesignFormAsActive;

            _view.RefreshData += PopulateCutOffDataSource;
            _view.RefreshData += PopulateRollSizeDataSource;
            _view.RefreshData += PopulateSheetSizeDataSource;
            _view.RefreshData += PopulatePrintingStyleValues;

        }

        private void HideErrorPanel(object sender, EventArgs e) {
            _view.ErrorBoxShown = false;
        }

        private void SetPrintingDesignFormAsActive(object sender, EventArgs e) {
            PrintingDesignForm form = Application.OpenForms["PrintingDesignForm"] as PrintingDesignForm;
            if (form != null) {
                form.Show();
                form.Focus();
            }
        }

        private void CalculateFinalImpositionPlates(object sender, EventArgs e) {
            //if sheet fed checked, and sheetwise not selected in drop down and sheetsize irregular not select in drop down
            try {
                if (_view.FinalImpositionOut != 0) {
                    if (_view.OverridePlates != 0) {
                        _view.Plates = _view.OverridePlates;
                    }
                    else if (_view.PrintingStyleChecked == PrintingStyle.SheetFed && (_view.CboImposition != "Sheetwise" || _view.CboImposition != "Irregular S\\Wise")) {
                        _view.Plates = _view.ColourSideOne;
                    }
                    else if (_view.ColourSideOne != 0 && _view.ColourSideTwo != 0) {
                        _view.Plates = _view.ColourSideOne + _view.ColourSideTwo;
                    }
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetSideOneColours(object sender, EventArgs e) {
            try {
                _view.ColourSideOne = _view.SideOneInkValue + _view.SideOneCoatingValue;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetSideTwoColours(object sender, EventArgs e) {
            try {
                _view.ColourSideTwo = _view.SideTwoInkValue + _view.SideTwoCoatingValue;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetDefaultValues(object sender, EventArgs e) {
            try {
                _view.PagesPerSignature = "2pg";
                _view.PageCount = 2;
                _view.CboSheetSize = "28 x 40";
                _view.SheetFedCheckedValue = true;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void PopulateInkDataSources(object sender, EventArgs e) {
            try {
                var data = _impostionCalculatorManager.ReturnInkDataSource();
                _view.InkSideOneDataSource = data;
                /* Need a copy which doesnt reference value because whenever changing the selected
            index, it changes any other combo boxes with the same data source */

                var copyOfArray = new List<ComboBoxItem>(data).ToArray();

                _view.InkSideTwoDataSource = copyOfArray;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void PopulateCoatingDataSources(object sender, EventArgs e) {
            try {
                var data = _impostionCalculatorManager.ReturnCoatingDataSource();
                _view.CoatingSideOneDataSource = data;
                /* Need a copy which doesnt reference value because whenever changing the selected
            index, it changes any other combo boxes with the same data source */
                var copyOfArray = new List<ComboBoxItem>(data).ToArray();
                _view.CoatingSideTwoDataSource = copyOfArray;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void LogErrorToView(object sender, ErrorEventArgs e) {
            _view.ErrorBoxShown = true;
            if (_view.ErrorMessage == null) {
                _view.ErrorMessage += e.ErrorMessage;
            }
            else {
                _view.ErrorMessage += $"{Environment.NewLine} {e.ErrorMessage}";
            }
        }

        private void HideShowPrintingDesignBtn(object sender, EventArgs e) {
            _view.PrintingDesignFromBtnVisibility = false;
        }

        private void CalculateSheetSizeAround(object sender, EventArgs e) {
            try {
                if (_view.OverrideSheetSizeAround != 0) {
                    _view.SheetSizeAround = _view.OverrideSheetSizeAround;
                }
                else if (_view.PrintingStyleChecked == PrintingStyle.SheetFed) {
                    string sheetSizeText = _view.CboSheetSize?.Replace(" ", "");
                    if (!string.IsNullOrEmpty(sheetSizeText) && sheetSizeText.Contains('x')) {
                        _view.SheetSizeAround = float.Parse(sheetSizeText.Split('x')[0]);
                    }
                    else if (!string.IsNullOrEmpty(sheetSizeText)) {
                        throw new Exception("Sheet size must also be in the format of: 'number x number'");
                    }
                }
                else {
                    _view.SheetSizeAround = _view.CutOff;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }

        }
        private void CalculateSheetSizeAcross(object sender, EventArgs e) {
            try {
                if (_view.OverrideSheetSizeAcross != 0) {
                    _view.SheetSizeAcross = _view.OverrideSheetSizeAcross;
                }
                else if (_view.PrintingStyleChecked == PrintingStyle.SheetFed) {
                    string sheetSizeText = _view.CboSheetSize?.Replace(" ", "");
                    if (!string.IsNullOrEmpty(sheetSizeText) && sheetSizeText.Contains('x')) {
                        _view.SheetSizeAcross = float.Parse(sheetSizeText.Split('x')[1]);
                    }
                    else if (!string.IsNullOrEmpty(sheetSizeText)) {
                        throw new Exception("Sheet size must also be in the format of: 'number x number'");
                    }
                }
                else {
                    _view.SheetSizeAcross = _view.RollSize;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void CalculateSignatureSizeValues(object sender, EventArgs e) {
            try {
                if (_view.PageCount != 0 && !string.IsNullOrEmpty(_view.PagesPerSignature) && _view.PageSizeLength != 0 && _view.PageSizeWidth != 0) {
                    var systemVariables = _systemVariablesManager.ReturnNudVariables();
                    _view.SignatureSizeLength = _impostionCalculatorManager.ReturnSignatureSizeLength(new SignatureSizeLength(_view.PagesPerSignature, _view.PageSizeLength, systemVariables.HeadTrim, systemVariables.FootTrim));
                    _view.SignatureSizeWidth = _impostionCalculatorManager.ReturnSignatureSizeWidth(new SignatureSizeWidth(_view.PagesPerSignature, _view.PageSizeWidth, _view.Bleeds, systemVariables.BindingLip));
                }
                else {
                    _view.SignatureSizeLength = 0;
                    _view.SignatureSizeWidth = 0;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void OpenSystemVariablesForm(object sender, EventArgs e) {
            try {
                SystemVariablesForm form = new SystemVariablesForm(_systemVariablesManager);
                form.Show();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void CalculateOptionOneValues(object sender, EventArgs e) {
            try {
                if (_view.PageSizeLength != 0 && _view.PageSizeWidth != 0 && _view.SignatureSizeWidth != 0 && _view.SignatureSizeLength != 0
                    && _view.SheetSizeAround != 0 && _view.SheetSizeAcross != 0) {
                    PropertiesToCalculateOptionsWith props = new PropertiesToCalculateOptionsWith(_view.PageSizeWidth, _view.PageSizeLength,
                        _view.SignatureSizeWidth, _view.SignatureSizeLength, _view.SheetSizeAround, _view.SheetSizeAcross, _view.Gripper,
                        _view.TailMargin, _view.Bleeds, _view.SideMargin);

                    var option = _impostionCalculatorManager.ReturnFinalOptionOne(props);

                    _view.OptionOneAcross = option.Across;
                    _view.OptionOneAround = option.Around;
                    _view.OptionOneOut = option.Out;
                    _view.OptionOneArea = option.Area;
                    _view.OptionOneWasteage = option.Wasteage;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void CalculateOptionTwoValues(object sender, EventArgs e) {
            try {
                if (_view.PageSizeLength != 0 && _view.PageSizeWidth != 0 && _view.SignatureSizeWidth != 0 && _view.SignatureSizeLength != 0
                    && _view.SheetSizeAround != 0 && _view.SheetSizeAcross != 0) {
                    PropertiesToCalculateOptionsWith props = new PropertiesToCalculateOptionsWith(_view.PageSizeWidth, _view.PageSizeLength,
                        _view.SignatureSizeWidth, _view.SignatureSizeLength, _view.SheetSizeAround, _view.SheetSizeAcross, _view.Gripper,
                        _view.TailMargin, _view.Bleeds, _view.SideMargin);

                    var option = _impostionCalculatorManager.ReturnFinalOptionTwo(props);

                    _view.OptionTwoAcross = option.Across;
                    _view.OptionTwoAround = option.Around;
                    _view.OptionTwoOut = option.Out;
                    _view.OptionTwoArea = option.Area;
                    _view.OptionTwoWasteage = option.Wasteage;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetOptionOneLabel(object sender, EventArgs e) {
            try {
                if ((_view.PrintingStyleChecked != PrintingStyle.SheetFed) || (_view.ColourSideOne + _view.ColourSideTwo == 0) || (_view.OptionOneAround == 0)
                    || (_view.PageSizeLength == 0)) {
                    _view.OptionOneMessage = "";
                }
                else if ((_view.ColourSideOne == 0 && _view.ColourSideTwo > 0) || (_view.ColourSideOne > 0 && _view.ColourSideTwo == 0)) {
                    _view.OptionOneMessage = "Irregular 1 Side";
                }
                else if (_view.OptionOneAcross % 2 == 0) {
                    _view.OptionOneMessage = "W/Turn";
                }
                else if (_view.OptionOneAround % 2 == 0) {
                    _view.OptionOneMessage = "W/Tumble";
                }
                else {
                    _view.OptionOneMessage = "Sheetwise";
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetOptionTwoLabel(object sender, EventArgs e) {
            try {
                if ((_view.PrintingStyleChecked != PrintingStyle.SheetFed) || (_view.ColourSideOne + _view.ColourSideTwo == 0)) {
                    _view.OptionTwoMessage = "";
                }
                else if ((_view.ColourSideOne == 0 && _view.ColourSideTwo > 0) || (_view.ColourSideOne > 0 && _view.ColourSideTwo == 0)) {
                    _view.OptionTwoMessage = "Irregular 1 Side";
                }
                else if (_view.OptionTwoAcross % 2 == 0) {
                    _view.OptionTwoMessage = "W/Turn";
                }
                else if (_view.OptionTwoAround % 2 == 0) {
                    _view.OptionTwoMessage = "W/Tumble";
                }
                else {
                    _view.OptionTwoMessage = "Sheetwise";
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetFinalImpositionOutValue(object sender, EventArgs e) {
            try {
                if (_view.OptionOneOut != 0) {
                    _view.FinalImpositionOut = _view.OptionOneOut;
                }
                else if (_view.OptionTwoOut != 0) {
                    _view.FinalImpositionOut = _view.OptionTwoOut;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetCboImpositionValues(object sender, EventArgs e) {
            try {
                if (_view.IsOptionOneChecked && !string.IsNullOrEmpty(_view.OptionOneMessage)) {
                    _view.CboImposition = _view.OptionOneMessage;
                }
                else if (!_view.IsOptionOneChecked && !string.IsNullOrEmpty(_view.OptionTwoMessage)) {
                    _view.CboImposition = _view.OptionTwoMessage;
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void ResetForm(object sender, EventArgs e) {
            try {

                foreach (Control item in _view.ImpositionForm.Controls) {
                    if (item is TextBox) {
                        item.Text = null;
                    }
                    else if (item is ComboBox) {
                        ComboBox cbo = (ComboBox)item;
                        cbo.SelectedIndex = -1;
                    }
                    else if (item is RadioButton) {
                        RadioButton rad = (RadioButton)item;
                        rad.Checked = false;
                    }
                    else if (item is NumericUpDown) {
                        NumericUpDown nud = (NumericUpDown)item;
                        nud.Value = 0;
                    }
                    else if (item is GroupBox) {
                        var grp = (GroupBox)item;

                        foreach (var grpItem in grp.Controls) {
                            if (grpItem is RadioButton) {
                                RadioButton radItem = (RadioButton)grpItem;
                                radItem.Checked = false;
                            }
                        }
                    }
                }
                _view.ErrorMessage = "";

                if (Application.OpenForms.OfType<PrintingDesignForm>().Any()) {
                    Application.OpenForms.OfType<PrintingDesignForm>().First().Close();
                }

            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetUpPrintingDiagram(object sender, EventArgs e) {
            try {
                //     Create a new instance of the form
                PrintingDesignForm form = new PrintingDesignForm(_printingDesignManager);

                //Create variables based on which option is checked, this is important and is used later down in this method
                int pagesUp = _view.IsOptionOneChecked ? _view.OptionOneAround : _view.OptionTwoAround;
                int pagesAcross = _view.IsOptionOneChecked ? _view.OptionOneAcross : _view.OptionTwoAcross;
                float pageWidth = _view.IsOptionOneChecked ? _view.PageSizeLength : _view.PageSizeWidth;
                float pageHeight = _view.IsOptionOneChecked ? _view.PageSizeWidth : _view.PageSizeLength;

                _printingAppParameters = new PagePrintingDesignParameters(_view.SheetSizeAcross, _view.SheetSizeAround, pagesUp,
                    pagesAcross, pageWidth, pageHeight, _view.IsOptionOneChecked, _view.Bleeds, form.ClientRectangle.Height, form.ClientRectangle.Width);

                //Check if form is open, if so close it
                if (Application.OpenForms.OfType<PrintingDesignForm>().Any()) {
                    Application.OpenForms.OfType<PrintingDesignForm>().First().Close();
                }

                //Save values to JSON file            
                _printingDesignManager.SavePagePrintingDesignParams(_printingAppParameters);

                form.Show();

                _view.PrintingDesignFromBtnVisibility = true;

            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void PopulateCutOffDataSource(object sender, EventArgs e) {
            try {
                _view.CutOffDataSource = _systemVariablesManager.ReturnListBoxValues("CutOff")?.ConvertAll(r => Convert.ToSingle(r)).OrderBy(r => r).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void PopulateRollSizeDataSource(object sender, EventArgs e) {
            try {
                _view.RollSizeDataSource = _systemVariablesManager.ReturnListBoxValues("RollSize")?.ConvertAll(r => Convert.ToSingle(r)).OrderBy(r => r).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void PopulateSheetSizeDataSource(object sender, EventArgs e) {
            try {
                _view.SheetSizeDataSource = _systemVariablesManager.ReturnListBoxValues("SheetSize")?.Select(r => r.ToString()).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void PopulatePrintingStyleValues(object sender, EventArgs e) {
            try {
                string printingStyleSelected = null;
                switch (_view.PrintingStyleChecked) {
                    case PrintingStyle.SheetFed:
                        printingStyleSelected = "Sheet Fed";
                        break;
                    case PrintingStyle.HeatSet:
                        printingStyleSelected = "Heat Set";
                        break;
                    case PrintingStyle.UV:
                        printingStyleSelected = "UV";
                        break;
                    case PrintingStyle.Digital:
                        printingStyleSelected = "Digital";
                        break;
                }

                var printingStyleValues = _systemVariablesManager.ReturnPrintingStyleValuesBasedOnPassedInStyle(printingStyleSelected);
                _view.Bleeds = printingStyleValues.Bleeds;
                _view.TailMargin = printingStyleValues.TailMargin;
                _view.SideMargin = printingStyleValues.SideMargin;
                _view.Gripper = printingStyleValues.Gripper;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }
    }
}