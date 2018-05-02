using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    public enum eScene
    {
        None = -1,
        Boost,
        Title,

        // 以下、デバッグ用シーン

#if UNITY_EDITOR || MY_DEBUG

#endif  //UNITY_EDITOR
    }

    private UnityEngine.SceneManagement.Scene mScene;

    private eScene mNextScene = eScene.None;

    private eScene mNowScene = eScene.None;
    public eScene nowScene
    {
        get { return mNowScene; }
    }

    private eScene mOldeScene = eScene.None;
    public eScene oldScene
    {
        get { return mOldeScene; }
    }
    public void SceneChange(eScene nextScene)
    {
        mNextScene = nextScene;
    }

    IEnumerator SceneLoader(eScene nextScene)
    {

        if (nextScene == eScene.None) yield break;

        //シーンの読み込み
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene.ToString(),
            UnityEngine.SceneManagement.LoadSceneMode.Single);

        //読み込みができるまで待つ
        while (async.isDone == false)
        {
            yield return null;
        }

        //現在アクティブになっているシーンを取得する
        mScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        //前回のシーンを設定
        mOldeScene = mNowScene;

        //今のシーンを設定
        mNowScene = mNextScene;

        //読み込み終了したなら次のシーンがないように設定
        mNextScene = eScene.None;

        yield break;
    }


    // Use this for initialization
    private void Start()
    {
        mNextScene = eScene.Boost;
    }


    // Update is called once per frame
    private void Update()
    {

        if (mNextScene == eScene.None) return;

        // 画面フェードアウト処理

        // 前回のシーンの削除

        // 新しいシーンの読み込み
        StartCoroutine(SceneLoader(mNextScene));

        // 画面フェードイン処理

        // シーン遷移終了

    }
}
