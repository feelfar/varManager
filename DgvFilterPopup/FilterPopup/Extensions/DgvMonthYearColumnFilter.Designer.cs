namespace DgvFilterPopup {

    /// <summary>
    /// An extended <i>column filter</i> implementation allowing filters on date, using months and years.
    /// </summary>
    partial class DgvMonthYearColumnFilter {
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
            this.ComboBoxYear = new System.Windows.Forms.ComboBox();
            this.ComboBoxMonth = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxYear
            // 
            this.ComboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxYear.FormattingEnabled = true;
            this.ComboBoxYear.Location = new System.Drawing.Point(102, 3);
            this.ComboBoxYear.Name = "ComboBoxYear";
            this.ComboBoxYear.Size = new System.Drawing.Size(78, 21);
            this.ComboBoxYear.TabIndex = 3;
            // 
            // comboBoxMonth
            // 
            this.ComboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMonth.FormattingEnabled = true;
            this.ComboBoxMonth.Location = new System.Drawing.Point(3, 3);
            this.ComboBoxMonth.MaxDropDownItems = 13;
            this.ComboBoxMonth.Name = "ComboBoxMonth";
            this.ComboBoxMonth.Size = new System.Drawing.Size(93, 21);
            this.ComboBoxMonth.TabIndex = 2;
            // 
            // DgvMonthYearColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ComboBoxYear);
            this.Controls.Add(this.ComboBoxMonth);
            this.Name = "DgvMonthYearColumnFilter";
            this.Size = new System.Drawing.Size(188, 27);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
