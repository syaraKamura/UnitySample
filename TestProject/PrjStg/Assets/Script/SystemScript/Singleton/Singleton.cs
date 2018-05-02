/*
				ファイル名		:Singleton.cs
				作成者			:
				作成日時		    :2017/11/10
				ソース説明		:
				
				    
				備考
				    シングルトンの基底クラス
                    
                    継承先でAwakeを実装する場合は

                    void Awake(){
                        base.Awake();
                    }

                    のように記述をすればいい
                    		
                    オブジェクトを破棄したい場合は、OnDestroyでgameObjectを破棄するような設定をする.

!*/
using UnityEngine;
using System.Collections;

namespace MyLib
{
    namespace Singleton
    {
        public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
        {

            private static T instance;

            //インスタンスを取得する
            public static T Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));

                        if (instance == null)
                        {
                            Debug.LogError(typeof(T) + "is nothing");
                        }

                    }
                    return instance;
                }
            }



            protected void Awake()
            {
                CheckInstance();
            }


            //インスタンスが生成されているか確認する
            protected bool CheckInstance()
            {
                if (this == Instance)
                {
                    return true;
                }

                Destroy(this);
                return false;
            }

        }
    }
}