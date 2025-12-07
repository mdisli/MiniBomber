namespace _Workspace.Scripts.Walls
{
    public class DestructibleWall : BaseWall
    {
        #region Abstracts

        protected override void OnDestruct()
        {
            Destroy(gameObject);
        }

        protected override void OnDamage()
        {
        }

        #endregion
    }
}