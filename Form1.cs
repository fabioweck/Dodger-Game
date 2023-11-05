using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DodgerGame
{
    public partial class Form1 : Form
    {

        Ship player = new Ship(new Point(600, 700));
        List<Asteroids> asteroidField;
        List<Bullets> bullets;
        int counter = 0;
        int collisions = 0;
        int score = 0;
        Font scoreFont = new Font("Verdana", 25, FontStyle.Bold);
 

        public Form1()
        {
            InitializeComponent();
            bullets = new List<Bullets>();
            this.StartPosition = FormStartPosition.CenterParent;
            this.DoubleBuffered = true;
        }

        private void Initialize()
        {
            int[] movement = { -4, -3, -2, -1, 1, 2, 3, 4 };

            Random rand = new Random();

            asteroidField = new List<Asteroids>();

            while (asteroidField.Count <= 20)
            {
                int asteroidX = rand.Next(100, 1100);
                int asteroidY = rand.Next(100, 600);
                int radius = rand.Next(10, 100);

                Asteroids asteroid = new Asteroids(new Point(asteroidX, asteroidY), radius);
                asteroid.MoveX = movement[rand.Next(movement.Length - 1)];
                asteroid.MoveY = movement[rand.Next(movement.Length - 1)];

                asteroidField.Add(asteroid);
            }
        }

        private void GenerateNewAsteroid()
        {
            int[] movement = { -4, -3, -2, -1, 1, 2, 3, 4 };

            Random rand = new Random();

            int asteroidX = rand.Next(100, 1100);
            int asteroidY = rand.Next(100, 600);
            int radius = rand.Next(20, 80);

            Asteroids asteroid = new Asteroids(new Point(asteroidX, asteroidY), radius);
            asteroid.MoveX = movement[rand.Next(movement.Length - 1)];
            asteroid.MoveY = movement[rand.Next(movement.Length - 1)];

            asteroidField.Add(asteroid);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameLoop.Interval = 10;
            GameLoop.Start();

            Initialize(); //Draws asteroids on the screen

            this.Size = new Size(1200, 900);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.Black;

            this.Paint += new PaintEventHandler(this.PaintObjects);
        }

        protected void PaintObjects(Object sender,  PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White, 2);

            Rectangle rectangle = new Rectangle(100, 100, 1000, 700);

            e.Graphics.DrawRectangle(pen, rectangle);

            Region innerLimits = new Region(rectangle);

            e.Graphics.Clip = innerLimits;

            player.Draw(e);

            int assetIndexAsteroids = asteroidField.Count - 1;
            int assetIndexBullets = bullets.Count - 1;

            while (assetIndexAsteroids > 0)
            {
                if(assetIndexBullets < 0)
                {
                    if (asteroidField[assetIndexAsteroids].Collision(player))
                    {
                        asteroidField.RemoveAt(assetIndexAsteroids);
                        collisions++;
                        GenerateNewAsteroid();
                        if (collisions == 100)
                        {
                            EndGame();
                        }
                    }
                    else
                    {
                        asteroidField[assetIndexAsteroids].Draw(e);
                    }
                }
                else
                { 
                    for(int i = 0; i <= assetIndexBullets; i++)
                    {
                        if (asteroidField[assetIndexAsteroids].Shot(bullets[i]))
                        {
                            asteroidField.RemoveAt(assetIndexAsteroids);
                        }
                        else
                        {
                            bullets[assetIndexBullets].Draw(e);
                            asteroidField[assetIndexAsteroids].Draw(e);
                        }
                    }

                    //foreach(Bullets bullet in bullets)
                    //{
                    //    
                    //    else
                    //    {
                    //        asteroidField[assetIndexAsteroids].Draw(e);
                    //        bullets[assetIndexBullets].Draw(e);
                    //    }
                    //}    



                    //else
                    //{
                    //    asteroidField[assetIndexAsteroids].Draw(e);
                    //    bullets[assetIndexBullets].Draw(e);
                    //}

                    //if (asteroidField[assetIndexAsteroids].Collision(player))
                    //{   
                    //    asteroidField.RemoveAt(assetIndexAsteroids);
                    //    collisions++;
                    //    GenerateNewAsteroid();
                    //    if (collisions == 100)
                    //    {
                    //        EndGame();
                    //    }
                    //} 

                }

               assetIndexAsteroids--;

            }
            
            e.Graphics.ResetClip();

            e.Graphics.DrawString($"Score: {score}", scoreFont, Brushes.White, new Point(200, 50));

            Rectangle healthFrame = new Rectangle(598, 55, 404, 34);
            e.Graphics.DrawRectangle(pen, healthFrame);

            Rectangle health = new Rectangle(600, 57, 400 - (collisions * 4), 30);
            Rectangle damage = new Rectangle(1000 - (collisions * 4), 57, 0 + (collisions * 4), 30);

            e.Graphics.FillRectangle(Brushes.Green, health);
            e.Graphics.FillRectangle(Brushes.Red, damage);

        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            player.Move(100, 1100, 100, 800);

            foreach(Asset asteroid in asteroidField)
            {
                asteroid.Move(0, this.Size.Width, 0, this.Size.Height);
            }

            foreach(Bullets bullet in bullets)
            {
                bullet.MoveOnlyY();
            }

            counter++;

            if(counter >= 60)
            {
                counter = 0;
                score++;
            }

            if(score != 0 && score % 10 == 0 && counter == 0)
            {
                if(1000 - (collisions * 4) > 400)
                {
                    collisions = 0;
                }
                else
                {
                    collisions -= 5;
                }
    
            }

            this.Refresh();
        }

        private void EndGame()
        {
            GameLoop.Stop();
            MessageBox.Show("You die");
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                player.MoveX = -2;
            }
            if (e.KeyCode == Keys.Right)
            {
                player.MoveX = 2;
            }
            if (e.KeyCode == Keys.Up)
            {
                player.MoveY = -2;
            }
            if (e.KeyCode == Keys.Down)
            {
                player.MoveY = 2;
            }
            if(e.KeyCode == Keys.Space)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Bullets newBullet = new Bullets(new Point(player.Center.X, player.Center.Y - 90));
            newBullet.MoveY = 10;
            bullets.Add(newBullet);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) player.MoveX = 0;
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) player.MoveY = 0; 
        }
    }
}
