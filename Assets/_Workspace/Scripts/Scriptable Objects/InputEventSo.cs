using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Workspace.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Input Event So", menuName = "So/Event/Input Event So", order = 0)]
    public class InputEventSo : ScriptableObject
    {
        #region Events

        public event UnityAction<ButtonType> OnButtonPressed;
        public event UnityAction<ButtonType> OnButtonReleased;

        #endregion

        #region Invoking

        public void InvokeOnButtonPressed(ButtonType buttonType) => OnButtonPressed?.Invoke(buttonType);
        public void InvokeOnButtonReleased(ButtonType buttonType) => OnButtonReleased?.Invoke(buttonType);

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
                OnButtonPressed = null;
                OnButtonReleased = null;
            }
        }
#endif

        #endregion
    }

    public enum ButtonType
    {
        Right,
        Left,
        Up,
        Down,
        Bomb
    }
}