using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownCharacter2D.Controllers
{
    /// <summary>
    ///     This class encapsulate all the input processing for a player using Unity's new input system
    /// </summary>
    public class TopDownInputController : TopDownCharacterController
    {
        private Camera _camera;

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        #region Methods called by unity input events

        /// <summary>
        ///     Method called when the user input a movement
        /// </summary>
        /// <param name="value"> The value of the input </param>
        public void OnMove(InputValue value)
        {
            if (IsDead) {
                OnMoveEvent.Invoke(Vector2.zero);
                return;
            }
            Vector2 moveInput = value.Get<Vector2>().normalized;
            OnMoveEvent.Invoke(moveInput);
        }

        /// <summary>
        ///     Method called when the user enter a look input
        /// </summary>
        /// <param name="value"> The value of the input </param>
        public void OnLook(InputValue value)
        {
            // Vector2 newAim = value.Get<Vector2>();
            // if (!(newAim.normalized == newAim))
            // {
            //     Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
            //     newAim = (worldPos - (Vector2) transform.position).normalized;
            // }

            // if (newAim.magnitude >= .9f)
            // {
            //     LookEvent.Invoke(newAim);
            // }
            if (IsDead) {
                return;
            }
            Vector2 newAim = value.Get<Vector2>();
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(newAim);
            float hitDist = 0.0f;

            if (playerPlane.Raycast(ray, out hitDist)) {
                Vector3 targetPoint = ray.GetPoint(hitDist) - transform.position;
                newAim = new Vector2(targetPoint.x, targetPoint.z).normalized;
                LookEvent.Invoke(newAim);

                // if (newAim.magnitude >= .9f)
                // {
                // }

                // Rotates object to face the mouse position
                // Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                // targetRotation.x = 0;
                // targetRotation.z = 0;
                // playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
            }
        }

        /// <summary>
        ///     Method called when the user enter a fire input
        /// </summary>
        /// <param name="value"> The value of the input </param>
        public void OnFire(InputValue value)
        {
            if (IsDead) {
                IsAttacking = false;
                return;
            }
            IsAttacking = value.isPressed;
        }

        /// <summary>
        ///     Method called when the user enter a dash input
        /// </summary>
        /// <param name="value"> The value of the input </param>
        public void OnDash(InputValue value)
        {
            if (IsDead) {
                IsDashing = false;
                return;
            }
            IsDashing = value.isPressed;
        }

        #endregion
    }
}