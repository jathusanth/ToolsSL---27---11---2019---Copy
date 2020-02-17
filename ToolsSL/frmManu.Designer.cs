namespace ToolsSL
{
    partial class frmMenu
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
            this.btnTools = new System.Windows.Forms.Button();
            this.btnClinet = new System.Windows.Forms.Button();
            this.btnHire = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnBrand = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTools
            // 
            this.btnTools.Location = new System.Drawing.Point(12, 12);
            this.btnTools.Name = "btnTools";
            this.btnTools.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnTools.Size = new System.Drawing.Size(197, 23);
            this.btnTools.TabIndex = 0;
            this.btnTools.Text = "Tools";
            this.btnTools.UseVisualStyleBackColor = true;
            this.btnTools.Click += new System.EventHandler(this.btnTools_Click);
            // 
            // btnClinet
            // 
            this.btnClinet.Location = new System.Drawing.Point(12, 41);
            this.btnClinet.Name = "btnClinet";
            this.btnClinet.Size = new System.Drawing.Size(197, 23);
            this.btnClinet.TabIndex = 1;
            this.btnClinet.Text = "Clients";
            this.btnClinet.UseVisualStyleBackColor = true;
            this.btnClinet.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // btnHire
            // 
            this.btnHire.Location = new System.Drawing.Point(12, 70);
            this.btnHire.Name = "btnHire";
            this.btnHire.Size = new System.Drawing.Size(197, 23);
            this.btnHire.TabIndex = 2;
            this.btnHire.Text = "Hire";
            this.btnHire.UseVisualStyleBackColor = true;
            this.btnHire.Click += new System.EventHandler(this.btnHire_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(12, 157);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(197, 23);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Tool Active Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 99);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(197, 23);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(12, 285);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(197, 26);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnBrand
            // 
            this.btnBrand.Location = new System.Drawing.Point(12, 128);
            this.btnBrand.Name = "btnBrand";
            this.btnBrand.Size = new System.Drawing.Size(197, 23);
            this.btnBrand.TabIndex = 6;
            this.btnBrand.Text = "Brand";
            this.btnBrand.UseVisualStyleBackColor = true;
            this.btnBrand.Click += new System.EventHandler(this.btnBrand_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Tool Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(197, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Tool Retired Report";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 405);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBrand);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnHire);
            this.Controls.Add(this.btnClinet);
            this.Controls.Add(this.btnTools);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMenu";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.Activated += new System.EventHandler(this.frmMenu_Activated);
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTools;
        private System.Windows.Forms.Button btnClinet;
        private System.Windows.Forms.Button btnHire;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnBrand;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}