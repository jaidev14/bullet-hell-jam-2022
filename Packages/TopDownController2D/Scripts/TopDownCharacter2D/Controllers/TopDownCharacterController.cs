using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Stats;
using TopDownCharacter2D.Health;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownCharacter2D.Controllers
{
    /// <summary>
    ///     A basic controller for a character
    /// </summary>
    [RequireComponent(typeof(CharacterStatsHandler))]
    public abstract class TopDownCharacterController : MonoBehaviour
    {
        private float _timeSinceLastAttack = float.MaxValue;
        protected bool IsAttacking { get; set; }

        private float _timeSinceLastDash = float.MaxValue;
        public bool IsDashing { get; set; }

        protected CharacterStatsHandler Stats { get; private set; }
        private HealthSystem _healthSystem;
        public bool IsDead = false;
        public bool IsPaused { get; set; }

        protected virtual void Awake()
        {
            Stats = GetComponent<CharacterStatsHandler>();
            _healthSystem = GetComponent<HealthSystem>();
        }

        void Start() {
            _healthSystem.OnDeath.AddListener(HandleDeath);
        }

        protected virtual void Update()
        {
            if (!LevelManager.Instance.levelActive) {
                return;
            }
            HandleAttackDelay();
            HandleDashDelay();
        }

        /// <summary>
        ///     Only trigger a attack event when the attack delay is over
        /// </summary>
        private void HandleAttackDelay()
        {
            if (Stats.CurrentStats.attackConfig == null)
            {
                return;
            }

            if (_timeSinceLastAttack <= Stats.CurrentStats.attackConfig.delay)
            {
                _timeSinceLastAttack += Time.deltaTime;
            }

            if (IsAttacking && _timeSinceLastAttack > Stats.CurrentStats.attackConfig.delay)
            {
                _timeSinceLastAttack = 0f;
                onAttackEvent.Invoke(Stats.CurrentStats.attackConfig);
            }
        }

        /// <summary>
        ///     Only trigger a dash event when the dash delay is over
        /// </summary>
        private void HandleDashDelay()
        {
            if (Stats.CurrentStats.dashConfig == null)
            {
                return;
            }

            if (_timeSinceLastDash <= Stats.CurrentStats.dashConfig.delay)
            {
                _timeSinceLastDash += Time.deltaTime;
            }

            if (IsDashing && _timeSinceLastDash > Stats.CurrentStats.dashConfig.delay)
            {
                _timeSinceLastDash = 0f;
                onDashEvent.Invoke(Stats.CurrentStats.dashConfig);
            }
        }

        /// <summary>
        ///     Changes the current direction of the aiming
        /// </summary>
        /// <param name="direction"></param>
        private void HandleDeath()
        {
            IsDead = true;
        }


        #region Events

        private readonly MoveEvent onMoveEvent = new MoveEvent();
        private readonly AttackEvent onAttackEvent = new AttackEvent();
        private readonly DashEvent onDashEvent = new DashEvent();
        private readonly LookEvent onLookEvent = new LookEvent();

        public UnityEvent<Vector2> OnMoveEvent => onMoveEvent;
        public UnityEvent<AttackConfig> OnAttackEvent => onAttackEvent;
        public UnityEvent<DashConfig> OnDashEvent => onDashEvent;
        public UnityEvent<Vector2> LookEvent => onLookEvent;

        #endregion
    }
}