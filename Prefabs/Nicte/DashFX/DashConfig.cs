using UnityEngine;

namespace TopDownCharacter2D
{
    /// <summary>
    ///     This class contains the configuration of a melee attack
    /// </summary>
    [CreateAssetMenu(fileName = "DashConfig", menuName = "TopDownController/Dash", order = 0)]
    public class DashConfig : ScriptableObject
    {
        [Tooltip("The time between two dashes. Must be greater than the dashing time")]
        public float delay;
        [Tooltip("The time during which the dash is being performed")]
        public float dashingTime;

        [Tooltip("The speed of the dash")]
        public float dashSpeed;

        [Tooltip("The particles instantiated during the dash")]
        public ParticleSystem dashParticles;

        [Tooltip("The duration of the screen shake")]
        public float screenShakeTime;
        [Tooltip("The amplitude of the screen shake")]
        public float screenShakeAmplitude;
        [Tooltip("The frequency of the screen shake")]
        public float screenShakeFrequency;

        
    }
}