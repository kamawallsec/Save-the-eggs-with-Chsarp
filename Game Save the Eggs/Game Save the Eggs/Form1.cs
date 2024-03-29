﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Save_the_Eggs
{
    public partial class Form1 : Form
    {

        bool goRight, goLeft;

        int speed = 8;
        int score = 0;
        int missed = 0;

        Random randX = new Random();
        Random randY = new Random();

        PictureBox splash = new PictureBox();


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {

            txtScore.Text = "Saved: " + score;
            txtMissed.Text = "Missed: " + missed;


            if (goLeft == true && player.Left > 0)
            {
                player.Left -= 12;
                player.Image = Properties.Resources.chc;
            }


            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 12;
                player.Image = Properties.Resources.chc2;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top += speed;


                    if (x.Top + x.Height > this.ClientSize.Height)
                    {

                        splash.Image = Properties.Resources.splash;
                        splash.Location = x.Location;
                        splash.Height = 60;
                        splash.Width = 60;
                        splash.BackColor = Color.Transparent;

                        this.Controls.Add(splash);


                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);

                        missed += 1;
                        player.Image = Properties.Resources.chichken_hurt;
                    }

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        score += 1;
                    } 



                }
            }


            if (score > 20 )
            {
                speed = 12;
            }

            if ( missed  > 10)
            {
                gameTimer.Stop();
                MessageBox.Show("Game Over!!!" + Environment.NewLine + "You Lost!!!" + Environment.NewLine + 
                    "Click OK to Retry!!!") ;
                RestartGame();
            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }




        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

        }



        private void RestartGame ()
        {
            foreach (Control x in this.Controls) // Look for the eggs in the game
            {
                if (x is PictureBox && (string)x.Tag == "eggs")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5, this.ClientSize.Width - x.Width); 
                }
            }

            player.Left = this.ClientSize.Width / 2;
            player.Image = Properties.Resources.chc;

            score = 0;
            missed = 0;
            speed = 8;

            goLeft = false;
            goRight = false;

            gameTimer.Start();
        }

    }
}
