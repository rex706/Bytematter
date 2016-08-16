namespace FormatToHex
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadButton = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.FormatTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(25, 53);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(145, 78);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.ForeColor = System.Drawing.Color.Green;
            this.InfoLabel.Location = new System.Drawing.Point(30, 9);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(135, 22);
            this.InfoLabel.TabIndex = 1;
            this.InfoLabel.Text = "Ready";
            this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormatTypeCheckBox
            // 
            this.FormatTypeCheckBox.AutoSize = true;
            this.FormatTypeCheckBox.Checked = true;
            this.FormatTypeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FormatTypeCheckBox.Location = new System.Drawing.Point(60, 30);
            this.FormatTypeCheckBox.Name = "FormatTypeCheckBox";
            this.FormatTypeCheckBox.Size = new System.Drawing.Size(75, 17);
            this.FormatTypeCheckBox.TabIndex = 2;
            this.FormatTypeCheckBox.Text = "0x0 format";
            this.FormatTypeCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 144);
            this.Controls.Add(this.FormatTypeCheckBox);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.LoadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToHex";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.CheckBox FormatTypeCheckBox;
    }
}

