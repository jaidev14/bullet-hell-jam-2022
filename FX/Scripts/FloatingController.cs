using UnityEngine;

namespace Sanktuary.Sometimes.Resources
{
    public class FloatingController : MonoBehaviour {

        [Header("Floating Variables")]
        public float amplitude;         // Amplitude of the floating movement
        public float speed;             // Speed of the floating movement
        private float initPos;
        [SerializeField] private bool horizontal = false;
        private Vector3 currentPos;     // Current position of the treasure

        void Start () 
        {
            if (!horizontal) {
                initPos = transform.position.y;
            } else {
                initPos = transform.position.x;
            }
            
        }

        void Update () 
        {   
            if (!horizontal) {
                currentPos.x = transform.position.x;
                currentPos.y = initPos + amplitude * Mathf.Sin(speed * Time.time);
            } else {
                currentPos.y = transform.position.y;
                currentPos.x = initPos + amplitude * Mathf.Sin(speed * Time.time);
            }
            transform.position = currentPos;
        }
    }
}
