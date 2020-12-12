namespace Biodex_Client
{
    partial class formMicrocontrollerStatus
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
            this.lblMicrocontrollerStatus = new System.Windows.Forms.Label();
            this.tbConnection = new System.Windows.Forms.TextBox();
            this.tlpMicrocontrollerStatus = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMicrocontrollerStatusAllControls = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMicroControllerStatusControls = new System.Windows.Forms.TableLayoutPanel();
            this.cbxSerialPort = new System.Windows.Forms.ComboBox();
            this.tlpMicrocontrollerStatus.SuspendLayout();
            this.tlpMicrocontrollerStatusAllControls.SuspendLayout();
            this.tlpMicroControllerStatusControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMicrocontrollerStatus
            // 
            this.lblMicrocontrollerStatus.AutoSize = true;
            this.lblMicrocontrollerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMicrocontrollerStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.lblMicrocontrollerStatus.Location = new System.Drawing.Point(3, 0);
            this.lblMicrocontrollerStatus.Name = "lblMicrocontrollerStatus";
            this.lblMicrocontrollerStatus.Size = new System.Drawing.Size(304, 75);
            this.lblMicrocontrollerStatus.TabIndex = 0;
            this.lblMicrocontrollerStatus.Text = "Microconroller Status:";
            this.lblMicrocontrollerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbConnection
            // 
            this.tbConnection.BackColor = System.Drawing.Color.Red;
            this.tbConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConnection.Enabled = false;
            this.tbConnection.Location = new System.Drawing.Point(3, 3);
            this.tbConnection.Name = "tbConnection";
            this.tbConnection.Size = new System.Drawing.Size(44, 32);
            this.tbConnection.TabIndex = 4;
            // 
            // tlpMicrocontrollerStatus
            // 
            this.tlpMicrocontrollerStatus.ColumnCount = 3;
            this.tlpMicrocontrollerStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMicrocontrollerStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMicrocontrollerStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMicrocontrollerStatus.Controls.Add(this.tlpMicrocontrollerStatusAllControls, 1, 1);
            this.tlpMicrocontrollerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMicrocontrollerStatus.Location = new System.Drawing.Point(0, 0);
            this.tlpMicrocontrollerStatus.Name = "tlpMicrocontrollerStatus";
            this.tlpMicrocontrollerStatus.RowCount = 4;
            this.tlpMicrocontrollerStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMicrocontrollerStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMicrocontrollerStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMicrocontrollerStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMicrocontrollerStatus.Size = new System.Drawing.Size(950, 625);
            this.tlpMicrocontrollerStatus.TabIndex = 5;
            // 
            // tlpMicrocontrollerStatusAllControls
            // 
            this.tlpMicrocontrollerStatusAllControls.ColumnCount = 1;
            this.tlpMicrocontrollerStatusAllControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMicrocontrollerStatusAllControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMicrocontrollerStatusAllControls.Controls.Add(this.lblMicrocontrollerStatus, 0, 0);
            this.tlpMicrocontrollerStatusAllControls.Controls.Add(this.tlpMicroControllerStatusControls, 0, 1);
            this.tlpMicrocontrollerStatusAllControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMicrocontrollerStatusAllControls.Location = new System.Drawing.Point(319, 159);
            this.tlpMicrocontrollerStatusAllControls.Name = "tlpMicrocontrollerStatusAllControls";
            this.tlpMicrocontrollerStatusAllControls.RowCount = 2;
            this.tlpMicrocontrollerStatusAllControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMicrocontrollerStatusAllControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMicrocontrollerStatusAllControls.Size = new System.Drawing.Size(310, 150);
            this.tlpMicrocontrollerStatusAllControls.TabIndex = 5;
            // 
            // tlpMicroControllerStatusControls
            // 
            this.tlpMicroControllerStatusControls.ColumnCount = 2;
            this.tlpMicroControllerStatusControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMicroControllerStatusControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMicroControllerStatusControls.Controls.Add(this.cbxSerialPort, 1, 0);
            this.tlpMicroControllerStatusControls.Controls.Add(this.tbConnection, 0, 0);
            this.tlpMicroControllerStatusControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMicroControllerStatusControls.Location = new System.Drawing.Point(3, 78);
            this.tlpMicroControllerStatusControls.Name = "tlpMicroControllerStatusControls";
            this.tlpMicroControllerStatusControls.RowCount = 1;
            this.tlpMicroControllerStatusControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMicroControllerStatusControls.Size = new System.Drawing.Size(304, 69);
            this.tlpMicroControllerStatusControls.TabIndex = 0;
            // 
            // cbxSerialPort
            // 
            this.cbxSerialPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.cbxSerialPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSerialPort.ForeColor = System.Drawing.Color.DarkGray;
            this.cbxSerialPort.FormattingEnabled = true;
            this.cbxSerialPort.Location = new System.Drawing.Point(53, 3);
            this.cbxSerialPort.Name = "cbxSerialPort";
            this.cbxSerialPort.Size = new System.Drawing.Size(248, 32);
            this.cbxSerialPort.TabIndex = 6;
            // 
            // formMicrocontrollerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(950, 625);
            this.Controls.Add(this.tlpMicrocontrollerStatus);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "formMicrocontrollerStatus";
            this.Text = "formMicrocontrollerStatus";
            this.tlpMicrocontrollerStatus.ResumeLayout(false);
            this.tlpMicrocontrollerStatusAllControls.ResumeLayout(false);
            this.tlpMicrocontrollerStatusAllControls.PerformLayout();
            this.tlpMicroControllerStatusControls.ResumeLayout(false);
            this.tlpMicroControllerStatusControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMicrocontrollerStatus;
        private System.Windows.Forms.TextBox tbConnection;
        private System.Windows.Forms.TableLayoutPanel tlpMicrocontrollerStatus;
        private System.Windows.Forms.TableLayoutPanel tlpMicrocontrollerStatusAllControls;
        private System.Windows.Forms.TableLayoutPanel tlpMicroControllerStatusControls;
        private System.Windows.Forms.ComboBox cbxSerialPort;
    }
}