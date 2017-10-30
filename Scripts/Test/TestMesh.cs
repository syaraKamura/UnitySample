using UnityEngine;
using System.Collections;

public class TestMesh : MonoBehaviour
{

    private Material mMaterial;
    private Mesh mMesh;
    private MeshFilter mMeshFilter;
    private MeshRenderer mMeshRenderer;

    private Vector3[] mVerticse = {

        new Vector3(-1.0f,-1.0f,0.0f),
        new Vector3(-1.0f,1.0f,0.0f),
        new Vector3(1.0f,1.0f,0.0f),
        new Vector3(1.0f,-1.0f,0.0f),

    };

    int[] mTriangles =
    {
        0,1,2,0,2,3
    };

    // Use this for initialization
    void Start()
    {

        /*
         * シェーダーについて
         * 引数のname変数は各マテリアルのシェーダーポップアップに表示される名称
         * 例えば,"Standard","Unlit/Texture","Legacy Shaders/Diffuse","Transparent/Diffuse"など
         * 上記の名称はUnityが持っている
         * 以下のURLを参照
         * https://docs.unity3d.com/jp/540/ScriptReference/Shader.Find.html
        **/
        mMaterial = new Material(Shader.Find("Transparent/Diffuse"));

        Debug.Log(mMaterial.shader.ToString());


        mMesh = new Mesh();
        mMesh.vertices = mVerticse;
        mMesh.triangles = mTriangles;

        //法線計算
        mMesh.RecalculateNormals();

        //メッシュフィルターを取得
        mMeshFilter = gameObject.GetComponent<MeshFilter>();

        //なければ生成する
        if (mMeshFilter == null)
        {
            mMeshFilter = gameObject.AddComponent<MeshFilter>();
        }


        //メッシュレンダラーを取得
        mMeshRenderer = gameObject.GetComponent<MeshRenderer>();

        //なければ生成する
        if (mMeshRenderer == null)
        {
            mMeshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        //メッシュをメッシュルターのメッシュに代入
        mMeshFilter.mesh = mMesh;

        mMaterial.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        
        mMeshRenderer.material = mMaterial;

        

        
    }

    // Update is called once per frame
    void Update()
    {

        Color c = mMeshRenderer.material.color;

        c.a -= 1.0f / 60.0f;

        mMeshRenderer.material.color = c;
        




    }
}
