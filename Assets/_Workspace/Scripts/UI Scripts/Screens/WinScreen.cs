using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Scripts.UI_Scripts.Screens
{
    public class WinScreen : BaseScreen
    {
        #region Variables

        [SerializeField] private Button playAgainButton;

        #endregion

        #region Unity Funcs

        private void OnEnable()
        {
            playAgainButton.onClick.AddListener(PlayAgainButtonClick);
        }

        private void OnDisable()
        {
            playAgainButton.onClick.RemoveListener(PlayAgainButtonClick);
        }

        #endregion

        #region Methods

        private void PlayAgainButtonClick()
        {
            gameEventSo.InvokeRestartLevel();
        }

        #endregion
    }
}