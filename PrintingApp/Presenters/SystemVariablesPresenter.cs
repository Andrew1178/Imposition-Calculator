using PrintingApp.Models.CustomEventArgs;
using PrintingApp.Views;
using PrintingAppRepository.SystemVariables;
using PrintingAppRepository.SystemVariables.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PrintingApp.Presenters {
    public class SystemVariablesPresenter {
        private readonly ISystemVariablesView _view;
        private readonly ISystemVariablesManager _systemVariablesManager;

        /// <summary>
        /// Inject all interfaces, assign in constructor and initialise events. 
        /// </summary>
        /// <param name="view"></param>
        /// <param name="systemVariablesManager"></param>
        public SystemVariablesPresenter(ISystemVariablesView view, ISystemVariablesManager systemVariablesManager) {
            _view = view;
            _systemVariablesManager = systemVariablesManager;
            InitialiseEvents();
        }

        /// <summary>
        /// Subscribe methods to event
        /// </summary>
        private void InitialiseEvents() {
            _view.OnFormLoad += DisplayNudSystemVariables;
            _view.OnFormLoad += DisplayCutOffs;
            _view.OnFormLoad += DisplayRollSizes;
            _view.OnFormLoad += DisplaySheetSizes;
            _view.OnFormLoad += PopulatePrintingKeysDataSource;

            _view.AddSheetSizeValue += ValidateSheetSizeToAdd;
            _view.AddSheetSizeValue += AddSheetSizeValue;
            _view.AddSheetSizeValue += DisplaySheetSizes;

            _view.RemoveSheetSizeValue += RemoveSheetSizeValues;
            _view.RemoveSheetSizeValue += DisplaySheetSizes;

            _view.AddCutOffValue += ValidateCutOffToAdd;
            _view.AddCutOffValue += AddCutOffValue;
            _view.AddCutOffValue += DisplayCutOffs;
            _view.RemoveCutOffValues += RemoveCutOffValues;
            _view.RemoveCutOffValues += DisplayCutOffs;

            _view.AddRollSizeValue += ValidateRollSizeToAdd;
            _view.AddRollSizeValue += AddRollSizeValue;
            _view.AddRollSizeValue += DisplayRollSizes;
            _view.RemoveRollSizeValues += RemoveRollSizeValues;
            _view.RemoveRollSizeValues += DisplayRollSizes;

            _view.SetSystemVariables += SetNudVariables;

            _view.Error += LogErrorToView;
            _view.Error += ShowErrorPanel;

            _view.ImpositionFormActivated += SetImpositionFormAsActive;

            _view.CboPrintingStyleChanged += DisplayCurrentPrintingValues;

            _view.ModifyPrintingStyle += ModifyPrintingValues;
            _view.ModifyPrintingStyle += DisplayCurrentPrintingValues;
        }

        //NUD - Numeric Up Down (WinForms control)
        private void DisplayNudSystemVariables(object sender, EventArgs e) {
            try {
                SystemVariables variables = _systemVariablesManager.ReturnNudVariables();
                _view.BindingLip = variables.BindingLip;
                _view.HeadTrim = variables.HeadTrim;
                _view.FootTrim = variables.FootTrim;
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void SetImpositionFormAsActive(object sender, EventArgs e) {
            try {
                ImpositionForm form = Application.OpenForms["ImpositionForm"] as ImpositionForm;
                if (form != null) {
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void LogErrorToView(object sender, ErrorEventArgs e) {
            if (_view.ErrorMessage == null) {
                _view.ErrorMessage += e.ErrorMessage;
            }
            else {
                _view.ErrorMessage += $"{Environment.NewLine} {e.ErrorMessage}";
            }
        }

        private void SetNudVariables(object sender, EventArgs e) {
            try {
                _systemVariablesManager.SetNudVariables(new SystemVariables(_view.BindingLip, _view.HeadTrim, _view.FootTrim));
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void ValidateCutOffToAdd(object sender, EventArgs e) {
            try {
                if (_view.CurrentCutOffValues != null && 
                    _view.CurrentCutOffValues.Contains(_view.CutOffValueToAdd))
                    throw new Exception("You cannot add a cut off value which is already present in the list.");
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void AddCutOffValue(object sender, EventArgs e) {
            try {
                _systemVariablesManager.AddListBoxValue("CutOff", _view.CutOffValueToAdd);
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void RemoveCutOffValues(object sender, EventArgs e) {
            try {
                _systemVariablesManager.DeleteListBoxValues("CutOff", "float",
                    _view.CutOffValuesToRemove.Cast<object>().ToList());
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void ShowErrorPanel(object sender, EventArgs e) {
            _view.IsErrorPanelShown = true;
        }

        /// <summary>
        /// Display all cut off values and convert them to singles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayCutOffs(object sender, EventArgs e) {
            try {
                _view.CurrentCutOffValues = _systemVariablesManager
                    .ReturnListBoxValues("CutOff").Select(r => Convert.ToSingle(r)).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void ValidateRollSizeToAdd(object sender, EventArgs e) {
            try {
                if (_view.CurrentRollSizeValues != null && 
                    _view.CurrentRollSizeValues.Contains(_view.RollSizeValueToAdd))
                    throw new Exception("You cannot add a roll size value which is already present in the list.");
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void AddRollSizeValue(object sender, EventArgs e) {
            try {
                _systemVariablesManager.AddListBoxValue("RollSize", _view.RollSizeValueToAdd);
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void RemoveRollSizeValues(object sender, EventArgs e) {
            try {
                _systemVariablesManager.DeleteListBoxValues("RollSize", "float",
                    _view.RollSizeValuesToRemove.Cast<object>().ToList());
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        /// <summary>
        /// Display all roll size values and convert all values to singles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayRollSizes(object sender, EventArgs e) {
            try {
                _view.CurrentRollSizeValues = _systemVariablesManager
                    .ReturnListBoxValues("RollSize").Select(r => Convert.ToSingle(r)).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void ValidateSheetSizeToAdd(object sender, EventArgs e) {
            try {
                if (!_view.SheetSizeToAdd.Contains(" "))
                    throw new Exception("The potential sheet size must have a space both before the x and after it.");

                if (_view.CurrentSheetSizeValues != null &&
                    _view.CurrentSheetSizeValues.Contains(_view.SheetSizeToAdd))
                    throw new Exception("You cannot add a sheet size value which is already present in the list.");
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void AddSheetSizeValue(object sender, EventArgs e) {
            try {
                _systemVariablesManager.AddListBoxValue("SheetSize", _view.SheetSizeToAdd);
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void RemoveSheetSizeValues(object sender, EventArgs e) {
            try {
                _systemVariablesManager.DeleteListBoxValues("SheetSize", "string", _view.SheetSizeValuesToRemove.Cast<object>().ToList());
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void DisplaySheetSizes(object sender, EventArgs e) {
            try {
                _view.CurrentSheetSizeValues = _systemVariablesManager
                    .ReturnListBoxValues("SheetSize").Select(r => r.ToString()).ToList();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void ModifyPrintingValues(object sender, EventArgs e) {
            try {
                _systemVariablesManager.ModifyPrintingStyleValues(_view.CurrentPrintingStyle,
                    _view.CurrentPrintingStyleValues);
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void DisplayCurrentPrintingValues(object sender, EventArgs e) {
            try {
                _view.CurrentPrintingStyleValues = _systemVariablesManager
                    .ReturnPrintingStyleValuesBasedOnPassedInStyle(_view.CurrentPrintingStyle);
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }
        private void PopulatePrintingKeysDataSource(object sender, EventArgs e) {
            try {
                _view.CboPrintingStyleDataSource = _systemVariablesManager
                    .ReturnAllPrintingStyles();
            }
            catch (Exception ex) {
                LogErrorToView(this, new ErrorEventArgs(ex.Message));
            }
        }
    }
}