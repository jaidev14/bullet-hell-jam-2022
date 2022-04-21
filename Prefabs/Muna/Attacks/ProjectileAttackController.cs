using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownCharacter2D.Attacks.Range
{
    /// <summary>
    ///     This script handles the logic of a single bullet
    /// </summary>
    public class ProjectileAttackController : MonoBehaviour
    {
        [Tooltip("The layer of the walls of the level")] [SerializeField]
        private LayerMask levelCollisionLayer;

        [SerializeField]
        private RangedAttackConfig _config;
        [SerializeField]
        private ParticleSystem _impactParticleSystem;
        private float _currentDuration;
        private Vector3 _direction;
        private bool _isReady;
        private Rigidbody _rb;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trail;
        private ProjectileManager _projectileManager;

        private bool fxOnDestroy = true;

        private bool DestroyOnHit { get; set; } = true;

        public ref Vector3 Direction => ref _direction;

        private void Awake()
        {
            _projectileManager = ProjectileManager.instance;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody>();
            _trail = GetComponent<TrailRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << other.gameObject.layer)))
            {
                if (DestroyOnHit)
                {
                    DestroyProjectile(other.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
                }
            }
            else if (_config.target.value == (_config.target.value | (1 << other.gameObject.layer)))
            {
                HealthSystem health = other.gameObject.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.ChangeHealth(-_config.power);
                    TopDownKnockBack knockBack = other.gameObject.GetComponent<TopDownKnockBack>();
                    if (knockBack != null)
                    {
                        knockBack.ApplyKnockBack(transform);
                    }
                }

                if (DestroyOnHit)
                {
                    DestroyProjectile(other.ClosestPoint(transform.position), fxOnDestroy);
                }
            }
        }

        /// <summary>
        ///     Destroys the projectile
        /// </summary>
        /// <param name="pos">The position where to create the particles</param>
        /// <param name="createFx">Whether to create particles or not</param>
        private void DestroyProjectile(Vector3 pos, bool createFx)
        {
            if (createFx)
            {
                _projectileManager.CreateImpactParticlesAtPosition(pos, _config, _impactParticleSystem);
            }
            gameObject.SetActive(false);
        }
    }
}