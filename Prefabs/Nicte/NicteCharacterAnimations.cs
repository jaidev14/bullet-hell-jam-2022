using TopDownCharacter2D;
using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownController2D.Scripts.TopDownCharacter2D.Animations
{
    public class NicteCharacterAnimations : TopDownAnimations
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsDashing = Animator.StringToHash("IsDashing");
        private static readonly int IsHurt = Animator.StringToHash("IsHurt");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        [SerializeField] private bool facingRight = true;
        
        [SerializeField] private bool createDustOnWalk = true;
        [SerializeField] private ParticleSystem dustParticleSystem;
        
        private TopDownDash _dashController = null;
        private HealthSystem _healthSystem;
        private bool _isDead = false;
        public Vector3 _initialRotation = new Vector3 (0, 0, 0);
        
        protected override void Awake()
        {
            base.Awake();
            _healthSystem = GetComponent<HealthSystem>();
            _dashController = GetComponent<TopDownDash>();
        }

        protected void Start()
        {
            controller.OnAttackEvent.AddListener(_ => Attacking());
            controller.OnMoveEvent.AddListener(Move);
            if (_dashController != null)
            {
                _dashController.IsDashing.AddListener(Dashing);
            }

            if (_healthSystem != null)
            {
                _healthSystem.OnDamage.AddListener(Hurt);
                _healthSystem.OnInvincibilityEnd.AddListener(InvincibilityEnd);
                _healthSystem.OnDeath.AddListener(Death);
            }
        }

        /// <summary>
        ///     To call when the character moves, change the animation to the walking one
        /// </summary>
        /// <param name="movementDirection"> The new movement direction </param>
        private void Move(Vector2 movementDirection)
        {
            if (_isDead || PauseManager.Instance.IsPaused) {
                return;
            }
            animator.SetBool(IsWalking, movementDirection.magnitude > .5f);
            if (movementDirection.x > 0 && !facingRight) {
                Flip();
            } else if (movementDirection.x < 0 && facingRight) {
                Flip();
            }
            if (movementDirection.magnitude > .5f) {
                CreateDustParticles();
            }
        }

        /// <summary>
        /// To call when the character changes direction, to flip it
        /// </summary>
        private void Flip()
        {
            Vector3 currentScale = animator.transform.localScale;
            currentScale.x *= -1;
            animator.transform.localScale = currentScale;
            facingRight = !facingRight;
        }

        /// <summary>
        ///     To call when the character attack
        /// </summary>
        private void Attacking()
        {
            animator.SetTrigger(Attack);
        }

        /// <summary>
        ///     To call when the character takes damage
        /// </summary>
        private void Hurt()
        {
            animator.SetBool(IsHurt, true);
        }

        /// <summary>
        ///     To call when the character ends its invincibility time
        /// </summary>
        public void InvincibilityEnd()
        {
            animator.SetBool(IsHurt, false);
        }

        /// <summary>
        /// To call when the character dies
        /// </summary>
        public void Death()
        {
            _isDead = true;
            animator.SetBool(IsDead, true);
        }

        /// <summary>
        ///     To call when the character dashes
        /// </summary>
        private void Dashing(bool isDashing)
        {
            animator.SetBool(IsDashing, isDashing);
            if (isDashing) {
                animator.transform.rotation = Quaternion.Euler(_initialRotation.x, _initialRotation.y, facingRight ? -45 : 45);
            } else {
                animator.transform.rotation = Quaternion.Euler(_initialRotation.x, _initialRotation.y, _initialRotation.z);
            }
        }

        /// <summary>
        ///     Creates dust particles when the character walks, called from an animation
        /// </summary>
        public void CreateDustParticles()
        {
            if (createDustOnWalk)
            {
                dustParticleSystem.Play();
            }
        }
    }
}