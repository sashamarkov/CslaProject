using Csla.Data;
using CslaProject.DataAccess.Contracts;
using System;
using System.Data.OracleClient;

#pragma warning disable 618

namespace CslaProject.DataAccess.OracleDB
{
    internal sealed class Transaction : ITransaction
    {
        public void Dispose( ) {
            if ( _manager != null ) {
                _manager.Dispose( );
                _manager = null;
            }
        }

        private TransactionManager<OracleConnection, OracleTransaction> _manager;

        private const string TransactionWasOpenedErrorText = "Транзакция уже открыта";

        private const string TransationWasNotOpenedErrorText = "Транзакция не открыта. Не было успешного вызова метода BeginTransaction";

        public void Begin( ) {
            if ( _manager != null ) {
                throw new InvalidOperationException( TransactionWasOpenedErrorText );
            }
            _manager = TransactionManager<OracleConnection, OracleTransaction>.GetManager( "PDM" );
        }

        void ITransaction.Commit( ) {
            if ( _manager == null ) {
                throw new InvalidOperationException( TransationWasNotOpenedErrorText );
            }
            _manager.Commit( );
            Dispose(  );
        }

        void ITransaction.Rollback( ) {
            if ( _manager == null ) {
                throw new InvalidOperationException( TransationWasNotOpenedErrorText );
            }
            _manager.Dispose( );
            Dispose(  );
        }
    }
}
