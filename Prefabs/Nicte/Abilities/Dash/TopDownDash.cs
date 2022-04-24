using System.Collections;
using TopDownCharacter2D.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownCharacter2D
{
    /// <summary>
    /// Handles the logic behind the dash
    /// </summary>
    [RequireComponent(typeof(TopDownCharacterController))]
    public class TopDownDash : MonoBehaviour
    {
        [SerializeField]
        private TrailRenderer _dashTrailRenderer;

        [SerializeField]
        private float _dashVelocity = 14f;
        private Vector2 _dashDir;
        private Vector2 _curDir;
        public bool _isDashing;
        private int _faceDirection = 1;
        private TopDownCharacterController _controller;
        private Rigidbody _rb;

        private void Awake()
        {
            _controller = GetComponent<TopDownCharacterController>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _controller.OnMoveEvent.AddListener(HandleFacing);
            _controller.OnDashEvent.AddListener(Dash);
        }

        private void Update()
        {
            if (_isDashing) {
                _rb.velocity = new Vector3(_dashDir.x, 0, _dashDir.y).normalized *_dashVelocity;
            }
        }

        /// <summary>
        ///     Changes the current direction of the aiming
        /// </summary>
        /// <param name="direction"></param>
        public void HandleFacing(Vector2 direction)
        {
            _curDir = direction;
            if (direction.x > 0 && _faceDirection == -1) {
                _faceDirection = 1;
            } else if (direction.x < 0 && _faceDirection == 1) {
                _faceDirection = -1;
            }
        }

        /// <summary>
        ///     To call when the dash is pressed
        /// </summary>
        /// <param name="newAimDirection"> The new aim direction </param>
        public void Dash(DashConfig config)
        {
            _isDashing = true;
            _dashTrailRenderer.emitting = true;
            _dashDir = _curDir;
            if (_dashDir == Vector2.zero) {
                _dashDir = new Vector2 (_faceDirection, 0);
            }
            IsDashing.Invoke(true);
            StartCoroutine(StopDashing(config));
            // Assign animation here
        }

        private IEnumerator StopDashing(DashConfig config)  {
            yield return new WaitForSeconds(config.dashingTime);
            this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            _dashTrailRenderer.emitting = false;
            _isDashing = false;
            IsDashing.Invoke(false);
        }

        public UnityEvent<bool> IsDashing;
    }
}