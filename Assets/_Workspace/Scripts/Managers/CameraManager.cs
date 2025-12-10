using System;
using _Workspace.Scripts.Grid_System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Workspace.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables

        [Header("References")] 
        [SerializeField] private GridManager gridManager;

        [SerializeField] private float _padding;

        private Camera _cam;
        #endregion

        #region Unity Functions

        private async void Start()
        {
            _cam = Camera.main;

            await UniTask.Delay(5000);
            
            AdjustCameraSize();
        }

        #endregion

        #region Funcs

        [ContextMenu("Adjust Camera Size")]
        private void AdjustCameraSize()
        {
            Bounds bounds = gridManager.GetGridBounds();
            
            transform.position = new Vector3(bounds.center.x, bounds.center.y, -10f);
            
            float targetHeight = bounds.size.y + _padding;
            float targetWidth = bounds.size.x + _padding;

            Debug.Log("Width :" + Screen.width);
            Debug.Log("Height :" + Screen.height);
            float screenRatio = (float)Screen.height / (float)Screen.width; // Cihazın oranı
            float targetRatio = targetWidth / targetHeight; // Haritanın oranı
            
            
            Debug.Log("Width :" + Screen.width);
            Debug.Log("Height :" + Screen.height);
            if (screenRatio >= targetRatio)
            { 
                _cam.DOOrthoSize(targetHeight / 2f, .3f).SetEase(Ease.Linear);
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                // _cam.orthographicSize = (targetHeight / 2f) * differenceInSize;
                _cam.DOOrthoSize((targetHeight / 2f) * differenceInSize, .3f).SetEase(Ease.Linear);
            }
        }

        #endregion
    }
}