namespace Biodex_Client
{
    partial class Biodex_Client
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.btnMicrocontrollerStatus = new System.Windows.Forms.Button();
            this.btnMeasurementProperties = new System.Windows.Forms.Button();
            this.btnGraphs = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panelChildForms = new System.Windows.Forms.Panel();
            this.lblBiodex1 = new System.Windows.Forms.Label();
            this.lblBiodex2 = new System.Windows.Forms.Label();
            this.panelSideMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.panelChildForms.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.AutoScroll = true;
            this.panelSideMenu.BackColor = System.Drawing.Color.Black;
            this.panelSideMenu.Controls.Add(this.btnMicrocontrollerStatus);
            this.panelSideMenu.Controls.Add(this.btnMeasurementProperties);
            this.panelSideMenu.Controls.Add(this.btnGraphs);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(250, 650);
            this.panelSideMenu.TabIndex = 0;
            // 
            // btnMicrocontrollerStatus
            // 
            this.btnMicrocontrollerStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnMicrocontrollerStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMicrocontrollerStatus.FlatAppearance.BorderSize = 0;
            this.btnMicrocontrollerStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMicrocontrollerStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMicrocontrollerStatus.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMicrocontrollerStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.btnMicrocontrollerStatus.Location = new System.Drawing.Point(0, 190);
            this.btnMicrocontrollerStatus.Name = "btnMicrocontrollerStatus";
            this.btnMicrocontrollerStatus.Size = new System.Drawing.Size(250, 45);
            this.btnMicrocontrollerStatus.TabIndex = 4;
            this.btnMicrocontrollerStatus.Text = "Microcontroller Status";
            this.btnMicrocontrollerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMicrocontrollerStatus.UseVisualStyleBackColor = false;
            this.btnMicrocontrollerStatus.Click += new System.EventHandler(this.btnMicrocontrollerStatus_Click);
            // 
            // btnMeasurementProperties
            // 
            this.btnMeasurementProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnMeasurementProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMeasurementProperties.FlatAppearance.BorderSize = 0;
            this.btnMeasurementProperties.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMeasurementProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeasurementProperties.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeasurementProperties.ForeColor = System.Drawing.Color.DarkGray;
            this.btnMeasurementProperties.Location = new System.Drawing.Point(0, 145);
            this.btnMeasurementProperties.Name = "btnMeasurementProperties";
            this.btnMeasurementProperties.Size = new System.Drawing.Size(250, 45);
            this.btnMeasurementProperties.TabIndex = 4;
            this.btnMeasurementProperties.Text = "Measurement Properties";
            this.btnMeasurementProperties.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMeasurementProperties.UseVisualStyleBackColor = false;
            this.btnMeasurementProperties.Click += new System.EventHandler(this.btnMeasurementProperties_Click);
            // 
            // btnGraphs
            // 
            this.btnGraphs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnGraphs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGraphs.FlatAppearance.BorderSize = 0;
            this.btnGraphs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGraphs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGraphs.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGraphs.ForeColor = System.Drawing.Color.DarkGray;
            this.btnGraphs.Location = new System.Drawing.Point(0, 100);
            this.btnGraphs.Name = "btnGraphs";
            this.btnGraphs.Size = new System.Drawing.Size(250, 45);
            this.btnGraphs.TabIndex = 4;
            this.btnGraphs.Text = "Graphs";
            this.btnGraphs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGraphs.UseVisualStyleBackColor = false;
            this.btnGraphs.Click += new System.EventHandler(this.btnGraphs_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.Black;
            this.panelLogo.Controls.Add(this.lblBiodex2);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 100);
            this.panelLogo.TabIndex = 1;
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.Black;
            this.panelControl.Controls.Add(this.btnMinimize);
            this.panelControl.Controls.Add(this.btnMaximize);
            this.panelControl.Controls.Add(this.btnClose);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(250, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(950, 25);
            this.panelControl.TabIndex = 1;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.DarkGray;
            this.btnMinimize.Image = global::Biodex_Client.Properties.Resources.mimimizeIcon;
            this.btnMinimize.Location = new System.Drawing.Point(875, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(25, 25);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaximize.ForeColor = System.Drawing.Color.DarkGray;
            this.btnMaximize.Image = global::Biodex_Client.Properties.Resources.maximizeIcon;
            this.btnMaximize.Location = new System.Drawing.Point(900, 0);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(25, 25);
            this.btnMaximize.TabIndex = 1;
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.DarkGray;
            this.btnClose.Image = global::Biodex_Client.Properties.Resources.closeIcon;
            this.btnClose.Location = new System.Drawing.Point(925, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panelControl;
            this.bunifuDragControl1.Vertical = true;
            // 
            // panelChildForms
            // 
            this.panelChildForms.Controls.Add(this.lblBiodex1);
            this.panelChildForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForms.Location = new System.Drawing.Point(250, 25);
            this.panelChildForms.Name = "panelChildForms";
            this.panelChildForms.Size = new System.Drawing.Size(950, 625);
            this.panelChildForms.TabIndex = 2;
            // 
            // lblBiodex1
            // 
            this.lblBiodex1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBiodex1.AutoSize = true;
            this.lblBiodex1.Font = new System.Drawing.Font("Baskerville Old Face", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBiodex1.ForeColor = System.Drawing.Color.DarkGray;
            this.lblBiodex1.Location = new System.Drawing.Point(299, 267);
            this.lblBiodex1.Name = "lblBiodex1";
            this.lblBiodex1.Size = new System.Drawing.Size(429, 110);
            this.lblBiodex1.TabIndex = 3;
            this.lblBiodex1.Text = "BIODEX";
            this.lblBiodex1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBiodex2
            // 
            this.lblBiodex2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBiodex2.AutoSize = true;
            this.lblBiodex2.Font = new System.Drawing.Font("Baskerville Old Face", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBiodex2.ForeColor = System.Drawing.Color.DarkGray;
            this.lblBiodex2.Location = new System.Drawing.Point(67, 40);
            this.lblBiodex2.Name = "lblBiodex2";
            this.lblBiodex2.Size = new System.Drawing.Size(143, 36);
            this.lblBiodex2.TabIndex = 4;
            this.lblBiodex2.Text = "BIODEX";
            this.lblBiodex2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Biodex_Client
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.panelChildForms);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panelSideMenu);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Biodex_Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biodex_Client";
            this.panelSideMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelControl.ResumeLayout(false);
            this.panelChildForms.ResumeLayout(false);
            this.panelChildForms.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnMicrocontrollerStatus;
        private System.Windows.Forms.Button btnMeasurementProperties;
        private System.Windows.Forms.Button btnGraphs;
        private System.Windows.Forms.Panel panelLogo;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Panel panelChildForms;
        private System.Windows.Forms.Label lblBiodex1;
        private System.Windows.Forms.Label lblBiodex2;
    }
}

