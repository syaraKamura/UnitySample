using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    private Animator mAnim;
    private Camera mCamera;
    // Use this for initialization
    void Start()
    {

        mAnim = this.gameObject.GetComponent<Animator>();

        //メインカメラを受け取る
        mCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");
        float speed = 0.0f;
        bool boJump = CrossPlatformInputManager.GetButton("Jump");
        bool isMove = false;
        
        Vector3 pos = this.transform.position;
        Vector3 camPos = mCamera.GetComponent<Transform>().position;
        Vector3 dir = Vector3.zero;

        CharacterController charCtr = this.gameObject.GetComponent<CharacterController>();

        if (boJump)
        {
            pos.x = 150.0f;
            pos.y = 0.0f;
            pos.z = 20.0f;
            this.transform.position = pos;
            isMove = false;
            Debug.Log("Came On");
        }

        //if (charCtr.isGrounded)
        //{
            if (horizontal != 0.0f || vertical != 0.0f)
            {
                isMove = true;
            }
        //}

        if (isMove)
        {
            speed = 0.01f;
            mAnim.SetBool("Walk", true);
            mAnim.SetBool("Run", false);
            if (horizontal > 0.3f || horizontal < -0.3f ||
                vertical > 0.3f || vertical < -0.3f)
            {
                speed = 0.5f;
                mAnim.SetBool("Run", true);
            }

            dir.x = speed * horizontal;
            dir.z = speed * vertical;

            pos += dir;

            //移動方向に向く
            transform.LookAt(pos, dir);

        }
        else
        {
            mAnim.SetBool("Walk", false);
            
        }

        

        this.transform.position = pos;
        camPos = pos;
        camPos.z -= 5.0f;
        camPos.y += 1.0f;
        mCamera.transform.position = camPos;
        mCamera.transform.LookAt(pos);

    }


    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name.ToString() + " Is Hit");

        Vector3 pos = this.transform.position;
        Terrain mTerrain = other.GetComponent<Terrain>();

        if (mTerrain != null)
        {
            float terrainHeight = 0.0f;
            terrainHeight = mTerrain.terrainData.GetInterpolatedHeight(pos.x/mTerrain.terrainData.size.x, pos.y/mTerrain.terrainData.size.y);
            pos.y = terrainHeight;

            Debug.Log("TerrainHeihgt:" + terrainHeight.ToString());

        }
        

        this.transform.position = pos;
    }

}
