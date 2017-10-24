using UnityEngine;
using System.Collections;
using MyLib.ResourceLoader;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour {

    private ResourceMgr mRes;

    // Use this for initialization
    void Start () {
        mRes = ResourceMgr.Instance;

        mRes.Load("Prefabs/Sphere");
        mRes.Load("Prefabs/unitychan");

    }
	
	// Update is called once per frame
	void Update () {

        if (mRes.IsLoadEnd())
        {
            SceneManager.LoadScene("TestScene");
        }

	}
}
