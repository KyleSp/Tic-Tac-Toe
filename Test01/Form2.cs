using System;
using System.Windows.Forms;

namespace Test01
{
    public partial class Form2 : Form
    {
        public int boardSizeX { get; set; }
        public int boardSizeY { get; set; }
        public int numToWin { get; set; }

        public bool formClosed { get; set; }

        public Form2()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Text = "Tic-Tac-Toe - Settings";

            this.textBoxWidth.Text = "3";
            this.textBoxHeight.Text = "3";
            this.textBoxWin.Text = "3";

            this.formClosed = false;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            int x, y, win;

            bool try1 = Int32.TryParse(this.textBoxWidth.Text, out x);
            bool try2 = Int32.TryParse(this.textBoxHeight.Text, out y);
            bool try3 = Int32.TryParse(this.textBoxWin.Text, out win);

            if (try1 && try2 && try3 && x > 2 && y > 2 && win > 2 && (win <= x || win <= y))
            {
                this.boardSizeX = x;
                this.boardSizeY = y;
                this.numToWin = win;

                this.formClosed = true;

                this.Close();
            }
            else
            {
                this.labelError.Text = "Invalid Input";
            }
        }
    }
}
