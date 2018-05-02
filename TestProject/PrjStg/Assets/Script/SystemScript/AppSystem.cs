using UnityEngine;
using System.Collections;
using MyLib.Singleton;

public class AppSystem : Singleton<AppSystem>
{

    private SceneManager mScneMager;



    public void SceneChange(SceneManager.eScene nextScene)
    {
        if (mScneMager == null) return;
        mScneMager.SceneChange(nextScene);

    }


    // Use this for initialization
    void Start()
    {
        //シーンマネージャーを追加する
        mScneMager = this.gameObject.AddComponent<SceneManager>();


        //シーンを切り替えても消えないオブジェクトとして扱う
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
