using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class DestructibleWall : MonoBehaviour
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
    }
}