using UnityEngine;
using UnityEngine.Events;

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