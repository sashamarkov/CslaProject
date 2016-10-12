using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using Csla.Data;


namespace CslaProject.DataAccess.OracleDB
{
#pragma warning disable 618

    public class RepositoryBase
    {
        private readonly string _database;

        protected RepositoryBase( string database ) {
            _database = database;
        }

        protected virtual string GenerateUniqueId( ) {
            return Guid.NewGuid( ).ToString( "N" );
        }

        protected virtual void ExecuteStatement( string statement, params OracleParameter[] parameters ) {
            using ( var manager = GetTransactionManager( ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = statement;
                    command.Transaction = manager.Transaction;
                    if ( parameters != null ) {
                        command.Parameters.AddRange( parameters );
                    }
                    command.ExecuteNonQuery( );
                    if ( manager.RefCount == 1 ) {
                        manager.Commit();    
                    }
                }
            }
        }

        protected virtual void ExecuteProcedure( string procName, params OracleParameter[] parameters ) {
            using ( var manager = GetTransactionManager( ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procName;
                    command.Transaction = manager.Transaction;
                    if ( parameters != null ) {
                        command.Parameters.AddRange( parameters );
                    }
                    command.ExecuteNonQuery( );
                    if ( manager.RefCount == 1 ) {
                        manager.Commit();    
                    }
                    
                    Debug.Print("Вызвана процедура {0}", procName);
                }
            }
        }

        protected virtual IEnumerable<T> GetRows<T>( string query, Func<SafeDataReader, T> fetchFromReader, params OracleParameter[] parameters ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( _database ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    if ( parameters != null ) {
                        command.Parameters.AddRange( parameters );
                    }
                    
                    using ( var reader = new SafeDataReader( command.ExecuteReader( ) ) ) {
                        while ( reader.Read( ) ) {
                            yield return fetchFromReader( reader );
                        }
                        reader.Close( );
                    }
                }
            }
        }

        protected OracleLob CreateBlob( byte[] data ) {
            using ( var manager = GetTransactionManager( ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"DECLARE xx blob; BEGIN dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; END;";
                    command.Transaction = manager.Transaction;
                    command.Parameters.Add( new OracleParameter( "tempblob", OracleType.Blob ) ).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery( );

                    var blob = ( OracleLob )command.Parameters[ 0 ].Value;
                    try {
                        blob.BeginBatch( OracleLobOpenMode.ReadWrite );
                        blob.Write( data, 0, data.Length );
                        if ( manager.RefCount == 1 ) {
                            manager.Commit( );
                        }
                    }
                    finally {
                        if ( blob.IsBatched ) {
                            blob.EndBatch( );
                        }
                    }
                    return blob;
                }
            }
        }

        private TransactionManager<OracleConnection, OracleTransaction> GetTransactionManager( ) {
            return TransactionManager<OracleConnection, OracleTransaction>.GetManager( _database );
        }
    }
#pragma warning restore 618
}