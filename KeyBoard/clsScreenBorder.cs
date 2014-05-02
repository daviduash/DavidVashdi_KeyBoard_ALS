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
    public class clsScreenBorder
    {
        Rectangle OriginalBounds;


        public clsScreenBorder()
        {
            this.OriginalBounds = Screen.PrimaryScreen.WorkingArea;
        }
                
        public struct Rect
        {
            public Int32 Left, Top, Right, Bottom;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(int uiAction, int uiParam, ref Rect pvParam, int fWinIni);

        public void SetWorkingArea(Rect rct)
        {
            bool b1;
            b1 = SystemParametersInfo(47, 0, ref rct, 2);
        }



        public void RevertWorkingArea()
        {

            bool b1;            
            Rect RectOriginalSize = new Rect();
            RectOriginalSize.Left = this.OriginalBounds.Left;
            RectOriginalSize.Right = this.OriginalBounds.Right;
            RectOriginalSize.Top = this.OriginalBounds.Top;
            RectOriginalSize.Bottom = this.OriginalBounds.Bottom;
            b1 = SystemParametersInfo(47/*SPI_SETWORKAREA*/, 0, ref RectOriginalSize, 2/*SPIF_SENDCHANGE*/);
        }
    }
}