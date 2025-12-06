using System;
using System.Collections.Generic;
using System.Linq;
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

        [Header("Movement Settings")] 
        [SerializeField] private LayerMask obstacleLayers;
        
        private MovementState _movementState;
        private List<MovementState> _inputStack = new List<MovementState>();
        
        private bool OnLeft => Input.GetKey(KeyCode.A);
        private bool OnRight => Input.GetKey(KeyCode.D);
        private bool OnUp => Input.GetKey(KeyCode.W);
        private bool OnDown => Input.GetKey(KeyCode.S);

        #endregion

        #region Unity Funcs

        private void Start()
        {
            PlayAnimationForState(MovementState.Idle);
        }

        private void Update()
        {
            HandleInput();
        }

        #endregion

        #region Input Funcs

        private void HandleInput()
        {
            var rightMove = OnRight;
            var leftMove = OnLeft;
            var upMove = OnUp;
            var downMove = OnDown;
            
            if(rightMove) 
                AddInput(MovementState.WalkingRight);
            else 
                RemoveInput(MovementState.WalkingRight);
            
            if(leftMove) 
                AddInput(MovementState.WalkingLeft);
            else
                RemoveInput(MovementState.WalkingLeft);
            
            if(upMove)
                AddInput(MovementState.WalkingUp);
            else
                RemoveInput(MovementState.WalkingUp);
            
            if(downMove)
                AddInput(MovementState.WalkingDown);
            else
                RemoveInput(MovementState.WalkingDown);
        }
        private void AddInput(MovementState state)
        {
            if (_inputStack.Contains(state)) return;
            _inputStack.Add(state); 
            UpdateState();
        }

        private void RemoveInput(MovementState state)
        {
            if (!_inputStack.Contains(state)) return;
            _inputStack.Remove(state);
            UpdateState();
        }

        private void UpdateState()
        {
            MovementState newState = _inputStack.Count > 0 ? _inputStack.Last() : MovementState.Idle;

            if (newState != _movementState)
            {
                _movementState = newState;
                PlayAnimationForState(_movementState);
            }
        }

        private void PlayAnimationForState(MovementState movementState)
        {
            spriteAnimator.SetSpriteList(movementState switch
            {
                MovementState.Idle => idleWalkSprites,
                MovementState.WalkingUp => upWalkSprites,
                MovementState.WalkingDown => downWalkSprites,
                MovementState.WalkingLeft => leftWalkSprites,
                MovementState.WalkingRight => rightWalkSprites,
            });
            
            spriteAnimator.StartAnimation();
        }

        #endregion
    }

    [Serializable]
    public enum MovementState
    {
        Idle,
        WalkingRight,
        WalkingLeft,
        WalkingUp,
        WalkingDown
    }
}