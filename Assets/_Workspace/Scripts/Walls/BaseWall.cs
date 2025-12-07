using _Workspace.Scripts.Interfaces;
using _Workspace.Scripts.TileMapClasses.Data;
using DG.Tweening;
using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public abstract class BaseWall : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("References")]
        [SerializeField] protected SpriteRenderer wallRenderer;
        [SerializeField] protected BoxCollider2D wallCollider;

        protected int _maxHealthCount;
        protected int _currentHealthCount;
        protected int _orderInLayer;

        #endregion

        #region Initializing

        public virtual void Initialize(BaseWallData wallData)
        {
            _currentHealthCount = wallData.healthCount;
            _maxHealthCount = wallData.healthCount;
            _orderInLayer = wallData.orderInLayer;
            
            wallRenderer.sortingOrder = _orderInLayer;
        }

        #endregion

        #region IDamageable

        public virtual void TakeDamage(int amount)
        {
            _currentHealthCount -= amount;
            if (_currentHealthCount <= 0)
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Abstracts

        public virtual void OnDamage()
        {
            wallRenderer.DOFade((float)_currentHealthCount/_maxHealthCount, 0.1f);
        }

        public abstract void OnDestruct();

        #endregion
    }
}