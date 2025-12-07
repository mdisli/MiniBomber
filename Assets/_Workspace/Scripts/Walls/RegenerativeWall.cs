using System;
using _Workspace.Scripts.Interfaces;
using _Workspace.Scripts.TileMapClasses.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class RegenerativeWall :BaseWall
    {
        #region Variables

        private float _regenerateAfter;

        #endregion

        #region Initializing

        public override void Initialize(BaseWallData data)
        {
            base.Initialize(data);
            
            _regenerateAfter = data.regenerateAfter;
        }

        #endregion
        private async UniTask RemoveAndStartCountDownForRegenerateAsync()
        {
            wallCollider.enabled = false;
            transform.localScale = Vector3.zero;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_regenerateAfter));
            
            _currentHealthCount = _maxHealthCount;
            transform.localScale = Vector3.one;
            wallCollider.enabled = true;
        }

        public override void OnDestruct()
        {
        }
    }
}