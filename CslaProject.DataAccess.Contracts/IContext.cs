namespace CslaProject.DataAccess.Contracts
{
    public interface IContext
    {
        ITransaction BeginTransaction( );
    }
}