using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class AssetBundleWindow : EditorWindow
{


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
}