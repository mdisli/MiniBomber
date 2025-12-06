using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Workspace.Scripts.Animation
{
    public class SpriteAnimator : MonoBehaviour
    {
        #region Variables

        [Header("Target Sprite Renderer")]
        [SerializeField] private SpriteRenderer targetSpriteRenderer;
        
        [Header("Sprite List")]
        [SerializeField] private Sprite[] spriteList;

        [Header("Animation Settings")] 
        [SerializeField] private bool isLooping;
        [SerializeField] private float animationFrameDuration;

        private CancellationTokenSource _cancellationTokenSource;
        #endregion

        #region Public Funcs

        public void SetSpriteList(Sprite[] newSpriteList) => this.spriteList = newSpriteList;

        public void StartAnimation()
        {
            StopAnimation();
            AnimateSpriteRenderer(_cancellationTokenSource.Token).Forget();
        }

        public void StopAnimation()
        {
            if (_cancellationTokenSource is not null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #endregion

        #region Unity Funcs

        private void OnDestroy()
        {
            StopAnimation();
        }

        #endregion
        private async UniTask AnimateSpriteRenderer(CancellationToken cancellationToken)
        {
            int spriteIndex = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (spriteIndex >= spriteList.Length)
                {
                    if(!isLooping) break;
                    spriteIndex = 0;
                }
                targetSpriteRenderer.sprite = spriteList[spriteIndex];
                spriteIndex++;

                await UniTask.Delay(TimeSpan.FromSeconds(animationFrameDuration), cancellationToken: cancellationToken);
            }
        }
    }
}