using System;
using _Workspace.Scripts.Scriptable_Objects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Workspace.Scripts.UI_Scripts
{
    public class InputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables

        [Header("Button Properties")] 
        [SerializeField] private ButtonType buttonType;
        
        [Header("So References")]
        [SerializeField] private InputEventSo inputEventSo;
        
        [Header("UI References")]
        [SerializeField] private Image buttonImage;
        
        [Header("Animation Settings")]
        [SerializeField] private Color imageColorOnClick = new Color(1f, 1f, 1f,0.6f);
        [SerializeField] private float imageScaleOnClick = 0.9f;

        private Color _buttonImageStandardColor;
        #endregion

        #region Unity Funcs

        private void Start()
        {
            _buttonImageStandardColor = buttonImage.color;
        }

        #endregion

        #region Pointer Handler

        public void OnPointerDown(PointerEventData eventData)
        {
            OnButtonClick();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnButtonReleased();
        }

        #endregion

        private void OnButtonClick()
        {
            transform.DOScale(imageScaleOnClick, .1f).SetEase(Ease.Linear);
            buttonImage.DOColor(imageColorOnClick,.1f).SetEase(Ease.Linear);
            inputEventSo.InvokeOnButtonPressed(buttonType);
        }

        private void OnButtonReleased()
        {
            transform.DOScale(1, .1f).SetEase(Ease.Linear);
            buttonImage.DOColor(_buttonImageStandardColor,.1f).SetEase(Ease.Linear);
            inputEventSo.InvokeOnButtonReleased(buttonType);
        }
    }
}