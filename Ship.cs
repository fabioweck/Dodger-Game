using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodgerGame
{
    public class Ship : Asset
    {
        public Rectangle Fuselage { get; set; }

        public Ship(Point center)
        {
            Center = center;
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Turquoise, 2);
            Graphics g = e.Graphics;

            //Defines engines
            Point[] pointsEngines = new Point[8];

            pointsEngines[0] = new Point(Center.X - 55, Center.Y - 15);
            pointsEngines[1] = new Point(Center.X - 55, Center.Y - 25);
            pointsEngines[2] = new Point(Center.X - 45, Center.Y - 25);
            pointsEngines[3] = new Point(Center.X - 45, Center.Y - 15);
            pointsEngines[4] = new Point(Center.X + 45, Center.Y - 15);
            pointsEngines[5] = new Point(Center.X + 45, Center.Y - 25);
            pointsEngines[6] = new Point(Center.X + 55, Center.Y - 25);
            pointsEngines[7] = new Point(Center.X + 55, Center.Y - 15);

            g.FillPolygon(Brushes.DarkMagenta, pointsEngines);


            //Defines wingspan and wings format
            Point[] pointsWings = new Point[8];

            pointsWings[0] = new Point(Center.X - 95, Center.Y + 7);
            pointsWings[1] = new Point(Center.X - 93, Center.Y);
            pointsWings[2] = new Point(Center.X - 80, Center.Y - 10);
            pointsWings[3] = new Point(Center.X, Center.Y - 30);
            pointsWings[4] = new Point(Center.X + 80, Center.Y - 10);
            pointsWings[5] = new Point(Center.X + 93, Center.Y);
            pointsWings[6] = new Point(Center.X + 95, Center.Y + 7);
            pointsWings[7] = new Point(Center.X, Center.Y + 15);

            g.FillPolygon(Brushes.DarkGray, pointsWings);


            //Defines cabin/main fuselage
            Point[] pointsCabin = new Point[5];

            pointsCabin[0] = new Point(Center.X + 2, Center.Y - 100);
            pointsCabin[1] = new Point(Center.X + 25, Center.Y);
            pointsCabin[2] = new Point(Center.X, Center.Y + 20);
            pointsCabin[3] = new Point(Center.X - 25, Center.Y);
            pointsCabin[4] = new Point(Center.X - 2, Center.Y - 100);
            g.FillPolygon(Brushes.White, pointsCabin);


            //Defines wing area and fuselage area to collide with asteroids
            Rectangle = new Rectangle(Center.X - 90, Center.Y - 15, 180, 25);
            Fuselage = new Rectangle(Center.X - 13, Center.Y - 100, 26, 140);

            //Uncomment to check the rectangles area that collide with asteroids
            //e.Graphics.DrawRectangle(pen, Rectangle);  
            //e.Graphics.DrawRectangle(pen, Fuselage);


            //Defines rear fuselage
            Point[] pointsRearFuselage = new Point[7];

            pointsRearFuselage[0] = new Point(Center.X - 25, Center.Y);
            pointsRearFuselage[1] = new Point(Center.X + 25, Center.Y);
            pointsRearFuselage[2] = new Point(Center.X + 15, Center.Y + 30);
            pointsRearFuselage[3] = new Point(Center.X + 5, Center.Y + 40);
            pointsRearFuselage[4] = new Point(Center.X, Center.Y + 40);
            pointsRearFuselage[5] = new Point(Center.X - 5, Center.Y + 40);
            pointsRearFuselage[6] = new Point(Center.X - 15, Center.Y + 30);

            g.FillPolygon(Brushes.White, pointsRearFuselage);

            //Defines pilot cabin/canopy
            Point[] canopy = new Point[6];

            canopy[0] = new Point(Center.X - 3, Center.Y - 15);
            canopy[1] = new Point(Center.X - 7, Center.Y - 25);
            canopy[2] = new Point(Center.X - 2, Center.Y - 55);
            canopy[3] = new Point(Center.X + 2, Center.Y - 55);
            canopy[4] = new Point(Center.X + 7, Center.Y - 25);
            canopy[5] = new Point(Center.X + 3, Center.Y - 15);

            g.FillPolygon(Brushes.DarkMagenta, canopy);

        }
    }
}
