using System;
using System.Collections.Generic;
using System.Threading;
using _Workspace.Scripts.Animation;
using _Workspace.Scripts.Bomb;
using _Workspace.Scripts.Grid_System;
using _Workspace.Scripts.Interfaces;
using _Workspace.Scripts.Scriptable_Objects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Workspace.Scripts.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("Animation")]
        [SerializeField] private SpriteAnimator spriteAnimator;
        [SerializeField] private Sprite[] rightWalkSprites;
        [SerializeField] private Sprite[] leftWalkSprites;
        [SerializeField] private Sprite[] upWalkSprites;
        [SerializeField] private Sprite[] downWalkSprites;
        [SerializeField] private Sprite[] idleSprites;
        [SerializeField] private Sprite[] deathSprites;
        
        [Header("Other References")]
        [SerializeField] private GridManager gridManager;

        [Header("So References")] 
        [SerializeField] private PlayerVariables enemyVariables;
        
        [Header("Movement")]
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private CircleCollider2D circleCollider2D;

        [Header("Bomb")] [SerializeField] private BombBag bombBag;

        //Direction control
        private List<EnemyRoute> _availableTargetPoints = new List<EnemyRoute>(4);
        private CancellationTokenSource _cancellationTokenSource;

        private int _healthCount;
        #endregion

        #region Unity Funcs

        private async void Start()
        {
            _healthCount = enemyVariables.healthCount;
            
            bombBag.Initialize(gridManager,enemyVariables);
            await UniTask.Delay(500);
            Move().Forget();
        }

        private void OnDestroy()
        {
            CancelMovement();
        }

        #endregion

        #region Movement

        private void FindAvailableRoutes()
        {
            _availableTargetPoints.Clear();
            
            Vector2 origin = gridManager.GetTileCenterWithPosition((Vector2)transform.position);

            var rightDir = CheckRouteAvailable(origin, Vector2.right);
            var leftDir = CheckRouteAvailable(origin, Vector2.left);
            var upDir = CheckRouteAvailable(origin, Vector2.up);
            var downDir = CheckRouteAvailable(origin, Vector2.down);

            if(rightDir.isAvailable)
                _availableTargetPoints.Add(rightDir);
            if(leftDir.isAvailable)
                _availableTargetPoints.Add(leftDir);
            if(upDir.isAvailable)
                _availableTargetPoints.Add(upDir);
            if(downDir.isAvailable)
                _availableTargetPoints.Add(downDir);
        }
        private EnemyRoute CheckRouteAvailable(Vector2 origin,Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity,enemyVariables.obstacleLayers);

            if (hit.collider == null) return new EnemyRoute()
            {
                isAvailable = false
            };
            
            Vector2 hitPosition = hit.point;
                
            var targetTilePosition = gridManager.GetTileCenterWithPosition(hitPosition); // engel olan tile

            float targetDistance = Vector2.Distance(targetTilePosition, (Vector2)transform.position);
            if (!(targetDistance >= 1.45f))
                return new EnemyRoute()
                {
                    isAvailable = false
                };
            
            targetTilePosition -= direction; // engelden bir önceki tile
            
            return new EnemyRoute()
            {
                distance = targetDistance,
                isAvailable = true,
                targetPoint = targetTilePosition,
                direction = direction
            };

        }
        private EnemyRoute ChooseRoute()
        {
            //_availableTargetPoints.Sort((x,y) => x.distance.CompareTo(y.distance));
            
            return _availableTargetPoints[Random.Range(0, _availableTargetPoints.Count)];
        }

        private async UniTask Move()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                FindAvailableRoutes();
                
                EnemyRoute route = ChooseRoute();

                spriteAnimator.StartAnimationAsync(GetSpriteSetWithDirection(route.direction)).Forget();
                await transform.DOMove(route.targetPoint, route.distance / enemyVariables.movementSpeed).SetEase(Ease.Linear);
            }
        }

        private void CancelMovement()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
        private Sprite[] GetSpriteSetWithDirection(Vector2 direction)
        {
            if(direction == Vector2.left)
                return leftWalkSprites;
            else if(direction == Vector2.right)
                return rightWalkSprites;
            else if(direction == Vector2.up)
                return upWalkSprites;
            else if(direction == Vector2.down)
                return downWalkSprites;
            else return idleSprites;
        }
        #endregion

        public void TakeDamage(int amount)
        {
            _healthCount -= amount;
            _healthCount = Mathf.Clamp(_healthCount, 0, int.MaxValue);

            if (_healthCount <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            circleCollider2D.enabled = false;
            rigidbody2D.simulated = false;
            spriteAnimator.ChangeLoopStatus(false);
            CancelMovement();
            
            spriteAnimator.StartAnimationAsync(deathSprites, onComplete: () =>
            {
                Destroy(gameObject);
            }).Forget();
        }
    }

    [Serializable]
    public class EnemyRoute
    {
        public Vector2 direction;
        public bool isAvailable;
        public Vector2 targetPoint;
        public float distance;
    }
}