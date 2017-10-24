using UnityEngine;
using System.Collections;
using MyLib.ResourceLoader;

public class GameMainMgr : MonoBehaviour
{

    private ResourceMgr mRes = ResourceMgr.Instance;

    // Use this for initialization
    void Start()
    {
        mRes.CreateInstantiate("unitychan");
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
