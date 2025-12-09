using System;
using _Workspace.Scripts.Animation;
using _Workspace.Scripts.Bomb.Explosion;
using _Workspace.Scripts.Interfaces;
using _Workspace.Scripts.Scriptable_Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Workspace.Scripts.Bomb
{
    public abstract class BaseBomb : MonoBehaviour
    {
        #region Variables

        [Header("So References")] 
        [SerializeField] protected BombVariables bombVariables;

        [Header("Animation")] 
        [SerializeField] protected SpriteAnimator spriteAnimator;
        [SerializeField] protected Sprite[] countDownAnimationSprites;


        [Header("Explosion")]
        [SerializeField] private Explosion.Explosion explosionPrefab;
        #endregion

        #region Virtual Methods

        public virtual async UniTask StartTimer(Action onExplode=null)
        {
            spriteAnimator.StartAnimationAsync(countDownAnimationSprites).Forget();

            await UniTask.Delay(TimeSpan.FromSeconds(bombVariables.explosionDelay));
            
            spriteAnimator.StopAnimation();

            onExplode?.Invoke();
            Explode();
        }

        protected virtual void Explode()
        {
            Vector2 curPos = transform.position;

            var directions = bombVariables.bombDirections;
            
            // Mevcut pozisyonu patlat
            ShowExplosion(Vector2.zero, curPos,ExplosionState.Start);
            
            if(directions.HasFlag(BombDirections.Right))
                SpreadFire(curPos,Vector2.right, bombVariables.range);
            
            if(directions.HasFlag(BombDirections.Left))
                SpreadFire(curPos,Vector2.left, bombVariables.range);
            
            if(directions.HasFlag(BombDirections.Down))
                SpreadFire(curPos,Vector2.down, bombVariables.range);
            
            if(directions.HasFlag(BombDirections.Up))
                SpreadFire(curPos,Vector2.up, bombVariables.range);
            
        }

        private void SpreadFire(Vector2 currentPosition,Vector2 direction, int range)
        {
            for (int i = 0; i < range-1; i++)
            {
                (bool canPass, RaycastHit2D hit) canExplosionPassThrough = CanExplosionPassThrough(currentPosition, direction); 
                if (!canExplosionPassThrough.canPass) // Bir nesneye çarpıyor, hasar alabilir ise hasar ver.
                {
                    canExplosionPassThrough.hit.transform.TryGetComponent<IDamageable>(out var damageable);
                    damageable?.TakeDamage(bombVariables.damage);
                    return;
                }
                
                currentPosition += direction;

                // Eğer sonuncu değil ise ve bir sonraki noktaya geçebiliyor ise
                bool canExplosionPassThroughNext = i < range - 2 && CanExplosionPassThrough(currentPosition, direction).canPass; 
                
                ShowExplosion(direction,currentPosition, canExplosionPassThroughNext ? ExplosionState.Mid : ExplosionState.End);
            }
        }

        private void ShowExplosion(Vector2 direction, Vector2 position, ExplosionState explosionState)
        {
            float angle = 0;

            if(direction != Vector2.zero)
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Explosion.Explosion explosion =
                Instantiate(explosionPrefab, position, Quaternion.AngleAxis(angle, Vector3.forward));
            
            explosion.InitializeExplosion(explosionState);
        }

        private (bool canPass, RaycastHit2D hit) CanExplosionPassThrough(Vector2 from, Vector2 direction)
        {
            var hit = Physics2D.Raycast(from, direction,1,bombVariables.obstacleMasks);
            return (hit.collider is null,hit);
        }
        #endregion

        
    }
}