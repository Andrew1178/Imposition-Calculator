namespace PrintingApp.Forms {
    partial class PrintingDesignForm {
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
            this.lblScale = new System.Windows.Forms.Label();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.lblErrors = new System.Windows.Forms.Label();
            this.btnShowImpositionCalculator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(56, 9);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(40, 13);
            this.lblScale.TabIndex = 0;
            this.lblScale.Text = "Scale: ";
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(96, 180);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(0, 13);
            this.lblErrorMessage.TabIndex = 1;
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Location = new System.Drawing.Point(12, 30);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(0, 13);
            this.lblErrors.TabIndex = 9;
            // 
            // btnShowImpositionCalculator
            // 
            this.btnShowImpositionCalculator.Location = new System.Drawing.Point(12, 4);
            this.btnShowImpositionCalculator.Name = "btnShowImpositionCalculator";
            this.btnShowImpositionCalculator.Size = new System.Drawing.Size(32, 23);
            this.btnShowImpositionCalculator.TabIndex = 10;
            this.btnShowImpositionCalculator.Text = "<-";
            this.btnShowImpositionCalculator.UseVisualStyleBackColor = true;
            this.btnShowImpositionCalculator.Click += new System.EventHandler(this.btnShowImpositionCalculator_Click);
            // 
            // PrintingDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnShowImpositionCalculator);
            this.Controls.Add(this.lblErrors);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.lblScale);
            this.DoubleBuffered = true;
            this.Name = "PrintingDesignForm";
            this.Text = "PrintingDesign";
            this.Load += new System.EventHandler(this.PrintingDesignForm_Load);
            this.Resize += new System.EventHandler(this.PrintingDesignForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.Button btnShowImpositionCalculator;
    }
}