using System.ComponentModel.Composition;
using CslaProject.DataAccess.Contracts;

#pragma warning disable 618

namespace CslaProject.DataAccess.OracleDB
{
    [Export(typeof(IContext))]
    internal sealed class Context : IContext
    {
        ITransaction IContext.BeginTransaction( ) {
            var transaction = new Transaction( );
            transaction.Begin(  );
            return transaction;
        }
    }
}
