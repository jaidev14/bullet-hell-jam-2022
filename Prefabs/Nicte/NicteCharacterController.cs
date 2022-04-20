using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Stats;
using UnityEngine;
using System.Collections;

namespace TopDownCharacter2D
{
    /// <summary>
    ///     This class contains the logic for movement in a 2D environment with a top down view
    /// </summary>
    [RequireComponent(typeof(CharacterStatsHandler))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TopDownCharacterController))]
    public class NicteCharacterController : MonoBehaviour
    {
        private TopDownCharacterController _controller;
        public GhostEffect _ghostEffect;
        public bool _isDashing;

        private Vector2 _movementDirection = Vector2.zero;
        private Rigidbody _rb;
        private CharacterStatsHandler _stats;
        private Vector2 _dashSpeed = Vector2.zero;

        private void Awake()
        {
            _controller = GetComponent<TopDownCharacterController>();
            _stats = GetComponent<CharacterStatsHandler>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _controller.OnMoveEvent.AddListener(Move);
            _controller.OnDashEvent.AddListener(Dash);
        }

        private void FixedUpdate()
        {
            if (!_isDashing) {
                ApplyMovement(_movementDirection);
            } else {
                _rb.velocity = new Vector3(_dashSpeed.x, 0, _dashSpeed.y) * 5;
            }
        }

        /// <summary>
        ///     Changes the current direction of the movement
        /// </summary>
        /// <param name="direction"></param>
        private void Move(Vector2 direction)
        {
            _movementDirection = direction;
        }

        private void Dash(DashConfig config)
        {
            Debug.Log("Triggering dash");
            if (!(config is DashConfig))
            {
                return;
            }
            StartCoroutine(DashCoroutine((DashConfig) config));
        }

        // private void ApplyDash() {
        //     _rb.velocity = new Vector3(dashSpeed.x, _rb.velocity.y, dashSpeed.y);
        // }

        /// <summary>
        /// Performs a dash
        /// </summary>
        /// <param name="dashConfig"> The configuration for the dash</param>
        IEnumerator DashCoroutine(DashConfig dashConfig)
        {
            _dashSpeed = _movementDirection * dashConfig.dashSpeed;
            // _rb.AddForce(new Vector3(_dashSpeed.x, 0, _dashSpeed.y), ForceMode.Impulse);
            _ghostEffect.active = true;
            _rb.velocity = Vector3.zero;
            _isDashing = true;
            yield return new WaitForSeconds(dashConfig.dashingTime);
            _ghostEffect.active = false;
            _isDashing = false;
        }


        /// <summary>
        ///     Used to apply a given movement vector to the rigidbody of the character
        /// </summary>
        /// <param name="direction"> The direction in which to move the rigidbody </param>
        private void ApplyMovement(Vector2 direction)
        {
            // Plane playerPlane = new Plane(Vector3.up, transform.position);
            // Ray ray = UnityEngine.Camera.main.ScreenPointToRay(_controller.);
            // float hitDist = 0.0f;

            // if (playerPlane.Raycast(ray, out hitDist)) {
            //     Vector3 targetPoint = ray.GetPoint(hitDist);
            //     Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            //     targetRotation.x = 0;
            //     targetRotation.z = 0;
            //     playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
            // }


            // _rb.velocity += direction * _stats.CurrentStats.speed;
            Vector2 speed = direction * _stats.CurrentStats.speed;

            _rb.velocity = new Vector3(speed.x, 0, speed.y);
        }
    }
}