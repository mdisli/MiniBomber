using UnityEngine;

namespace _Workspace.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Player Variables", menuName = "So/Player Variables", order = 0)]
    public class PlayerVariables : ScriptableObject
    {
        [Header("Movement")]
        public float movementSpeed = 5;
        public LayerMask obstacleLayers;

        [Header("Bomb")] 
        public int bombCapacity = 1;
        public float bombRegenDuration = 3;
    }
}