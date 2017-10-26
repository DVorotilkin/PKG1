using System;
using System.Drawing;
using System.Windows.Forms;

namespace LAB1
{
    public partial class Form1 : Form
    {
        static Pen pen1;
        static Graphics g;
        static Pen pen2;

        public Form1()
        {
            InitializeComponent();
        }


        //рекурсивная функция рисования кривой Коха
        static int Fractal(PointF p1, PointF p2, PointF p3, int iter)
        {
            //n -количество итераций
            if (iter > 0)  //условие выхода из рекурсии
            {
                //средняя треть отрезка
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                //координаты вершины угла
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                //рисуем его
                g.DrawLine(pen2, p4, p5);
                g.DrawLine(pen1, p4, pn);
                g.DrawLine(pen1, p5, pn);
                //рекурсивно вызываем функцию нужное число раз
                Fractal(p4, pn, p5, iter - 1);
                Fractal(pn, p5, p4, iter - 1);
                Fractal(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iter - 1);
                Fractal(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iter - 1);
            }
            return iter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int NumOfIter = (int)numericUpDown1.Value;
            //Выбираем цвета зарисовки 
            pen1 = new Pen(Color.Red, 1);
            pen2 = new Pen(Color.White, 1);
            //Объявляем объект "g" класса Graphics
            g = CreateGraphics();
            g.Clear(Color.White);

            //Определим коорднаты исходного треугольника
            var point1 = new PointF(60, 80);
            var point2 = new PointF(360, 80);
            var point3 = new PointF(210, 290);

            //Зарисуем треугольник
            g.DrawLine(pen1, point1, point2);
            g.DrawLine(pen1, point2, point3);
            g.DrawLine(pen1, point3, point1);

            //Вызываем функцию Fractal для того, чтобы
            //нарисовать три кривых Коха на сторонах треугольника
            Fractal(point1, point2, point3, NumOfIter);
            Fractal(point2, point3, point1, NumOfIter);
            Fractal(point3, point1, point2, NumOfIter);
        }
    }
}

