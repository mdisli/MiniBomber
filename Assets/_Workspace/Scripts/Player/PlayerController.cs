using System;
using System.Collections.Generic;
using System.Linq;
using _Workspace.Scripts.Animation;
using _Workspace.Scripts.Bomb;
using _Workspace.Scripts.Grid_System;
using _Workspace.Scripts.Scriptable_Objects;
using Cysharp.Threading.Tasks;
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

        [Header("Other References")] 
        [SerializeField] private GridManager gridManager;

        [Header("Movement")] 
        [SerializeField] private Rigidbody2D rigidbody2D;
        
        [Header("Bomb")] 
        [SerializeField] private BaseBomb standardBombPrefab;
        
        private MovementState _currentMovementState;
        private List<MovementState> _inputStack = new List<MovementState>();
        
        private bool OnLeft => Input.GetKey(KeyCode.A);
        private bool OnRight => Input.GetKey(KeyCode.D);
        private bool OnUp => Input.GetKey(KeyCode.W);
        private bool OnDown => Input.GetKey(KeyCode.S);
        private bool OnSpace => Input.GetKeyDown(KeyCode.Space);

        #endregion

        #region Unity Funcs

        private void Start()
        {
            PlayAnimationForState(MovementState.Idle);
        }

        private void Update()
        {
            HandleInput();
            HandleMovement();
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

            if (OnSpace)
                DropBomb();
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
            
            rigidbody2D.velocity = direction * playerVariables.movementSpeed;
        }
        

        #endregion

        #region Bomb Functions

        private void DropBomb()
        {
            Vector2 position = transform.position;
            position = gridManager.GetTileCenterWithPosition(position);
            
            BaseBomb bomb = Instantiate(standardBombPrefab, position, Quaternion.identity);
            bomb.StartTimer().Forget();
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