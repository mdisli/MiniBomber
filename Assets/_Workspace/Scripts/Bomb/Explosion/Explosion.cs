using _Workspace.Scripts.Animation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Workspace.Scripts.Bomb.Explosion
{
    public class Explosion : MonoBehaviour
    {
        #region Variables

        [Header("Animations")] 
        [SerializeField] private SpriteAnimator spriteAnimator;
        [SerializeField] private Sprite[] explosionStartSprites;
        [SerializeField] private Sprite[] explosionMiddleSprites;
        [SerializeField] private Sprite[] explosionEndSprites;

        #endregion

        #region Funcs

        public void InitializeExplosion(ExplosionState  state)
        {
            spriteAnimator.SetSpriteList(state switch
            {
                ExplosionState.Start => explosionStartSprites,
                ExplosionState.Mid => explosionMiddleSprites,
                ExplosionState.End => explosionEndSprites,
                _ => explosionStartSprites
            });
            
            spriteAnimator.StartAnimationAsync(()=> Destroy(gameObject)).Forget();
        }

        #endregion
    }
}