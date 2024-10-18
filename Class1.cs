
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System.Windows.Forms;
namespace Better_paint
{
    internal class MatMethod
    {
        //public string test { get; set; } = "owo";
        public Mat canvas { get; set; }
        public void UpdatePicturebox(PictureBox target,string filePath = "")
        {
            if(filePath != "")
            {
                canvas = Cv2.ImRead(filePath);
            }
            if (target.Image != null)
            {
                target.Image.Dispose();
            }
            target.Image = BitmapConverter.ToBitmap(canvas);
            return;
        }

    }
}
