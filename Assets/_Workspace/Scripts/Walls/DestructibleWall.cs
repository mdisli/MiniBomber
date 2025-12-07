using _Workspace.Scripts.Interfaces;
using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class DestructibleWall : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("References")]
        [SerializeField] private SpriteRenderer wallRenderer;
        
        private int _healthCount;
        private int _orderInLayer;

        #endregion

        #region Initializing

        public void Initialize(int healthCount, int orderInLayer)
        {
            _healthCount = healthCount;
            _orderInLayer = orderInLayer;
            
            wallRenderer.sortingOrder = _orderInLayer;
        }

        #endregion

        #region IDamageable

        public void TakeDamage(int amount)
        {
            _healthCount -= amount;
            if (_healthCount <= 0)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}