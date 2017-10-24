using UnityEngine;
using System.Collections;

namespace Common
{
    public class Position : MonoBehaviour
    {

        private Vector3 m_Position;

        //コンストラクタ
        public Position()
        {
            m_Position = Vector3.zero;
        }

        /*
            座標を設定する
        */
        public void SetPosition(Vector3 pos)
        {
            m_Position = pos;
        }

        /*
         *  座標を取得する 
         */
        public Vector3 GetPosition()
        {
            return m_Position;
        }

        public void AddPositionX(float posX)
        {
            m_Position.x += posX;
        }

        public void AddPositionY(float posY)
        {
            m_Position.y += posY;
        }

        public void AddPositionZ(float posZ)
        {
            m_Position.z += posZ;
        }

        public void AddPosition(Vector3 pos)
        {
            m_Position += pos;
        }

    }
}