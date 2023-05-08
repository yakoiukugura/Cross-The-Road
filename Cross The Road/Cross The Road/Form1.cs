using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cross_The_Road
{
    public partial class Form1 : Form
    {
        // variabilele pentru viteza masinilor, viteza gainii si nivelul curent
        int CAR_SPEED = 3;
        int CHICKEN_SPEED = 15;
        int LEVEL = 0;
        // calea catre fisierul imagine
        string PATH = @"Aici cheama-l pe Denis";

        // functia apelata la pornirea jocului
        public Form1()
        {
            InitializeComponent();
            // initializam orientarea imaginilor masinilor
            car1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            car2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            // activam timerul si dezactivam butoanele si titlul
            timer1.Enabled = true;

            menu.Enabled = false;
            menu.Visible = false;

            title.Enabled = false;
            title.Visible = false;

            start_button.Enabled = false;
            start_button.Visible = false;

            quit_button.Enabled = false;
            quit_button.Visible = false;

            // pozitionam gaina si setam viteza si nivelul la valori initiale
            chicken.Location = new Point(237, 473);
            CAR_SPEED = 3;
            LEVEL = 0;

            menu.BackgroundImage = null;
            // initializam imaginile masinilor
            car1.Image = Image.FromFile(PATH + "\\green_car.png");
            car2.Image = Image.FromFile(PATH + "\\green_car.png");
            // rotim imaginile masinilor
            car1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            car2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            // trecem la urmatorul nivel
            nextLevel();
        }

        private void quit_button_Click(object sender, EventArgs e)
        {
            // afisam un mesaj de confirmare si inchidem aplicatia daca se alege Yes
            if (MessageBox.Show("Are you sure you want to quit?", "Quit Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // miscam masinile si verificam daca s-au intersectat cu gaina
            car1.Left += CAR_SPEED;
            car2.Left -= CAR_SPEED;
            if (car1.Left > 557)
                car1.Left = -500;
            if (car2.Left + car2.Width < 0)
                car2.Left = 600;
            if (chicken.Bounds.IntersectsWith(car1.Bounds) || chicken.Bounds.IntersectsWith(car2.Bounds))
            {
                // daca s-a produs intersectia, jocul se incheie cu esec
                gameOver(false);
            }
            // daca gaina a ajuns la marginea superioara, se trece la urmatorul nivel sau se incheie jocul cu victorie
            if (chicken.Top + chicken.Height < 0)
            {
                if (LEVEL == 4)
                    gameOver(true);
                else
                    nextLevel();
            }

        }

        /* Functia nextLevel() creste nivelul de dificultate al jocului incrementand la final variabila SPEED. */
        private void nextLevel()
        {
            chicken.Location = new Point(237, 473);
            LEVEL++;
            //In functie de fiecare nivel masinile isi schimb culoarea:
            if (LEVEL == 1)
            {
                menu.BackColor = Color.Green;
                menu.Visible = true;
                title.Visible = true;
                title.Text = "Level: 1 (easy)";
                wait(2000);
                title.Visible = false;
                menu.Visible = false;
                car1.Image = Image.FromFile(PATH + "\\green_car.png");
                car2.Image = Image.FromFile(PATH + "\\green_car.png");
                car1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                car2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (LEVEL == 2)
            {
                menu.BackColor = Color.Blue;
                menu.Visible = true;
                title.Visible = true;
                title.Text = "Level: 2 (medium)";
                wait(2000);
                title.Visible = false;
                menu.Visible = false;
                car1.Image = Image.FromFile(PATH + "\\blue_car.png");
                car2.Image = Image.FromFile(PATH + "\\blue_car.png");
                car1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                car2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (LEVEL == 3)
            {
                menu.BackColor = Color.Purple;
                menu.Visible = true;
                title.Visible = true;
                title.Text = "Level: 3 (hard)";
                wait(2000);
                title.Visible = false;
                menu.Visible = false;
                car1.Image = Image.FromFile(PATH + "\\purple_car.png");
                car2.Image = Image.FromFile(PATH + "\\purple_car.png");
                car1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                car2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (LEVEL == 4)
            {
                menu.BackColor = Color.Red;
                menu.Visible = true;
                title.Visible = true;
                title.Text = "Level: 4 (extreme)";
                wait(2000);
                title.Visible = false;
                menu.Visible = false;
                car1.Image = Image.FromFile(PATH + "\\red_car.png");
                car2.Image = Image.FromFile(PATH + "\\red_car.png");
                car1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                car2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            CAR_SPEED += 2;
        }

        //Functia gameOver() reprezinta finalul jocului. In functie de parametrul "win" determinam daca jocul a fost castigat sau pierdut. 
        private void gameOver(bool win)
        {
            timer1.Enabled = false;

            menu.BackgroundImage = Image.FromFile(PATH + "\\background.png");
            menu.BackColor = Color.FromArgb(158, 205, 50);
            menu.Enabled = true;
            menu.Visible = true;

            if (win == true)
                title.Text = "You Win!";
            else
                title.Text = "Game Over!";
            title.Enabled = true;
            title.Visible = true;

            start_button.Text = "Play Again";
            start_button.Enabled = true;
            start_button.Visible = true;

            quit_button.Enabled = true;
            quit_button.Visible = true;
        }

        //Functia Form1_KeyDown() modifica pozitia gainii in functie de tasta apasata.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                chicken.Top -= CHICKEN_SPEED;
            else if (e.KeyCode == Keys.Down)
                chicken.Top += CHICKEN_SPEED;
            else if (e.KeyCode == Keys.Left)
                chicken.Left -= CHICKEN_SPEED;
            else if (e.KeyCode == Keys.Right)
                chicken.Left += CHICKEN_SPEED;
        }

        // Functia wait() opreste programul pentru o perioada scurta de timp specificata.
        // https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program
        public void wait(int milliseconds)
        {
            this.Enabled = false;
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                this.Enabled = true;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
