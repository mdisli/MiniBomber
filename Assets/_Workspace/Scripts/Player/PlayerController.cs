using System;
using System.Collections.Generic;
using System.Linq;
using _Workspace.Scripts.Animation;
using _Workspace.Scripts.Scriptable_Objects;
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

        [Header("So References")]
        [SerializeField] private PlayerVariables playerVariables;

        [Header("Movement")] 
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Transform[] rayPoints;
        [SerializeField]private float _rayDistance;
        private float _movementSpeed;
        private LayerMask _obstacleMask;
        
        // Bomb
        private int _bombCapacity;
        private float _bombRegenDuration;
        
        private MovementState _currentMovementState;
        private List<MovementState> _inputStack = new List<MovementState>();
        
        private bool OnLeft => Input.GetKey(KeyCode.A);
        private bool OnRight => Input.GetKey(KeyCode.D);
        private bool OnUp => Input.GetKey(KeyCode.W);
        private bool OnDown => Input.GetKey(KeyCode.S);

        #endregion

        #region Unity Funcs

        private void Start()
        {
            ApplyVariables();
            PlayAnimationForState(MovementState.Idle);
        }

        private void Update()
        {
            HandleInput();
            HandleMovement();
        }

        private void OnDrawGizmos()
        {
            // foreach (var rayPoint in rayPoints)
            // {
            //     Gizmos.DrawRay(rayPoint.transform.position,Vector2.right * _rayDistance);
            //     Gizmos.DrawRay(rayPoint.transform.position,Vector2.left * _rayDistance);
            //     Gizmos.DrawRay(rayPoint.transform.position,Vector2.up * _rayDistance);
            //     Gizmos.DrawRay(rayPoint.transform.position,Vector2.down * _rayDistance);    
            // }
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _rayDistance);
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

            if (newState == _currentMovementState) return;
            
            _currentMovementState = newState;
            PlayAnimationForState(_currentMovementState);
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
                _ => idleWalkSprites
            });
            
            spriteAnimator.StartAnimation();
        }

        #endregion

        #region Movement Functions

        private void HandleMovement()
        {
            Vector2 direction = Vector2.zero;

            switch (_currentMovementState)
            {
                case MovementState.WalkingRight:
                    direction = Vector2.right;
                    break;
                case MovementState.WalkingLeft:
                    direction = Vector2.left;
                    break;
                case MovementState.WalkingUp:
                    direction = Vector2.up;
                    break;
                case MovementState.WalkingDown:
                    direction = Vector2.down;
                    break;
                case MovementState.Idle:
                    direction = Vector2.zero;
                    break;
            }
            
            rigidbody2D.velocity = direction * _movementSpeed;
        }
        

        #endregion

        #region Player Variables

        private void ApplyVariables()
        {
            _movementSpeed = playerVariables.movementSpeed;
            _bombRegenDuration = playerVariables.bombRegenDuration;
            _obstacleMask = playerVariables.obstacleLayers;
            _bombCapacity = playerVariables.bombCapacity;
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