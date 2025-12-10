using System.Threading;
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
        private CancellationTokenSource _cancellationTokenSource;
        #endregion

        #region Unity Functions

        private async void Start()
        {
            _cam = Camera.main;
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await UniTask.Delay(5000, cancellationToken: _cancellationTokenSource.Token);
                AdjustCameraSize();
            }
            catch (System.OperationCanceledException)
            {
                // PlayMode çıkışında normal - ignore
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
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
            
            float screenRatio = (float)Screen.width / (float)Screen.height; // Cihazın oranı
            float targetRatio = targetWidth / targetHeight; // Haritanın oranı
            
            if (screenRatio >= targetRatio)
            {
                _cam.DOOrthoSize(targetHeight / 2f, .3f).SetEase(Ease.Linear).SetLink(gameObject);
            }
            else
            {
                float differenceInSize = targetRatio / screenRatio;
                // _cam.orthographicSize = (targetHeight / 2f) * differenceInSize;
                _cam.DOOrthoSize((targetHeight / 2f) * differenceInSize, .3f).SetEase(Ease.Linear).SetLink(gameObject);
            }
        }

        #endregion
    }
}