using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class OpenScene : EditorWindow
{

    class SceneData
    {
        public List<string> mName;
        public List<string> mPath;

        public SceneData()
        {
            mName = new List<string>();
            mPath = new List<string>();
        }

        public void SetSceneData(string name, string path)
        {
            mName.Add(name);
            mPath.Add(path);
        }

    }

    private int mNexetSceneNumber;
    private int mNowSceneNumber;

    private SceneData mList;

    private int CreateSceneList(ref List<string> scenePathList,ref List<string> sceneNameList)
    {

        int size = 0;

        string path = Application.dataPath + "/Scene";

        if (System.IO.Directory.Exists(path))
        {
            string[] directry = System.IO.Directory.GetDirectories(path);

            if (directry != null && directry.Length > 0)
            {

                foreach (string dir in directry)
                {
                    string[] tmp = System.IO.Directory.GetFiles(dir);

                    foreach (string filePath in tmp)
                    {
                        if (filePath.Contains(".meta") || filePath.Contains(".unity.meta")) continue;
                        string sceneName = filePath.Remove(0, dir.Length + 1);

                        sceneNameList.Add(sceneName);

                        string scenePath = filePath.Remove(0, Application.dataPath.Length);
                        scenePath = "Assets" + scenePath;

                        scenePathList.Add(scenePath);
                        size++;

                    }

                }

            }

            string[] files = System.IO.Directory.GetFiles(path);


            foreach (string filePath in files)
            {
                if (filePath.Contains(".meta") || filePath.Contains(".unity.meta")) continue;
                string sceneName = filePath.Remove(0, path.Length + 1);

                sceneNameList.Add(sceneName);

                string scenePath = filePath.Remove(0, Application.dataPath.Length);
                scenePath = "Assets" + scenePath;

                scenePathList.Add(scenePath);

                size++;

            }

        }
        return size;
    }

    private void initialize()
    {
        if (mList == null)
        {

            mList = new SceneData();

            List<string> name = new List<string>();
            List<string> path = new List<string>();
            int size = CreateSceneList(ref path, ref name);

            for (int i = 0; i < size; i++)
            {
                mList.SetSceneData(name[i], path[i]);
            }

            for (int i = 0; i < size; i++)
            {
                if (EditorSceneManager.GetActiveScene().name.Equals(name[i]))
                {
                    mNexetSceneNumber = i;
                    mNowSceneNumber = i;
                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        initialize();
    }

    private void OnFocus()
    {
        initialize();
    }

    private void OnGUI()
    {

        if (Application.isPlaying || EditorApplication.isPlaying || EditorApplication.isCompiling)
        {
            EditorGUILayout.HelpBox("コンパイル中、実行中は変更できません", MessageType.Warning);
            return;
        }

        mNexetSceneNumber = EditorGUILayout.Popup(mNexetSceneNumber, mList.mName.ToArray());
        GUILayout.TextField(mList.mPath[mNexetSceneNumber]);

        if (GUILayout.Button("移動する"))
        {
            if (mNexetSceneNumber != mNowSceneNumber)
            {

                if (EditorUtility.DisplayDialog("確認", "保存をして移動しますか?", "はい", "いいえ"))
                {
                    AssetDatabase.SaveAssets();
                }
                    

                EditorSceneManager.OpenScene(mList.mPath[mNexetSceneNumber], OpenSceneMode.Single);
                mNowSceneNumber = mNexetSceneNumber;
            }
        }

    }

    [MenuItem("Tools/OpenSceneWindow &F1")]
    private static void OpenWindow()
    {
        GetWindow<OpenScene>();
    }
}