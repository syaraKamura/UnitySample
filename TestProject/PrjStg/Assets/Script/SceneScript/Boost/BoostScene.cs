using UnityEngine;
using System.Collections;

public class BoostScene : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey)
        {
            AppSystem.Instance.SceneChange(SceneManager.eScene.Title);
        }

    }
}
