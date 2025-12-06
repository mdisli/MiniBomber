using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Workspace.Scripts.Grid_System
{
    public class GridManager : MonoBehaviour
    {
        #region Variables

        [Header("TileMaps")]
        [SerializeField] private Tilemap destructibleTilemap;
        [SerializeField] private Tilemap groundAndWallsTilemap;
        #endregion

        #region Funcs

        private void GenerateDestructible()
        {
        }

        #endregion
    }
}