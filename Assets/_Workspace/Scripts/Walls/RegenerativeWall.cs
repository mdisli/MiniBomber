using System;
using _Workspace.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Workspace.Scripts.Walls
{
    public class RegenerativeWall : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("References")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        
        private int _orderInLayer;
        private int _maxHealthCount;
        private int _currentHealthCount;
        private float _regenerateAfter;
        

        #endregion

        #region Initialization

        public void Initialize(int orderInLayer, int healthCount, float regenerateAfter)
        {
            _orderInLayer = orderInLayer;
            _maxHealthCount = healthCount;
            _currentHealthCount = healthCount;
            _regenerateAfter = regenerateAfter;
            
            _spriteRenderer.sortingOrder = _orderInLayer;
        }

        #endregion

        public void TakeDamage(int amount)
        {
            _currentHealthCount -= amount;

            if (_currentHealthCount <= 0)
            {
                RemoveAndStartCountDownForRegenerateAsync().Forget();
            }
        }

        private async UniTask RemoveAndStartCountDownForRegenerateAsync()
        {
            _boxCollider2D.enabled = false;
            transform.localScale = Vector3.zero;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_regenerateAfter));
            
            _currentHealthCount = _maxHealthCount;
            transform.localScale = Vector3.one;
            _boxCollider2D.enabled = true;
        }
    }
}