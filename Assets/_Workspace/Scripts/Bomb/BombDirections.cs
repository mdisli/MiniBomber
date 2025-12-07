namespace _Workspace.Scripts.Bomb
{
    [System.Flags, System.Serializable]
    public enum BombDirections
    {
        None = 0,
        Right = 1,
        Left = 2,
        Up = 4,
        Down = 8
    }
}