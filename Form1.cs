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
        List<Bullet> bullets;
        int counter = 0;
        int collisions = 0;
        int score = 0;
        Font scoreFont = new Font("Verdana", 25, FontStyle.Bold);
 

        public Form1()
        {
            InitializeComponent();
            bullets = new List<Bullet>(); //Initializes the list of bullets
            this.DoubleBuffered = true;
            this.Location = new Point(10, 10);
            
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

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameLoop.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gameInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The player moves the ship with arrow keys and shoots with space bar.\nIf the ship reaches any of the screen borders a teleportation occurs moving the ship to the opposite side of the screen.\nDuring the game, the player must avoid colliding the ship with asteroids on the screen in order to maintain health as much as possible.\nPoints increase over time and, as a bonus, every 15 seconds the health increases by 5%.\nGame is over when the health is completely depleted.", "How to play");
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

            //Iterates over all asteroids
            int assetIndexAsteroids = asteroidField.Count - 1;
            while (assetIndexAsteroids > 0)
            {
                //Checks collision between an asteroid and the ship
                //If a collision is detected, the asteroid is removed from the list
                //Another asteroid is added afterwards
                if (asteroidField[assetIndexAsteroids].Collision(player))
                {
                    asteroidField.RemoveAt(assetIndexAsteroids);
                    collisions++;
                    this.BackColor = Color.FromArgb(255, 75, 0, 0);
                    GenerateNewAsteroid();

                    //If the player reaches the limit of collisions, the game finishes
                    if (collisions == 100)
                    {
                        EndGame();
                    }
                }

                else
                {
                    //Draws the asteroid and then check if it's been shot by bullet
                    asteroidField[assetIndexAsteroids].Draw(e);

                    //This foreach iterates over list of bullets
                    foreach (Bullet bullet in bullets)
                    {
                        //If the asteroid has been shot, it is removed from the list
                        //And the bullet is set out of the border, instead of being removed
                        //When the bullet crosses the split border, another loop inside GameLoop_Tick
                        //removes it from the list
                        if (asteroidField[assetIndexAsteroids].Shot(bullet))
                        {
                            asteroidField.RemoveAt(assetIndexAsteroids);
                            bullet.Center = new Point(bullet.Center.X, 0);
                            GenerateNewAsteroid();
                        }
                        else
                        {    
                            bullet.Draw(e);
                        }
                    }
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
            
            foreach(Bullet bullet in bullets)
            {
                bullet.MoveOnlyY();
            }

            //This verifies if a bullet crossed the game border, if yes, removes the bullet 
            int assetIndexBullets = bullets.Count - 1;
            while (assetIndexBullets >= 0)
            {
                if (bullets[assetIndexBullets].Center.Y < 100)
                {
                    bullets.RemoveAt(assetIndexBullets);
                }
                assetIndexBullets--;
            }

            counter++;

            if(counter >= 60)
            {
                counter = 0;
                score++;
            }

            if(score != 0 && score % 15 == 0 && counter == 0)
            {
                if(400 - (collisions * 4) > 380)
                {
                    collisions = 0;
                }
                else
                {
                    collisions -= 5;
                }
    
            }

            this.BackColor = Color.Black;
            this.Refresh();
        }

        private void EndGame()
        {
            GameLoop.Stop();
            DialogResult = MessageBox.Show($"You finished the game with {score} points\nWould you like to play again?", "Game over!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if(DialogResult == DialogResult.Yes)
            {
                player = new Ship(new Point(600, 700));
                asteroidField.Clear();
                bullets.Clear();
                Initialize();
                score = 0;
                collisions = 0;
                GameLoop.Start();
            }
            else
            {
                Application.Exit();
            }
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
            Bullet newBullet = new Bullet(new Point(player.Center.X, player.Center.Y - 90));
            newBullet.MoveY = 15;
            bullets.Add(newBullet);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) player.MoveX = 0;
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) player.MoveY = 0; 
        }
    }
}
