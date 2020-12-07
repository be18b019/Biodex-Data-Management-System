using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Biodex_Client
{
    public partial class Biodex_Client : Form
    {
        public Biodex_Client()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true); //needed for resizing
        }
        #region initializing of variables and objects
        // needed for resizing form
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        //initializing of childforms and data object   
        private Form activeForm = null;
        private static SerialPortSave serialportsave = new SerialPortSave();
        public static Data data = new Data();
        public static formGraphs FormGraphs = new formGraphs(data, serialportsave);
        public static formMeasurementProperties FormMeasurementProperties = new formMeasurementProperties(FormGraphs, data);
        public static formMicrocontrollerStatus FormMicrocontrollerStatus = new formMicrocontrollerStatus(serialportsave);
        #endregion

        #region title bar and dragging control

        //needed for form resizing
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {

            if(WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else 
            {
                WindowState = FormWindowState.Normal;
            }     
        }

        #endregion

        #region form control buttons

        private void btnGraphs_Click(object sender, EventArgs e)
        {
            openChildForm(FormGraphs);
            setActiveFormButtonColor(btnGraphs, btnMeasurementProperties, btnMicrocontrollerStatus);
        }

        private void btnMeasurementProperties_Click(object sender, EventArgs e)
        {
            openChildForm(FormMeasurementProperties);
            setActiveFormButtonColor(btnMeasurementProperties, btnMicrocontrollerStatus, btnGraphs);
        }

        private void btnMicrocontrollerStatus_Click(object sender, EventArgs e)
        {
            openChildForm(FormMicrocontrollerStatus);
            setActiveFormButtonColor(btnMicrocontrollerStatus, btnGraphs, btnMeasurementProperties);
        }

        private void setActiveFormButtonColor(Button activeButton, Button passiveButton1, Button passiveButton2)
        {
            activeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            passiveButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            passiveButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm == null)
            {
                activeForm = childForm;
            }
            else
            {
                activeForm.Hide();
                activeForm = childForm;
            }
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            panelChildForms.Controls.Add(childForm);
            panelChildForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        #endregion

    }
}

#region Johnny's notes ¯\_(ツ)_/¯

//Johnny's notes:

// using cartesianchart as liveplot
// https://stackoverflow.com/questions/55567258/how-to-update-livecharts-cartesian-chart-in-a-winform-when-variable-in-external

//

// tablelayoutcontainer for better layout of wf components as well as setting anchors

//UI design idea (if there is time left for that)
//https://www.youtube.com/watch?v=K9Ps66GoD-k
//https://www.youtube.com/watch?v=JP5rgXO_5Sk better link



//order of implementing:
//      1. Design proper UI
//           --> exporting of excercise information for database
// 
//           get form resizeable and dragable --> done  with link and bunifudragcontrol
//           https://stackoverflow.com/questions/29024910/how-to-design-a-custom-close-minimize-and-maximize-button-in-windows-form-appli
//          
//            measurement Properties:
//              for setting information that will be sent to database
//              needs: loadbutton and testloadbutton(for testloadbutton create folderbrowserdialog), exportbutton, clearbutton (needs popup window to ask if someone is sure to clear the data), textboxes for each measurement property as well as labels, 
//                      kind of checkbox that shows if data of measurement is available, load or measurment mode checkbox (likely to have something in the code that ensures not mixing up things)
//                      comboboxes for things to choose like exercises or things that have fixed data
//                      maybe button to create csv file for data and measurement properties
//                      change database at hip flexion and so on to start position
//                      limit:Rom?
//
//                          
//            Graphs:
//              three livechart cartesianchart graphs, start and stop button for interaction with measurement, savebutton
//              for saving measuremnet data temporally for later export, something (or code part that ensures that measured and
//              loaded data cant be mixed up)
//           
//            Microontroller Status:
//              not sure which components needed but, is important as soon as we are able to come  to lab, maybe something like 
//              like checkbox
//           
//
//
//      2. Implement test Load button with function of loading existing matlabfiles (convert them into csv files)
//      3. implement real load button that loads data from database
//      4. Implement test export button with loaded data from test load 
//      Can only be proceeded if we get access to biodex
//      5. Implement observation panel for microcontroller
//      . Implement start button
//      . Implement stop button
//        use Serialport from toolbox
#endregion