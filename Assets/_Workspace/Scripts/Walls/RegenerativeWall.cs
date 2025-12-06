using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class RegenerativeWall : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private int _orderInLayer;
        private int _healthCount;
        private float _regenerateAfter;
        

        #endregion

        #region Initialization

        public void Initialize(int orderInLayer, int healthCount, float regenerateAfter)
        {
            _orderInLayer = orderInLayer;
            _healthCount = healthCount;
            _regenerateAfter = regenerateAfter;
            
            _spriteRenderer.sortingOrder = _orderInLayer;
        }

        #endregion
    }
}