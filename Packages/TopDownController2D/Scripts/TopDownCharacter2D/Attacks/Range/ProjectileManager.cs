using UnityEngine;

namespace TopDownCharacter2D.Attacks.Range
{
    /// <summary>
    ///     Handles the pool of projectiles and their creation
    /// </summary>
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _impactParticleSystem = null;
        public static ProjectileManager instance;

        private ObjectPool _projectilesPool;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _projectilesPool = ObjectPool.sharedInstance;
        }

        public void CreateImpactParticlesAtPosition(Vector3 position, RangedAttackConfig config, ParticleSystem impactParticleSystem = null)
        {
            if (impactParticleSystem == null) {
                impactParticleSystem = _impactParticleSystem;
                impactParticleSystem.transform.position = position;
                ParticleSystem.EmissionModule em = impactParticleSystem.emission;
                em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(config.size * 5f)));
                ParticleSystem.MainModule mainModule = impactParticleSystem.main;
                mainModule.startSpeedMultiplier = config.size * 10f;
                impactParticleSystem.Play();
            } else {
                ParticleSystem particles = Instantiate(impactParticleSystem, position, Quaternion.identity, transform);
            }
        }

        /// <summary>
        ///     Create a projectile from the pool and initialize it
        /// </summary>
        /// <param name="startPosition"> The start position of the projectile</param>
        /// <param name="direction"> The direction of the projectile</param>
        /// <param name="config"> The parameters of the projectile to shoot</param>
        public void ShootBullet(Vector2 startPosition, Vector2 direction, RangedAttackConfig config)
        {
            GameObject projectileObject = _projectilesPool.GetPooledObject();

            projectileObject.transform.position = startPosition;
            RangedAttackController rangedAttack = projectileObject.GetComponent<RangedAttackController>();
            rangedAttack.InitializeAttack(direction, config, this);

            projectileObject.SetActive(true);
        }
    }
}