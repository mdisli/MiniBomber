using System.Collections.Generic;
using _Workspace.Scripts.Enemy;
using _Workspace.Scripts.Scriptable_Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Workspace.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        [Header("So References")] 
        [SerializeField] private GameEventSo gameEventSo;

        [Header("Enemy References")] 
        [SerializeField] private List<BaseEnemy> enemies;
        
        //Scene Management
        private AsyncOperation _sceneLoadOperation;
        
        
        #endregion

        #region Unity Funcs

        private void Start()
        {
            Application.targetFrameRate = 60;
            
            gameEventSo.InvokeGameStart();
            
            LoadScene();
        }

        private void OnEnable()
        {
            gameEventSo.OnRestartLevel += GameEventSo_OnRestartLevel;
            gameEventSo.OnEnemyDeath += GameEventSo_OnEnemyDeath;
        }

        private void OnDisable()
        {
            gameEventSo.OnRestartLevel -= GameEventSo_OnRestartLevel;
            gameEventSo.OnEnemyDeath -= GameEventSo_OnEnemyDeath;
        }

        #endregion

        #region Scene Management

        private void LoadScene()
        {
            _sceneLoadOperation = SceneManager.LoadSceneAsync(0);
            _sceneLoadOperation!.allowSceneActivation = false;
        }

        private void ChangeScene()
        {
            _sceneLoadOperation.allowSceneActivation = true;
        }

        #endregion

        #region Callbacks

        private void GameEventSo_OnRestartLevel()
        {
            ChangeScene();
        }

        private void GameEventSo_OnEnemyDeath(BaseEnemy arg0)
        {
            enemies.Remove(arg0);
            
            if(enemies.Count == 0)
                gameEventSo.InvokeGameFinish();
        }
        #endregion
    }
}