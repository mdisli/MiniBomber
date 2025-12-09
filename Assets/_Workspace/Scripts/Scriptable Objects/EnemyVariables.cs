using UnityEngine;

namespace _Workspace.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Enemy Variables", menuName = "So/Enemy Variables", order = 0)]
    public class EnemyVariables : ScriptableObject
    {
        #region Variables

        [Header("Movement")] 
        public float movementSpeed;
        public LayerMask obstacleMask;

        [Header("Bomb")] 
        public float bombInterval;

        #endregion
    }
}