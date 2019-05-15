using System.Collections.Generic;
using System.Drawing;
using MineSweeper_JSJ.Constants;
using Pcg;

namespace MineSweeper_JSJ.GameField
{
    //필드 배열 요소 하나를 셀(Cell)이라 칭하고
    //셀 하나에 필요한 정보를 구조체에 담아 정의
    struct FieldCell
    {
        public sbyte closeValue; //셀이 닫혀있을 때 상태
        public sbyte openValue; //셀이 열려있을 때 상태
    }

    //필드를 생성, 제어하는 클래스
    //게임 시작 시 주어진 필드의 크기와 지뢰 갯수에 맞춰 필드를 랜덤 생성하고
    //유저의 입력에 따라 필드 상태를 체크, 변화시켜준다.
    class MineField
    {
        private FieldCell[,] fieldGrid; //지뢰밭 배열
        private int fieldWidth = ConstantsPack.minWidthCells; //지뢰밭 가로
        private int fieldHeight = ConstantsPack.minHeightCells; //지뢰밭 세로
        private int fieldMines = ConstantsPack.minMines; //지뢰밭에 설치된 지뢰 갯수
        private int remainSafeCells = 99999999; //남은 안전 셀 갯수. 지뢰를 밟지 않고 이 값이 0이 되면 클리어
        private ConstantsPack.gameState fieldState = ConstantsPack.gameState.gameState_Init; //게임판 상태

        private readonly int[] adjDx = { -1, 1, 0, 0, -1, 1, 1, -1 }; //주변 8셀 좌표 검사를 위한 변위값
        private readonly int[] adjDy = { 0, 0, -1, 1, -1, 1, -1, 1 };


        //주어진 셀 좌표의 유효범위 체크. false면 범위를 벗어났음을 의미 
        private bool CellRangeCheck(int checkX, int checkY)
        {
            return (checkX >= 0 && checkX < fieldWidth && checkY >= 0 && checkY < fieldHeight);
        }

        //주어진 필드의 크기와 지뢰 갯수로 필드 생성 
        public void InitMineField(int width = ConstantsPack.minWidthCells, int height = ConstantsPack.minHeightCells, int mines = ConstantsPack.minMines)
        {
            fieldState = ConstantsPack.gameState.gameState_Init;
            fieldWidth = width; fieldHeight = height; fieldMines = mines;
            remainSafeCells = (fieldWidth * fieldHeight) - fieldMines;
            fieldGrid = new FieldCell[fieldHeight, fieldWidth];

            PcgRandom randObj = new PcgRandom();

            //주어진 지뢰 갯수로 필드에 지뢰를 임의로 배치시킨다. 
            int remainMines = fieldMines;
            while(remainMines > 0)
            {
                int setX = randObj.Next(0, fieldWidth);
                int setY = randObj.Next(0, fieldHeight);
                
                if (fieldGrid[setY, setX].openValue == (sbyte)ConstantsPack.openCellValue.cellValue_mine)
                    continue;

                fieldGrid[setY, setX].openValue = (sbyte)ConstantsPack.openCellValue.cellValue_mine;
                remainMines--;
            }

            //배치가 끝나면 빈 칸들을 탐색하여 주변 지뢰가 있으면 그 갯수를 체크한다.
            int x = 0, y = 0, k = 0;  
            for (y = 0; y < fieldHeight; ++y)
            {
                for (x = 0; x < fieldWidth; ++x)
                {
                    FieldCell curCell = fieldGrid[y, x];
                    if (curCell.openValue == (sbyte)ConstantsPack.openCellValue.cellValue_blank)
                    {
                        //빈 칸 주변 8셀에 지뢰가 있는지 체크하여 카운팅
                        sbyte totalAdjMines = 0;
                        for (k = 0; k < 8; ++k)
                        {
                            int adjX = x + adjDx[k], adjY = y + adjDy[k];
                            if (!CellRangeCheck(adjX, adjY))
                                continue;
                            if (fieldGrid[adjY, adjX].openValue == (sbyte)ConstantsPack.openCellValue.cellValue_mine)
                                totalAdjMines++;
                        }
                        //카운팅된 값을 셀에 대입
                        fieldGrid[y, x].openValue = totalAdjMines;
                    }
                }
            }

            //필드 생성 끝나면 인게임 상태로 전환
            fieldState = ConstantsPack.gameState.gameState_InGame;
        } //InitMineField 함수 끝

        //해당 셀을 연다.
        public void OpenCell(int clickX, int clickY)
        {
            if (fieldState != ConstantsPack.gameState.gameState_InGame || !CellRangeCheck(clickX, clickY))
                return;

            sbyte closeValue = fieldGrid[clickY, clickX].closeValue;
            //만약 순수하게 닫힌 셀이 아니면(열려있거나, 깃발 꽂았거나, 물음표 표시하면) 처리 안함
            if (closeValue != (sbyte)ConstantsPack.closeCellValue.cellValue_closed)
                return;

            //너비 우선 탐색을 이용해 주변의 빈 칸들을 찾아 재귀적으로 열어준다.
            Queue<Point> searchQ = new Queue<Point>();
            Point firstCell = new Point(clickX, clickY);
            searchQ.Enqueue(firstCell);

            //열린 상태로 갱신
            fieldGrid[clickY, clickX].closeValue = (sbyte)ConstantsPack.closeCellValue.cellValue_opened;

            //열린 셀이 지뢰였음 게임오버
            if (fieldGrid[clickY, clickX].openValue == (sbyte)ConstantsPack.openCellValue.cellValue_mine)
                ProcessGameOver(clickX, clickY);
            else
                remainSafeCells--;

            int k = 0;
            while (searchQ.Count > 0)
            {
                Point curCell = searchQ.Peek();
                searchQ.Dequeue();
                //현재 체크 중인 셀 주변에 지뢰가 1개 이상 있으면 더 이상 열지 않는다.
                if (fieldGrid[curCell.Y, curCell.X].openValue != 0)
                    continue;
                
                for(k = 0; k < 8; ++k)
                {
                    int adjX = curCell.X + adjDx[k], adjY = curCell.Y + adjDy[k];
                    //주변 셀이 범위 내에 있고 순수하게 닫힌 셀인지 체크
                    if (!CellRangeCheck(adjX, adjY) || fieldGrid[adjY, adjX].closeValue != (sbyte)ConstantsPack.closeCellValue.cellValue_closed)
                        continue;

                    Point adjCell = new Point(adjX, adjY);
                    searchQ.Enqueue(adjCell);

                    //열린 상태로 갱신
                    fieldGrid[adjY, adjX].closeValue = (sbyte)ConstantsPack.closeCellValue.cellValue_opened;
                    remainSafeCells--;
                }
            }

            //셀 열기 작업 끝난 후 클리어 여부 체크
            ProcessGameClear();
        } //OpenCell 함수 끝

        //닫혀있는 셀을 우클릭할 때마다 상태를 바꿔준다. 
        //(순수히 닫힌 상태 --> 깃발 표시 --> 물음표 표시 --> 다시 순수히 닫힌 상태... 반복) 
        public void ToggleCloseCell(int clickX, int clickY)
        {
           if (!CellRangeCheck(clickX, clickY))
               return;

            sbyte closeValue = fieldGrid[clickY, clickX].closeValue;
            //셀이 열려있음 처리 안함
            if (closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_opened)
                return;

            //기본적으로 값을 증가시켜준다.
            fieldGrid[clickY, clickX].closeValue++;
            //만약 변화된 값이 범위를 벗어나면 다시 0으로 되돌아감
            if (fieldGrid[clickY, clickX].closeValue > (sbyte)ConstantsPack.closeCellValue.cellValue_dontknow)
                fieldGrid[clickY, clickX].closeValue = 0;
        } //ToggleCloseCell 함수 종료

        //화음(Chord) : 숫자가 적힌 열린 셀에 마우스 양쪽 클릭 시 처리
        //해당 셀의 숫자와 주변 8셀에 표시한 깃발 갯수와 일치하면 주변 8셀을 한꺼번에 연다.
        public void Chord(int clickX, int clickY)
        {
            if (!CellRangeCheck(clickX, clickY))
                return;

            sbyte closeValue = fieldGrid[clickY, clickX].closeValue;
            sbyte openValue = fieldGrid[clickY, clickX].openValue;

            //해당 셀이 닫혀있거나, 열려있지만 주변에 지뢰가 없는 빈 셀이면 실행하지 않음
            if (closeValue != (sbyte)ConstantsPack.closeCellValue.cellValue_opened ||
               openValue <= (sbyte)ConstantsPack.openCellValue.cellValue_blank)
                return;

            //기준 셀의 숫자와 주변 8셀의 깃발의 수와 일치하는지 검사
            int k = 0; sbyte flags = 0;
            for(k = 0; k < 8; ++k)
            {
                int adjX = clickX + adjDx[k], adjY = clickY + adjDy[k];
                if (!CellRangeCheck(adjX, adjY))
                    continue;

                if (fieldGrid[adjY, adjX].closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_flaged)
                    flags++;
            }
            //기준 셀 숫자와 주변 8셀의 깃발의 수가 다르면 진행하지 않음
            if (flags != openValue)
                return;
            //주변 8셀 중 순수하게 닫혀있는 셀들만 열어준다.
            for (k = 0; k < 8; ++k)
            {
                int adjX = clickX + adjDx[k], adjY = clickY + adjDy[k];
                if (!CellRangeCheck(adjX, adjY))
                    continue;

                if (fieldGrid[adjY, adjX].closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_closed)
                    OpenCell(adjX, adjY);
            }
        } //Chord 함수 종료

        //게임 오버처리. 지뢰를 밟은 셀의 좌표를 가져온다.
        private void ProcessGameOver(int mineX, int mineY)
        {
            fieldState = ConstantsPack.gameState.gameState_GameOver;
            //밟은 셀은 빨간 지뢰 표시
            fieldGrid[mineY, mineX].openValue = (sbyte)ConstantsPack.openCellValue.cellValue_minePressed;
            //나머지 셀들 중 깃발 표시된 셀을 체크, 지뢰가 없었다면 X 표시
            int x = 0, y = 0;
            for(y = 0; y < fieldHeight; ++y)
            {
                for (x = 0; x < fieldWidth; ++x)
                {
                    if( (fieldGrid[y, x].closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_closed ||
                        fieldGrid[y, x].closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_dontknow) &&
                        fieldGrid[y, x].openValue == (sbyte)ConstantsPack.openCellValue.cellValue_mine )
                    {
                        fieldGrid[y, x].closeValue = (sbyte)ConstantsPack.closeCellValue.cellValue_opened;
                    }
                    else if (fieldGrid[y, x].closeValue == (sbyte)ConstantsPack.closeCellValue.cellValue_flaged &&
                       fieldGrid[y, x].openValue != (sbyte)ConstantsPack.openCellValue.cellValue_mine)
                    {
                        fieldGrid[y, x].closeValue = (sbyte)ConstantsPack.closeCellValue.cellValue_opened;
                        fieldGrid[y, x].openValue = (sbyte)ConstantsPack.openCellValue.cellValue_flagFailed;
                    }
                }
            }

        } //ProcessGameOver 함수 종료

        //게임 클리어 체크 (업데이트 루프 내에서 호출)
        private void ProcessGameClear()
        {
            if (fieldState == ConstantsPack.gameState.gameState_InGame &&
                remainSafeCells <= 0)
            {
                fieldState = ConstantsPack.gameState.gameState_Clear;
                //열리지 않은 셀들 다 열어준다. 어차피 지뢰만 남았음.
                int x = 0, y = 0;
                for (y = 0; y < fieldHeight; ++y)
                {
                    for (x = 0; x < fieldWidth; ++x)
                    {
                        if (fieldGrid[y, x].closeValue != (sbyte)ConstantsPack.closeCellValue.cellValue_opened)
                            fieldGrid[y, x].closeValue = (sbyte)ConstantsPack.closeCellValue.cellValue_opened;
                    }
                }
            }

        } //ProcessGameClear 함수 종료

        //현재 게임 상태 얻어오기
        public ConstantsPack.gameState GetGameState()
        {
            return fieldState;
        }

        public int GetFieldWidth()
        {
            return fieldWidth;
        }

        public int GetFieldHeight()
        {
            return fieldHeight;
        }

        public FieldCell GetFieldCell(int getX, int getY)
        {
            FieldCell getCell = new FieldCell();

            if (CellRangeCheck(getX, getY))
                getCell = fieldGrid[getY, getX];

            return getCell;
        }
    }
}
