using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Scripts.UI_Scripts.Screens
{
    public class LoseScreen : BaseScreen
    {
        #region Variables

        [SerializeField] private Button tryAgainButton;

        #endregion

        #region Unity Funcs

        private void OnEnable()
        {
            tryAgainButton.onClick.AddListener(PlayAgainButtonClick);
        }

        private void OnDisable()
        {
            tryAgainButton.onClick.RemoveListener(PlayAgainButtonClick);
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