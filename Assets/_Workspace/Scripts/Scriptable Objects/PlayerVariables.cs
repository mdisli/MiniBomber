using UnityEngine;

namespace _Workspace.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Player Variables", menuName = "So/Player Variables", order = 0)]
    public class PlayerVariables : ScriptableObject
    {
        [Header("Health")]
        public int healthCount;
        
        [Header("Movement")]
        public float movementSpeed = 5;
        public LayerMask obstacleLayers;

        [Header("Bomb")] 
        public float bombRegenDuration = 3;
    }
}