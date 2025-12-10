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
        }

        private void OnDisable()
        {
            gameEventSo.OnRestartLevel -= GameEventSo_OnRestartLevel;
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

        #endregion
    }
}