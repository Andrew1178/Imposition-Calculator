using System;
using System.Windows.Forms;
using PrintingApp.Views;
using PrintingApp.Presenters;
using PrintingAppRepository.SystemVariables;
using PrintingAppRepository.ImpositionCalculator.Model;
using PrintingApp.Models.CustomEventArgs;
using PrintingAppRepository.ImpositionCalculator;
using PrintingAppRepository.PrintingDesign;
using System.Linq;
using System.Collections.Generic;

namespace PrintingApp {
    /// <summary>
    /// This application works off of the MVP approach (Model-View-Presenter)
    /// The presenter does not know about the status of the view
    /// And the view does not have any knowledge on how to perform any calculations
    /// 
    /// The view raises any events which it requires and the presenter updates them
    /// Please see here for more information: https://msdn.microsoft.com/en-us/library/ff649571.aspx
    /// </summary>
    public partial class ImpositionForm : Form, IImpositionFormView {
        private ImpositionCalculatorPresenter _presenter;
        private readonly SystemVariablesPresenter _systemVariablesFormPresenter;
        private readonly IImpositionCalculatorManager _signatureSizeManager;
        private readonly ISystemVariablesManager _systemVariablesManager;
        private readonly IPrintingDesignManager _printingDesignManager;
        private PrintingStyle _printingStyle;

        /* These are the events which dictate how the form operates. I have split them down into their
        core features
        */

        public event EventHandler<EventArgs> CalculateSheetSizes;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<EventArgs> ClearPage;
        public event EventHandler<EventArgs> CalculateSignatureSize;
        public event EventHandler<EventArgs> OpenSystemVariablesForm;
        public event EventHandler<EventArgs> FormOnLoad;
        public event EventHandler<EventArgs> CalculatePossibleOptions;
        public event EventHandler<EventArgs> CalculateSideOneColourValue;
        public event EventHandler<EventArgs> CalculateSideTwoColourValue;
        public event EventHandler<EventArgs> CalculateFinalImpositionPlates;
        public event EventHandler<EventArgs> SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged;
        public event EventHandler<EventArgs> PopulateOptionLabels;
        public event EventHandler<EventArgs> SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked;
        public event EventHandler<EventArgs> CreatePrintingDesign;
        public event EventHandler<EventArgs> SetPrintingDesignFormAsActive;
        public event EventHandler<EventArgs> RefreshData;

        /// <summary>
        /// Inject all interfaces, assign in constructor and initialise events. 
        /// </summary>
        /// <param name="impositionFormManager"></param>
        /// <param name="systemVariablesManager"></param>
        /// <param name="systemVariablesFormPresenter"></param>
        /// <param name="printingDesignManager"></param>
        public ImpositionForm(IImpositionCalculatorManager impositionFormManager, ISystemVariablesManager systemVariablesManager,
            SystemVariablesPresenter systemVariablesFormPresenter, IPrintingDesignManager printingDesignManager) {
            _systemVariablesManager = systemVariablesManager;
            _signatureSizeManager = impositionFormManager;
            _systemVariablesFormPresenter = systemVariablesFormPresenter;
            _printingDesignManager = printingDesignManager;
            InitializeComponent();
        }

        /// <summary>
        /// On form load, assign the presenter. This will mean that all events will have methods 
        /// attached when invoked.
        /// Also call FormOnLoad to set the page up 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpositionForm_Load(object sender, EventArgs e) {
            //Cannot use inject this presenter otherwise you get an inifinte loop of two classes injecting each other
            Presenter = new ImpositionCalculatorPresenter(this, _signatureSizeManager, _systemVariablesManager, _systemVariablesFormPresenter, _printingDesignManager);

            try {
                FormOnLoad(this, e);
            }
            catch (Exception ex) {
                Error(this, new ErrorEventArgs(ex.Message));
            }
        }

        public int PageCount {
            get => (int)nudPageCount.Value;
            set => nudPageCount.Value = value;
        }

        public string PagesPerSignature {
            get {
                string toReturn = null;
                if (cboPagesPerSignature.SelectedIndex != -1) {
                    toReturn = cboPagesPerSignature.Text;
                }
                return toReturn;
            }
            set {
                cboPagesPerSignature.SelectedItem = value;
            }
        }

        public float CutOff {
            get {
                if (!string.IsNullOrEmpty(cboCutOff.Text)) {
                    return float.Parse(cboCutOff.Text);
                }
                else {
                    return 0;
                }
            }
            set => cboCutOff.SelectedItem = value;
        }

        public float RollSize {
            get {
                if (!string.IsNullOrEmpty(cboRollSize.Text)) {
                    return float.Parse(cboRollSize.Text);
                }
                else {
                    return 0;
                }
            }
            set => cboRollSize.SelectedItem = value;
        }

        public float Gripper {
            get {
                return (float)nudGripper.Value;
            }
            set => nudGripper.Value = (decimal)value;
        }

        public float TailMargin {
            get {
                return (float)nudTailMargin.Value;
            }
            set => nudTailMargin.Value = (decimal)value;
        }
        public float SideMargin {
            get {
                return (float)nudSideMargin.Value;
            }
            set => nudSideMargin.Value = (decimal)value;
        }

        public float Bleeds {
            get {
                return (float)nudBleeds.Value;
            }
            set => nudBleeds.Value = (decimal)value;
        }

        public float SheetSizeAcross {
            get {
                return string.IsNullOrEmpty(txtSheetSizeAcross.Text) ? 0 : float.Parse(txtSheetSizeAcross.Text);
            }
            set {
                txtSheetSizeAcross.Text = value.ToString();
            }
        }
        public float SheetSizeAround {
            get {
                return string.IsNullOrEmpty(txtSheetSizeAround.Text) ? 0 : float.Parse(txtSheetSizeAround.Text);
            }
            set {
                txtSheetSizeAround.Text = value.ToString();
            }
        }
        public int SideOneInkValue {
            get {
                return cboInkSide1.SelectedValue == null ? 0 : (int)cboInkSide1.SelectedValue;
            }
            set => cboInkSide1.SelectedItem = value;
        }

        public int SideOneCoatingValue {
            get {
                return cboCoatingSide1.SelectedValue == null ? 0 : (int)cboCoatingSide1.SelectedValue;
            }
            set => cboCoatingSide1.SelectedItem = value;
        }

        public int SideTwoInkValue {
            get {
                return cboInkSide2.SelectedValue == null ? 0 : (int)cboInkSide2.SelectedValue;
            }
            set => cboInkSide2.SelectedItem = value;

        }
        public int SideTwoCoatingValue {
            get {
                return cboCoatingSide2.SelectedValue == null ? 0 : (int)cboCoatingSide2.SelectedValue;
            }
            set => cboCoatingSide2.SelectedItem = value;

        }
        public int PageSizeWidth {
            get => (int)nudPageSizeWidth.Value;
            set => nudPageSizeWidth.Value = value;
        }
        public int PageSizeLength {
            get => (int)nudPageSizeLength.Value;
            set => nudPageSizeLength.Value = value;
        }
        public float SignatureSizeWidth {
            get {
                return float.Parse(txtSignatureSizeWidth.Text);
            }
            set {
                txtSignatureSizeWidth.Text = value.ToString();
            }
        }
        public float SignatureSizeLength {
            get {
                return float.Parse(txtSignatureSizeLength.Text);
            }
            set {
                txtSignatureSizeLength.Text = value.ToString();
            }
        }
        public int OptionOneAround {
            get {
                return string.IsNullOrEmpty(txtOption1Around.Text) == true ? 0 : int.Parse(txtOption1Around.Text);
            }
            set {
                txtOption1Around.Text = value.ToString();
            }
        }
        public int OptionOneAcross {
            get {
                return string.IsNullOrEmpty(txtOption1Across.Text) == true ? 0 : int.Parse(txtOption1Across.Text);
            }
            set {
                txtOption1Across.Text = value.ToString();
            }
        }
        public int OptionOneOut {
            get {
                return string.IsNullOrEmpty(txtOption1Out.Text) ? 0 : int.Parse(txtOption1Out.Text);
            }
            set {
                txtOption1Out.Text = value.ToString();
            }
        }
        public float OptionOneArea {
            get {
                return float.Parse(txtOption1Area.Text);
            }
            set {
                txtOption1Area.Text = value.ToString();
            }
        }
        public int OptionOneWasteage {
            get {
                return int.Parse(txtOption1Waste.Text);
            }
            set {
                txtOption1Waste.Text = value.ToString();
            }
        }
        public int OptionTwoAround {
            get {
                return string.IsNullOrEmpty(txtOption2Around.Text) == true ? 0 : int.Parse(txtOption2Around.Text);
            }
            set {
                txtOption2Around.Text = value.ToString();
            }
        }
        public int OptionTwoAcross {
            get {
                return string.IsNullOrEmpty(txtOption2Across.Text) == true ? 0 : int.Parse(txtOption2Across.Text);
            }
            set {
                txtOption2Across.Text = value.ToString();
            }
        }
        public int OptionTwoOut {
            get {
                return string.IsNullOrEmpty(txtOption1Out.Text) ? 0 : int.Parse(txtOption2Out.Text);
            }
            set {
                txtOption2Out.Text = value.ToString();
            }
        }
        public float OptionTwoArea {
            get {
                return float.Parse(txtOption2Area.Text);
            }
            set {
                txtOption2Area.Text = value.ToString();
            }
        }
        public int OptionTwoWasteage {
            get {
                return int.Parse(txtOption2Waste.Text);
            }
            set {
                txtOption2Waste.Text = value.ToString();
            }
        }

        public float OverrideSheetSizeAround {
            get => (float)nudOverrideSheetSizeAround.Value;
            set => nudOverrideSheetSizeAround.Value = (decimal)value;
        }

        public float OverrideSheetSizeAcross {
            get => (float)nudOverrideSheetSizeAcross.Value;
            set => nudOverrideSheetSizeAcross.Value = (decimal)value;
        }

        public string CboSheetSize {
            get => cboSheetSize.Text;
            set => cboSheetSize.SelectedItem = value;
        }

        public string ErrorMessage {
            get {
                return lblErrors.Text;
            }
            set {
                lblErrors.Text = value;
            }
        }

        public ComboBoxItem[] InkSideOneDataSource {
            get => cboInkSide1.Items.Cast<ComboBoxItem>().ToArray();
            set {
                cboInkSide1.DisplayMember = "Text";
                cboInkSide1.ValueMember = "Value";
                cboInkSide1.DataSource = value;
            }
        }

        public ComboBoxItem[] InkSideTwoDataSource {
            get => cboInkSide2.Items.Cast<ComboBoxItem>().ToArray();
            set {
                cboInkSide2.DisplayMember = "Text";
                cboInkSide2.ValueMember = "Value";
                cboInkSide2.DataSource = value;
            }
        }

        public ComboBoxItem[] CoatingSideOneDataSource {
            get => cboCoatingSide1.Items.Cast<ComboBoxItem>().ToArray();
            set {
                cboCoatingSide1.DisplayMember = "Text";
                cboCoatingSide1.ValueMember = "Value";
                cboCoatingSide1.DataSource = value;
            }
        }

        public ComboBoxItem[] CoatingSideTwoDataSource {
            get => cboCoatingSide2.Items.Cast<ComboBoxItem>().ToArray();
            set {
                cboCoatingSide2.DisplayMember = "Text";
                cboCoatingSide2.ValueMember = "Value";
                cboCoatingSide2.DataSource = value;
            }
        }

        public int FinalImpositionOut {
            get {
                return (int)nudFinalImpositionOut.Value;
            }
            set {
                nudFinalImpositionOut.Value = value;
            }
        }

        public string CboImposition {
            get {
                return cboFinalImposition.Text;
            }
            set => cboFinalImposition.SelectedItem = value;
        }

        public int ColourSideOne {
            get {
                return string.IsNullOrEmpty(txtFinalImpositionSide1.Text) ? 0 : int.Parse(txtFinalImpositionSide1.Text);
            }
            set => txtFinalImpositionSide1.Text = value.ToString();
        }

        public int ColourSideTwo {
            get {
                return string.IsNullOrEmpty(txtFinalImpositionSide2.Text) ? 0 : int.Parse(txtFinalImpositionSide2.Text);
            }
            set => txtFinalImpositionSide2.Text = value.ToString();
        }

        public int Plates {
            get {
                return int.Parse(txtFinalImpositionPlates.Text);
            }
            set => txtFinalImpositionPlates.Text = value.ToString();
        }

        public int OverridePlates {
            get => (int)nudOverridePlates.Value; set => nudOverridePlates.Value = value;
        }

        public string OptionTwoMessage {
            get {
                return lblOptionTwoMessage.Text;
            }
            set => lblOptionTwoMessage.Text = value;
        }

        public string OptionOneMessage {
            get {
                return lblOptionOneMessage.Text;
            }
            set => lblOptionOneMessage.Text = value;
        }

        public bool PrintingDesignFromBtnVisibility {
            get => btnShowDesign.Visible;
            set => btnShowDesign.Visible = value;
        }

        public bool SheetFedCheckedValue {
            get => radSheetFed.Checked;
            set => radSheetFed.Checked = value;
        }

        public PrintingStyle PrintingStyleChecked {
            get => _printingStyle;
            set => _printingStyle = value;
        }

        public bool IsOptionOneChecked {
            get => radOption1.Checked;
            set => radOption1.Checked = value;
        }

        //Set the Form to be this current form as we will need to use it in the Presenter.
        Form IImpositionFormView.ImpositionForm => this;

        public ImpositionCalculatorPresenter Presenter { get => _presenter; set => _presenter = Presenter; }

        public bool ErrorBoxShown { set => pnlErrors.Visible = value; }
        public List<float> CutOffDataSource {
            get => cboCutOff.Items.Cast<float>().ToList();
            set {
                cboCutOff.Items.Clear();
                cboCutOff.Items.AddRange(value.Cast<object>().ToArray());
            }
        }
        public List<float> RollSizeDataSource {
            get => cboRollSize.Items.Cast<float>().ToList();
            set {
                cboRollSize.Items.Clear();
                cboRollSize.Items.AddRange(value.Cast<object>().ToArray());
            }
        }
        public List<string> SheetSizeDataSource {
            get => CboSheetSize.Cast<string>().ToList();
            set {
                cboSheetSize.Items.Clear();
                cboSheetSize.Items.AddRange(value.Cast<object>().ToArray());
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            ClearPage(this, e);
        }

        private void btnLayout_Click(object sender, EventArgs e) {
            CreatePrintingDesign(this, e);
        }

        private void nudPageCount_ValueChanged(object sender, EventArgs e) {
            CalculateSignatureSize(this, e);
        }

        private void btnSetVariables_Click(object sender, EventArgs e) {
            OpenSystemVariablesForm(this, e);
        }

        private void cboSheetSize_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSheetSizes(this, e);
        }

        private void nudOverrideSheetSizeAround_ValueChanged(object sender, EventArgs e) {
            CalculateSheetSizes(this, e);
        }

        private void nudOverrideSheetSizeAcross_ValueChanged(object sender, EventArgs e) {
            CalculateSheetSizes(this, e);
        }

        private void cboPagesPerSignature_SelectionChangeCommitted(object sender, EventArgs e) {
            CalculateSignatureSize(this, e);
        }

        private void nudBleeds_ValueChanged(object sender, EventArgs e) {
            CalculateSignatureSize(this, e);
        }

        private void nudPageSizeWidth_ValueChanged(object sender, EventArgs e) {
            CalculateSignatureSize(this, e);
        }

        private void nudPageSizeLength_ValueChanged(object sender, EventArgs e) {
            CalculateSignatureSize(this, e);
        }

        private void txtSignatureSizeWidth_TextChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void txtSignatureSizeLength_TextChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void txtSheetSizeAround_TextChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void txtSheetSizeAcross_TextChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void nudGripper_ValueChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void nudTailMargin_ValueChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void nudSideMargin_ValueChanged(object sender, EventArgs e) {
            CalculatePossibleOptions(this, e);
        }

        private void cboInkSide1_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSideOneColourValue(this, e);
        }

        private void cboInkSide2_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSideTwoColourValue(this, e);
        }

        private void cboCoatingSide1_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSideOneColourValue(this, e);
        }

        private void cboCoatingSide2_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSideTwoColourValue(this, e);
        }

        private void nudOverridePlates_ValueChanged(object sender, EventArgs e) {
            CalculateFinalImpositionPlates(this, e);
        }

        private void txtFinalImpositionSide1_TextChanged(object sender, EventArgs e) {
            PopulateOptionLabels(this, e);
            CalculateFinalImpositionPlates(this, e);

        }

        private void txtFinalImpositionSide2_TextChanged(object sender, EventArgs e) {
            PopulateOptionLabels(this, e);
            CalculateFinalImpositionPlates(this, e);
        }

        private void cboCutOff_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSheetSizes(this, e);
        }

        private void cboRollSize_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateSheetSizes(this, e);
        }

        private void radSheetFed_CheckedChanged(object sender, EventArgs e) {
            if (radSheetFed.Checked) {
                PrintingStyleChecked = PrintingStyle.SheetFed;
                CalculateSheetSizes(this, e);
                SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged(this, e);
                CalculateFinalImpositionPlates(this, e);
            }
        }

        private void radHeatSet_CheckedChanged(object sender, EventArgs e) {
            if (radHeatSet.Checked) {
                PrintingStyleChecked = PrintingStyle.HeatSet;
                SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged(this, e);
                CalculateSheetSizes(this, e);
                CalculateFinalImpositionPlates(this, e);
            }
        }

        private void radUV_CheckedChanged(object sender, EventArgs e) {
            if (radUV.Checked) {
                PrintingStyleChecked = PrintingStyle.UV;
                SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged(this, e);
                CalculateSheetSizes(this, e);
                CalculateFinalImpositionPlates(this, e);
            }
        }

        private void cboFinalImposition_SelectedIndexChanged(object sender, EventArgs e) {
            CalculateFinalImpositionPlates(this, e);
        }

        private void nudFinalImpositionOut_ValueChanged(object sender, EventArgs e) {
            CalculateFinalImpositionPlates(this, e);
        }

        private void radOption1_CheckedChanged(object sender, EventArgs e) {
            SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked(this, e);
        }

        private void radOption2_CheckedChanged(object sender, EventArgs e) {
            SetFinalImpositionValuesAfterOptionRadioButtonsAreChecked(this, e);
        }

        private void txtOption1Around_TextChanged(object sender, EventArgs e) {
            PopulateOptionLabels(this, e);
        }

        private void txtOption1Across_TextChanged(object sender, EventArgs e) {
            PopulateOptionLabels(this, e);
        }

        private void txtOption2Around_TextChanged(object sender, EventArgs e) {
            PopulateOptionLabels(this, e);
        }

        private void txtOption2Across_TextChanged(object sender, EventArgs e) {
            PopulateOptionLabels(this, e);
        }

        private void btnShowDesign_Click(object sender, EventArgs e) {
            SetPrintingDesignFormAsActive(this, e);
        }

        private void radDigital_CheckedChanged(object sender, EventArgs e) {
            if (radDigital.Checked) {
                PrintingStyleChecked = PrintingStyle.Digital;
                SetPrintingValuesAfterPrintStyleRadioButtonsCheckedChanged(this, e);
                CalculateSheetSizes(this, e);
                CalculateFinalImpositionPlates(this, e);
            }
        }

        private void btnRefreshData_Click(object sender, EventArgs e) {
            RefreshData(this, e);
        }
    }
}