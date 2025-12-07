namespace _Workspace.Scripts.Walls
{
    public class DestructibleWall : BaseWall
    {
        #region Abstracts

        public override void OnDestruct()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}