using System;
using System.Windows.Forms;
using MineSweeper_JSJ.Constants;

namespace MineSweeper_JSJ
{
    public partial class InitGameForm : Form
    {
        public delegate void sendGameDataDelegate(int width, int height, int mines);
        public event sendGameDataDelegate sendGameData; 

        public InitGameForm()
        {
            InitializeComponent();

            trackBar_width.SetRange(ConstantsPack.minWidthCells, ConstantsPack.maxWidthCells);
            trackBar_height.SetRange(ConstantsPack.minHeightCells, ConstantsPack.maxHeightCells);
            trackBar_mine.SetRange(ConstantsPack.minMines, 64);

            textBox_width.Text = trackBar_width.Value.ToString();
            textBox_heigth.Text = trackBar_height.Value.ToString();
            textBox_mines.Text = trackBar_mine.Value.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.sendGameData(trackBar_width.Value, trackBar_height.Value, trackBar_mine.Value);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar_width_Scroll(object sender, EventArgs e)
        {
            int maxMines = (trackBar_width.Value - 1) * (trackBar_height.Value - 1);
            trackBar_mine.SetRange(ConstantsPack.minMines, maxMines);

            textBox_width.Text = trackBar_width.Value.ToString();
            textBox_mines.Text = trackBar_mine.Value.ToString();
        }

        private void trackBar_height_Scroll(object sender, EventArgs e)
        {
            int maxMines = (trackBar_width.Value - 1) * (trackBar_height.Value - 1);
            trackBar_mine.SetRange(ConstantsPack.minMines, maxMines);

            textBox_heigth.Text = trackBar_height.Value.ToString();
            textBox_mines.Text = trackBar_mine.Value.ToString();
        }

        private void trackBar_mine_Scroll(object sender, EventArgs e)
        {
            textBox_mines.Text = trackBar_mine.Value.ToString();
        }
    }
}
