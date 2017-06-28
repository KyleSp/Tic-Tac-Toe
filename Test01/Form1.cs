using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Test01
{
    public partial class Form1 : Form
    {
        const int BUTTON_ORIGIN_X = 12;
        const int BUTTON_ORIGIN_Y = 12;
        const int BUTTON_SIZE = 100;
        const int BUTTON_GAP = 5;
        const int BUTTON_FONT_SIZE = 50;

        public int BOARD_SIZE_X { get; set; }
        public int BOARD_SIZE_Y { get; set; }
        public int NUM_TO_WIN { get; set; }

        public int FORM_SIZE_X { get; set; }
        public int FORM_SIZE_Y { get; set; }

        /*
        const int BOARD_SIZE_X = 4;
        const int BOARD_SIZE_Y = 3;

        const int NUM_TO_WIN = 4;

        const int BUTTON_ORIGIN_X = 12;
        const int BUTTON_ORIGIN_Y = 12;
        const int BUTTON_SIZE = 100;
        const int BUTTON_GAP = 5;
        const int BUTTON_FONT_SIZE = 50;

        const int FORM_SIZE_X = 352 + (BOARD_SIZE_X - 3) * (BUTTON_SIZE + BUTTON_GAP);
        const int FORM_SIZE_Y = 372 + (BOARD_SIZE_Y - 3) * (BUTTON_SIZE + BUTTON_GAP) + 50;
        */

        public char currentPlayer { get; set; }

        private List<List<CustomButton>> buttons;

        private Label labelWinner;

        public Form1(int boardSizeX, int boardSizeY, int numToWin)
        {
            this.BOARD_SIZE_X = boardSizeX;
            this.BOARD_SIZE_Y = boardSizeY;
            this.NUM_TO_WIN = numToWin;

            this.FORM_SIZE_X = 352 + (BOARD_SIZE_X - 3) * (BUTTON_SIZE + BUTTON_GAP);
            this.FORM_SIZE_Y = 372 + (BOARD_SIZE_Y - 3) * (BUTTON_SIZE + BUTTON_GAP) + 50;

            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Size = new System.Drawing.Size(FORM_SIZE_X, FORM_SIZE_Y);

            this.Text = "Tic-Tac-Toe";

            currentPlayer = 'X';

            buttons = new List<List<CustomButton>>();

            Font buttonFont = new Font("Arial", BUTTON_FONT_SIZE, FontStyle.Bold);

            for (int i = 0; i < BOARD_SIZE_X; ++i)
            {
                List<CustomButton> row = new List<CustomButton>();
                for (int j = 0; j < BOARD_SIZE_Y; ++j)
                {
                    CustomButton button = new CustomButton(i, j);
                    button.Font = buttonFont;
                    button.Text = "";

                    button.Size = new System.Drawing.Size(BUTTON_SIZE, BUTTON_SIZE);
                    button.Location = new System.Drawing.Point(BUTTON_ORIGIN_X + i * BUTTON_SIZE + i * BUTTON_GAP, BUTTON_ORIGIN_Y + j * BUTTON_SIZE + j * BUTTON_GAP);

                    button.Click += new EventHandler(this.buttonClicked);

                    row.Add(button);

                    this.Controls.Add(button);
                }
                buttons.Add(row);
            }


            Font buttonResetFont = new Font("Arial", 18, FontStyle.Bold);

            Button buttonReset = new Button();
            buttonReset.Font = buttonResetFont;
            buttonReset.Text = "Reset";

            buttonReset.Size = new System.Drawing.Size(BUTTON_SIZE, 50);
            buttonReset.Location = new System.Drawing.Point(BUTTON_ORIGIN_X, FORM_SIZE_Y - 95);

            buttonReset.Click += new EventHandler(this.buttonResetClicked);

            this.Controls.Add(buttonReset);

            labelWinner = new Label();
            labelWinner.Font = buttonResetFont;
            labelWinner.Text = "";
            labelWinner.Size = new System.Drawing.Size(2 * BUTTON_SIZE + BUTTON_GAP, 50);
            labelWinner.Location = new System.Drawing.Point(BUTTON_ORIGIN_X + 100 + BUTTON_GAP, FORM_SIZE_Y - 85);

            this.Controls.Add(labelWinner);
        }

        private void switchCurrentPlayer()
        {
            if (currentPlayer == 'X')
            {
                currentPlayer = 'O';
            }
            else
            {
                currentPlayer = 'X';
            }
        }

        private char checkWinner(int i, int j)
        {
            //Console.WriteLine(i + "," + j + ": " + buttons[i][j].owner);

            int count = 1;
            char owner = buttons[i][j].owner;
            int k;

            //check horizontal
            k = -1;
            while (i + k >= 0)
            {
                if (buttons[i + k][j].owner == owner)
                {
                    //Console.WriteLine("hit at " + (i + k) + "," + j);
                    ++count;
                } else
                {
                    break;
                }

                --k;
            }

            k = 1;
            while (i + k < BOARD_SIZE_X)
            {
                if (buttons[i + k][j].owner == owner)
                {
                    //Console.WriteLine("hit at " + (i + k) + "," + j);
                    ++count;
                } else
                {
                    break;
                }

                ++k;
            }

            if (count >= NUM_TO_WIN)
            {
                return owner;
            }

            //check vertical
            k = -1;
            count = 1;
            while (j + k >= 0)
            {
                if (buttons[i][j + k].owner == owner)
                {
                    ++count;
                } else
                {
                    break;
                }

                --k;
            }

            k = 1;
            while (j + k < BOARD_SIZE_Y)
            {
                if (buttons[i][j + k].owner == owner)
                {
                    ++count;
                }
                else
                {
                    break;
                }

                ++k;
            }

            if (count >= NUM_TO_WIN)
            {
                return owner;
            }

            //check neg diagonal
            k = -1;
            count = 1;
            while (i + k >= 0 && j + k >= 0)
            {
                if(buttons[i + k][j + k].owner == owner)
                {
                    ++count;
                }
                else
                {
                    break;
                }

                --k;
            }

            k = 1;
            while (i + k < BOARD_SIZE_X && j + k < BOARD_SIZE_Y)
            {
                if (buttons[i + k][j + k].owner == owner)
                {
                    ++count;
                }
                else
                {
                    break;
                }

                ++k;
            }

            if (count >= NUM_TO_WIN)
            {
                return owner;
            }

            //check pos diagonal
            k = -1;
            int m = 1;
            count = 1;
            while (i + k >= 0 && j + m < BOARD_SIZE_Y)
            {
                if (buttons[i + k][j + m].owner == owner)
                {
                    ++count;
                }
                else
                {
                    break;
                }

                --k;
                ++m;
            }

            k = 1;
            m = -1;
            while (i + k < BOARD_SIZE_X && j + m >= 0)
            {
                if (buttons[i + k][j + m].owner == owner)
                {
                    ++count;
                }
                else
                {
                    break;
                }

                ++k;
                --m;
            }

            if (count >= NUM_TO_WIN)
            {
                return owner;
            }

            return 'N';
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            CustomButton button = (CustomButton)sender;

            if (button.owner == ' ' && labelWinner.Text == "")
            {
                Console.WriteLine("player " + currentPlayer + " clicked at " + button.x + "," + button.y);

                button.setOwner(currentPlayer);

                switchCurrentPlayer();

                //check for winner
                char winner = checkWinner(button.x, button.y);
                if (winner == 'X')
                {
                    Console.WriteLine("Player X won!");
                    labelWinner.Text = "Player X won!";
                }
                else if (winner == 'O')
                {
                    Console.WriteLine("Player O won!");
                    labelWinner.Text = "Player O won!";
                }
                else
                {
                    //check for tie
                    int count = 0;
                    for (int i = 0; i < BOARD_SIZE_X; ++i)
                    {
                        for (int j = 0; j < BOARD_SIZE_Y; ++j)
                        {
                            if (buttons[i][j].owner != ' ')
                            {
                                ++count;
                            }
                        }
                    }

                    if (count == BOARD_SIZE_X * BOARD_SIZE_Y)
                    {
                        Console.WriteLine("There is a tie!");
                        labelWinner.Text = "There is a tie!";
                    }
                }
            }
        }

        private void buttonResetClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Reset");

            labelWinner.Text = "";

            for (int i = 0; i < BOARD_SIZE_X; ++i)
            {
                for (int j = 0; j < BOARD_SIZE_Y; ++j)
                {
                    buttons[i][j].setOwner(' ');
                    buttons[i][j].Text = "";
                    currentPlayer = 'X';
                }
            }
        }

        class CustomButton : Button
        {
            public int x { get; set; }
            public int y { get; set; }
            public char owner { get; set; }

            public CustomButton(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.owner = ' ';
            }

            public void setOwner(char owner)
            {
                this.owner = owner;
                this.Text = owner + "";
            }
        }
    }
}
