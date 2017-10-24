using UnityEngine;
using System.Collections;

namespace Common
{
    public class Scale : MonoBehaviour
    {
        private Vector3 m_Scale;

        //コンストラクタ
        public Scale()
        {
            m_Scale = Vector3.zero;
        }


        /*
         *  スケール関係
         */
        public void SetScale(Vector3 Scale)
        {
            m_Scale = Scale;
        }

        public Vector3 GetScale()
        {
            return m_Scale;
        }

        public void AddScaleX(float ScaleX)
        {
            m_Scale.x += ScaleX;
        }

        public void AddScaleY(float ScaleY)
        {
            m_Scale.y += ScaleY;
        }

        public void AddScaleZ(float ScaleZ)
        {
            m_Scale.z += ScaleZ;
        }

        public void AddScale(Vector3 Scale)
        {
            m_Scale += Scale;
        }
    }
}