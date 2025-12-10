using System;
using System.Threading;
using _Workspace.Scripts.Interfaces;
using _Workspace.Scripts.TileMapClasses.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class RegenerativeWall :BaseWall
    {
        #region Variables

        private float _regenerateAfter;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        #endregion

        #region Unity Funcs

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }

        #endregion
        #region Abstracts

        public override void Initialize(BaseWallData data)
        {
            base.Initialize(data);
            
            _regenerateAfter = data.regenerateAfter;
        }
        
        protected override void OnDestruct()
        {
            RemoveAndStartCountDownForRegenerateAsync().Forget();
        }

        protected override void OnDamage()
        {
            wallRenderer.DOFade((float)_currentHealthCount / _maxHealthCount, .1f).SetEase(Ease.Linear);
        }

        #endregion
        private async UniTask RemoveAndStartCountDownForRegenerateAsync()
        {
            if(_cancellationTokenSource is null || _cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource = new CancellationTokenSource();
            
            wallCollider.enabled = false;
            wallRenderer.DOFade(0, 0);

            await wallRenderer.DOFade(1, _regenerateAfter).SetEase(Ease.Linear).WithCancellation(_cancellationTokenSource.Token);
            
            _currentHealthCount = _maxHealthCount;
            wallCollider.enabled = true;
        }
    }
}