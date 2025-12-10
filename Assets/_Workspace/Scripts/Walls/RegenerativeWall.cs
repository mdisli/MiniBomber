using _Workspace.Scripts.TileMapClasses.Data;
using DG.Tweening;
using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class RegenerativeWall : BaseWall
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
            RemoveAndStartCountDownForRegenerate();
        }

        protected override void OnDamage()
        {
            wallRenderer.DOFade((float)_currentHealthCount / _maxHealthCount, .1f).SetEase(Ease.Linear).SetLink(gameObject);
        }

        #endregion

        private void RemoveAndStartCountDownForRegenerate()
        {
            wallCollider.enabled = false;
            wallRenderer.DOFade(0, 0).SetLink(gameObject);

            wallRenderer.DOFade(1, _regenerateAfter)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    _currentHealthCount = _maxHealthCount;
                    wallCollider.enabled = true;
                });
        }
    }
}