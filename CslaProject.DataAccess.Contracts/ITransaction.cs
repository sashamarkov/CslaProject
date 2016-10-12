using System;


namespace CslaProject.DataAccess.Contracts
{
    public interface ITransaction : IDisposable
    {
        void Commit( );

        void Rollback( );
    }
}