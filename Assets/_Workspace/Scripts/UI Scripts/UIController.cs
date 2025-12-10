using _Workspace.Scripts.Scriptable_Objects;
using _Workspace.Scripts.UI_Scripts.Screens;
using UnityEngine;

namespace _Workspace.Scripts.UI_Scripts
{
    public class UIController : MonoBehaviour
    {
        #region Variables

        [Header("So References")] 
        [SerializeField] private GameEventSo gameEventSo;

        [Header("Screen References")] 
        [SerializeField] private BaseScreen winScreen;
        [SerializeField] private BaseScreen loseScreen;

        #endregion

        #region Unity Funcs

        private void OnEnable()
        {
            gameEventSo.OnGameFinish += GameEventSo_OnGameFinish;
            gameEventSo.OnGameOver += GameEventSo_OnGameOver;
        }

        private void OnDisable()
        {
            gameEventSo.OnGameFinish -= GameEventSo_OnGameFinish;
            gameEventSo.OnGameOver -= GameEventSo_OnGameOver;
        }

        #endregion

        #region Callbacks

        private void GameEventSo_OnGameOver()
        {
            loseScreen.OpenScreen();
        }

        private void GameEventSo_OnGameFinish()
        {
            winScreen.OpenScreen();
        }

        #endregion
    }
}