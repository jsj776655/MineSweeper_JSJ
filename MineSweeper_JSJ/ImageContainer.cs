using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MineSweeper_JSJ.Image
{
    //이미지 조각 하나에 대한 데이터
    public class ImageInfo
    {
        [JsonProperty("startX")]
        public int startX { get; set; } //출력 시작점 X좌표
        [JsonProperty("startY")]
        public int startY { get; set; } //출력 시작점 Y좌표
        [JsonProperty("width")]
        public int width { get; set; } //이미지 가로 길이
        [JsonProperty("height")]
        public int height { get; set; } //이미지 세로 길이
    }

    class ImageContainer
    {
        //이미지 내 조각 이미지 출력 정보를 담은 딕셔너리
        //imagemap.json 파일에 정의되어 있으며 {"이름": (ImageInfo 오브젝트)} 형태의 쌍으로 구성되어있다.
        private Dictionary<string, ImageInfo> imageInfoList;
        //원본 이미지 파일 객체
        private Bitmap imageFile;
        //실제 게임에 사용할 이미지 데이터를 담은 딕셔너리
        private Dictionary<string, Bitmap> imageDataList;

        //imagemap json 파일 로드
        private bool LoadImageMap()
        {
            try
            {
                //json 파일 내 텍스트 데이터를 가져옴
                string jsonData = File.ReadAllText("imagemap.json");
                //json deserialize 처리
                imageInfoList = JsonConvert.DeserializeObject<Dictionary<string, ImageInfo>>(jsonData);
            }
            catch (JsonException)
            {
                MessageBox.Show("LoadImageMap() Error!! Check the imagemap.json file.");
                return false;
            }

            return true;
        }

        private bool LoadImageFile()
        {
            try
            {
                imageFile = new Bitmap(@"minesweeper.png");
            }
            catch(ArgumentException)
            {
                MessageBox.Show("LoadImage() Error!! Check the minesweeper.png file.");
                return false;
            }

            return true;
        }

        private bool InitImageData()
        {
            if (imageDataList == null)
                imageDataList = new Dictionary<string, Bitmap>();

            try
            {
                foreach(KeyValuePair<string, ImageInfo> valuePair in imageInfoList)
                {
                    Bitmap newImage;
                    string valueName = valuePair.Key;
                    ImageInfo valueInfo = valuePair.Value;
                    Rectangle cropArea = new Rectangle(valueInfo.startX, valueInfo.startY, valueInfo.width, valueInfo.height);
              
                    newImage = imageFile.Clone(cropArea, imageFile.PixelFormat);
                    imageDataList.Add(valueName, newImage);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("InitImageData() Error!!");
                return false;
            }

            return true;
        }

        public bool LoadImage()
        {
            return (LoadImageFile() && LoadImageMap() && InitImageData());
        }

        public ImageInfo GetImageInfo(string imageKey)
        {
            ImageInfo getInfo = null;
            if (imageInfoList.ContainsKey(imageKey))
                getInfo = imageInfoList[imageKey];

            return getInfo;
        }

        public Bitmap GetImageData(string imageKey)
        {
            Bitmap getData = null;
            if (imageDataList.ContainsKey(imageKey))
                getData = imageDataList[imageKey];

            return getData;
        }
    }
}
