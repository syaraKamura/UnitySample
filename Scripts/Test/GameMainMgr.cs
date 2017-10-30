using UnityEngine;
using System.Collections;
using MyLib.ResourceLoader;

public class GameMainMgr : MonoBehaviour
{

    private ResourceMgr mRes = ResourceMgr.Instance;
    GameObject mPalyer;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(mRes.AssetBundle_Load("original", true));

        mPalyer = mRes.CreateInstantiate("unitychan") as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mPalyer == null)
        {
            mPalyer = mRes.CreateInstantiate("unitychan") as GameObject;
        }

    }
}
