using System;
using _Workspace.Scripts.Animation;
using UnityEngine;

namespace _Workspace.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [Header("Animation Settings")]
        [SerializeField] private SpriteAnimator spriteAnimator;
        [SerializeField] private Sprite[] rightWalkSprites;
        [SerializeField] private Sprite[] leftWalkSprites;
        [SerializeField] private Sprite[] upWalkSprites;
        [SerializeField] private Sprite[] downWalkSprites;
        [SerializeField] private Sprite[] idleWalkSprites;
        private MovementState _movementState = MovementState.None;
        
        private bool OnLeft => Input.GetKey(KeyCode.A);
        private bool OnRight => Input.GetKey(KeyCode.D);
        private bool OnUp => Input.GetKey(KeyCode.W);
        private bool OnDown => Input.GetKey(KeyCode.S);

        #endregion

        #region Unity Funcs

        private void Update()
        {
            
        }

        #endregion

        #region Movement Funcs

        private void HandleMovement()
        {
        }

        #endregion
    }

    public enum MovementState
    {
        None,
        Idle,
        WalkingRight,
        WalkingLeft,
        WalkingUp,
        WalkingDown
    }
}