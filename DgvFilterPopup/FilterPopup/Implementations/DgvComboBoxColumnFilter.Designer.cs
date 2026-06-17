namespace DgvFilterPopup {
    partial class DgvComboBoxColumnFilter {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.ComboBoxOperator = new System.Windows.Forms.ComboBox();
            this.ComboBoxValue = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxOperator
            // 
            this.ComboBoxOperator.FormattingEnabled = true;
            this.ComboBoxOperator.Location = new System.Drawing.Point(3, 3);
            this.ComboBoxOperator.Name = "ComboBoxOperator";
            this.ComboBoxOperator.Size = new System.Drawing.Size(49, 21);
            this.ComboBoxOperator.TabIndex = 0;
            // 
            // comboBoxValue
            // 
            this.ComboBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxValue.FormattingEnabled = true;
            this.ComboBoxValue.Location = new System.Drawing.Point(58, 3);
            this.ComboBoxValue.Name = "ComboBoxValue";
            this.ComboBoxValue.Size = new System.Drawing.Size(215, 21);
            this.ComboBoxValue.TabIndex = 1;
            // 
            // DgvComboBoxColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ComboBoxValue);
            this.Controls.Add(this.ComboBoxOperator);
            this.Name = "DgvComboBoxColumnFilter";
            this.Size = new System.Drawing.Size(276, 30);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
