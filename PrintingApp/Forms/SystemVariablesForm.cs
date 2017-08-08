using PrintingApp.Models.CustomEventArgs;
using PrintingApp.Presenters;
using PrintingApp.Views;
using PrintingAppRepository.SystemVariables;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using PrintingAppRepository.SystemVariables.Models;

namespace PrintingApp {
    public partial class SystemVariablesForm : Form, ISystemVariablesView {

        private SystemVariablesPresenter presenter;
        private readonly ISystemVariablesManager manager;

        /// <summary>
        /// Inject all interfaces, assign in constructor and initialise events. 
        /// </summary>
        /// <param name="_manager"></param>
        public SystemVariablesForm(ISystemVariablesManager _manager) {
            manager = _manager;
            InitializeComponent();
        }

        public float BindingLip {
            get => (float)nudBindingLip.Value;
            set => nudBindingLip.Value = (decimal)value;
        }
        public float HeadTrim {
            get => (float)nudHeadTrim.Value;
            set => nudHeadTrim.Value = (decimal)value;
        }
        public float FootTrim {
            get => (float)nudFootTrim.Value;
            set => nudFootTrim.Value = (decimal)value;
        }

        public string ErrorMessage {
            get {
                return lblErrors.Text;
            }
            set => lblErrors.Text = value;
        }

        public float CutOffValueToAdd { get => (float)nudCutOff.Value; set => nudCutOff.Value = (decimal)value; }

        public List<float> CutOffValuesToRemove {
            get => libCutOffValues.SelectedItems.Cast<float>().ToList();
            set {
                libCutOffValues.Items.Clear();
                libCutOffValues.Items.AddRange(value.Cast<object>().ToArray());
            }
        }
        public List<float> CurrentCutOffValues {
            get => libCutOffValues.Items.Cast<float>().ToList();
            set {
                libCutOffValues.Items.Clear();
                libCutOffValues.Items.AddRange(value.Cast<object>().ToArray());
            }
        }

        public List<float> RollSizeValuesToRemove {
            get => libRollSizeValues.SelectedItems.Cast<float>().ToList();
            set {
                libRollSizeValues.Items.Clear();
                libRollSizeValues.Items.AddRange(value.Cast<object>().ToArray());
            }
        }
        public List<float> CurrentRollSizeValues {
            get => libRollSizeValues.Items.Cast<float>().ToList();
            set {
                libRollSizeValues.Items.Clear();
                libRollSizeValues.Items.AddRange(value.Cast<object>().ToArray());
            }
        }
        public float RollSizeValueToAdd { get => (float)nudRollSize.Value; set => nudRollSize.Value = (decimal)value; }

        public List<string> SheetSizeValuesToRemove {
            get => libSheetSizeValues.SelectedItems.Cast<string>().ToList();
            set {
                libSheetSizeValues.Items.Clear();
                libSheetSizeValues.Items.AddRange(value.Cast<object>().ToArray());
            }
        }
        public List<string> CurrentSheetSizeValues {
            get => libSheetSizeValues.Items.Cast<string>().ToList();
            set {
                libSheetSizeValues.Items.Clear();
                libSheetSizeValues.Items.AddRange(value.Cast<object>().ToArray());
            }
        }

        public string SheetSizeToAdd {
            get => $"{nudSheetSizeAround.Value} x {nudSheetSizeAcross.Value}";
            set {
                string[] temp = value.ToString().Split('x');
                nudSheetSizeAround.Value = decimal.Parse(temp[0]);
                nudSheetSizeAcross.Value = decimal.Parse(temp[1]);
            }
        }

        public bool IsErrorPanelShown { get => pnlErrors.Visible; set => pnlErrors.Visible = value; }

        public List<string> CboPrintingStyleDataSource {
            get => cboPrintingStyle.Items.Cast<string>().ToList();
            set {
                cboPrintingStyle.Items.Clear();
                cboPrintingStyle.Items.AddRange(value.Cast<object>().ToArray());
            }
        }

        public string CurrentPrintingStyle { get => cboPrintingStyle.SelectedItem.ToString(); set => cboPrintingStyle.SelectedItem = value; }

        public PrintingStyleClass CurrentPrintingStyleValues {
            get {
                return new PrintingStyleClass((float)nudGripper.Value, (float)nudSideMargin.Value, (float)nudTailMargin.Value, (float)nudBleeds.Value);
            }
            set {
                nudGripper.Value = (decimal)value.Gripper;
                nudSideMargin.Value = (decimal)value.SideMargin;
                nudTailMargin.Value = (decimal)value.TailMargin;
                nudBleeds.Value = (decimal)value.Bleeds;
            }
        }

        public event EventHandler<EventArgs> SetSystemVariables;
        public event EventHandler<EventArgs> OnFormLoad;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<EventArgs> ImpositionFormActivated;
        public event EventHandler<EventArgs> AddCutOffValue;
        public event EventHandler<EventArgs> RemoveCutOffValues;
        public event EventHandler<EventArgs> AddRollSizeValue;
        public event EventHandler<EventArgs> RemoveRollSizeValues;
        public event EventHandler<EventArgs> AddSheetSizeValue;
        public event EventHandler<EventArgs> RemoveSheetSizeValue;
        public event EventHandler<EventArgs> CboPrintingStyleChanged;
        public event EventHandler<EventArgs> ModifyPrintingStyle;

        private void SetSystemVariables_Load(object sender, EventArgs e) {           
            presenter = new SystemVariablesPresenter(this, manager);
            try {
                OnFormLoad(this, e);
            }
            catch (Exception ex) {
                Error(this, new ErrorEventArgs(ex.Message));
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e) {
            SetSystemVariables(this, e);
            OnFormLoad(this, e);
        }

        private void btnShowImpositionForm_Click(object sender, EventArgs e) {
            ImpositionFormActivated(this, e);
        }

        private void btnAddCutOffValue_Click(object sender, EventArgs e) {
            AddCutOffValue(this, e);
        }

        private void btnRemoveCutOffValues_Click(object sender, EventArgs e) {
            RemoveCutOffValues(this, e);
        }

        private void btnAddRollSize_Click(object sender, EventArgs e) {
            AddRollSizeValue(this, e);
        }

        private void btnRemoveRollSizeValues_Click(object sender, EventArgs e) {
            RemoveRollSizeValues(this, e);
        }

        private void btnAddSheetSize_Click(object sender, EventArgs e) {
            AddSheetSizeValue(this, e);
        }

        private void btnRemoveSheetSizeValues_Click(object sender, EventArgs e) {
            RemoveSheetSizeValue(this, e);
        }

        private void cboPrintingStyle_SelectedIndexChanged(object sender, EventArgs e) {
            CboPrintingStyleChanged(this, e);
        }

        private void btnSubmitPrintingStyles_Click(object sender, EventArgs e) {
            ModifyPrintingStyle(this, e);
        }
    }
}