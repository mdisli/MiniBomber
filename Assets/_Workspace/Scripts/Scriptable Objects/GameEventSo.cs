using _Workspace.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

        #region Editor PlayMode Cleanup

        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                OnGameStart = null;
                OnGameFinish = null;
                OnGameOver = null;
                OnRestartLevel = null;
                OnEnemyDeath = null;
                GameState = default;
            }
        }
#endif

        #endregion
    }

    public enum GameState
    {
        Started,
        Finished,
        Failed,
    }
}