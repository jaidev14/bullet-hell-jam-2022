using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Health;
using UnityEngine;

namespace TopDownController2D.Scripts.TopDownCharacter2D.Animations
{
    /// <summary>
    ///     This class contains the animation logic for a 2D sprite in a top down view
    /// </summary>
    // [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(TopDownCharacterController))]
    public abstract class TopDownAnimations : MonoBehaviour
    {
        [SerializeField] protected Animator animator = null;
        protected TopDownCharacterController controller;
        
        protected virtual void Awake()
        {
            if (animator == null) {
                animator = GetComponent<Animator>();
            }
            controller = GetComponent<TopDownCharacterController>();
        }
    }
}