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
            OnGameFinish?.Invoke();
            GameState = GameState.Finished;
        }

        public void InvokeGameOver()
        {
            OnGameOver?.Invoke();
            GameState = GameState.Failed;
        }
        
        public void InvokeRestartLevel() => OnRestartLevel?.Invoke();

        #endregion
    }

    public enum GameState
    {
        Started,
        Finished,
        Failed,
    }
}