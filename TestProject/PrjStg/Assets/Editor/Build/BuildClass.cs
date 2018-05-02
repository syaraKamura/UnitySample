using UnityEngine;
using UnityEditor;

public class BuildClass : Editor
{

    //ビルド対象のシーンリスト
    private static string[] SceneList =
    {
        "./Assets/Scene/Boot.unity",
        "./Assets/Scene/Title/Title.unity",
        "./Assets/Scene/Test/TestScene.unity",
        "./Assets/Scene/GameMain/GameMain.unity",
    };

    private static readonly string mProjectName = Application.productName;

    [MenuItem("Build/Build Windows64")]
    static void BuildForWindows64()
    {

        Debug.Log("ProductName: " + mProjectName);

        Build("../Build/Windows64/", BuildTarget.StandaloneWindows64, BuildOptions.Development);
    }

    [MenuItem("Build/Build Android")]
    static void BuildForAndroid()
    {
        Build("../Build/Android/", BuildTarget.Android, BuildOptions.Development);
    }


    //ビルド
    private static void Build(string outPutPath,BuildTarget target,BuildOptions options)
    {

        Debug.Log("Start " + target.ToString() + " Build");

        //ディレクトリが存在しなければ作成する
        if (System.IO.Directory.Exists(outPutPath) == false)
        {
            System.IO.Directory.CreateDirectory(outPutPath);
        }


        string Path = outPutPath + mProjectName + GetExtension(target);

        string errorMsg = BuildPipeline.BuildPlayer(SceneList, Path, target, options);

        //文字列が何も入っていない状態でないか
        if (string.IsNullOrEmpty(errorMsg) == false)
        {
            Debug.Log("[Error!] " + errorMsg.ToString());
        }
        else
        {
            Debug.Log("[Success!]");
        }

    }

    //拡張子の取得
    private static string GetExtension(BuildTarget target)
    {
        string str = string.Empty;

        switch (target)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                str = ".exe";
                break;
            case BuildTarget.Android:
                str = ".apk";
                break;
        }

        return str;
    }

}