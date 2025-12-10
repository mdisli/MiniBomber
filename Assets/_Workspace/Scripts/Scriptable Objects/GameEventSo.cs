using _Workspace.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace _Workspace.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Game Event So", menuName = "So/Event/GameEventSo", order = 0)]
    public class GameEventSo : ScriptableObject
    {
        #region Events
        
        public event UnityAction OnGameStart;
        public event UnityAction OnGameFinish;
        public event UnityAction OnGameOver;

        public event UnityAction OnRestartLevel;

        public event UnityAction<BaseEnemy> OnEnemyDeath; 
        
        public GameState GameState{get; private set;}
        #endregion

        #region Invoking

        public void InvokeGameStart()
        {
            OnGameStart?.Invoke();
            GameState = GameState.Started;
        }

        public void InvokeGameFinish()
        {
            if(GameState is not GameState.Started) return;
            GameState = GameState.Finished;
            OnGameFinish?.Invoke();
            
        }

        public void InvokeGameOver()
        {
            if(GameState is not GameState.Started) return;
            GameState = GameState.Failed;
            OnGameOver?.Invoke();
            
        }
        
        public void InvokeRestartLevel() => OnRestartLevel?.Invoke();

        public void InvokeOnEnemyDeath(BaseEnemy enemy) => OnEnemyDeath?.Invoke(enemy);

        #endregion
    }

    public enum GameState
    {
        Started,
        Finished,
        Failed,
    }
}