using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureBox
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private double step;
        private List<int> num;
        private List<float> xx = new List<float>();
        private List<float> yy = new List<float>();
        private int xyStep;
        private string fileName;

        public Form1()
        {
            InitializeComponent();

            textBox1.Visible = true;
            textBox2.Text = "0";
            label1.Text = "Step = ";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            
            textBox1.Visible = true;

            graphics = pictureBox1.CreateGraphics();

            int w = pictureBox1.Width / 2;
            int h = pictureBox1.Height / 2;

            PointF[] point = new PointF[w];

            graphics.TranslateTransform(w, h);

            DrawXAxis(new Point(-w, 0), new Point(w, 0), graphics);
            DrawYAxis(new Point(0, h), new Point(0, -h), graphics);

            graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);

            for (double i = -step*5; i < step*5; i+=step/5)
            {
                xx.Add((float)i);
            }
            for (int i = 0; i < xx.Count; i++)
            {
                double res = 0;
                for (int j = 0; j < num.Count; j++)
                {
                    res += num[j] * Math.Pow(xx[i], num.Count - j - 1);
                }
                yy.Add(-(float)res);
            }
            for (int i = 0; i < xx.Count-1; i++)
            {
                float t = (float) (pictureBox1.Height / (10 * step));
                graphics.DrawLine(Pens.Black, xx[i]*t, yy[i]*t,xx[i+1]*t, yy[i+1]*t);
            }
        }

        private void DrawXAxis(Point start, Point end, Graphics g)
        {
            double xStep = step;
            for (int i = xyStep; i < end.X; i += xyStep)
            {
                g.DrawLine(Pens.Black, i, -5, i, 5);
                DrawText(new Point(i, 5), (xStep).ToString(), g);
                xStep += step;
            }

            xStep = -step;
            for (int i = -xyStep; i > start.X; i -= xyStep)
            {
                g.DrawLine(Pens.Black, i, -5, i, 5);
                DrawText(new Point(i, 5), (xStep).ToString(), g);
                xStep -= step;
            }

            g.DrawLine(Pens.Black, start, end);
        }

        private void DrawYAxis(Point start, Point end, Graphics g)
        {
            double yStep = step;
            for (int i = xyStep; i < start.Y; i += xyStep)
            {
                g.DrawLine(Pens.Black, -5, i, 5, i);
                DrawText(new Point(5, i), (-yStep).ToString(), g, true);
                yStep += step;
            }

            yStep = -step;
            for (int i = -xyStep; i > end.Y; i -= xyStep)
            {
                g.DrawLine(Pens.Black, -5, i, 5, i);
                DrawText(new Point(5, i), (-yStep).ToString(), g, true);
                yStep -= step;
            }

            g.DrawLine(Pens.Black, start, end);
        }

        private void DrawText(Point point, string text, Graphics g, bool isYAxis = false)
        {
            var f = new Font(Font.FontFamily, 6);
            var size = g.MeasureString(text, f);
            var pt = isYAxis
                ? new PointF(point.X + 1, point.Y - size.Height / 2)
                : new PointF(point.X - size.Width / 2, point.Y + 1);
            var rect = new RectangleF(pt, size);
            g.DrawString(text, f, Brushes.Black, rect);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar1.Value.ToString();
            step = trackBar1.Value;
            xyStep = pictureBox1.Height / 8;
        }


        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox2.Text) > 50)
                {
                    trackBar1.Value = 50;
                    textBox2.Text = "50";
                    step = 50;
                }

                else if (Int32.Parse(textBox2.Text) < 0)
                {
                    trackBar1.Value = 0;
                    textBox2.Text = "0";
                    step = 0;
                }

                else
                {
                    trackBar1.Value = Int32.Parse(textBox2.Text);
                    step = Int32.Parse(textBox2.Text);
                }
            }
            catch (Exception exception)
            {
                textBox2.Text = "0";
                step = 0;
                return;
            }

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            num = new List<int>();
            String str = textBox1.Text, res = "";
            char[] chars = str.ToCharArray();
            bool sign = false;

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != '!')
                {
                    if (chars[i] == '-')
                    {
                        sign = true;
                    }
                    else
                    {
                        res += chars[i];
                    }
                }

                if (chars[i] == '!')
                {
                    if (sign == true)
                    {
                        num.Add(-Int32.Parse(res));
                    }
                    else
                    {
                        num.Add(Int32.Parse(res));
                    }
                    sign = false;
                    res = null;
                }
            }

            String ss = null;
            for (int i = 0; i < num.Count; i++)
            {
                ss += num[i] + "x^" + (num.Count - i - 1);
                ss += " + ";
            }
            label2.Text = ss;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();

            textBox1.Visible = true;

            graphics = pictureBox1.CreateGraphics();

            int w = pictureBox1.Width / 2;
            int h = pictureBox1.Height / 2;

            PointF[] point = new PointF[w];

            graphics.TranslateTransform(w, h);

            DrawXAxis(new Point(-w, 0), new Point(w, 0), graphics);
            DrawYAxis(new Point(0, h), new Point(0, -h), graphics);

            graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
            
            fileName = textBox3.Text;
            StreamReader streamReader = new StreamReader(fileName, System.Text.Encoding.Default);
            string line = null, x = null, y = null;
            SortedDictionary<float, float> xyFile = new SortedDictionary<float, float>();

            while ((line = streamReader.ReadLine())!=null)
            {
                Console.WriteLine(line);
                x = line.Substring(0, line.IndexOf(" ")).Trim();
                y = line.Substring(line.IndexOf(" ")).Trim();
                xyFile.Add(float.Parse(x), float.Parse(y));
            }

            streamReader.Close();

            String xFile = "x: ", yFile = "y: ";
            foreach (var keys in xyFile)
            {
                xFile += keys.Key + " ";
                yFile += keys.Value + " ";
            }
            label3.Text = xFile + '\n' + yFile;

            float xX = 0;
            float yY = 0;
            foreach (var keys in xyFile)
            {
                xx.Add(keys.Key);
                yy.Add(keys.Value);
            }

            for (int i = 0; i < xx.Count - 1; i++)
            {
                float t = (float)(pictureBox1.Height / (step * 10));
                graphics.DrawLine(Pens.Black, xx[i] * t, yy[i] * t, xx[i + 1] * t, yy[i + 1] * t);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}