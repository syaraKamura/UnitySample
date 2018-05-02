using UnityEngine;
using System.Collections;
using MyLib.Singleton;

public class CameraSystem : Singleton<CameraSystem>
{

    private Camera mMainCamera;

    // Use this for initialization
    void Start()
    {
        var obj = GameObject.Find("Main Camera");
        mMainCamera = obj.GetComponent<Camera>();

#if USE_LOCAL_RESORUCES_LOAD

#else

#endif


    }

    // Update is called once per frame
    void Update()
    {

    }
}
