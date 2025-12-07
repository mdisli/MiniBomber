using System;
using _Workspace.Scripts.Animation;
using _Workspace.Scripts.Scriptable_Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;

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

        #endregion

        #region Virtual Methods

        public virtual async UniTask StartTimer()
        {
            spriteAnimator.SetSpriteList(countDownAnimationSprites);
            spriteAnimator.StartAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(bombVariables.explosionDelay));
            spriteAnimator.StopAnimation();

            Explode();
        }

        public virtual void Explode()
        {
            Destroy(gameObject);
        }
        #endregion

        
    }
}