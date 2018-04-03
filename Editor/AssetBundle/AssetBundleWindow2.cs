using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleWindow : EditorWindow
{

    public class AssetBundleData
    {
        public List<string> mPathList;
        public string mOutPutFileName;
        public string mAssetBundleName;

        public AssetBundleData()
        {
            mPathList = new List<string>();
            mOutPutFileName = string.Empty;
            mAssetBundleName = "original";
        }

    }

    private readonly static BuildAssetBundleOptions BundleOptions = BuildAssetBundleOptions.ChunkBasedCompression;

    private AssetBundleData mAssetBundleData = new AssetBundleData();

    /// <summary>
    /// アセットバンドル化できるリソースか確認する
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private bool CheckResource(string path)
    {

        if (path.Contains(".meta")) return false;
        else if(path.Equals("")) return false;
        
        return true;
    }

    /// <summary>
    /// パスを作成する
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string CreatePath(string path)
    {
        string str = path.Remove(0,Application.dataPath.Length);
        str = "Assets" + str;

        return str;
    }

    private void CreateAssetBundle(string outputPath,string assetBundleName, string[] paths)
    {

        bool result = true;

        if (outputPath.Equals("")) result = false;
        else if (assetBundleName.Equals("")) result = false;
        else if (paths.Length == 0) result = false;

        if (result)
        {
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

            buildMap[0].assetBundleName = assetBundleName;
            buildMap[0].assetBundleVariant = "";
            buildMap[0].assetNames = paths;

            //現在設定されているプラットフォーム向けにアセットバンドルを作成する
            BuildPipeline.BuildAssetBundles(outputPath, buildMap, BundleOptions, EditorUserBuildSettings.activeBuildTarget);

            EditorUtility.DisplayDialog("確認", "アセットバンドルの作成が終了しました", "はい");
        }
        else
        {
            EditorUtility.DisplayDialog("確認", "アセットバンドルの作成に失敗しました", "はい");
        }
    }

    // -- Unity固有メッセージ

    private void Awake()
    {
    
    }

    private void OnGUI()
    {

        //横一列にする
        GUILayout.BeginHorizontal(GUI.skin.box);
        {
            GUILayout.Label("書き出し先設定");
            if (mAssetBundleData.mOutPutFileName.Equals(""))
            {
                GUILayout.TextField("書き出し先を設定してください");
            }
            else
            {
                GUILayout.TextField(mAssetBundleData.mOutPutFileName);
            }

            if (GUILayout.Button("..."))
            {
                //書き出しフォルダ参照
                string title = "アセットバンドル書き出し先フォルダ選択";
                string dirPath = "Assets/";
                string defaultName = "StreamingAssets";
                string path = EditorUtility.OpenFolderPanel(title, dirPath, defaultName);

                //文字列があるならパスを設定する
                if (path.Equals("") == false)
                {
                    mAssetBundleData.mOutPutFileName = CreatePath(path);
                }

            }

        }
        GUILayout.EndHorizontal();

        for (int i = 0; i < mAssetBundleData.mPathList.Count; i++)
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            {
                mAssetBundleData.mPathList[i] = EditorGUILayout.TextField(mAssetBundleData.mPathList[i]);

                if (GUILayout.Button("..."))
                {
                    //書き出しフォルダ参照
                    string title = "アセットバンドル化するリソース選択";
                    string dirPath = "Assets/";
                    string extension = "";
                    string path = EditorUtility.OpenFilePanel(title, dirPath, extension);

                    //アセットバンドル化できるか判断する
                    if (CheckResource(path))
                    {
                        mAssetBundleData.mPathList[i] = CreatePath(path);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("警告", "アセットバンドル化できないデータです。", "はい");
                    }
                }

                if (GUILayout.Button("削除"))
                {
                    mAssetBundleData.mPathList.RemoveAt(i);
                }
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal(GUI.skin.box);
        {
            if (GUILayout.Button("追加"))
            {
                mAssetBundleData.mPathList.Add(string.Empty);
            }
            if (GUILayout.Button("すべて削除"))
            {
                for (int i = 0; i < mAssetBundleData.mPathList.Count; i++)
                {
                    mAssetBundleData.mPathList.RemoveAt(i);
                }
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal(GUI.skin.box);
        {
            GUILayout.Label("アセットバンドル名");
            mAssetBundleData.mAssetBundleName = GUILayout.TextField(mAssetBundleData.mAssetBundleName);

            if (GUILayout.Button("アセットバンドル作成"))
            {
                CreateAssetBundle(mAssetBundleData.mOutPutFileName, mAssetBundleData.mAssetBundleName, mAssetBundleData.mPathList.ToArray());
            }
        }
        GUILayout.EndHorizontal();
    }

    private static void OnProjectChanged()
    {
        Debug.Log("OnProjectChanged");
    }

    // -- エディターウィンドウ呼び出し

    [MenuItem("AssetBundle/Build AssetBundle Window &F3")]
    private static void OpenWindow()
    {
        GetWindow<AssetBundleWindow>();
    }


#if false
    private readonly static string AssetBundleOutPutPath = "AssetBundle";                             //外部参照用出力先
    private readonly static string StreamAssetOutPutPath = Application.streamingAssetsPath.ToString();  //内部参照用出力先
    private readonly static BuildAssetBundleOptions BundleOptions = BuildAssetBundleOptions.ChunkBasedCompression;

    

    [MenuItem("AssetBundle/Build AssetBundle")]
    static void BuildAssetundle()
    {

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        buildMap[0].assetBundleName = "original";
        buildMap[0].assetBundleVariant = "";
        buildMap[0].assetNames = new string[1]
        {
            "Assets/Resources/Prefabs/unitychan.prefab",
        };


        //Windows
        CreateAssetBundle(buildMap, StreamAssetOutPutPath, BuildTarget.StandaloneWindows64, BundleOptions);
        //Android
        CreateAssetBundle(buildMap, StreamAssetOutPutPath, BuildTarget.Android, BundleOptions);
        //


    }


    private static void CreateAssetBundle(AssetBundleBuild[] buildMaps,string outputPath,BuildTarget targetPlatform, BuildAssetBundleOptions options)
    {

        string distPath = outputPath + "/" + targetPlatform + "/";
        string path = AssetBundleOutPutPath + "/" + targetPlatform + "/";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        

        BuildPipeline.BuildAssetBundles(path, buildMaps, BundleOptions, targetPlatform);

        if (!Directory.Exists(distPath))
        {
            Directory.CreateDirectory(distPath);
        }


        string fileName = buildMaps[0].assetBundleName;
        string srctFile = Path.Combine(path, fileName);

        string dist = distPath + fileName;

        Debug.Log(srctFile);
        Debug.Log(dist);

        File.Copy(srctFile, dist, true);



    }



    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        AssetBundleWindow window = (AssetBundleWindow)EditorWindow.GetWindow(typeof(AssetBundleWindow));
        window.Show();
    }

    void OnGUI()
    {

#if false
        mAssetImporter.assetBundleName = "";    //アセット名設定
        mAssetImporter.assetBundleVariant = ""; //拡張子設定
        mAssetImporter.userData = "";           //ユーザーデータ

        if (mBuildMap == null)
        {
            mBuildMap = new AssetBundleBuild[1];
        }

        mBuildMap[0].assetBundleName = "";
        mBuildMap[0].assetBundleVariant = "";
        mBuildMap[0].assetNames = new string[]
        {
            "",
        };

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);


        EditorWindow.GetWindow<AssetBundleWindow>();

        //EditorApplication.hierarchyWindowItemOnGUI(0,new Rect(0 ,0 ,100,100));

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
#endif

        //Pathを指定してその配下にある全ファイルのGUIDを取得
        var guids = AssetDatabase.FindAssets("", new[] { "Assets/Resources" });


        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        

        foreach (var guid in guids)
        {
            // GUIDからAssetPathを取得
            string path = AssetDatabase.GUIDToAssetPath(guid);

            // PathからAsset取得
            AssetImporter asip = AssetImporter.GetAtPath(path);

            // assetBundleNameとAssetPathがすでに一緒だったら何もしない
            if (asip.assetBundleName != path.ToLower())
            {
                Debug.Log("assetBundleName path = " + path);

                path = EditorGUILayout.TextField("Asset Name", path);

                // AssetPathを名前に設定
                asip.assetBundleName = path;
            }

            // AssetVariantsを設定
            asip.assetBundleVariant = "";
        }
    }
#endif
}