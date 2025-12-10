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

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        #endregion

        #region Public Funcs

        private void SetSpriteList(Sprite[] newSpriteList) => this.spriteList = newSpriteList;

        public async UniTask StartAnimationAsync(Sprite[] spriteSet = null, Action onComplete = null, Action onStart = null)
        {
            StopAnimation();

            if (spriteSet is not null)
                SetSpriteList(spriteSet);

            onStart?.Invoke();

            var token = _cancellationTokenSource.Token;
            await AnimateSpriteRenderer(token);

            if (!token.IsCancellationRequested)
                onComplete?.Invoke();
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

        public void ChangeLoopStatus(bool status)
        {
            isLooping = status;
        }
        #endregion

        #region Unity Funcs

        private void OnDestroy()
        {
            if (_cancellationTokenSource is not null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        #endregion
        private async UniTask AnimateSpriteRenderer(CancellationToken cancellationToken)
        {
            if (spriteList is null or { Length: 0 })
            {
                Debug.LogWarning("SpriteList is null",gameObject);
                return;
            }

            try
            {
                int spriteIndex = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (spriteIndex >= spriteList.Length)
                    {
                        if (!isLooping)
                        {
                            targetSpriteRenderer.sprite = null;
                            break;
                        }
                        spriteIndex = 0;
                    }
                    targetSpriteRenderer.sprite = spriteList[spriteIndex];
                    spriteIndex++;

                    await UniTask.Delay(TimeSpan.FromSeconds(animationFrameDuration), cancellationToken: cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // PlayMode çıkışında normal - ignore
            }
        }
    }
}