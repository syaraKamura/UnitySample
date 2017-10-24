using UnityEngine;
using System.Collections;

namespace Common
{
    public class Rotation : MonoBehaviour
    {
        private Quaternion m_Rota;

        //コンストラクタ
        public Rotation()
        {
            m_Rota.Set(0.0f, 0.0f, 0.0f, 0.0f);  
        }

        /*
            回転関係 
        */
        public void SetRotation(Quaternion rota)
        {
            m_Rota = rota;
        }

        public Quaternion GetRotation()
        {
            return m_Rota;
        }

        public void AddRotationX(float rotaX)
        {
            m_Rota.x += rotaX;
        }

        public void AddRotationY(float rotaY)
        {
            m_Rota.y += rotaY;
        }

        public void AddRotationZ(float rotaZ)
        {
            m_Rota.z += rotaZ;
        }
    }
}