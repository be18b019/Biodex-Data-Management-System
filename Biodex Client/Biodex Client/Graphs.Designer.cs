namespace Biodex_Client
{
    partial class formGraphs
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
            this.tlpGraphsButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tlpGraphs = new System.Windows.Forms.TableLayoutPanel();
            this.chartAngle = new LiveCharts.WinForms.CartesianChart();
            this.chartAngularSpeed = new LiveCharts.WinForms.CartesianChart();
            this.chartTorque = new LiveCharts.WinForms.CartesianChart();
            this.tlpGraphsButtons.SuspendLayout();
            this.tlpGraphs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpGraphsButtons
            // 
            this.tlpGraphsButtons.ColumnCount = 3;
            this.tlpGraphsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpGraphsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpGraphsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpGraphsButtons.Controls.Add(this.btnSave, 0, 0);
            this.tlpGraphsButtons.Controls.Add(this.btnStop, 0, 0);
            this.tlpGraphsButtons.Controls.Add(this.btnStart, 0, 0);
            this.tlpGraphsButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpGraphsButtons.Location = new System.Drawing.Point(0, 554);
            this.tlpGraphsButtons.Name = "tlpGraphsButtons";
            this.tlpGraphsButtons.RowCount = 1;
            this.tlpGraphsButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGraphsButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGraphsButtons.Size = new System.Drawing.Size(950, 71);
            this.tlpGraphsButtons.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.DarkGray;
            this.btnSave.Location = new System.Drawing.Point(635, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(312, 65);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.DarkGray;
            this.btnStop.Location = new System.Drawing.Point(319, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(310, 65);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.DarkGray;
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(310, 65);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            // 
            // tlpGraphs
            // 
            this.tlpGraphs.ColumnCount = 1;
            this.tlpGraphs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGraphs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpGraphs.Controls.Add(this.chartAngle, 0, 2);
            this.tlpGraphs.Controls.Add(this.chartAngularSpeed, 0, 1);
            this.tlpGraphs.Controls.Add(this.chartTorque, 0, 0);
            this.tlpGraphs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGraphs.Location = new System.Drawing.Point(0, 0);
            this.tlpGraphs.Name = "tlpGraphs";
            this.tlpGraphs.RowCount = 3;
            this.tlpGraphs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpGraphs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpGraphs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpGraphs.Size = new System.Drawing.Size(950, 554);
            this.tlpGraphs.TabIndex = 2;
            // 
            // chartAngle
            // 
            this.chartAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAngle.Location = new System.Drawing.Point(3, 371);
            this.chartAngle.Name = "chartAngle";
            this.chartAngle.Size = new System.Drawing.Size(944, 180);
            this.chartAngle.TabIndex = 2;
            this.chartAngle.Text = "Angle";
            // 
            // chartAngularSpeed
            // 
            this.chartAngularSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAngularSpeed.Location = new System.Drawing.Point(3, 187);
            this.chartAngularSpeed.Name = "chartAngularSpeed";
            this.chartAngularSpeed.Size = new System.Drawing.Size(944, 178);
            this.chartAngularSpeed.TabIndex = 1;
            this.chartAngularSpeed.Text = "Angular Speed";
            // 
            // chartTorque
            // 
            this.chartTorque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTorque.Location = new System.Drawing.Point(3, 3);
            this.chartTorque.Name = "chartTorque";
            this.chartTorque.Size = new System.Drawing.Size(944, 178);
            this.chartTorque.TabIndex = 0;
            this.chartTorque.Text = "Torque";
            // 
            // formGraphs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(950, 625);
            this.Controls.Add(this.tlpGraphs);
            this.Controls.Add(this.tlpGraphsButtons);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "formGraphs";
            this.Text = "formGraphs";
            this.tlpGraphsButtons.ResumeLayout(false);
            this.tlpGraphs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpGraphsButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TableLayoutPanel tlpGraphs;
        private LiveCharts.WinForms.CartesianChart chartAngle;
        private LiveCharts.WinForms.CartesianChart chartAngularSpeed;
        private LiveCharts.WinForms.CartesianChart chartTorque;
    }
}