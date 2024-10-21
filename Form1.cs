using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System.Windows.Forms.DataVisualization.Charting;
namespace Better_paint {
    public partial class Form1 : Form {
        string temp = "123";
        MatMethod PictureBoxHelper = new MatMethod();
        System.Drawing.Size originsize;
        public Form1() {
            InitializeComponent();
            PictureBoxHelper.test(out temp);
            label1.Text= temp;
            PictureBoxHelper.target = pictureBox1;
            originsize = this.pictureBox1.Size;
        }
        Series series = new Series
        {
            Name = "亮度",
            Color = System.Drawing.Color.Blue,
            ChartType = SeriesChartType.Column
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBoxHelper.canvas = ((Bitmap)pictureBox1.Image).ToMat();
            Cv2.CvtColor(PictureBoxHelper.canvas, PictureBoxHelper.graycanvas, ColorConversionCodes.BGR2GRAY);
            chart1.Titles.Add("長條圖");
            chart1.Series.Add(series);
            series.Points.AddXY(1, 10);
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 255;

        }
        private void Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
                openFileDialog.Title = "打開圖片";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    PictureBoxHelper.UpdatePicturebox(pictureBox1, filePath);
                }
            }
        }
        bool isDrawing = false;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing&& e.Button== MouseButtons.Right)
            {
                PictureBoxHelper.DrawRight_Move();
            }
            label1.Text = e.X + " " + e.Y;
            OpenCvSharp.Point cur = PictureBoxHelper.GetPos(e.X,e.Y);
            series.Points[0].SetValueY(PictureBoxHelper.graycanvas.Get<byte>(cur.Y, cur.X));
            label2.Text = PictureBoxHelper.graycanvas.Get<byte>(cur.Y, cur.X).ToString();
            chart1.Series.ResumeUpdates();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDrawing = true;
                PictureBoxHelper.DrawRight_Down();
            }
        }
        Double ratio = 1.0;
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            label3.Text=PictureBoxHelper.target.Width.ToString();
            double ratioStep = 0.1;
            if (e.Delta > 0)
            {
                ratio += ratioStep;
                if (ratio > 3) // 放大上限
                    ratio = 3;
                else
                {
                    PictureBoxHelper.changePictureBoxSize(ratio, e,originsize);
                }
            }
            else
            {
                ratio -= ratioStep;
                if (ratio < 0.5)  // 放大下限
                    ratio = 0.5;
                else
                {
                    PictureBoxHelper.changePictureBoxSize(ratio, e, originsize);

                }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;
            }
        }
    }
}
