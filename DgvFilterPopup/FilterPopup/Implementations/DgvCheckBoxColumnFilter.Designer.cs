namespace DgvFilterPopup {
    partial class DgvCheckBoxColumnFilter {
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
            this.CheckBoxValue = new System.Windows.Forms.CheckBox();
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
            // checkBoxValue
            // 
            this.CheckBoxValue.AutoSize = true;
            this.CheckBoxValue.Location = new System.Drawing.Point(67, 7);
            this.CheckBoxValue.Name = "CheckBoxValue";
            this.CheckBoxValue.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxValue.TabIndex = 1;
            this.CheckBoxValue.UseVisualStyleBackColor = true;
            // 
            // DgvCheckBoxColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.CheckBoxValue);
            this.Controls.Add(this.ComboBoxOperator);
            this.Name = "DgvCheckBoxColumnFilter";
            this.Size = new System.Drawing.Size(98, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
