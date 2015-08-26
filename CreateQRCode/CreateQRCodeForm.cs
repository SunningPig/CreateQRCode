using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.qrcode.decoder;

namespace CreateQRCode
{
    public partial class CreateQrCodeForm : Form
    {
        #region Initialization
        public CreateQrCodeForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            //create QRCode
            CreateQr();
            base.OnLoad(e);
        }
        #endregion

        #region Method
        private void CreateQr()
        {
            string text = @"http://www.cnblogs.com/xuliangxing/";
            QRCodeWriter writer = new QRCodeWriter();
            Hashtable ht = new Hashtable();
            ht.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            ht.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            ht.Add(EncodeHintType.VERSION_START, 5);
            Bitmap image = writer.encode(text, BarcodeFormat.QR_CODE, 400, 400, ht).ToBitmap();
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(image, 0, 0);
            image.Dispose();
            SizeF ef = new SizeF();

            #region 设置左上角特效颜色
            Bitmap bitmapLeftTop = SetBitmap(bitmap.Width, bitmap.Height);
            Color color = Color.FromArgb(200, 0xe0, 0x72, 1);
            int num = 122 - (Encoding.UTF8.GetBytes(text).Length - 20) / 2;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color leftTopColor;
                    Color pixel = bitmap.GetPixel(i, j);
                    if ((i < num) && (j < num))
                    {
                        leftTopColor = ((pixel.A == 0xff) && (pixel.B == 0)) ? color : pixel;
                    }
                    else
                    {
                        leftTopColor = ((pixel.A == 0xff) && (pixel.B == 0)) ? bitmapLeftTop.GetPixel(i, j) : pixel;
                    }
                    bitmap.SetPixel(i, j, leftTopColor);
                }
            }
            bitmapLeftTop.Dispose();
            #endregion

            #region 设置标题特效
            string str2 = "我的博客";
            float emSize = 18f;
            emSize -= (str2.Length - 4) * 1.8f;
            Font font = new Font("微软雅黑", emSize, FontStyle.Bold);
            ef = graphics.MeasureString(str2, font);
            float num7 = (bitmap.Width - ef.Width) / 2f;
            Brush brush = new SolidBrush(Color.FromArgb(0xff, 0x3a, 0xb2, 0xc2));
            Brush brush2 = new SolidBrush(Color.White);
            int y = 45;
            graphics.FillRectangle(brush2, new Rectangle((int)num7, y, (int)ef.Width - 3, (int)ef.Height - 3));
            graphics.DrawString(str2, font, brush, (float)((int)num7), (float)y);

            #endregion

            #region 设置中心特效
            Brush brush3 = new SolidBrush(Color.FromArgb(0xff, 0x3a, 0xb2, 0xc2));
            const int width = 140;
            graphics.FillEllipse(brush2, (bitmap.Width - width) / 2, (bitmap.Height - width) / 2, width, width);
            const int num10 = 0x80;
            graphics.FillEllipse(brush3, (bitmap.Width - num10) / 2, (bitmap.Height - num10) / 2, num10, num10);
            const int num11 = 110;
            graphics.FillEllipse(brush2, (bitmap.Width - num11) / 2, (bitmap.Height - num11) / 2, num11, num11);
            str2 = "Monks";
            float num12 = 32f;
            num12 -= (str2.Length - 3) * 3.5f;
            Font font2 = new Font("Meiryo", num12, FontStyle.Bold);
            ef = graphics.MeasureString(str2, font2);
            float x = ((bitmap.Width - ef.Width) / 2f) + 2f;
            float num14 = ((bitmap.Height - ef.Height) / 2f) + 8f;
            graphics.DrawString(str2, font2, brush3, x, num14);
            graphics.Dispose();

            #endregion

            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image.Dispose();
            }
            this.pictureBox1.Height = bitmap.Height;
            this.pictureBox1.Width = bitmap.Width;
            this.pictureBox1.Image = bitmap;
        }
        private static Bitmap SetBitmap(int a, int b)
        {
            Bitmap image = new Bitmap(a, b, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, a, b);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(230, 0x23, 0xa9, 160), Color.FromArgb(0xff, 8, 60, 0x63), LinearGradientMode.Vertical);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(brush, rect);
            graphics.Dispose();
            return image;
        }
        #endregion
    }
}
