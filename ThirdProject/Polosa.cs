using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ThirdProject
{
    class Polosa
    {
        Rectangle rect;
        Color color;
        Random rand;

        public Polosa(int x, int y, int w, int h)
        {
            rand = new Random(x);
            rect = new Rectangle(x, y, w, h);
            color = Color.FromArgb(rand.Next(0, 240), rand.Next(0, 240), rand.Next(0, 240));
        }

        internal void Paint(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillRectangle(brush, rect);
            g.DrawRectangle(Pens.Black, rect);
        }

        internal bool isInside(int x, int y)
        {
            if(x < rect.Left || x > rect.Right)
            {
                return false;
            }
            if(y < rect.Top || y > rect.Bottom)
            {
                return false;
            }
            return true;
        }

        internal bool isOver(Polosa p)
        {
            return p.rect.IntersectsWith(this.rect);
        }

    }
}
