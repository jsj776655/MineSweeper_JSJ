using System.Windows.Forms;
using System.Drawing;
using MineSweeper_JSJ.Image;
using MineSweeper_JSJ.GameField;
using MineSweeper_JSJ.Constants;

namespace MineSweeper_JSJ
{
    public partial class Form1 : Form
    {
        //게임 필드
        private MineField gameField;
        //게임 이미지 컨테이너
        private ImageContainer imageContainer;
        //게임 생성 다이얼로그
        private InitGameForm initGameForm;

        public Form1()
        {
            InitializeComponent();
            //최초 실행 처리
            //1. 이미지 컨테이너 생성 처리
            imageContainer = new ImageContainer();
            bool bLoad = imageContainer.LoadImage();

            if (!bLoad)
                Application.Exit();

            //2. 게임 스타트 버튼 이미지 세팅
            gameStartButton.BackgroundImage = imageContainer.GetImageData("smile_init");
        }

        //닫힌 셀 클릭 처리
        private void mineFieldRenderBox_MouseClick(object sender, MouseEventArgs e)
        {
            int clickCellX = 0, clickCellY = 0;
            clickCellX = e.X / ConstantsPack.cellPixels;
            clickCellY = e.Y / ConstantsPack.cellPixels;

            if (e.Button == MouseButtons.Left)
                gameField.OpenCell(clickCellX, clickCellY);
            else if (e.Button == MouseButtons.Right)
                gameField.ToggleCloseCell(clickCellX, clickCellY);

            mineFieldRenderBox.Invalidate();
        }

        //게임 필드 그리기 갱신
        private void mineFieldRenderBox_Paint(object sender, PaintEventArgs e)
        {
            if (gameField == null || gameField.GetGameState() == ConstantsPack.gameState.gameState_Init)
                return;

            int getWidth = gameField.GetFieldWidth(), getHeight = gameField.GetFieldHeight();
            int x = 0, y = 0;
            for(y = 0; y < getHeight; ++y)
            {
                for(x = 0; x < getWidth; ++x)
                {
                    string imageKey = "";
                    Rectangle drawArea = new Rectangle(0,0,0,0);
                    FieldCell curCell = gameField.GetFieldCell(x, y);

                    if (curCell.closeValue != (sbyte)ConstantsPack.closeCellValue.cellValue_opened)
                    {
                        if (curCell.closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_closed)
                            imageKey = "cell_init";
                        else if (curCell.closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_flaged)
                            imageKey = "cell_flag";
                        else
                            imageKey = "cell_question_mark";
                    }
                    else
                    {
                        if (curCell.openValue == (sbyte)ConstantsPack.openCellValue.cellValue_mine)
                            imageKey = "mine";
                        else if (curCell.openValue >= (sbyte)ConstantsPack.openCellValue.cellValue_blank)
                            imageKey = "cell_" + curCell.openValue.ToString();
                        else if (curCell.openValue == (sbyte)ConstantsPack.openCellValue.cellValue_minePressed)
                            imageKey = "mine_lose";
                        else if (curCell.openValue == (sbyte)ConstantsPack.openCellValue.cellValue_flagFailed)
                            imageKey = "mine_wrong";
                    }

                    ImageInfo drawInfo = imageContainer.GetImageInfo(imageKey);
                    Bitmap drawBitmap = imageContainer.GetImageData(imageKey);

                    drawArea.X = x * drawInfo.width; drawArea.Y = y * drawInfo.height;
                    drawArea.Width = drawInfo.width; drawArea.Height = drawInfo.height;

                    e.Graphics.DrawImageUnscaledAndClipped(drawBitmap, drawArea);
                }
            }

            if (gameField.GetGameState() == ConstantsPack.gameState.gameState_GameOver)
                gameStartButton.BackgroundImage = imageContainer.GetImageData("smile_lose");
            else if (gameField.GetGameState() == ConstantsPack.gameState.gameState_Clear)
                gameStartButton.BackgroundImage = imageContainer.GetImageData("smile_win");
        }

        //셀 더블 클릭 시 화음
        private void mineFieldRenderBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int clickCellX = 0, clickCellY = 0;
            clickCellX = e.X / ConstantsPack.cellPixels;
            clickCellY = e.Y / ConstantsPack.cellPixels;

            if (e.Button == MouseButtons.Left)
                gameField.Chord(clickCellX, clickCellY);

            mineFieldRenderBox.Invalidate();
        }

        //게임 생성 폼 생성처리
        private void gameStartButton_MouseClick(object sender, MouseEventArgs e)
        {
            //폼 생성 및 델리게이트 이벤트 등록 
            initGameForm = new InitGameForm();
            initGameForm.sendGameData += new InitGameForm.sendGameDataDelegate(GameStart);
            //폼 출력
            initGameForm.Show();
        }

        //initGameForm에 이벤트 함수로 등록하여 필드 생성 처리
        public void GameStart(int fieldWidth, int fieldHeight, int mines)
        {
            gameField = new MineField();
            gameField.InitMineField(fieldWidth, fieldHeight, mines);
            mineFieldRenderBox.Invalidate();
        }
    }
}
