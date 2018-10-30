namespace Jordan_van_Zyl___18013347___GADE_POE
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            this.lblMap = new System.Windows.Forms.Label();
            this.GameTime = new System.Windows.Forms.Timer(this.components);
            this.rchDisplay = new System.Windows.Forms.RichTextBox();
            this.cmbUnits = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMap.Font = new System.Drawing.Font("Courier New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMap.Location = new System.Drawing.Point(45, 41);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(2, 33);
            this.lblMap.TabIndex = 0;
            // 
            // GameTime
            // 
            this.GameTime.Enabled = true;
            this.GameTime.Interval = 1000;
            this.GameTime.Tick += new System.EventHandler(this.GameTime_Tick);
            // 
            // rchDisplay
            // 
            this.rchDisplay.Location = new System.Drawing.Point(566, 41);
            this.rchDisplay.Name = "rchDisplay";
            this.rchDisplay.Size = new System.Drawing.Size(280, 233);
            this.rchDisplay.TabIndex = 1;
            this.rchDisplay.Text = "";
            // 
            // cmbUnits
            // 
            this.cmbUnits.FormattingEnabled = true;
            this.cmbUnits.Location = new System.Drawing.Point(566, 311);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Size = new System.Drawing.Size(824, 24);
            this.cmbUnits.TabIndex = 2;
            this.cmbUnits.SelectedIndexChanged += new System.EventHandler(this.cmbUnits_SelectedIndexChanged);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 788);
            this.Controls.Add(this.cmbUnits);
            this.Controls.Add(this.rchDisplay);
            this.Controls.Add(this.lblMap);
            this.Name = "Game";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.Timer GameTime;
        private System.Windows.Forms.RichTextBox rchDisplay;
        private System.Windows.Forms.ComboBox cmbUnits;
    }
}

