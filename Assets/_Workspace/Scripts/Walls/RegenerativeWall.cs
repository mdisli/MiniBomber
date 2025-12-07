using System;
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
            wallRenderer.DOFade((float)_maxHealthCount / _currentHealthCount, .1f).SetEase(Ease.Linear);
        }

        #endregion
        private async UniTask RemoveAndStartCountDownForRegenerateAsync()
        {
            wallCollider.enabled = false;
            wallRenderer.DOFade(0, 0);

            await wallRenderer.DOFade(1, _regenerateAfter).SetEase(Ease.Linear);
            // await UniTask.Delay(TimeSpan.FromSeconds(_regenerateAfter));
            
            _currentHealthCount = _maxHealthCount;
            wallCollider.enabled = true;
        }
    }
}