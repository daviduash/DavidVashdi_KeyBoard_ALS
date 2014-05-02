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
    public partial class frmDown : Form
    {
        private frmMain m_frmTop;


        public frmDown(frmMain myFrmTop)
        {
            m_frmTop = myFrmTop;
            InitializeComponent();
        }

        private void frmDown_Load(object sender, EventArgs e)
        {
            btn0.Tag = "Zibi";
        }


        private void frmDown_MouseEnter(object sender, EventArgs e)
        {
            m_frmTop.ShowForms();
        }

        private void frmDown_MouseLeave(object sender, EventArgs e)
        {
            m_frmTop.HideFormsDelay();
        }

        private void kbtn0_Click(object sender, EventArgs e)
        {
            SendKeys.Send((string)btn0.Tag);
        }
    }
}
