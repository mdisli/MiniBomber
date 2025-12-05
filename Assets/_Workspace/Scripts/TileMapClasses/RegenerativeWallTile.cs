using _Workspace.Scripts.Walls;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Workspace.Scripts.TileMapClasses
{
    [CreateAssetMenu(menuName = "MiniBomber/Regenerative Wall")]
    public class RegenerativeWallTile : Tile
    {
        [Header("Layer Order")]
        public int orderInLayer;
        
        [Header("Prefab")]
        public RegenerativeWall regenerativeWallPrefab;
        
        [Header("Options")]
        [Range(1,3)]public int healthCount;
        [Range(3,10)]public float regenerateAfter;
        
        // Unity Editöründe bir değer değiştiğinde otomatik çalışır
        private void OnValidate()
        {
            float alphaValue = 1f / healthCount; // Can arttıkça şeffaflaşır

            float t = Mathf.InverseLerp(3f, 10f, regenerateAfter);

            // Color.Lerp iki renk arasında geçiş yapar
            var targetColor = Color.Lerp(Color.green, Color.red, t); // Regenerate süresi uzadıkça renk kırmızıya döner

            // 3. Hesaplanan Alpha ve Rengi Birleştir
            targetColor.a = alphaValue;
        
            // Tile'ın rengini güncelle
            this.color = targetColor;
        }
    }
}