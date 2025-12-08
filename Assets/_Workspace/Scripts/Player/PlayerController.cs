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
        [SerializeField] private InputEventSo inputEventSo;

        [Header("Other References")] 
        [SerializeField] private GridManager gridManager;

        [Header("Movement")] 
        [SerializeField] private Rigidbody2D rigidbody2D;
        
        [Header("Bomb")] 
        [SerializeField] private BaseBomb standardBombPrefab;
        
        private MovementState _currentMovementState;
        [SerializeField]private List<MovementState> _inputStack = new List<MovementState>();
        
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
            UpdateState();
            HandleMovement();
            
        }

        private void OnEnable()
        {
            inputEventSo.OnButtonPressed += InputEventSo_OnButtonPressed;
            inputEventSo.OnButtonReleased += InputEventSo_OnButtonReleased;
        }

        private void OnDisable()
        {
            inputEventSo.OnButtonPressed -= InputEventSo_OnButtonPressed;
            inputEventSo.OnButtonReleased -= InputEventSo_OnButtonReleased;
        }

        #endregion

        #region Input Funcs

        private void HandleInput()
        {
            if(Input.GetKeyDown(KeyCode.D)) 
                AddInput(MovementState.WalkingRight);
            else if(Input.GetKeyUp(KeyCode.D))
                RemoveInput(MovementState.WalkingRight);
            
            if(Input.GetKeyDown(KeyCode.A)) 
                AddInput(MovementState.WalkingLeft);
            else if(Input.GetKeyUp(KeyCode.A))
                RemoveInput(MovementState.WalkingLeft);
            
            if(Input.GetKeyDown(KeyCode.W))
                AddInput(MovementState.WalkingUp);
            else if(Input.GetKeyUp(KeyCode.W))
                RemoveInput(MovementState.WalkingUp);
            
            if(Input.GetKeyDown(KeyCode.S))
                AddInput(MovementState.WalkingDown);
            else if(Input.GetKeyUp(KeyCode.S))
                RemoveInput(MovementState.WalkingDown);

            if (OnSpace)
                DropBomb();
        }
        private void AddInput(MovementState state)
        {
            if (_inputStack.Contains(state)) return;
            _inputStack.Add(state); 
            
        }

        private void RemoveInput(MovementState state)
        {
            if (!_inputStack.Contains(state)) return;
            _inputStack.Remove(state);
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
            var spriteSet = movementState switch
            {
                MovementState.Idle => idleWalkSprites,
                MovementState.WalkingUp => upWalkSprites,
                MovementState.WalkingDown => downWalkSprites,
                MovementState.WalkingLeft => leftWalkSprites,
                MovementState.WalkingRight => rightWalkSprites,
                _ => idleWalkSprites
            };
            
            spriteAnimator.StartAnimationAsync(spriteSet).Forget();
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
            // Vector2 targetPosition = (Vector2)transform.position + (direction * (playerVariables.movementSpeed * Time.deltaTime));
            // rigidbody2D.MovePosition(targetPosition);
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

        #region Callbacks

        private void InputEventSo_OnButtonReleased(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Right:
                    RemoveInput(MovementState.WalkingRight);
                    break;
                case ButtonType.Left:
                    RemoveInput(MovementState.WalkingLeft);
                    break;
                case ButtonType.Up:
                    RemoveInput(MovementState.WalkingUp);
                    break;
                case ButtonType.Down:
                    RemoveInput(MovementState.WalkingDown);
                    break;
            }
        }

        private void InputEventSo_OnButtonPressed(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Right:
                    AddInput(MovementState.WalkingRight);
                    break;
                case ButtonType.Left:
                    AddInput(MovementState.WalkingLeft);
                    break;
                case ButtonType.Up:
                    AddInput(MovementState.WalkingUp);
                    break;
                case ButtonType.Down:
                    AddInput(MovementState.WalkingDown);
                    break;
                case ButtonType.Bomb:
                    DropBomb();
                    break;
            }
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