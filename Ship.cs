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
            Brush brushWhite = new SolidBrush(Color.White);
            Brush brushBlue = new SolidBrush(Color.Blue);
            Graphics g = e.Graphics;

            Point[] pointsWings = new Point[6];

            pointsWings[0] = new Point(Center.X - 100, Center.Y);
            pointsWings[1] = new Point(Center.X - 90, Center.Y - 10);
            pointsWings[2] = new Point(Center.X, Center.Y - 20);
            pointsWings[3] = new Point(Center.X + 90, Center.Y - 10);
            pointsWings[4] = new Point(Center.X + 100, Center.Y);
            pointsWings[5] = new Point(Center.X, Center.Y + 20);

            g.FillPolygon(brushBlue, pointsWings);

            Point[] pointsGuns = new Point[8];

            pointsGuns[0] = new Point(Center.X - 70, Center.Y - 12);
            pointsGuns[1] = new Point(Center.X - 70, Center.Y - 25);
            pointsGuns[2] = new Point(Center.X - 67, Center.Y - 25);
            pointsGuns[3] = new Point(Center.X - 67, Center.Y - 12);
            pointsGuns[4] = new Point(Center.X + 67, Center.Y - 12);
            pointsGuns[5] = new Point(Center.X + 67, Center.Y - 25);
            pointsGuns[6] = new Point(Center.X + 70, Center.Y - 25);
            pointsGuns[7] = new Point(Center.X + 70, Center.Y - 12);

            g.FillPolygon(brushWhite, pointsGuns);

            Point[] pointsCabin = new Point[5];

            pointsCabin[0] = new Point(Center.X + 2, Center.Y - 100);
            pointsCabin[1] = new Point(Center.X + 20, Center.Y);
            pointsCabin[2] = new Point(Center.X, Center.Y + 20);
            pointsCabin[3] = new Point(Center.X - 20, Center.Y);
            pointsCabin[4] = new Point(Center.X - 2, Center.Y - 100);
            g.FillPolygon(brushWhite, pointsCabin);


            Rectangle = new Rectangle(Center.X - 90, Center.Y - 20, 180, 40);
            Fuselage = new Rectangle(Center.X - 15, Center.Y - 90, 30, 140);

            //e.Graphics.DrawRectangle(pen, Rectangle);  //Uncomment to check the rectangles area
            //e.Graphics.DrawRectangle(pen, Fuselage);


            Point[] pointsRearFuselage = new Point[4];

            pointsRearFuselage[0] = new Point(Center.X - 20, Center.Y);
            pointsRearFuselage[1] = new Point(Center.X + 20, Center.Y);
            pointsRearFuselage[2] = new Point(Center.X + 5, Center.Y + 50);
            pointsRearFuselage[3] = new Point(Center.X - 5, Center.Y + 50);

            g.FillPolygon(brushWhite, pointsRearFuselage);
            
        }
    }
}
