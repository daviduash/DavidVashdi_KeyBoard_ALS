using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyBoard
{
    public partial class frmLeft : Form
    {
        private frmMain m_frmTop;

        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST = 0x00000008;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                //param.ExStyle = param.ExStyle | WS_EX_NOACTIVATE;
                param.ExStyle = param.ExStyle | WS_EX_NOACTIVATE | WS_EX_TOPMOST;
                return param;
            }
        }


        public frmLeft(frmMain myFrmTop)
        {
            m_frmTop = myFrmTop;
            InitializeComponent();
        }

        //private void btnA_Click(object sender, EventArgs e)
        //{
        //    m_frmTop.zibi();
        //}

        private void frmLeft_Load(object sender, EventArgs e)
        {

        }

        private void frmLeft_MouseEnter(object sender, EventArgs e)
        {
            m_frmTop.sw1.Stop();
            m_frmTop.ShowForms();
        }

        private void frmLeft_MouseLeave(object sender, EventArgs e)
        {
            if (Cursor.Position.X >= m_frmTop.nSideWidth2)
            {
                m_frmTop.HideFormsDelay();
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("0");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            m_frmTop.SendChar("9");
        }

        private void kbtnN1_Click(object sender, EventArgs e)
        {

        }

        private void kbtnLeft1_Click(object sender, EventArgs e)
        {
            //m_frmTop.ChangeLanguage();
        }

    }
}
