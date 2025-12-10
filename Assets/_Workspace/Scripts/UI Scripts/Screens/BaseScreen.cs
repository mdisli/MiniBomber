using _Workspace.Scripts.Scriptable_Objects;
using DG.Tweening;
using UnityEngine;

namespace _Workspace.Scripts.UI_Scripts.Screens
{
    public class BaseScreen : MonoBehaviour
    {
        #region Variables

        [Header("So References")] 
        [SerializeField] protected GameEventSo gameEventSo;
        
        [Header("Screen References")]
        [SerializeField] protected CanvasGroup canvasGroup;

        #endregion

        #region Public Funcs

        public void OpenScreen()
        {
            transform.localScale = Vector3.one;
            canvasGroup.DOFade(1, .3f).SetEase(Ease.Linear);
        }

        public void CloseScreen()
        {
            canvasGroup.DOFade(1, .3f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    transform.localScale = Vector3.zero;
                });
        }

        

        #endregion
    }
}