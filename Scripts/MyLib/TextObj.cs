using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MyLib
{
    public class TextObj
    {

        private GameObject mObj;
        private Text mText;
        private RectTransform mRect;

        public TextObj(string canvasObjectName, string textObjectName,string fontName,int fontSize,FontStyle fontStyle,Vector3 position)
        {

            GameObject parentObj = GameObject.Find(canvasObjectName);

            if (parentObj == null)
            {
                parentObj = new GameObject(canvasObjectName);
                Canvas canvas = parentObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                parentObj.AddComponent<CanvasScaler>();
                parentObj.AddComponent<GraphicRaycaster>();
            }

            mObj = new GameObject();
            mObj.transform.SetParent(parentObj.transform);
            mText = mObj.AddComponent<Text>();
            mText.name = textObjectName;
            mText.font = Font.CreateDynamicFontFromOSFont(fontName, fontSize);
            mText.fontStyle = fontStyle;            
            


            mRect = mText.rectTransform;
            mRect.localPosition = position;

        }

        /// <summary>
        /// フォントのステータスを設定する
        /// </summary>
        /// <param name="fontSize"></param>
        /// <param name="fontStyle"></param>
        /// <param name="color"></param>
        public void SetFontState(int fontSize,FontStyle fontStyle,Color color)
        {
            mText.fontSize = fontSize;
            mText.fontStyle = fontStyle;
            mText.color = color;
        }

// --- プロパティ

        //文字列を設定、返却する
        public string Text
        {
            set
            {
                mText.text = value;
            }
            get
            {
                return mText.text;
            }
        }
        
        //文字のスタイルを設定、返却する
        public FontStyle fontStyle
        {
            set
            {
                mText.fontStyle = value;
            }
            get
            {
                return mText.fontStyle;
            }
        }

        //色を設定、返却
        public Color color
        {
            set
            {
                mText.color = value;
            }
            get
            {
                return mText.color;
            }
        }

    }
}
