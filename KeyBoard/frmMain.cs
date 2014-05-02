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
    public partial class frmMain : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]      
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(int uiAction, int uiParam, ref RECT pvParam, int fWinIni);


        [DllImport("user32.dll", SetLastError = true)]      
        static extern void keybd_event (byte bVk,byte bScan,int dwFlag, int dwExtraInfo);
        private const int KEY_DOWN = 0x001 ;
        private const int KEY_UP = 0x0002 ;
        private const int LEFT_SHIFT = 0xA0;
        private const int LEFT_CONTROL = 0xA2;
        



        #region VARS
        private const Int32 SPIF_SENDWININICHANGE = 2;
        private const Int32 SPIF_UPDATEINIFILE = 1;
        private const Int32 SPIF_change = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;
        //private const Int32 SPI_SETWORKAREA = 47;
        private const uint SPI_SETWORKAREA = 0x002F;
        private const Int32 SPI_GETWORKAREA = 48;
        private int nLanguage = 0;
        public clsScreenBorder zibilibi;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 Left;
            public Int32 Right;
            public Int32 Top;
            public Int32 Bottom;
        }


        private frmLeft frmLeft1;
        private frmRight frmRight1;
        private frmDown frmDown1;

        public int nLeft = Screen.PrimaryScreen.WorkingArea.Left;
        public int nRight = Screen.PrimaryScreen.WorkingArea.Right;
        public int nTop = Screen.PrimaryScreen.WorkingArea.Top;
        public int nDown = Screen.PrimaryScreen.WorkingArea.Bottom;
        public int nBottom = Screen.PrimaryScreen.Bounds.Bottom;
        
        Color origBtnColor = new Color();
        Color newBtnColor = new Color();

        int nFactor = 25;

        private const int nSideWidth = 25; //must be as nFactor 
        public int nSideWidth2 = nSideWidth;
        public int nSideHeight = Screen.PrimaryScreen.WorkingArea.Bottom;

        public int nTopDownWidth = Screen.PrimaryScreen.WorkingArea.Right - 2 * nSideWidth;
        private const int nTopDownHeight = 25; //must be as nFactor 
        public int nTopDownHeight2 = nTopDownHeight;

        public Point m_pntTopLeft = new Point();
        public Point pntTopRight = new Point();
        public Point pntDownLeft = new Point();
        public Point pntDownRight = new Point();

        public Size szSide = new Size(30, 100);
        public Size szTopDown = new Size(100, 30);
        private bool m_bIsRunnning = true;
        public string strKeyBoardInput = "English";

        public Stopwatch sw1 = new Stopwatch();

        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST = 0x00000008;

        #endregion

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

        public frmMain()
        {            
            InitializeComponent();
        }

  
        

        private void Form1_Load(object sender, EventArgs e)
        {
            ////////////////////
            // Create 3 forms //
            ////////////////////
            frmLeft1 = new frmLeft(this);
            frmRight1 = new frmRight(this);
            frmDown1 = new frmDown(this);
            
            // Show the 3 forms //
            frmLeft1.Show();
            frmRight1.Show();
            frmDown1.Show();            
            
            // Set the size and possition of the 3 forms //
            ReSizeForms();
            LocateForms();

            // Arrange buttons on all forms //
            ArrangeButtons();

            // Set startup Language as English //
            strKeyBoardInput = "English";
            SetKeys(strKeyBoardInput);


            //origBtnColor = btnA.BackColor;

            //////////////////////////
            // Set new Working area //
            //////////////////////////    
            Rectangle rctWorkingArea = Screen.PrimaryScreen.WorkingArea;
            clsScreenBorder.Rect rct = new clsScreenBorder.Rect();
            rct.Left = rctWorkingArea.Left + nFactor;
            rct.Right = rctWorkingArea.Right - nFactor;
            rct.Top = rctWorkingArea.Top + nFactor;
            rct.Bottom = rctWorkingArea.Bottom - nFactor;
            zibilibi = new clsScreenBorder();
            zibilibi.SetWorkingArea(rct);
        }

        public void zibi()
        {
            Random rnd = new Random();
            this.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        

        #region hideForm
        public void HideForms()
        {
            //frmLeft1.Location = new Point(-nSideWidth + 5, 0);
            //frmRight1.Location = new Point(nRight - 5, 0);
            //frmDown1.Location = new Point(nSideWidth, nBottom - 5);
            //this.Location = new Point(nSideWidth, -nTopDownHeight + 5);
        }

        public void ShowForms()
        {
            LocateForms();
        }

        private void frmMain_MouseLeave(object sender, EventArgs e)
        {
            if (Cursor.Position.Y >= nTopDownHeight)
            {
                HideFormsDelay();
            }
        }

        private void frmMain_MouseEnter(object sender, EventArgs e)
        {   
            ShowForms();
        }

        public void HideFormsDelay()
        {
            sw1.Reset();
            sw1.Start();

            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }
            HideForms();
        }
        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            zibilibi.RevertWorkingArea();
            m_bIsRunnning = false;
            this.Close();
        }



#region Key_Region
//        #region btnA
//        private void btnA_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("A");
//        }
//        private void btnA_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnA.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnA.BackColor = Color.LightGreen;
//                SendKeys.Send("A");
//            }
//        }
//        private void btnA_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnA.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnB
//        private void btnB_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("B");
//        }
//        private void btnB_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnB.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnB.BackColor = Color.LightGreen;
//                SendKeys.Send("B");
//            }
//        }
//        private void btnB_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnB.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnC
//        private void btnC_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("C");
//        }
//        private void btnC_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnC.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnC.BackColor = Color.LightGreen;
//                SendKeys.Send("C");
//            }
//        }
//        private void btnC_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnC.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnD
//        private void btnD_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("D");
//        }
//        private void btnD_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
                
//                Application.DoEvents();
//                btnD.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnD.BackColor = Color.LightGreen;
//                SendKeys.Send("D");
//            }
//        }
//        private void btnD_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnD.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnE
//        private void btnE_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("E");
//        }
//        private void btnE_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnE.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnE.BackColor = Color.LightGreen;
//                SendKeys.Send("E");
//            }
//        }
//        private void btnE_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnE.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//#endregion
//        #region btnF
//        private void btnF_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("F");
//        }
//        private void btnF_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnF.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnF.BackColor = Color.LightGreen;
//                SendKeys.Send("F");
//            }
//        }
//        private void btnF_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnF.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnG
//        private void btnG_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("G");
//        }
//        private void btnG_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnG.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnG.BackColor = Color.LightGreen;
//                SendKeys.Send("G");
//            }
//        }
//        private void btnG_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnG.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnH
//        private void btnH_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("H");
//        }
//        private void btnH_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnH.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnH.BackColor = Color.LightGreen;
//                SendKeys.Send("H");
//            }
//        }
//        private void btnH_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnH.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnI
//        private void btnI_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("I");
//        }
//        private void btnI_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnI.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnI.BackColor = Color.LightGreen;
//                SendKeys.Send("I");
//            }
//        }
//        private void btnI_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnI.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnJ
//        private void btnJ_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("J");
//        }
//        private void btnJ_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnJ.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnJ.BackColor = Color.LightGreen;
//                SendKeys.Send("J");
//            }
//        }
//        private void btnJ_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnJ.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnK
//        private void btnK_Click(object sender, EventArgs e)
//        {
//            SendKeys.Send("K");
//        }
//        private void btnK_MouseEnter(object sender, EventArgs e)
//        {
//            newBtnColor = new Color();
//            m_bIsRunnning = true;
//            int i = 215;
//            sw1.Reset();
//            sw1.Start();

//            while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//            {
//                Thread.Sleep(10);
//                newBtnColor = Color.FromArgb(255, i, i);
//                Application.DoEvents();
//                btnK.BackColor = newBtnColor;
//                i--;
//            }

//            if (m_bIsRunnning == true)
//            {
//                btnK.BackColor = Color.LightGreen;
//                SendKeys.Send("K");
//            }
//        }
//        private void btnK_MouseLeave(object sender, EventArgs e)
//        {
//            newBtnColor = Color.LightGray;
//            btnK.BackColor = Color.LightGray;
//            sw1.Stop();
//            m_bIsRunnning = false;
//        }
//        #endregion
//        #region btnL
//        //private void btnL_Click(object sender, EventArgs e)
//        //{
//        //    SendKeys.Send("L");
//        //}
//        //private void btnL_MouseEnter(object sender, EventArgs e)
//        //{
//        //    newBtnColor = new Color();
//        //    m_bIsRunnning = true;
//        //    int i = 215;
//        //    sw1.Reset();
//        //    sw1.Start();

//        //    while (sw1.ElapsedMilliseconds < 2000 && m_bIsRunnning == true)
//        //    {
//        //        Thread.Sleep(10);
//        //        newBtnColor = Color.FromArgb(255, i, i);
//        //        Application.DoEvents();
//        //        btnL.BackColor = newBtnColor;
//        //        i--;
//        //    }

//        //    if (m_bIsRunnning == true)
//        //    {
//        //        btnL.BackColor = Color.LightGreen;
//        //        SendKeys.Send("L");
//        //    }
//        //}
//        //private void btnL_MouseLeave(object sender, EventArgs e)
//        //{
//        //    newBtnColor = Color.LightGray;
//        //    btnL.BackColor = Color.LightGray;
//        //    sw1.Stop();
//        //    m_bIsRunnning = false;
//        //}
        #endregion


        public void SendChar(string strCharToSend)
        {
            int i = 0;
            if (strCharToSend == "SpecialKeys")
            {
                if (i % 2 == 0)
                {
                    #region MainForm
                    this.kbtnA.Text = @"!";
                    this.kbtnA.KeyValue = @"!";

                    this.kbtnB.Text = @"@";
                    this.kbtnB.KeyValue = @"@";

                    this.kbtnC.Text = @"#";
                    this.kbtnC.KeyValue = @"#";

                    this.kbtnD.Text = @"$";
                    this.kbtnD.KeyValue = @"$";

                    this.kbtnE.Text = @"%";
                    this.kbtnE.KeyValue = "{%}";

                    this.kbtnF.Text = @"^";
                    this.kbtnF.KeyValue = "{^}";

                    this.kbtnG.Text = "&";
                    this.kbtnG.KeyValue = "{&}";

                    this.kbtnH.Text = @"*";
                    this.kbtnH.KeyValue = "{*}";

                    this.kbtnI.Text = @"(";
                    this.kbtnI.KeyValue = "{(}";

                    this.kbtnJ.Text = @")";
                    this.kbtnJ.KeyValue = "{)}";

                    this.kbtnK.Text = @"~";
                    this.kbtnK.KeyValue = "{~}";

                    this.kbtnL.Text = @"/";
                    this.kbtnL.KeyValue = "{/}";
                    #endregion

                    #region RightForm

                    frmRight1.kbtnM.Text = @"/";
                    frmRight1.kbtnM.KeyValue = "{/}";

                    frmRight1.kbtnN.Text = @"\";
                    frmRight1.kbtnN.KeyValue = "{\\}";

                    frmRight1.kbtnO.Text = @"_";
                    frmRight1.kbtnO.KeyValue = "{_}";

                    frmRight1.kbtnP.Text = @"↑";
                    frmRight1.kbtnP.KeyValue = "{UP}";

                    frmRight1.kbtnQ.Text = @"↓";
                    frmRight1.kbtnQ.KeyValue = "{DOWN}";

                    frmRight1.kbtnR.Text = @"←";
                    frmRight1.kbtnR.KeyValue = "{LEFT}";

                    frmRight1.kbtnS.Text = @"→";
                    frmRight1.kbtnS.KeyValue = "{RIGHT}";

                    frmRight1.kbtnT.Text = @":";
                    frmRight1.kbtnT.KeyValue = "{:}";

                    frmRight1.kbtnU.Text = @"'";
                    frmRight1.kbtnU.KeyValue = "{'}";

                    frmRight1.kbtnV.Text = "\"";
                    frmRight1.kbtnV.KeyValue = "{\"}";

                    frmRight1.kbtnRight1.Text = "";
                    frmRight1.kbtnRight1.KeyValue = "";

                    #endregion
                }
                else
                {
                    nLanguage--;
                    ChangeLanguage();
                }
                i++;
            }
            else if (strCharToSend == "ChangeKeys")
            {
                ChangeLanguage();
            }
            else if (strCharToSend == "Close")
            {
                CloseProgram();
            }
            else if (strCharToSend == "Dir")
            {
                //keybd_event((byte)Keys.LControlKey, 0, KEY_DOWN, 0);
                //keybd_event((byte)Keys.LShiftKey, 0, KEY_DOWN, 0);
                //System.Threading.Thread.Sleep(2000);
                SendKeys.Send("+^");
                //keybd_event((byte)Keys.LShiftKey, 0, KEY_DOWN | KEY_UP, 0);
                //keybd_event((byte)Keys.LControlKey, 0, KEY_DOWN | KEY_UP, 0);
                //System.Threading.Thread.Sleep(2000);
            }
            else
            {
                SendKeys.Send(strCharToSend);
            }
        }







        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            zibilibi.RevertWorkingArea();
        }



        private void ArrangeButtons()
        {
            Control ctrlTemp = new Control();
            List<Control> lstBtnFrmMain = new List<Control>();
            List<Control> lstBtnFrmDown = new List<Control>();
            List<Control> lstBtnFrmRight = new List<Control>();
            List<Control> lstBtnFrmLeft = new List<Control>();
            

            int n = 0;

            #region arrangeButtonMain
            int nNumberOfButtonsMain = 0;            
            foreach(Control control in this.Controls)
            {
                if(control.GetType() == typeof(KeyBoardButton))
                {
                    lstBtnFrmMain.Add(control);
                    nNumberOfButtonsMain++;
                }
            }
            
            for (int i = 0; i < nNumberOfButtonsMain; i++)
            {
                for (int j = 0; j < nNumberOfButtonsMain-1; j++)
                {
                    if (lstBtnFrmMain[j].Location.X > lstBtnFrmMain[j + 1].Location.X)
                    {
                        ctrlTemp = lstBtnFrmMain[j];
                        lstBtnFrmMain[j] = lstBtnFrmMain[j + 1];
                        lstBtnFrmMain[j + 1] = ctrlTemp;
                    }
                }
            }

            int nSpaceSizeMain = 0;
            int nBtnWidthMain = this.kbtnA.Width;
            n = 0;
            nSpaceSizeMain = (nTopDownWidth - nBtnWidthMain * nNumberOfButtonsMain) / (nNumberOfButtonsMain - 1);
            foreach(Control ctrl in lstBtnFrmMain)
            {
                ctrl.Location = new Point(n*(nBtnWidthMain+nSpaceSizeMain), ctrl.Location.Y);
                n++;
            }
            #endregion

            #region arrangeButtonDown
            int nNumberOfButtonsDown = 0;            
            foreach(Control control in frmDown1.Controls)
            {
                if(control.GetType() == typeof(KeyBoardButton))
                {
                    lstBtnFrmDown.Add(control);
                    nNumberOfButtonsDown++;
                }
            }

            for (int i = 0; i < nNumberOfButtonsDown; i++)
            {
                for (int j = 0; j < nNumberOfButtonsDown - 1; j++)
                {
                    if (lstBtnFrmDown[j].Location.X > lstBtnFrmDown[j + 1].Location.X)
                    {
                        ctrlTemp = lstBtnFrmDown[j];
                        lstBtnFrmDown[j] = lstBtnFrmDown[j + 1];
                        lstBtnFrmDown[j + 1] = ctrlTemp;
                    }
                }
            }

            int nSpaceSizeDown = 0;
            int nBtnWidthDown = btnA.Width;
            n = 0;
            nSpaceSizeDown = (nTopDownWidth - nBtnWidthDown * nNumberOfButtonsDown) / (nNumberOfButtonsDown - 1);
            foreach (Control ctrl in lstBtnFrmDown)
            {
                ctrl.Location = new Point(n * (nBtnWidthDown + nSpaceSizeDown), ctrl.Location.Y);
                n++;
            }
            #endregion

            #region arrangeButtonRight
            int nNumberOfButtonsRight = 0;
            foreach (Control control in frmRight1.Controls)
            {
                if (control.GetType() == typeof(KeyBoardButton))
                {
                    lstBtnFrmRight.Add(control);
                    nNumberOfButtonsRight++;
                }
            }
            //MessageBox.Show(i.ToString());


            for (int i = 0; i < nNumberOfButtonsRight; i++)
            {
                for (int j = 0; j < nNumberOfButtonsRight - 1; j++)
                {
                    if (lstBtnFrmRight[j].Location.Y > lstBtnFrmRight[j + 1].Location.Y)
                    {
                        ctrlTemp = lstBtnFrmRight[j];
                        lstBtnFrmRight[j] = lstBtnFrmRight[j + 1];
                        lstBtnFrmRight[j + 1] = ctrlTemp;
                    }
                }
            }

            int nSpaceSizeRight = 0;
            int nBtnHeightRight = frmRight1.kbtnM.Height;
            n = 0;
            nSpaceSizeRight = (nSideHeight - 2*nTopDownHeight2 - nBtnHeightRight * nNumberOfButtonsRight) / (nNumberOfButtonsRight - 1);
            foreach (Control ctrl in lstBtnFrmRight)
            {
                ctrl.Location = new Point(ctrl.Location.X,nTopDownHeight2 + n * (nBtnHeightRight + nSpaceSizeRight));
                n++;
            }
            #endregion

            #region arrangeButtonLeft
            int nNumberOfButtonsLeft = 0;
            foreach (Control control in frmLeft1.Controls)
            {
                if (control.GetType() == typeof(KeyBoardButton))
                {
                    lstBtnFrmLeft.Add(control);
                    nNumberOfButtonsLeft++;
                }
            }
            //MessageBox.Show(i.ToString());


            for (int i = 0; i < nNumberOfButtonsLeft; i++)
            {
                for (int j = 0; j < nNumberOfButtonsLeft - 1; j++)
                {
                    if (lstBtnFrmLeft[j].Location.Y > lstBtnFrmLeft[j + 1].Location.Y)
                    {
                        ctrlTemp = lstBtnFrmLeft[j];
                        lstBtnFrmLeft[j] = lstBtnFrmLeft[j + 1];
                        lstBtnFrmLeft[j + 1] = ctrlTemp;
                    }
                }
            }

            int nSpaceSizeLeft = 0;
            int nBtnHeightLeft = frmLeft1.kbtnX.Height;
            n = 0;
            nSpaceSizeLeft = (nSideHeight - 2 * nTopDownHeight2 - nBtnHeightRight * nNumberOfButtonsLeft) / (nNumberOfButtonsLeft - 1);
            foreach (Control ctrl in lstBtnFrmLeft)
            {
                ctrl.Location = new Point(ctrl.Location.X, nTopDownHeight2 + n * (nBtnHeightRight + nSpaceSizeLeft));
                n++;
            }
            #endregion

        }


        public void SetKeys(string strKeyBoardInputToSet)
        {
                
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.GetType() == typeof(KeyBoardButton))
                    {
                        KeyBoardButton kbtn = (KeyBoardButton)ctrl;
                        kbtn.KeyValue = ctrl.Text;
                        kbtn.m_frmTop = this;
                    }
                }
                this.kbtnClose.KeyValue = "Close";


                foreach (Control ctrl in frmDown1.Controls)
                {
                    if (ctrl.GetType() == typeof(KeyBoardButton))
                    {
                        KeyBoardButton kbtn = (KeyBoardButton)ctrl;
                        kbtn.KeyValue = ctrl.Text;
                        kbtn.m_frmTop = this;
                    }
                }
                frmDown1.kbtnSpace.KeyValue = " ";
                frmDown1.kbtnEnter.KeyValue = "{ENTER}";
                frmDown1.kbtnBackSpace.KeyValue = "{BS}";

                foreach (Control ctrl in frmLeft1.Controls)
                {
                    if (ctrl.GetType() == typeof(KeyBoardButton))
                    {
                        KeyBoardButton kbtn = (KeyBoardButton)ctrl;
                        kbtn.KeyValue = ctrl.Text;
                        kbtn.m_frmTop = this;
                    }
                }
                frmLeft1.kbtnSpecialKeys.KeyValue = "SpecialKeys";
                frmLeft1.kbtnChangeKeys.KeyValue = "ChangeKeys";
            

                foreach (Control ctrl in frmRight1.Controls)
                {
                    if (ctrl.GetType() == typeof(KeyBoardButton))
                    {
                        KeyBoardButton kbtn = (KeyBoardButton)ctrl;
                        kbtn.KeyValue = ctrl.Text;
                        kbtn.m_frmTop = this;
                    }
                }
        }

        private void kbtnClose_Click(object sender, EventArgs e)
        {
            CloseProgram();
        }

        public void CloseProgram()
        {
            zibilibi.RevertWorkingArea();
            m_bIsRunnning = false;
            this.Close();
        }
        


        public void ChangeLanguage()
        {
            nLanguage++;

            if (nLanguage % 2 == 1)
            {
                #region MainForm
                this.kbtnA.Text = "א";
                this.kbtnA.KeyValue = "א";

                this.kbtnB.Text = "ב";
                this.kbtnB.KeyValue = "ב";

                this.kbtnC.Text = "ג";
                this.kbtnC.KeyValue = "ג";

                this.kbtnD.Text = "ד";
                this.kbtnD.KeyValue = "ד";

                this.kbtnE.Text = "ה";
                this.kbtnE.KeyValue = "ה";

                this.kbtnF.Text = "ו";
                this.kbtnF.KeyValue = "ו";

                this.kbtnG.Text = "ז";
                this.kbtnG.KeyValue = "ז";

                this.kbtnH.Text = "ח";
                this.kbtnH.KeyValue = "ח";

                this.kbtnI.Text = "ט";
                this.kbtnI.KeyValue = "ט";

                this.kbtnJ.Text = "י";
                this.kbtnJ.KeyValue = "י";

                this.kbtnK.Text = "כ";
                this.kbtnK.KeyValue = "כ";

                this.kbtnL.Text = "ל";
                this.kbtnL.KeyValue = "ל";
                #endregion

                #region RightForm

                frmRight1.kbtnM.Text = "מ";
                frmRight1.kbtnM.KeyValue = "מ";

                frmRight1.kbtnN.Text = "נ";
                frmRight1.kbtnN.KeyValue = "נ";
                
                frmRight1.kbtnO.Text = "ס";
                frmRight1.kbtnO.KeyValue = "ס";

                frmRight1.kbtnP.Text = "ע";
                frmRight1.kbtnP.KeyValue = "ע";

                frmRight1.kbtnQ.Text = "פ";
                frmRight1.kbtnQ.KeyValue = "פ";

                frmRight1.kbtnR.Text = "צ";
                frmRight1.kbtnR.KeyValue = "צ";

                frmRight1.kbtnS.Text = "ק";
                frmRight1.kbtnS.KeyValue = "ק";

                frmRight1.kbtnT.Text = "ר";
                frmRight1.kbtnT.KeyValue = "ר";

                frmRight1.kbtnU.Text = "ש";
                frmRight1.kbtnU.KeyValue = "ש";

                frmRight1.kbtnV.Text = "ת";
                frmRight1.kbtnV.KeyValue = "ת";

                frmRight1.kbtnRight1.Text = "(L)";
                frmRight1.kbtnRight1.KeyValue = "^+";
                
                #endregion

                #region LeftForm

                frmLeft1.kbtnW.Text = "ך";
                frmLeft1.kbtnW.KeyValue = "ך";

                frmLeft1.kbtnX.Text = "ם";
                frmLeft1.kbtnX.KeyValue = "ם";

                frmLeft1.kbtnY.Text = "ן";
                frmLeft1.kbtnY.KeyValue = "ן";

                frmLeft1.kbtnZ.Text = "ף";
                frmLeft1.kbtnZ.KeyValue = "ף";

                frmLeft1.kbtnN5.Text = "ץ";
                frmLeft1.kbtnN5.KeyValue = "ץ";
                #endregion
            }
            else if (nLanguage % 2 == 0)
            {
                #region MainForm
                this.kbtnA.Text = "A";
                this.kbtnA.KeyValue = "A";

                this.kbtnB.Text = "B";
                this.kbtnB.KeyValue = "B";

                this.kbtnC.Text = "C";
                this.kbtnC.KeyValue = "C";

                this.kbtnD.Text = "D";
                this.kbtnD.KeyValue = "D";

                this.kbtnE.Text = "E";
                this.kbtnE.KeyValue = "E";

                this.kbtnF.Text = "F";
                this.kbtnF.KeyValue = "F";

                this.kbtnG.Text = "G";
                this.kbtnG.KeyValue = "G";

                this.kbtnH.Text = "H";
                this.kbtnH.KeyValue = "H";

                this.kbtnI.Text = "I";
                this.kbtnI.KeyValue = "I";

                this.kbtnJ.Text = "J";
                this.kbtnJ.KeyValue = "J";

                this.kbtnK.Text = "K";
                this.kbtnK.KeyValue = "K";

                this.kbtnL.Text = "L";
                this.kbtnL.KeyValue = "L";
                #endregion

                #region RightForm

                frmRight1.kbtnM.Text = "M";
                frmRight1.kbtnM.KeyValue = "M";

                frmRight1.kbtnN.Text = "N";
                frmRight1.kbtnN.KeyValue = "N";

                frmRight1.kbtnO.Text = "O";
                frmRight1.kbtnO.KeyValue = "O";

                frmRight1.kbtnP.Text = "P";
                frmRight1.kbtnP.KeyValue = "P";

                frmRight1.kbtnQ.Text = "Q";
                frmRight1.kbtnQ.KeyValue = "Q";

                frmRight1.kbtnR.Text = "R";
                frmRight1.kbtnR.KeyValue = "R";

                frmRight1.kbtnS.Text = "S";
                frmRight1.kbtnS.KeyValue = "S";

                frmRight1.kbtnT.Text = "T";
                frmRight1.kbtnT.KeyValue = "T";

                frmRight1.kbtnU.Text = "U";
                frmRight1.kbtnU.KeyValue = "U";

                frmRight1.kbtnV.Text = "V";
                frmRight1.kbtnV.KeyValue = "V";

                frmRight1.kbtnRight1.Text = "";
                frmRight1.kbtnRight1.KeyValue = "";

                #endregion

                #region LeftForm

                frmLeft1.kbtnW.Text = "W";
                frmLeft1.kbtnW.KeyValue = "W";

                frmLeft1.kbtnX.Text = "X";
                frmLeft1.kbtnX.KeyValue = "X";

                frmLeft1.kbtnY.Text = "Y";
                frmLeft1.kbtnY.KeyValue = "Y";

                frmLeft1.kbtnZ.Text = "Z";
                frmLeft1.kbtnZ.KeyValue = "Z";

                frmLeft1.kbtnN5.Text = "";
                frmLeft1.kbtnN5.KeyValue = "";
                #endregion
            }       
        }

        private void ReSizeForms()
        {

            /////////////////////////////////////////////////
            ///                     LEFT                  ///
            /////////////////////////////////////////////////

            //////////////////////
            /// SET LEFT WiDTH ///
            //////////////////////
            while (frmLeft1.Size.Width < nSideWidth)
            {
                frmLeft1.Size = new Size(frmLeft1.Size.Width + 1, frmLeft1.Size.Height);
            }
            while (frmLeft1.Size.Width > nSideWidth)
            {
                frmLeft1.Size = new Size(frmLeft1.Size.Width - 1, frmLeft1.Size.Height);
            }

            ///////////////////////
            /// SET LEFT HEIGHT ///
            ///////////////////////
            while (frmLeft1.Size.Height < nSideHeight)
            {
                frmLeft1.Size = new Size(frmLeft1.Size.Width, frmLeft1.Size.Height + 1);
            }
            while (frmLeft1.Size.Height > nSideHeight)
            {
                frmLeft1.Size = new Size(frmLeft1.Size.Width, frmLeft1.Size.Height - 1);
            }


            //////////////////////////////////////////////////
            ///                     RIGHT                  ///
            //////////////////////////////////////////////////

            ///////////////////////
            /// SET RIGHT WiDTH ///
            ///////////////////////
            while (frmRight1.Size.Width < nSideWidth)
            {
                frmRight1.Size = new Size(frmRight1.Size.Width + 1, frmRight1.Size.Height);
            }
            while (frmRight1.Size.Width > nSideWidth)
            {
                frmRight1.Size = new Size(frmRight1.Size.Width - 1, frmRight1.Size.Height);
            }

            ////////////////////////
            /// SET RIGHT HEIGHT ///
            ////////////////////////
            while (frmRight1.Size.Height < nSideHeight)
            {
                frmRight1.Size = new Size(frmRight1.Size.Width, frmRight1.Size.Height + 1);
            }
            while (frmRight1.Size.Height > nSideHeight)
            {
                frmRight1.Size = new Size(frmRight1.Size.Width, frmRight1.Size.Height - 1);
            }

            /////////////////////////////////////////////////
            ///                     DOWN                  ///
            /////////////////////////////////////////////////

            //////////////////////
            /// SET DOWN WiDTH ///
            //////////////////////
            while (frmDown1.Size.Width < nTopDownWidth)
            {
                frmDown1.Size = new Size(frmDown1.Size.Width + 1, frmDown1.Size.Height);
            }
            while (frmDown1.Size.Width > nTopDownWidth)
            {
                frmDown1.Size = new Size(frmDown1.Size.Width - 1, frmDown1.Size.Height);
            }

            ///////////////////////
            /// SET DOWN HEIGHT ///
            ///////////////////////
            while (frmDown1.Size.Height < nTopDownHeight)
            {
                frmDown1.Size = new Size(frmDown1.Size.Width, frmDown1.Size.Height + 1);
            }
            while (frmDown1.Size.Height > nTopDownHeight)
            {
                frmDown1.Size = new Size(frmDown1.Size.Width, frmDown1.Size.Height - 1);
            }

            /////////////////////////////////////////////////
            ///                     TOP                   ///
            /////////////////////////////////////////////////

            //////////////////////
            /// SET TOP WiDTH ///
            //////////////////////
            while (this.Size.Width < nTopDownWidth)
            {
                this.Size = new Size(this.Size.Width + 1, this.Size.Height);
            }
            while (this.Size.Width > nTopDownWidth)
            {
                this.Size = new Size(this.Size.Width - 1, this.Size.Height);
            }

            ///////////////////////
            /// SET TOP HEIGHT ///
            ///////////////////////
            while (this.Size.Height < nTopDownHeight)
            {
                this.Size = new Size(this.Size.Width, this.Size.Height + 1);
            }
            while (this.Size.Height > nTopDownHeight)
            {
                this.Size = new Size(this.Size.Width, this.Size.Height - 1);
            }



        }

        private void LocateForms()
        {
            frmLeft1.Location = new Point(0, 0);
            frmRight1.Location = new Point(nRight - nSideWidth, 0);
            frmDown1.Location = new Point(nSideWidth, nDown - nTopDownHeight);
            this.Location = new Point(nSideWidth, 0);
        }



    }
}
