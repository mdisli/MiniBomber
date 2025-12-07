using _Workspace.Scripts.Bomb;
using UnityEngine;

namespace _Workspace.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Bomb Variables", menuName = "So/Bomb Variables", order = 0)]
    public class BombVariables : ScriptableObject
    {
        public BombDirections bombDirections;
        public LayerMask obstacleMasks;
        public int damage = 1;
        public int range = 3;
        public float explosionDelay = 3;
    }

    
}