using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgerGame
{
    public class Bullets : Asset
    {

        public Bullets(Point center)
        {
            Center = center;
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bullet = new Rectangle(Center.X, Center.Y, 5, 10);
            g.FillRectangle(Brushes.Yellow, bullet);

            Rectangle = new Rectangle(Center.X, Center.Y, 5, 10);
            Pen pen = new Pen(Brushes.Blue, 2);
            g.DrawRectangle(pen, bullet);
        }

        public void MoveOnlyY()
        {
            int newY = Center.Y - MoveY;
            Center = new Point(Center.X, newY);
        }
    }
}
