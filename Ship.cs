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

            g.FillPolygon(Brushes.Gray, pointsWings);


            //Defines cabin/main fuselage
            Point[] pointsCabin = new Point[6];

            pointsCabin[0] = new Point(Center.X + 7, Center.Y - 95);
            pointsCabin[1] = new Point(Center.X + 25, Center.Y);
            pointsCabin[2] = new Point(Center.X, Center.Y + 10);
            pointsCabin[3] = new Point(Center.X - 25, Center.Y);
            pointsCabin[4] = new Point(Center.X - 7, Center.Y - 95);
            pointsCabin[5] = new Point(Center.X, Center.Y - 100);

            g.FillPolygon(Brushes.DarkGray, pointsCabin);

            //Defines the afterburner
            Point[] burnerYellow = new Point[11];

            burnerYellow[0] = new Point(Center.X - 15, Center.Y + 30);
            burnerYellow[1] = new Point(Center.X + 15, Center.Y + 30);
            burnerYellow[2] = new Point(Center.X + 20, Center.Y + 60);
            burnerYellow[3] = new Point(Center.X + 10, Center.Y + 52);
            burnerYellow[4] = new Point(Center.X + 7, Center.Y + 59);
            burnerYellow[5] = new Point(Center.X + 3, Center.Y + 52);
            burnerYellow[6] = new Point(Center.X, Center.Y + 60);
            burnerYellow[7] = new Point(Center.X - 3, Center.Y + 52);
            burnerYellow[8] = new Point(Center.X - 7, Center.Y + 59);
            burnerYellow[9] = new Point(Center.X - 10, Center.Y + 52);
            burnerYellow[10] = new Point(Center.X - 20, Center.Y + 60);

            g.FillPolygon(Brushes.Yellow, burnerYellow);

            Point[] burnerRed = new Point[11];

            burnerRed[0] = new Point(Center.X - 9, Center.Y + 20);
            burnerRed[1] = new Point(Center.X + 9, Center.Y + 20);
            burnerRed[2] = new Point(Center.X + 12, Center.Y + 50);
            burnerRed[3] = new Point(Center.X + 10, Center.Y + 42);
            burnerRed[4] = new Point(Center.X + 7, Center.Y + 49);
            burnerRed[5] = new Point(Center.X + 3, Center.Y + 42);
            burnerRed[6] = new Point(Center.X, Center.Y + 50);
            burnerRed[7] = new Point(Center.X - 3, Center.Y + 42);
            burnerRed[8] = new Point(Center.X - 7, Center.Y + 49);
            burnerRed[9] = new Point(Center.X - 10, Center.Y + 42);
            burnerRed[10] = new Point(Center.X - 12, Center.Y + 50);

            g.FillPolygon(Brushes.Red, burnerRed);

            //Defines rear fuselage
            Point[] pointsRearFuselage = new Point[7];

            pointsRearFuselage[0] = new Point(Center.X - 25, Center.Y);
            pointsRearFuselage[1] = new Point(Center.X + 25, Center.Y);
            pointsRearFuselage[2] = new Point(Center.X + 15, Center.Y + 30);
            pointsRearFuselage[3] = new Point(Center.X + 5, Center.Y + 40);
            pointsRearFuselage[4] = new Point(Center.X, Center.Y + 40);
            pointsRearFuselage[5] = new Point(Center.X - 5, Center.Y + 40);
            pointsRearFuselage[6] = new Point(Center.X - 15, Center.Y + 30);

            g.FillPolygon(Brushes.DarkGray, pointsRearFuselage);

            //Defines pilot cabin/canopy
            Point[] pointsCanopy = new Point[6];

            pointsCanopy[0] = new Point(Center.X - 3, Center.Y - 15);
            pointsCanopy[1] = new Point(Center.X - 8, Center.Y - 25);
            pointsCanopy[2] = new Point(Center.X - 3, Center.Y - 55);
            pointsCanopy[3] = new Point(Center.X + 3, Center.Y - 55);
            pointsCanopy[4] = new Point(Center.X + 8, Center.Y - 25);
            pointsCanopy[5] = new Point(Center.X + 3, Center.Y - 15);

            g.FillPolygon(Brushes.DarkMagenta, pointsCanopy);

            //Defines wing area and fuselage area to collide with asteroids
            Rectangle = new Rectangle(Center.X - 90, Center.Y - 15, 180, 25);
            Fuselage = new Rectangle(Center.X - 13, Center.Y - 95, 26, 150);

            //Uncomment the lines below to check the rectangles area that collide with asteroids
            //e.Graphics.DrawRectangle(pen, Rectangle);
            //e.Graphics.DrawRectangle(pen, Fuselage);

        }
    }
}
