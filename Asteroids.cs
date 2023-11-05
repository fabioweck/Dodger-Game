using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgerGame
{
    public class Asteroids : Asset
    {
        Pen pen = new Pen(Color.Goldenrod, 2);
        public int Radius {  get; set; }

        public Asteroids(Point center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public override void Draw(PaintEventArgs e)
        {
            Rectangle = new Rectangle(Center.X, Center.Y, Radius, Radius);
            e.Graphics.DrawEllipse(pen, Rectangle);
        }
        
        public bool Collision(Ship ship)
        {
            if (Rectangle.IntersectsWith(ship.Rectangle) || Rectangle.IntersectsWith(ship.Fuselage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Shot(Bullet bullet)
        {
            if (Rectangle.IntersectsWith(bullet.Rectangle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
