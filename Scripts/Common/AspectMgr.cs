using UnityEngine;
using System.Collections;

public class AspectMgr : MonoBehaviour {

    private const float BASE_SCREEN_WIDTH = 1080.0f;
    private const float BASE_SCREEN_HEIGHT = 1920.0f;

    private void Awake()
    {
        //縦のアスペクト比
        float baseAspectVartical = BASE_SCREEN_HEIGHT / BASE_SCREEN_WIDTH;

        //横のアスペクト比
        //float baseAsepctHorizontal = BASE_SCREEN_WIDTH / BASE_SCREEN_HEIGHT;

        //実機のアスペクト比を取得
        float deviceAspect = (float)Screen.width / (float)Screen.height;

        float scale = deviceAspect / baseAspectVartical;

        //メインカメラを取得
        Camera mainCamera = Camera.main;


        // カメラに設定していたorthographicSizeを実機との対比でスケール
        float deviceSize = mainCamera.orthographicSize;
        // scaleの逆数
        float deviceScale = 1.0f / scale;
        // orthographicSizeを計算し直す
        mainCamera.orthographicSize = deviceSize * deviceScale;
        

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
