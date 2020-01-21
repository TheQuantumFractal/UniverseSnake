using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace kamyar_snake
{
    public partial class Form1 : Form
    {
        Random gen = new Random();
        Graphics gfx;
        Snake snake;
        Food food;

        public Form1()
        {
            InitializeComponent();
        }
        public int slope;
        private void Form1_Load(object sender, EventArgs e)
        {
            gfx = CreateGraphics();
            snake = new Snake(new Point(ClientSize.Width / 2, ClientSize.Height / 2), new Size(20, 20), Color.ForestGreen);
            food = new Food(ClientSize, gen);    
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            snake.Erase(gfx, BackColor);
            food.Draw(gfx, BackColor);
            //int x = 0, y = 0;
            //for (int i = 50; i < 600; i += 5)
            //{
            //   y = (int)(Math.Sqrt((double)(Math.Abs(Math.Pow(10, 2) - Math.Pow(i, 2)))));
            //   gfx.FillRectangle(Brushes.IndianRed, i, y, 5, 5);
            //}

            snake.Move(ClientSize);
            //if intersect
            if (food.EllipseHit.IntersectsWith(new Rectangle(snake.pieces._location, snake.pieces._size)))
            {
                food.generate(ClientSize, gen);
                //if (snake.pieces.Count >= 2)
                //{
                //    for (int i = snake.pieces.Count - 1; i > 1; i--)
                //    {
                //        snake.pieces[i]._direction = snake.pieces[i - 1]._direction;
                //    }
                //}
                snake.pieces._size = new Size(2 * (int)(Math.Sqrt(Math.Pow((snake.pieces._size.Height / 2), 2) + Math.Pow((20 / 2), 2))), 2 * (int)(Math.Sqrt(Math.Pow((snake.pieces._size.Height / 2), 2) + Math.Pow((20 / 2), 2))));
            }
            if (!(food.x - snake.pieces._location.X < 1 && food.x - snake.pieces._location.X > -1 || food.y - snake.pieces._location.Y < 1 && food.y - snake.pieces._location.Y > -1))
            {
                slope = (int)(food.y - snake.pieces._location.Y) / (food.x - snake.pieces._location.X);
            }
            if (snake.pieces._size.Height >= 145)
            {
                snake.pieces._color = Color.Black;
                if (!(food.x - snake.pieces._location.X < 1 && food.x - snake.pieces._location.X > -1 || food.y - snake.pieces._location.Y < 1 && food.y - snake.pieces._location.Y > -1))
                {
                    food.y += 2*slope;
                    food.x += 2;
                }
            }
            else if(snake.pieces._size.Height >= 50)
            {
                snake.pieces._color = Color.Gold;
                if (!(food.x - snake.pieces._location.X < 1 && food.x - snake.pieces._location.X > -1 || food.y - snake.pieces._location.Y < 1 && food.y - snake.pieces._location.Y > -1))
                {
                    if (food.x - snake.pieces._location.X > 0)
                    {
                        food.y -= slope;
                        food.x -= 1;
                    }
                    else if (food.x - snake.pieces._location.X < 0)
                    {
                        food.y += slope;
                        food.x += 1;
                    }
                }
            }
            snake.Draw(gfx);
            food.Draw(gfx, Color.Blue);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                snake.pieces.Direct = SnakePiece.Direction.Right;
            }
            if (e.KeyCode == Keys.Left)
            {
                snake.pieces.Direct = SnakePiece.Direction.Left;
            }
            if (e.KeyCode == Keys.Up)
            {
                snake.pieces.Direct = SnakePiece.Direction.Up;
            }
            if (e.KeyCode == Keys.Down)
            {
                snake.pieces.Direct = SnakePiece.Direction.Down;
            }
        }
       
    }
}
