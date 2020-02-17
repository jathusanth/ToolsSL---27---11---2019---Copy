namespace ToolsSL
{
    partial class frmSettings
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
            this.btnChangeColour = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChangeColour
            // 
            this.btnChangeColour.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnChangeColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeColour.Location = new System.Drawing.Point(12, 12);
            this.btnChangeColour.Name = "btnChangeColour";
            this.btnChangeColour.Size = new System.Drawing.Size(173, 23);
            this.btnChangeColour.TabIndex = 6;
            this.btnChangeColour.Text = "Change Background Colour";
            this.btnChangeColour.UseVisualStyleBackColor = false;
            this.btnChangeColour.Click += new System.EventHandler(this.btnChangeColour_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(12, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(173, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 95);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnChangeColour);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Activated += new System.EventHandler(this.frmSettings_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChangeColour;
        private System.Windows.Forms.Button btnClose;
    }
}