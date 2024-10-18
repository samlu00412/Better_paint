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
namespace Better_paint {
    public partial class Form1 : Form {
        MatMethod PictureBoxHelper = new MatMethod();
        public Form1() {
            InitializeComponent();
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
                    //PictureBoxHelper.canvas = Cv2.ImRead(filePath);
                    PictureBoxHelper.UpdatePicturebox(pictureBox1, filePath);
                }
            }
        }

    }
}
