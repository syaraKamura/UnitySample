using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyLib
{
    namespace ResourceLoader
    {
        public class ResourceMgr
        {

            private struct RES_OBJ
            {
                public ResourceRequest resObject;
                public int Id;
                public string resouceName;
            };

            private static List<RES_OBJ> mLoadResouceList;
            private int mResouceCnt;

            private static ResourceMgr mInstance;

            private void OnDestroy()
            {
                //AllDelete();
            }

            private ResourceMgr()
            {
                mResouceCnt = 0;
                mLoadResouceList = new List<RES_OBJ>();

            }

            public void AllDelete()
            {
                int length = mLoadResouceList.Count;

                for (int i = 0; i < length; i++)
                {

                    RES_OBJ res = mLoadResouceList[i];
                    GameObject.Destroy(res.resObject.asset);
                    mLoadResouceList.Remove(res);

                }

                mLoadResouceList.Clear();
            }

            /// <summary>
            /// 非同期でリソースの読み込みをします。
            /// </summary>
            /// <param name="name"></param>
            public void Load(string name)
            {
                RES_OBJ res = new RES_OBJ();

                res.Id = mResouceCnt;
                res.resObject = Resources.LoadAsync(name);

                char[] split = { '/' };
                string[] resNmae = name.Split(split);
                int length = resNmae.Length - 1;

                res.resouceName = resNmae[length];

                Debug.Log(res.resouceName.ToString());

                mLoadResouceList.Add(res);

                mResouceCnt++;
            }

            public void Delete(string name)
            {

                int length = mLoadResouceList.Count;

                for (int i = 0; i < length; i++)
                {
                    if (mLoadResouceList[i].resouceName.Equals(name.ToString()))
                    {
                        RES_OBJ res = mLoadResouceList[i];
                        GameObject.Destroy(res.resObject.asset);
                        mLoadResouceList.Remove(res);
                        break;
                    }
                }
            }

            public Object GetResouce(string name)
            {
                int length = mLoadResouceList.Count;

                Debug.Log(length.ToString() + "Items(GetResouce)");

                for (int i = 0; i < length; i++)
                {

                    if (mLoadResouceList[i].resouceName.Equals(name.ToString()))
                    {
                        RES_OBJ res = mLoadResouceList[i];

                        return (res.resObject.asset);

                    }
                }
                return null;
            }

            /// <summary>
            /// 読み込みが終了しているか？
            /// </summary>
            /// <returns></returns>
            public bool IsLoadEnd()
            {
                int length = mLoadResouceList.Count;

                Debug.Log(length.ToString() + "Items");

                if (length == 0) return true;

                for (int i = 0; i < length; i++)
                {
                    if (mLoadResouceList[i].resObject.isDone == false)
                    {
                        return false;
                    }
                }
                return true;
            }

            /*
                プレハブのリソースデータからインスタンスを生成する
                ※Prefabs以降からパスを記述する
                Object obj          :オブジェクト
                string path         :インスタンス化したいプレハブのパス
                return Object   :生成したオブジェクトの実態
            */
            public Object CreateInstantiate(string name)
            {
                return CreateInstantiate(name, Vector3.zero, new Quaternion());
            }

            /*
                プレハブのリソースデータからインスタンスを生成する
                ※Prefabs以降からパスを記述する
                Object obj          :オブジェクト
                string path         :インスタンス化したいプレハブのパス
                Vector3 pos         :座標
                Quaternion rotation :回転
                return Object   :生成したオブジェクトの実態
            */
            public Object CreateInstantiate(string name, Vector3 pos, Quaternion rotation)
            {
                Object resObj;
                Object InstObj = null;

                resObj = GetResouce(name);

                if (resObj == null) return null;

                InstObj = GameObject.Instantiate(resObj, pos, rotation);
                InstObj.name = resObj.name;

                return InstObj;
            }

            public static ResourceMgr Instance
            {
                get
                {
                    if (mInstance == null) mInstance = new ResourceMgr();
                    return mInstance;
                }
            }

        }

    }

}
