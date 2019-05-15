
namespace MineSweeper_JSJ.Constants
{
    //게임에 사용할 상수들 모음집
    class ConstantsPack
    {
        public const int cellPixels = 26; //셀 이미지 픽셀 수, 가로 세로 동일
        public const int minWidthCells = 9; //최소 가로 셀 갯수
        public const int minHeightCells = 9; //최소 세로 셀 갯수
        public const int maxWidthCells = 30; //최소 가로 셀 갯수
        public const int maxHeightCells = 30; //최소 세로 셀 갯수
        public const int minMines = 10; //최소 지뢰 갯수, 최대 지뢰갯수는 (현재 가로 셀 갯수 - 1) * (현재 세로 셀 갯수 - 1) 로 한다.

        public enum openCellValue : sbyte //셀을 열었을 때의 상태 정의
        {
            cellValue_mine = -1, //지뢰, 열면 게임 오버
            cellValue_blank = 0, //빈 칸, 주변 지뢰갯수가 0개
            //1 이상부터 셀 열 때 주변 지뢰갯수가 1개 이상임을 의미 (1부터 주변 지뢰 1개, 2는 주변 지뢰 2개 .... 이런식)
            cellValue_minePressed = -2, //지뢰를 밟음, 빨간 지뢰 표시
            cellValue_flagFailed = -3, //게임 오버 후 깃발 체크할 때 지뢰가 없었으면 X 표시
        };

        public enum closeCellValue : sbyte //셀이 닫혀있을 때의 상태 정의
        {
            cellValue_opened = -1, //열려있는 상태, 해당 셀의 closeCellValue가 이 값이면 openCellValue만 체크하면 됨
            cellValue_closed = 0, //닫혀있는 상태
            cellValue_flaged, //깃발 꽂은 상태
            cellValue_dontknow, //물음표 표시한 상태
        };

        public enum gameState : byte //게임 상태 정의
        {
            gameState_Init, //초기 상태
            gameState_InGame, //게임 중
            gameState_GameOver, //게임 오버
            gameState_Clear, //게임 클리어
        };
    }
}
