using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownController2D.Scripts.TopDownCharacter2D.Animations
{
    public class SampleCharacterAnimation : TopDownAnimations
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsDash = Animator.StringToHash("Dash");
        private static readonly int IsHurt = Animator.StringToHash("IsHurt");
        [SerializeField] private bool facingRight = true;
        
        [SerializeField] private bool createDustOnWalk = true;
        [SerializeField] private ParticleSystem dustParticleSystem;
        
        private HealthSystem _healthSystem;
        
        protected override void Awake()
        {
            base.Awake();
            _healthSystem = GetComponent<HealthSystem>();
        }

        protected void Start()
        {
            controller.OnAttackEvent.AddListener(_ => Attacking());
            controller.OnDashEvent.AddListener(_ => Dashing());
            controller.OnMoveEvent.AddListener(Move);

            if (_healthSystem != null)
            {
                _healthSystem.OnDamage.AddListener(Hurt);
                _healthSystem.OnInvincibilityEnd.AddListener(InvincibilityEnd);
            }
        }

        /// <summary>
        ///     To call when the character moves, change the animation to the walking one
        /// </summary>
        /// <param name="movementDirection"> The new movement direction </param>
        private void Move(Vector2 movementDirection)
        {
            animator.SetBool(IsWalking, movementDirection.magnitude > .5f);
            if (movementDirection.x > 0 && !facingRight) {
                Flip();
            } else if (movementDirection.x < 0 && facingRight) {
                Flip();
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
        ///     To call when the character dashes
        /// </summary>
        private void Dashing()
        {
            animator.SetBool(IsDash, true);
        }

        /// <summary>
        ///     To call when the character ends its dash time
        /// </summary>
        public void DashEnd()
        {
            animator.SetBool(IsDash, false);
        }

        /// <summary>
        ///     Creates dust particles when the character walks, called from an animation
        /// </summary>
        public void CreateDustParticles()
        {
            if (createDustOnWalk)
            {
                dustParticleSystem.Stop();
                dustParticleSystem.Play();
            }
        }
    }
}