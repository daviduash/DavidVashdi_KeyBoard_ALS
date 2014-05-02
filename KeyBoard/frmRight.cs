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
using System.Threading;
using System.Diagnostics;

namespace KeyBoard
{
    public partial class frmRight : Form
    {
        private frmMain m_frmTop;

        private Color newBtnColor = new Color();
        bool m_bIsRunnning;
        Stopwatch sw1 = new Stopwatch();
        

        public frmRight(frmMain myFrmTop)
        {
            m_frmTop = myFrmTop;
            InitializeComponent();
        }

        private void frmRight_Load(object sender, EventArgs e)
        {

        }

        private void frmRight_MouseEnter(object sender, EventArgs e)
        {
            m_frmTop.ShowForms();
        }

        private void frmRight_MouseLeave(object sender, EventArgs e)
        {
            if (Cursor.Position.X <= m_frmTop.nRight - m_frmTop.nSideWidth2)
            {
                m_frmTop.HideFormsDelay();
            }

        }

        private void btnL_MouseHover(object sender, EventArgs e)
        {

        }

        private void btnL_MouseLeave(object sender, EventArgs e)
        {

        }

        private void btnL_Click(object sender, EventArgs e)
        {

        }


        #region btnM
        private void btnM_Click(object sender, EventArgs e)
        {
            SendKeys.Send("M");
        }
        private void btnM_MouseEnter(object sender, EventArgs e)
        {
            newBtnColor = new Color();
            m_bIsRunnning = true;
            int i = 215;
            sw1.Reset();
            sw1.Start();

            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
            {
                Thread.Sleep(10);
                newBtnColor = Color.FromArgb(255, i, i);
                Application.DoEvents();
                btnM.BackColor = newBtnColor;
                i--;
            }

            if (m_bIsRunnning == true)
            {
                btnM.BackColor = Color.LightGreen;
                SendKeys.Send("M");
            }
        }
        private void btnM_MouseLeave(object sender, EventArgs e)
        {
            newBtnColor = Color.LightGray;
            btnM.BackColor = Color.LightGray;
            sw1.Stop();
            m_bIsRunnning = false;
        }
        #endregion

        private void keyBoardButton7_Click(object sender, EventArgs e)
        {

        }




    }
}
