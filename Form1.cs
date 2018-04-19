using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.cmbM.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void Calculation(double t, out double x, out double y)
        {
            string m1 = cmbM.SelectedItem.ToString();
            int m = Convert.ToInt32(m1);
            x = 100 * (Math.Cos(4*t));
            y = 76 * (Math.Sin(m*t));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            double h = 0.02f, k_max = 0, tt, x1, y1, x2, y2;
            for (double t = 0; t <= 2 * Math.PI; t = t + h)
            {
                Calculation(t, out x2, out y2);
                if (t == 0)
                    k_max = (x2 > y2) ? x2 : y2;
                else
                {
                    tt = (x2 > y2) ? x2 : y2;
                    k_max = (tt > k_max) ? tt : k_max;
                }
            }
            int pict_size = (pctDraw.Width < pctDraw.Height) ? pctDraw.Width : pctDraw.Height;
            pict_size /= 2;
            //Подготовка к рисованию            
            Graphics g = pctDraw.CreateGraphics();
            g.Clear(button1.BackColor);
            g.ScaleTransform(pict_size / (float)k_max, pict_size / (float)k_max);
            g.TranslateTransform(pctDraw.Width / 2, pctDraw.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
            Pen p1 = new Pen(Color.Black);
            // Оси координат
            g.DrawLine(p1, -pctDraw.Width / 2, 0, pctDraw.Width / 2, 0);
            g.DrawLine(p1, 0, -pctDraw.Height / 2, 0, pctDraw.Height / 2);
            // Рисование графика функции
            if (textBox2.Text == "")
            {
                MessageBox.Show(this, "Введите толщину линии", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Stop); return;
            }
            double Size = Convert.ToDouble(textBox2.Text);
            Pen p = new Pen(button2.BackColor, (float)Size);
            Calculation(0, out x1, out y1);
            double xmax = 0, ymax = 0;
            for (double t = h; t <= 2 * Math.PI + h; t = t + h)
            {
                Calculation(t, out x2, out y2);
                g.DrawLine(p, (float)x1, (float)y1, (float)x2, (float)y2);
                x1 = x2;
                y1 = y2;
                xmax = (x1 > xmax) ? x1 : xmax;
                ymax = (y1 > ymax) ? y1 : ymax;
            }
            // Подпись осей координат
            g.DrawString((xmax).ToString(), new System.Drawing.Font("Calibri", 10), new SolidBrush(Color.Red), (float)xmax, 0);
            g.DrawString((ymax).ToString(), new System.Drawing.Font("Calibri", 10), new SolidBrush(Color.Red), 0, (float)-ymax);
        }
        //Изменение цвета фона
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = button1.BackColor;
            if (dlg.ShowDialog() == DialogResult.OK)
                button1.BackColor = dlg.Color;
        }
        
        //Выход
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
