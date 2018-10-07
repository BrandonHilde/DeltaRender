using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using XDelta;

namespace DeltaRender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Eye view;
        Bitmap back;
        Graphics g;

        private void Form1_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            view = new Eye();
            Model torus = new Model("verty.obj");
            Model cube = new Cube(0, 0, 4, 4).GenerateModel();

            view.LoadWorldObjects(
                new Model[]
                {
                    cube,
                    torus
                });

            new Thread(() =>
            {
                OpenEye();
            }).Start();

        }

        public void BtcTrainingData()
        {

        }

        public void OpenEye()
        {
            back = view.GiveEyeGraphic(Size);
            g.DrawImage(back, Point.Empty);
        }

        public byte avPXL(byte r, byte g, byte b)
        {
            int av = (r + g + b) / 3;
            return (byte)av;
        }

        double mov = 0.33;

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                view.IncrementViewScalar(mov * 5);
            }

            if (e.KeyCode == Keys.E)
            {
                view.IncrementViewScalar(mov * -5);
            }

            if (e.KeyCode == Keys.W)
            {
                view.MovePerspective(0, mov);
            }

            if (e.KeyCode == Keys.A)
            {
                view.MovePerspective(mov, 0);
            }

            if (e.KeyCode == Keys.S)
            {
                view.MovePerspective(0, -mov);
            }

            if (e.KeyCode == Keys.D)
            {
                view.MovePerspective(-mov, 0);
            }

            if (e.KeyCode == Keys.X)
            {
                view.Reset();
            }

            OpenEye();
        }
    }
}
