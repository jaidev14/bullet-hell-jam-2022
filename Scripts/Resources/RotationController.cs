using UnityEngine;

namespace Sanktuary.Sometimes
{
    public class RotationController : MonoBehaviour
    {
        [Header("Rotation Variables")]
        public float rotationSpeed = 3f;
        public Vector3 rotationDirection = new Vector3(0, 0, -1);   

        void Update() {
            transform.RotateAround(transform.position, new Vector3(0, 0, -1), rotationSpeed * Time.deltaTime);
        }
    }
}
