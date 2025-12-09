using System;
using System.Collections.Generic;
using _Workspace.Scripts.Grid_System;
using _Workspace.Scripts.Scriptable_Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Workspace.Scripts.Bomb
{
    public class BombBag : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")] 
        [SerializeField] private BaseBomb standardBombPrefab;

        [Header("Bombs")] 
        [SerializeField] private List<BaseBomb> standardBombPool;
        [SerializeField] private List<BaseBomb> collectedSpecialBombList;

        private GridManager _gridManager;
        private Transform _bombParent;
        private PlayerVariables _playerVariables;
        private bool _canDropBomb = true;
        #endregion

        #region Initializing

        public void Initialize(GridManager gridManager, PlayerVariables  playerVariables)
        {
            _gridManager = gridManager;
            _bombParent = new GameObject("Bomb Pool").transform;
            _playerVariables = playerVariables;
        }

        #endregion

        #region Bomb Functions
        
        public void DropBomb()
        {
            Vector2 position = transform.position;
            position = _gridManager.GetTileCenterWithPosition(position);
            
            BaseBomb bomb = collectedSpecialBombList.Count > 0  ? collectedSpecialBombList[0] : GetStandardBomb(); // Collected bomb var ise onu kullan yok ise standard bomb
            
            if(bomb is null) // Kullanılabilir bomb yok
                return;
            
            bomb.gameObject.SetActive(true);
            bomb.transform.position = position;
            bomb.StartTimer(()=>ReleaseBomb(bomb)).Forget();
            StartCountDownTimer();
        }

        private async void StartCountDownTimer()
        {
            _canDropBomb = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_playerVariables.bombRegenDuration));
            _canDropBomb = true;
        }
        #endregion

        #region Pool Functions

        private BaseBomb GetStandardBomb()
        {
            if (!_canDropBomb)
                return null;
            
            if (standardBombPool.Count > 0)
            {
                var poolBomb =  standardBombPool[0];
                standardBombPool.RemoveAt(0);
                return poolBomb;
            }
            
            BaseBomb bomb = Instantiate(standardBombPrefab, Vector3.zero, Quaternion.identity,_bombParent);

            return bomb;
        }

        private void ReleaseBomb(BaseBomb bomb)
        {
            standardBombPool.Add(bomb);
            bomb.gameObject.SetActive(false);
        }
        #endregion
        
    }
}