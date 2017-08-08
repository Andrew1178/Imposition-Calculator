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
    /// <summary>
    /// This is the presenter, it contains most of the logic which is required for the calculator.
    /// I have opted to use the Manager and Repository approach also. 
    /// 
    /// This is used whenever you need to make CRUD request to the DB/File source. I have seperated the
    /// CRUD funtionality into the Repositories and if anymore modifications need to be made to the
    /// data, I make these modifications in the Manager and then serve them up to the Presenter.
    /// </summary>
    public class ImpositionCalculatorPresenter {
        private readonly IImpositionCalculatorManager _impostionCalculatorManager;
        private readonly ISystemVariablesManager _systemVariablesManager;
        private readonly SystemVariablesPresenter _systemVariablesFormPresenter;
        private readonly IPrintingDesignManager _printingDesignManager;
        private readonly string _pathToAppSettings = $"{AppDomain.CurrentDomain.BaseDirectory}/PrintAppSettings.txt";
        private PagePrintingDesignParameters _printingAppParameters;
        private readonly IImpositionFormView _view;

        /// <summary>
        /// Inject all interfaces, assign in constructor and initialise events. 
        /// </summary>
        /// <param name="View"></param>
        /// <param name="impositionCalculatorManager"></param>
        /// <param name="systemVariablesManager"></param>
        /// <param name="systemVariablesFormPresenter"></param>
        /// <param name="printingDesignManager"></param>
        public ImpositionCalculatorPresenter(IImpositionFormView View,
            IImpositionCalculatorManager impositionCalculatorManager,
            ISystemVariablesManager systemVariablesManager,
            SystemVariablesPresenter systemVariablesFormPresenter,
            IPrintingDesignManager printingDesignManager) {
            _view = View;
            _impostionCalculatorManager = impositionCalculatorManager;
            _systemVariablesManager = systemVariablesManager;
            _systemVariablesFormPresenter = systemVariablesFormPresenter;
            _printingDesignManager = printingDesignManager;
            InitialiseEvents();
        }

        /// <summary>
        /// This adds all the conscriptions to the views events
        /// </summary>
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

        /// <summary>
        /// Set focus to ImpositionForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetPrintingDesignFormAsActive(object sender, EventArgs e) {
            PrintingDesignForm form = Application.OpenForms["PrintingDesignForm"] as PrintingDesignForm;
            if (form != null) {
                form.Show();
                form.Focus();
            }
        }

        private void CalculateFinalImpositionPlates(object sender, EventArgs e) {
            try {
                if (_view.FinalImpositionOut != 0) {
                    //If the plates have been manually overridden by the user then assign these as the plates
                    if (_view.OverridePlates != 0) {
                        _view.Plates = _view.OverridePlates;
                    }
                    /*If the PrintingStlye is SheetFed and the Imposition Value is sheetwise or an
                     * irregular one sided print then use colour side one as the plate value.
                     */
                    else if (_view.PrintingStyleChecked == PrintingStyle.SheetFed && (_view.CboImposition != "Sheetwise" || _view.CboImposition != "Irregular S\\Wise")) {
                        _view.Plates = _view.ColourSideOne;
                    }
                    // If either colour side values have been populated the user must want to use both
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

        /// <summary>
        /// Set the default values for the Imposition Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Call manager to request data from the data source and assign these ink values the 
        /// returned data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateInkDataSources(object sender, EventArgs e) {
            try {
                var data = _impostionCalculatorManager.ReturnInkDataSource();

                _view.InkSideOneDataSource = data;

                /* Need a fresh list which doesnt reference value because whenever changing the
                 * selected index on either ink values, it changes both combo boxes */

                var newInstanceOfInkData = new List<ComboBoxItem>(data).ToArray();

                _view.InkSideTwoDataSource = newInstanceOfInkData;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        /// <summary>
        /// Call manager to request data from the data source and assign these ink values the 
        /// returned data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateCoatingDataSources(object sender, EventArgs e) {
            try {
                var data = _impostionCalculatorManager.ReturnCoatingDataSource();

                _view.CoatingSideOneDataSource = data;

                /* Need a fresh list which doesnt reference value because whenever changing the
                 * selected index on either ink values, it changes both combo boxes */

                var newInstanceOfCoatingData = new List<ComboBoxItem>(data).ToArray();

                _view.CoatingSideTwoDataSource = newInstanceOfCoatingData;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        /// <summary>
        /// Show the box which the error message is located and either append onto the error message
        /// or create a new one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Determine how to calculate sheet size around based on whether the user has manually
        /// overridden it or which printing style has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Determine how to calculate sheet size across based on whether the user has manually
        /// overridden it or which printing style has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (_view.PageCount != 0 && !string.IsNullOrEmpty(_view.PagesPerSignature)
                    && _view.PageSizeLength != 0 && _view.PageSizeWidth != 0) {

                    var systemVariables = _systemVariablesManager.ReturnNudVariables();

                    _view.SignatureSizeLength = _impostionCalculatorManager.ReturnSignatureSizeLength(
                        new SignatureSizeLength(_view.PagesPerSignature, _view.PageSizeLength,
                        systemVariables.HeadTrim, systemVariables.FootTrim));

                    _view.SignatureSizeWidth = _impostionCalculatorManager.ReturnSignatureSizeWidth(
                        new SignatureSizeWidth(_view.PagesPerSignature, _view.PageSizeWidth,
                        _view.Bleeds, systemVariables.BindingLip));
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
                    PropertiesToCalculateOptionsWith props = new PropertiesToCalculateOptionsWith(
                        _view.PageSizeWidth, _view.PageSizeLength, _view.SignatureSizeWidth,
                        _view.SignatureSizeLength, _view.SheetSizeAround, _view.SheetSizeAcross,
                        _view.Gripper, _view.TailMargin, _view.Bleeds, _view.SideMargin);

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
                    PropertiesToCalculateOptionsWith props = new PropertiesToCalculateOptionsWith(
                        _view.PageSizeWidth, _view.PageSizeLength, _view.SignatureSizeWidth,
                        _view.SignatureSizeLength, _view.SheetSizeAround, _view.SheetSizeAcross,
                        _view.Gripper, _view.TailMargin, _view.Bleeds, _view.SideMargin);

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
                if ((_view.PrintingStyleChecked != PrintingStyle.SheetFed)
                    || (_view.ColourSideOne + _view.ColourSideTwo == 0)
                    || (_view.OptionOneAround == 0) || (_view.PageSizeLength == 0)) {
                    _view.OptionOneMessage = "";
                }
                else if ((_view.ColourSideOne == 0 && _view.ColourSideTwo > 0)
                    || (_view.ColourSideOne > 0 && _view.ColourSideTwo == 0)) {
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
                if ((_view.PrintingStyleChecked != PrintingStyle.SheetFed)
                    || (_view.ColourSideOne + _view.ColourSideTwo == 0)) {
                    _view.OptionTwoMessage = "";
                }
                else if ((_view.ColourSideOne == 0 && _view.ColourSideTwo > 0)
                    || (_view.ColourSideOne > 0 && _view.ColourSideTwo == 0)) {
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

        /// <summary>
        /// Loop around all objects in the form and clear them and/or set them to their default values
        /// Clear error message and close any open printing designs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Setup any printing parameters which will be used to calculate the printing design
        /// Close any previously created printing designs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetUpPrintingDiagram(object sender, EventArgs e) {
            try {
                // Create a new instance of the form
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

        /// <summary>
        /// If returned data is not null, convert to single and order by largest values
        /// Set this data to be the cut off data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateCutOffDataSource(object sender, EventArgs e) {
            try {
                _view.CutOffDataSource = _systemVariablesManager.ReturnListBoxValues("CutOff")?
                    .ConvertAll(r => Convert.ToSingle(r)).OrderBy(r => r).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }


        /// <summary>
        /// If returned data is not null, convert to single and order by largest values
        /// Set this data to be the roll size data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateRollSizeDataSource(object sender, EventArgs e) {
            try {
                _view.RollSizeDataSource = _systemVariablesManager.ReturnListBoxValues("RollSize")?
                    .ConvertAll(r => Convert.ToSingle(r)).OrderBy(r => r).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }


        /// <summary>
        /// If returned data is not null, convert values to string
        /// Set this data to be the sheet size data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateSheetSizeDataSource(object sender, EventArgs e) {
            try {
                _view.SheetSizeDataSource = _systemVariablesManager.ReturnListBoxValues("SheetSize")?
                    .Select(r => r.ToString()).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        /// <summary>
        /// Once a printing style has been selected, calculate the printing parameters according to
        /// the system variables returned from the data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                var printingStyleValues = _systemVariablesManager
                    .ReturnPrintingStyleValuesBasedOnPassedInStyle(printingStyleSelected);

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