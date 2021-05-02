using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdProject
{
    public partial class Form1 : Form
    {
        List<Polosa> polosaList;
        Random rand;
        Timer timer; 
        const int w = 150, h = 50 ; // Ширина и высота полосок
        int max;
        int N = 10; // Число полосок в начале

        public Form1()
        {
            InitializeComponent();
            max = w > h ? w : h;
            polosaList = new List<Polosa>();
            rand = new Random();
            timer = new Timer();
            timer.Interval = 550;
            timer.Tick += Timer_tick;
            Reset();
            
        }

        private void PolosaAdd(int n)
        {
            for (int i = 0; i < n; i++)
            {
                int x = rand.Next(ClientSize.Width - max);
                int y = rand.Next(ClientSize.Height - max);
                Random dir = new Random(x);
                if(dir.Next(0,2) == 1)
                {
                    polosaList.Add(new Polosa(x, y, w, h));
                }
                else
                {
                    polosaList.Add(new Polosa(x, y, h, w));
                }
            }
        }

        private void Reset()
        {
            polosaList.Clear();
            timer.Start();
            PolosaAdd(N);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int idel = -1;
            for (int i = 0; i < polosaList.Count; i++)
            {
                if (polosaList[i].isInside(e.X, e.Y))
                {
                    idel = i;
                }
            }

            if (idel == -1) return;

           for(int i = idel + 1; i < polosaList.Count; i++)
            {
                if (polosaList[i].isOver(polosaList[idel])) return;
            }

            polosaList.Remove(polosaList[idel]);
            Invalidate();
                       
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();
            g.Clear(Color.Aqua);

            if(polosaList.Count == 0)
            {
                timer.Stop();
                DialogResult result = MessageBox.Show("Вы выиграли. Начать новую игру ?",
                    "Win", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Reset();
                else
                    Application.Exit();

            }
            else if(polosaList.Count == N * 2)
            {
                timer.Stop();
                DialogResult result = MessageBox.Show("Вы проиграли. Начать новую игру ?",
                   "Failed", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Reset();
                else
                    Application.Exit();
            }

            foreach(Polosa pol in polosaList)
            {
                pol.Paint(g);
            }
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            PolosaAdd(1);
            Invalidate();
        }
    }
}
