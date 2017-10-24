using UnityEngine;
using System.Collections;
using Common;
using MyLib;
using MyLib.Util;
using MyLib.ResourceLoader;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Test : MonoBehaviour {

    public Position m_Pos;
    GameObject[] obj;

    TextObj mTextObj;

    private ResourceMgr mRes;

	// Use this for initialization
	void Start () {

        mRes = ResourceMgr.Instance;

        m_Pos = new Position();

        obj = new GameObject[100];

        for (int i = 0; i < 100; i++)
        {
            obj[i] = null;
        }

        mTextObj = new TextObj("TestCanvas", "TestText", "Arial", 32, FontStyle.Normal, new Vector3(0, 0, -9));
        mTextObj.Text = "TEST TEXT !!\n2Lins";
        mTextObj.color = Color.black;
        

    }

    // Update is called once per frame
    void Update () {

        m_Pos.SetPosition(transform.position);

        float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");
        bool boJump = CrossPlatformInputManager.GetButton("Jump");

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Pos.AddPositionX(0.1f);
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Pos.AddPositionX(-0.1f);
        }

        //シーン切り替え
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadSceneAsync("GameMain");
        }

        TouchInfo info = AppUtil.GetTouch();
        if (Input.GetKey(KeyCode.Z) || boJump)
        {
            for (int i = 0; i < 100; i++)
            {
                if (obj[i] == null)
                {
                    obj[i] = mRes.CreateInstantiate("Sphere") as GameObject;
                    break;
                }
            }
        }

        for (int i = 0; i < 100; i++)
        {
            if (obj[i])
            {
                if (obj[i].transform.position.y <= -10.0f)
                {
                    Destroy(obj[i]);
                    obj[i] = null;
                }
            }
        }
        transform.position = m_Pos.GetPosition();
        

	}
}
