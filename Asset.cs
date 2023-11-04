using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgerGame
{
    public abstract class Asset
    {
        public Point Center { get; set; }
        public Rectangle Rectangle {  get; set; }

        public int MoveX { get; set; } = 0;
        public int MoveY { get; set; } = 0;

        public abstract void Draw(PaintEventArgs e);

        public virtual void Move(int x1, int x2, int y1, int y2)
        {
            int newX = Center.X + MoveX;

            if(newX < x1) newX = x2;
            else if (newX > x2) newX = x1;

            int newY = Center.Y + MoveY;

            if (newY < y1) newY = y2;
            else if (newY > y2) newY = y1;

            Center = new Point(newX, newY);
        }
    }
}
