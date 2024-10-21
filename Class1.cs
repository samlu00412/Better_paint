
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
using OpenCvSharp.Dnn;
using System.Reflection;
namespace Better_paint
{
    internal class MatMethod
    {
        public void test(out string s)
        {
            s = "gg";
            return;
        }
        //public string test { get; set; } = "owo";
        public Mat canvas { get; set; } = new Mat();
        public Mat graycanvas { get; set; } = new Mat();

        public PictureBox target { get; set; }
        public void UpdatePicturebox(PictureBox target,string filePath = "")
        {
            if(filePath != "")
            {
                canvas = Cv2.ImRead(filePath);
                //graycanvas.Dispose();
                Cv2.CvtColor(canvas, graycanvas, ColorConversionCodes.BGR2GRAY);
            }
            if (target.Image != null)
            {
                target.Image.Dispose();
            }
            target.Image = BitmapConverter.ToBitmap(graycanvas);
            return;
        }
        public OpenCvSharp.Point GetPos(int X, int Y)
        {
            int originalWidth = this.target.Image.Width;
            int originalHeight = this.target.Image.Height;

            PropertyInfo rectangleProperty = this.target.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle rectangle = (Rectangle)rectangleProperty.GetValue(this.target, null);

            int currentWidth = rectangle.Width;
            int currentHeight = rectangle.Height;

            double rate = (double)currentHeight / (double)originalHeight;

            int black_left_width = (currentWidth == this.target.Width) ? 0 : (this.target.Width - currentWidth) / 2;
            int black_top_height = (currentHeight == this.target.Height) ? 0 : (this.target.Height - currentHeight) / 2;

            int zoom_x = X - black_left_width;
            int zoom_y = Y - black_top_height;
            return new OpenCvSharp.Point((double)zoom_x / rate, (double)zoom_y / rate);
        }
        


        public void changePictureBoxSize(double ratio, MouseEventArgs e,System.Drawing.Size originsize)
        {
            int ow = target.Width;
            int oh = target.Height;
            int VX, VY;
            int x = e.X;
            int y = e.Y;
            System.Drawing.Size t = target.Size;
            t.Width = Convert.ToInt32(originsize.Width * ratio);
            t.Height = Convert.ToInt32(originsize.Height * ratio);
            target.Size = t;

            VX = (int)((double)x * (ow - target.Width) / ow);
            VY = (int)((double)y * (oh - target.Height) / oh);
            target.Location = new System.Drawing.Point(target.Location.X + VX, target.Location.Y + VY);
        }


        public void DrawRight_Down()
        {
            dragStartPoint.X = Cursor.Position.X;
            dragStartPoint.Y = Cursor.Position.Y;
        }
        OpenCvSharp.Point dragStartPoint;
        public void DrawRight_Move()
        {
            //OpenCvSharp.Point currentPoint = GetPos(e.X, e.Y);
            target.Location = new System.Drawing.Point
            (
                target.Location.X + Cursor.Position.X - dragStartPoint.X,
                target.Location.Y + Cursor.Position.Y - dragStartPoint.Y
            );
            dragStartPoint.X = Cursor.Position.X;
            dragStartPoint.Y = Cursor.Position.Y;
        }
    }
}
