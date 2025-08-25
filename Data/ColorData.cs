using UnityEngine;

namespace Pathfinding.Data
{
    public static class ColorData
    {
        public static readonly Color32 Normal = new Color32(139, 69, 19, 255);  // Brown
        public static readonly Color32 Inaccessible = new Color32(128, 128, 128, 255); // Gray
        public static readonly Color32 Entrance = new Color32(30, 60, 150, 255); // Blue
        public static readonly Color32 Exit = new Color32(30, 120, 30, 255); // Green
        public static readonly Color32 Forest = new Color32(34, 139, 34, 255); // Forest Green
        public static readonly Color32 Steep = new Color32(255, 140, 0, 255); // Orange
        public static readonly Color32 High = new Color32(128, 0, 128, 255); // Purple
    }
}
