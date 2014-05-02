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
    public class KeyBoardButton : Button
    {        
        private Color newBtnColor = new Color();
        private bool m_bIsRunnning;
        private Stopwatch sw1 = new Stopwatch();
        private long lTimeToPress = 2000;
        public string KeyValue = string.Empty;
        public frmMain m_frmTop;

        public KeyBoardButton()
        {
        }

        #region Key
        protected override void OnMouseEnter(System.EventArgs e)
        {
            newBtnColor = new Color();
            m_bIsRunnning = true;
            int i = 215;
            sw1.Reset();
            sw1.Start();

            while (sw1.ElapsedMilliseconds < lTimeToPress && m_bIsRunnning == true)
            {
                Thread.Sleep(10);
                newBtnColor = Color.FromArgb(255, i, i);
                Application.DoEvents();
                this.BackColor = newBtnColor;
                i--;
            }

            if (m_bIsRunnning == true)
            {
                this.BackColor = Color.LightGreen;
                m_frmTop.SendChar(this.KeyValue);
                //SendKeys.Send(this.KeyValue);
            }
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            
            newBtnColor = SystemColors.Control;
            this.BackColor = SystemColors.Control;
            sw1.Stop();
            m_bIsRunnning = false;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            m_frmTop.SendChar(this.KeyValue);
            //SendKeys.Send(this.KeyValue);
        }
    
        #endregion
    }
}
