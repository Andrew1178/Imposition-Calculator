namespace PrintingApp {
    partial class SystemVariablesForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemVariablesForm));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBindingLip = new System.Windows.Forms.Label();
            this.lblFootTrim = new System.Windows.Forms.Label();
            this.nudFootTrim = new System.Windows.Forms.NumericUpDown();
            this.nudHeadTrim = new System.Windows.Forms.NumericUpDown();
            this.nudBindingLip = new System.Windows.Forms.NumericUpDown();
            this.pnlErrors = new System.Windows.Forms.Panel();
            this.lblErrors = new System.Windows.Forms.Label();
            this.lblPanelTitle = new System.Windows.Forms.Label();
            this.btnShowImpositionForm = new System.Windows.Forms.Button();
            this.tabSystemVariables = new System.Windows.Forms.TabControl();
            this.tabBasicVariables = new System.Windows.Forms.TabPage();
            this.tabSheetSize = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSheetSizeAcross = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.libSheetSizeValues = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemoveSheetSizeValues = new System.Windows.Forms.Button();
            this.btnAddSheetSize = new System.Windows.Forms.Button();
            this.nudSheetSizeAround = new System.Windows.Forms.NumericUpDown();
            this.tabCutOff = new System.Windows.Forms.TabPage();
            this.libCutOffValues = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemoveCutOffValues = new System.Windows.Forms.Button();
            this.btnAddCutOffValue = new System.Windows.Forms.Button();
            this.nudCutOff = new System.Windows.Forms.NumericUpDown();
            this.tabRollSize = new System.Windows.Forms.TabPage();
            this.libRollSizeValues = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveRollSizeValues = new System.Windows.Forms.Button();
            this.btnAddRollSize = new System.Windows.Forms.Button();
            this.nudRollSize = new System.Windows.Forms.NumericUpDown();
            this.tabPrintingStyles = new System.Windows.Forms.TabPage();
            this.btnSubmitPrintingStyles = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudBleeds = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudGripper = new System.Windows.Forms.NumericUpDown();
            this.nudSideMargin = new System.Windows.Forms.NumericUpDown();
            this.nudTailMargin = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cboPrintingStyle = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFootTrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadTrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBindingLip)).BeginInit();
            this.pnlErrors.SuspendLayout();
            this.tabSystemVariables.SuspendLayout();
            this.tabBasicVariables.SuspendLayout();
            this.tabSheetSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSheetSizeAcross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSheetSizeAround)).BeginInit();
            this.tabCutOff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCutOff)).BeginInit();
            this.tabRollSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRollSize)).BeginInit();
            this.tabPrintingStyles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBleeds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGripper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSideMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailMargin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(124, 119);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(93, 34);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Head Trim:";
            // 
            // lblBindingLip
            // 
            this.lblBindingLip.AutoSize = true;
            this.lblBindingLip.Location = new System.Drawing.Point(78, 44);
            this.lblBindingLip.Name = "lblBindingLip";
            this.lblBindingLip.Size = new System.Drawing.Size(62, 13);
            this.lblBindingLip.TabIndex = 4;
            this.lblBindingLip.Text = "Binding Lip:";
            // 
            // lblFootTrim
            // 
            this.lblFootTrim.AutoSize = true;
            this.lblFootTrim.Location = new System.Drawing.Point(78, 95);
            this.lblFootTrim.Name = "lblFootTrim";
            this.lblFootTrim.Size = new System.Drawing.Size(54, 13);
            this.lblFootTrim.TabIndex = 6;
            this.lblFootTrim.Text = "Foot Trim:";
            // 
            // nudFootTrim
            // 
            this.nudFootTrim.DecimalPlaces = 4;
            this.nudFootTrim.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudFootTrim.Location = new System.Drawing.Point(155, 93);
            this.nudFootTrim.Name = "nudFootTrim";
            this.nudFootTrim.Size = new System.Drawing.Size(120, 20);
            this.nudFootTrim.TabIndex = 2;
            // 
            // nudHeadTrim
            // 
            this.nudHeadTrim.DecimalPlaces = 4;
            this.nudHeadTrim.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudHeadTrim.Location = new System.Drawing.Point(155, 67);
            this.nudHeadTrim.Name = "nudHeadTrim";
            this.nudHeadTrim.Size = new System.Drawing.Size(120, 20);
            this.nudHeadTrim.TabIndex = 1;
            // 
            // nudBindingLip
            // 
            this.nudBindingLip.DecimalPlaces = 4;
            this.nudBindingLip.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudBindingLip.Location = new System.Drawing.Point(155, 42);
            this.nudBindingLip.Name = "nudBindingLip";
            this.nudBindingLip.Size = new System.Drawing.Size(120, 20);
            this.nudBindingLip.TabIndex = 0;
            // 
            // pnlErrors
            // 
            this.pnlErrors.Controls.Add(this.lblErrors);
            this.pnlErrors.Controls.Add(this.lblPanelTitle);
            this.pnlErrors.Location = new System.Drawing.Point(89, 246);
            this.pnlErrors.Name = "pnlErrors";
            this.pnlErrors.Size = new System.Drawing.Size(353, 76);
            this.pnlErrors.TabIndex = 7;
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Location = new System.Drawing.Point(5, 31);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(0, 13);
            this.lblErrors.TabIndex = 9;
            // 
            // lblPanelTitle
            // 
            this.lblPanelTitle.AutoSize = true;
            this.lblPanelTitle.Location = new System.Drawing.Point(3, 9);
            this.lblPanelTitle.Name = "lblPanelTitle";
            this.lblPanelTitle.Size = new System.Drawing.Size(37, 13);
            this.lblPanelTitle.TabIndex = 8;
            this.lblPanelTitle.Text = "Errors:";
            // 
            // btnShowImpositionForm
            // 
            this.btnShowImpositionForm.Location = new System.Drawing.Point(11, 12);
            this.btnShowImpositionForm.Name = "btnShowImpositionForm";
            this.btnShowImpositionForm.Size = new System.Drawing.Size(68, 47);
            this.btnShowImpositionForm.TabIndex = 8;
            this.btnShowImpositionForm.Text = "<-";
            this.btnShowImpositionForm.UseVisualStyleBackColor = true;
            this.btnShowImpositionForm.Click += new System.EventHandler(this.btnShowImpositionForm_Click);
            // 
            // tabSystemVariables
            // 
            this.tabSystemVariables.Controls.Add(this.tabBasicVariables);
            this.tabSystemVariables.Controls.Add(this.tabSheetSize);
            this.tabSystemVariables.Controls.Add(this.tabCutOff);
            this.tabSystemVariables.Controls.Add(this.tabRollSize);
            this.tabSystemVariables.Controls.Add(this.tabPrintingStyles);
            this.tabSystemVariables.Location = new System.Drawing.Point(85, 12);
            this.tabSystemVariables.Name = "tabSystemVariables";
            this.tabSystemVariables.SelectedIndex = 0;
            this.tabSystemVariables.Size = new System.Drawing.Size(357, 228);
            this.tabSystemVariables.TabIndex = 1;
            // 
            // tabBasicVariables
            // 
            this.tabBasicVariables.Controls.Add(this.lblBindingLip);
            this.tabBasicVariables.Controls.Add(this.label1);
            this.tabBasicVariables.Controls.Add(this.lblFootTrim);
            this.tabBasicVariables.Controls.Add(this.btnSubmit);
            this.tabBasicVariables.Controls.Add(this.nudBindingLip);
            this.tabBasicVariables.Controls.Add(this.nudFootTrim);
            this.tabBasicVariables.Controls.Add(this.nudHeadTrim);
            this.tabBasicVariables.Location = new System.Drawing.Point(4, 22);
            this.tabBasicVariables.Name = "tabBasicVariables";
            this.tabBasicVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabBasicVariables.Size = new System.Drawing.Size(349, 202);
            this.tabBasicVariables.TabIndex = 0;
            this.tabBasicVariables.Text = "Basic Variables";
            this.tabBasicVariables.UseVisualStyleBackColor = true;
            // 
            // tabSheetSize
            // 
            this.tabSheetSize.Controls.Add(this.label13);
            this.tabSheetSize.Controls.Add(this.label6);
            this.tabSheetSize.Controls.Add(this.nudSheetSizeAcross);
            this.tabSheetSize.Controls.Add(this.label5);
            this.tabSheetSize.Controls.Add(this.libSheetSizeValues);
            this.tabSheetSize.Controls.Add(this.label4);
            this.tabSheetSize.Controls.Add(this.btnRemoveSheetSizeValues);
            this.tabSheetSize.Controls.Add(this.btnAddSheetSize);
            this.tabSheetSize.Controls.Add(this.nudSheetSizeAround);
            this.tabSheetSize.Location = new System.Drawing.Point(4, 22);
            this.tabSheetSize.Name = "tabSheetSize";
            this.tabSheetSize.Size = new System.Drawing.Size(349, 202);
            this.tabSheetSize.TabIndex = 3;
            this.tabSheetSize.Text = "SheetSize";
            this.tabSheetSize.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Current Values:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Across:";
            // 
            // nudSheetSizeAcross
            // 
            this.nudSheetSizeAcross.DecimalPlaces = 3;
            this.nudSheetSizeAcross.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudSheetSizeAcross.Location = new System.Drawing.Point(164, 86);
            this.nudSheetSizeAcross.Name = "nudSheetSizeAcross";
            this.nudSheetSizeAcross.Size = new System.Drawing.Size(51, 20);
            this.nudSheetSizeAcross.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Around:";
            // 
            // libSheetSizeValues
            // 
            this.libSheetSizeValues.FormattingEnabled = true;
            this.libSheetSizeValues.Location = new System.Drawing.Point(92, 139);
            this.libSheetSizeValues.Name = "libSheetSizeValues";
            this.libSheetSizeValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.libSheetSizeValues.Size = new System.Drawing.Size(95, 56);
            this.libSheetSizeValues.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 56);
            this.label4.TabIndex = 8;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // btnRemoveSheetSizeValues
            // 
            this.btnRemoveSheetSizeValues.Location = new System.Drawing.Point(193, 139);
            this.btnRemoveSheetSizeValues.Name = "btnRemoveSheetSizeValues";
            this.btnRemoveSheetSizeValues.Size = new System.Drawing.Size(124, 23);
            this.btnRemoveSheetSizeValues.TabIndex = 7;
            this.btnRemoveSheetSizeValues.Text = "Remove value(s)";
            this.btnRemoveSheetSizeValues.UseVisualStyleBackColor = true;
            this.btnRemoveSheetSizeValues.Click += new System.EventHandler(this.btnRemoveSheetSizeValues_Click);
            // 
            // btnAddSheetSize
            // 
            this.btnAddSheetSize.Location = new System.Drawing.Point(225, 85);
            this.btnAddSheetSize.Name = "btnAddSheetSize";
            this.btnAddSheetSize.Size = new System.Drawing.Size(92, 23);
            this.btnAddSheetSize.TabIndex = 4;
            this.btnAddSheetSize.Text = "Add sheet size";
            this.btnAddSheetSize.UseVisualStyleBackColor = true;
            this.btnAddSheetSize.Click += new System.EventHandler(this.btnAddSheetSize_Click);
            // 
            // nudSheetSizeAround
            // 
            this.nudSheetSizeAround.DecimalPlaces = 3;
            this.nudSheetSizeAround.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudSheetSizeAround.Location = new System.Drawing.Point(57, 86);
            this.nudSheetSizeAround.Name = "nudSheetSizeAround";
            this.nudSheetSizeAround.Size = new System.Drawing.Size(51, 20);
            this.nudSheetSizeAround.TabIndex = 1;
            // 
            // tabCutOff
            // 
            this.tabCutOff.Controls.Add(this.libCutOffValues);
            this.tabCutOff.Controls.Add(this.label2);
            this.tabCutOff.Controls.Add(this.btnRemoveCutOffValues);
            this.tabCutOff.Controls.Add(this.btnAddCutOffValue);
            this.tabCutOff.Controls.Add(this.nudCutOff);
            this.tabCutOff.Location = new System.Drawing.Point(4, 22);
            this.tabCutOff.Name = "tabCutOff";
            this.tabCutOff.Padding = new System.Windows.Forms.Padding(3);
            this.tabCutOff.Size = new System.Drawing.Size(349, 202);
            this.tabCutOff.TabIndex = 1;
            this.tabCutOff.Text = "Cut Off";
            this.tabCutOff.UseVisualStyleBackColor = true;
            // 
            // libCutOffValues
            // 
            this.libCutOffValues.FormattingEnabled = true;
            this.libCutOffValues.Location = new System.Drawing.Point(20, 130);
            this.libCutOffValues.Name = "libCutOffValues";
            this.libCutOffValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.libCutOffValues.Size = new System.Drawing.Size(84, 69);
            this.libCutOffValues.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(340, 56);
            this.label2.TabIndex = 4;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // btnRemoveCutOffValues
            // 
            this.btnRemoveCutOffValues.Location = new System.Drawing.Point(110, 130);
            this.btnRemoveCutOffValues.Name = "btnRemoveCutOffValues";
            this.btnRemoveCutOffValues.Size = new System.Drawing.Size(107, 23);
            this.btnRemoveCutOffValues.TabIndex = 3;
            this.btnRemoveCutOffValues.Text = "Remove value(s)";
            this.btnRemoveCutOffValues.UseVisualStyleBackColor = true;
            this.btnRemoveCutOffValues.Click += new System.EventHandler(this.btnRemoveCutOffValues_Click);
            // 
            // btnAddCutOffValue
            // 
            this.btnAddCutOffValue.Location = new System.Drawing.Point(110, 81);
            this.btnAddCutOffValue.Name = "btnAddCutOffValue";
            this.btnAddCutOffValue.Size = new System.Drawing.Size(75, 23);
            this.btnAddCutOffValue.TabIndex = 1;
            this.btnAddCutOffValue.Text = "Add cut off";
            this.btnAddCutOffValue.UseVisualStyleBackColor = true;
            this.btnAddCutOffValue.Click += new System.EventHandler(this.btnAddCutOffValue_Click);
            // 
            // nudCutOff
            // 
            this.nudCutOff.DecimalPlaces = 3;
            this.nudCutOff.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudCutOff.Location = new System.Drawing.Point(20, 84);
            this.nudCutOff.Name = "nudCutOff";
            this.nudCutOff.Size = new System.Drawing.Size(51, 20);
            this.nudCutOff.TabIndex = 0;
            // 
            // tabRollSize
            // 
            this.tabRollSize.Controls.Add(this.libRollSizeValues);
            this.tabRollSize.Controls.Add(this.label3);
            this.tabRollSize.Controls.Add(this.btnRemoveRollSizeValues);
            this.tabRollSize.Controls.Add(this.btnAddRollSize);
            this.tabRollSize.Controls.Add(this.nudRollSize);
            this.tabRollSize.Location = new System.Drawing.Point(4, 22);
            this.tabRollSize.Name = "tabRollSize";
            this.tabRollSize.Size = new System.Drawing.Size(349, 202);
            this.tabRollSize.TabIndex = 2;
            this.tabRollSize.Text = "Roll Size";
            this.tabRollSize.UseVisualStyleBackColor = true;
            // 
            // libRollSizeValues
            // 
            this.libRollSizeValues.FormattingEnabled = true;
            this.libRollSizeValues.Location = new System.Drawing.Point(25, 129);
            this.libRollSizeValues.Name = "libRollSizeValues";
            this.libRollSizeValues.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.libRollSizeValues.Size = new System.Drawing.Size(80, 69);
            this.libRollSizeValues.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(340, 56);
            this.label3.TabIndex = 86;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // btnRemoveRollSizeValues
            // 
            this.btnRemoveRollSizeValues.Location = new System.Drawing.Point(111, 129);
            this.btnRemoveRollSizeValues.Name = "btnRemoveRollSizeValues";
            this.btnRemoveRollSizeValues.Size = new System.Drawing.Size(107, 23);
            this.btnRemoveRollSizeValues.TabIndex = 3;
            this.btnRemoveRollSizeValues.Text = "Remove value(s)";
            this.btnRemoveRollSizeValues.UseVisualStyleBackColor = true;
            this.btnRemoveRollSizeValues.Click += new System.EventHandler(this.btnRemoveRollSizeValues_Click);
            // 
            // btnAddRollSize
            // 
            this.btnAddRollSize.Location = new System.Drawing.Point(111, 80);
            this.btnAddRollSize.Name = "btnAddRollSize";
            this.btnAddRollSize.Size = new System.Drawing.Size(75, 23);
            this.btnAddRollSize.TabIndex = 1;
            this.btnAddRollSize.Text = "Add roll size";
            this.btnAddRollSize.UseVisualStyleBackColor = true;
            this.btnAddRollSize.Click += new System.EventHandler(this.btnAddRollSize_Click);
            // 
            // nudRollSize
            // 
            this.nudRollSize.DecimalPlaces = 3;
            this.nudRollSize.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudRollSize.Location = new System.Drawing.Point(25, 83);
            this.nudRollSize.Name = "nudRollSize";
            this.nudRollSize.Size = new System.Drawing.Size(51, 20);
            this.nudRollSize.TabIndex = 0;
            // 
            // tabPrintingStyles
            // 
            this.tabPrintingStyles.Controls.Add(this.btnSubmitPrintingStyles);
            this.tabPrintingStyles.Controls.Add(this.label12);
            this.tabPrintingStyles.Controls.Add(this.label11);
            this.tabPrintingStyles.Controls.Add(this.label10);
            this.tabPrintingStyles.Controls.Add(this.nudBleeds);
            this.tabPrintingStyles.Controls.Add(this.label8);
            this.tabPrintingStyles.Controls.Add(this.label9);
            this.tabPrintingStyles.Controls.Add(this.nudGripper);
            this.tabPrintingStyles.Controls.Add(this.nudSideMargin);
            this.tabPrintingStyles.Controls.Add(this.nudTailMargin);
            this.tabPrintingStyles.Controls.Add(this.label7);
            this.tabPrintingStyles.Controls.Add(this.cboPrintingStyle);
            this.tabPrintingStyles.Location = new System.Drawing.Point(4, 22);
            this.tabPrintingStyles.Name = "tabPrintingStyles";
            this.tabPrintingStyles.Size = new System.Drawing.Size(349, 202);
            this.tabPrintingStyles.TabIndex = 4;
            this.tabPrintingStyles.Text = "Printing Styles";
            this.tabPrintingStyles.UseVisualStyleBackColor = true;
            // 
            // btnSubmitPrintingStyles
            // 
            this.btnSubmitPrintingStyles.Location = new System.Drawing.Point(248, 93);
            this.btnSubmitPrintingStyles.Name = "btnSubmitPrintingStyles";
            this.btnSubmitPrintingStyles.Size = new System.Drawing.Size(83, 35);
            this.btnSubmitPrintingStyles.TabIndex = 10;
            this.btnSubmitPrintingStyles.Text = "Submit";
            this.btnSubmitPrintingStyles.UseVisualStyleBackColor = true;
            this.btnSubmitPrintingStyles.Click += new System.EventHandler(this.btnSubmitPrintingStyles_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(5, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(341, 29);
            this.label12.TabIndex = 15;
            this.label12.Text = "Please choose a printing style and then enter the values you would. Press submit " +
    "to save this information.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Gripper:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Tail Margin:";
            // 
            // nudBleeds
            // 
            this.nudBleeds.DecimalPlaces = 4;
            this.nudBleeds.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudBleeds.Location = new System.Drawing.Point(106, 154);
            this.nudBleeds.Name = "nudBleeds";
            this.nudBleeds.Size = new System.Drawing.Size(120, 20);
            this.nudBleeds.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Bleeds:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Side Margin:";
            // 
            // nudGripper
            // 
            this.nudGripper.DecimalPlaces = 4;
            this.nudGripper.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudGripper.Location = new System.Drawing.Point(107, 77);
            this.nudGripper.Name = "nudGripper";
            this.nudGripper.Size = new System.Drawing.Size(120, 20);
            this.nudGripper.TabIndex = 3;
            // 
            // nudSideMargin
            // 
            this.nudSideMargin.DecimalPlaces = 4;
            this.nudSideMargin.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudSideMargin.Location = new System.Drawing.Point(107, 128);
            this.nudSideMargin.Name = "nudSideMargin";
            this.nudSideMargin.Size = new System.Drawing.Size(120, 20);
            this.nudSideMargin.TabIndex = 7;
            // 
            // nudTailMargin
            // 
            this.nudTailMargin.DecimalPlaces = 4;
            this.nudTailMargin.Increment = new decimal(new int[] {
            125,
            0,
            0,
            196608});
            this.nudTailMargin.Location = new System.Drawing.Point(107, 102);
            this.nudTailMargin.Name = "nudTailMargin";
            this.nudTailMargin.Size = new System.Drawing.Size(120, 20);
            this.nudTailMargin.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Style:";
            // 
            // cboPrintingStyle
            // 
            this.cboPrintingStyle.FormattingEnabled = true;
            this.cboPrintingStyle.Location = new System.Drawing.Point(106, 50);
            this.cboPrintingStyle.Name = "cboPrintingStyle";
            this.cboPrintingStyle.Size = new System.Drawing.Size(121, 21);
            this.cboPrintingStyle.TabIndex = 1;
            this.cboPrintingStyle.SelectedIndexChanged += new System.EventHandler(this.cboPrintingStyle_SelectedIndexChanged);
            // 
            // SystemVariablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 345);
            this.Controls.Add(this.tabSystemVariables);
            this.Controls.Add(this.btnShowImpositionForm);
            this.Controls.Add(this.pnlErrors);
            this.Name = "SystemVariablesForm";
            this.Text = "Set System Variables";
            this.Load += new System.EventHandler(this.SetSystemVariables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFootTrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeadTrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBindingLip)).EndInit();
            this.pnlErrors.ResumeLayout(false);
            this.pnlErrors.PerformLayout();
            this.tabSystemVariables.ResumeLayout(false);
            this.tabBasicVariables.ResumeLayout(false);
            this.tabBasicVariables.PerformLayout();
            this.tabSheetSize.ResumeLayout(false);
            this.tabSheetSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSheetSizeAcross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSheetSizeAround)).EndInit();
            this.tabCutOff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCutOff)).EndInit();
            this.tabRollSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRollSize)).EndInit();
            this.tabPrintingStyles.ResumeLayout(false);
            this.tabPrintingStyles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBleeds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGripper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSideMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTailMargin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBindingLip;
        private System.Windows.Forms.Label lblFootTrim;
        private System.Windows.Forms.NumericUpDown nudFootTrim;
        private System.Windows.Forms.NumericUpDown nudHeadTrim;
        private System.Windows.Forms.NumericUpDown nudBindingLip;
        private System.Windows.Forms.Panel pnlErrors;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.Label lblPanelTitle;
        private System.Windows.Forms.Button btnShowImpositionForm;
        private System.Windows.Forms.TabControl tabSystemVariables;
        private System.Windows.Forms.TabPage tabBasicVariables;
        private System.Windows.Forms.TabPage tabRollSize;
        private System.Windows.Forms.ListBox libRollSizeValues;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemoveRollSizeValues;
        private System.Windows.Forms.Button btnAddRollSize;
        private System.Windows.Forms.NumericUpDown nudRollSize;
        private System.Windows.Forms.TabPage tabSheetSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox libSheetSizeValues;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRemoveSheetSizeValues;
        private System.Windows.Forms.Button btnAddSheetSize;
        private System.Windows.Forms.NumericUpDown nudSheetSizeAround;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudSheetSizeAcross;
        private System.Windows.Forms.TabPage tabPrintingStyles;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudBleeds;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudGripper;
        private System.Windows.Forms.NumericUpDown nudSideMargin;
        private System.Windows.Forms.NumericUpDown nudTailMargin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPrintingStyle;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabCutOff;
        private System.Windows.Forms.ListBox libCutOffValues;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemoveCutOffValues;
        private System.Windows.Forms.Button btnAddCutOffValue;
        private System.Windows.Forms.NumericUpDown nudCutOff;
        private System.Windows.Forms.Button btnSubmitPrintingStyles;
    }
}