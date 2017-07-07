using PrintingApp.Views;
using PrintingAppRepository.PrintingDesign;
using System;
using System.Windows.Forms;
using PrintingApp.Presenters;
using PrintingApp.Models.CustomEventArgs;

namespace PrintingApp.Forms {
    public partial class PrintingDesignForm : Form, IPrintingDesignView {
        private readonly IPrintingDesignManager _printingAppDesignManager;
        private PrintingDesignPresenter _presenter;

        public PrintingDesignForm(IPrintingDesignManager printingAppDesignManager) {
            _printingAppDesignManager = printingAppDesignManager;
            InitializeComponent();
        }

        string IPrintingDesignView.Scale { set => lblScale.Text = value; }
        public Form Form { get => this; }
        public string ErrorMessage { get => lblErrorMessage.Text; set => lblErrorMessage.Text = value; }

        public event EventHandler<EventArgs> ResizeForm;
        public event EventHandler<EventArgs> ClearPaintedItems;
        public event EventHandler<ErrorEventArgs> LogErrorToView;
        public event EventHandler<EventArgs> OnFormLoad;
        public event EventHandler<EventArgs> SetImpositionFormAsActive;

        private void PrintingDesignForm_Resize(object sender, EventArgs e) {
            ClearPaintedItems(this, e);
            ResizeForm(this, e);
        }

        private void PrintingDesignForm_Load(object sender, EventArgs e) {
            _presenter = new PrintingDesignPresenter(_printingAppDesignManager, this);
            OnFormLoad(this, e);
        }

        private void btnShowImpositionCalculator_Click(object sender, EventArgs e) {
            SetImpositionFormAsActive(this, e);
        }
    }
}
