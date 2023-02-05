using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Stats;
using TopDownCharacter2D.Health;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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

        [Header("Dashing")]
        public bool _isDashing;

        private Vector2 _movementDirection = Vector2.zero;
        private Vector2 _aimingDirection = Vector2.zero;
        private Rigidbody _rb;
        private CharacterStatsHandler _stats;
        private Vector2 _dashSpeed = Vector2.zero;
        private HealthSystem _healthSystem;
        private bool _isDead;
        public bool _isHurt;
        [Range(0.0f, 1.0f)]
        public float _speedSlowPercentage = 0.8f;

        private void Awake()
        {
            _controller = GetComponent<TopDownCharacterController>();
            _stats = GetComponent<CharacterStatsHandler>();
            _rb = GetComponent<Rigidbody>();
            _healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            _controller.OnMoveEvent.AddListener(Move);
            _controller.OnDashEvent.AddListener(Dash);
            _controller.LookEvent.AddListener(OnAim);
            _healthSystem.OnDeath.AddListener(OnDeath);
            _healthSystem.OnDamage.AddListener(OnDamage);
            _healthSystem.OnInvincibilityEnd.AddListener(OnInvincibilityEnd);
        }

        private void FixedUpdate()
        {
            // if (!LevelManager.Instance.levelActive) {
            //     return;
            // }
            if (_isDead) {
                return;
            }
            if (!_isDashing) {
                ApplyMovement(_movementDirection);
            } else {
                // _rb.velocity = new Vector3(_dashSpeed.x, 0, _dashSpeed.y) * 5;

                // RaycastHit slash;
                // Vector3 pre_pos = transform.position;
                // transform.Translate(new Vector3(_dashSpeed.x, 0, _dashSpeed.y) * 5 * Time.deltaTime);
                // if (Physics.Linecast(pre_pos, transform.position, out slash)) {
                //     Debug.Log("HIT " + slash.collider.name);
                //     Debug.DrawRay(pre_pos, transform.position, Color.red, 5f);
                //     if (slash.collider.tag == "Enemy") {
                //         Destroy(slash.collider.gameObject);
                //     }
                // }
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
            if (!(config is DashConfig))
            {
                return;
            }
            _isDashing = true;
            StartCoroutine(DashCoroutine((DashConfig) config));
        }

        /// <summary>
        ///     Changes the current direction of the aiming
        /// </summary>
        /// <param name="direction"></param>
        private void OnAim(Vector2 direction)
        {
            _aimingDirection = direction;
        }

        /// <summary>
        /// Performs a dash
        /// </summary>
        /// <param name="dashConfig"> The configuration for the dash</param>
        IEnumerator DashCoroutine(DashConfig dashConfig)
        {
            yield return new WaitForSeconds(dashConfig.dashingTime);
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
            if (_isHurt) {
                speed = speed * _speedSlowPercentage;
            }

            _rb.velocity = new Vector3(speed.x, 0, speed.y);
        }

        /// <summary>
        /// Changes the state of the player to death
        /// </summary>
        private void OnDeath()
        {
            _isDead = true;
            _rb.velocity = Vector3.zero;
            LevelManager.Instance.Finish();
        }

        /// <summary>
        /// Changes the state of the player to being hurt
        /// </summary>
        private void OnDamage()
        {
            _isHurt = true;
        }

        /// <summary>
        /// Changes the state of the player to stop being hurt
        /// </summary>
        private void OnInvincibilityEnd()
        {
            _isHurt = false;
        }
    }
}