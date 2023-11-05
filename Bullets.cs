using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgerGame
{
    public class Bullet : Asset
    {

        public Bullet(Point center)
        {
            Center = center;
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bullet = new Rectangle(Center.X, Center.Y, 5, 10);
            g.FillRectangle(Brushes.Yellow, bullet);

            Rectangle = new Rectangle(Center.X, Center.Y, 5, 10);

            //Uncomment lines below to check the bullet rectangle
            //Pen pen = new Pen(Brushes.Blue, 2);
            //g.DrawRectangle(pen, bullet);
        }

        //This unique method makes the bullets move in just one direction (from bottom to top)
        public void MoveOnlyY()
        {
            int newY = Center.Y - MoveY;
            Center = new Point(Center.X, newY);
        }
    }
}
