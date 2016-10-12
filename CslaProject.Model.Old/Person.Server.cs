using Csla;
using Csla.Data;
using System.Data;
using System.Data.OracleClient;

#pragma warning disable 618

namespace CslaProject.Model.Old
{
    public partial class Person
    {
        public static readonly PropertyInfo<object> LastChangedProperty = RegisterProperty<object>( c => c.LastChanged );

        public object LastChanged {
            get { return ReadProperty( LastChangedProperty ); }
            private set { LoadProperty( LastChangedProperty, value ); }
        }

        protected override void DataPortal_Create( ) {
            BusinessRules.CheckRules( );
        }

// ReSharper disable once UnusedMember.Local
        private void DataPortal_Fetch( SingleCriteria<Person, int> idCriteria ) {
            const string query = @"SELECT id 
                                         ,first_name
                                         ,second_name
                                         ,age
                                         ,comment
                                         ,last_changed
                                         ,address_id
                                         ,first_address
                                         ,second_address
                                    FROM All_persons
                                   WHERE id = :p_id";
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue( "p_id", idCriteria.Value );
                    using ( var reader = new SafeDataReader( command.ExecuteReader( CommandBehavior.SingleRow ) ) ) {
                        if ( reader.Read( ) ) {
                            FetchFromReader( reader );
                        }
                    }
                }
                LoadProperty( OrdersProperty, Orders.GetOrders( this ) );
            }
        }

// ReSharper disable once UnusedMember.Local
        private void DataPortal_Fetch( SingleCriteria<Person, string> nameCriteria ) {
            const string query = @"SELECT id 
                                         ,first_name
                                         ,second_name
                                         ,age
                                         ,comment
                                         ,last_changed
                                         ,address_id
                                         ,first_address
                                         ,second_address
                                     FROM All_persons
                                    WHERE second_name = :p_second_name";
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue( "p_second_name", nameCriteria.Value );
                    using ( var reader = new SafeDataReader( command.ExecuteReader( CommandBehavior.SingleRow ) ) ) {
                        if ( reader.Read( ) ) {
                            FetchFromReader( reader );
                        }
                    }
                }
                LoadProperty( OrdersProperty, Orders.GetOrders( this ) );
            }
        }

        private void FetchFromReader( SafeDataReader reader ) {
            LoadProperty( FirstNameProperty, reader.GetString( "first_name" ) );
            LoadProperty( SecondNameProperty, reader.GetString( "second_name" ) );
            LoadProperty( CommentProperty, reader.GetString( "comment" ) );
            LoadProperty( AddressProperty, Address.GetAddress( reader ) );
            Id = reader.GetInt32( "Id" );
            LastChanged = reader[ "last_changed" ];
        }

        //[Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert( ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var transaction = manager.Connection.BeginTransaction( ) ) {
                    using ( var command = manager.Connection.CreateCommand( ) ) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "csla_project.add_person";
                        var personParameters = GetPersonParameters( );
                        command.Parameters.AddRange( personParameters );
                        command.Transaction = transaction;
                        command.ExecuteNonQuery( );

                        Id = ( int )command.Parameters[ "p_id" ].Value;
                        LastChanged = command.Parameters[ "p_last_changed" ];
                    }
                    FieldManager.UpdateChildren( this, transaction );
                    transaction.Commit( );
                }
            }
        }

        //[Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update( ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var transaction = manager.Connection.BeginTransaction( ) ) {
                    using ( var command = manager.Connection.CreateCommand( ) ) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "csla_project.add_person";
                        var personParameters = GetPersonParameters( );
                        command.Parameters.AddRange( personParameters );
                        command.Transaction = transaction;
                        command.ExecuteNonQuery( );

                        LastChanged = command.Parameters[ "p_last_changed" ];
                    }
                    FieldManager.UpdateChildren( this, transaction );
                    transaction.Commit( );
                }
            }
        }

// ReSharper disable once UnusedMember.Local
        private void DataPortal_Delete( SingleCriteria<Person, int> idCriteria  ) {
            DoDelete( idCriteria.Value );
        }

        protected override void DataPortal_DeleteSelf( ) {
            DoDelete( Id );
        }

        private void DoDelete( int personId ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var transaction = manager.Connection.BeginTransaction( ) ) {
                    using ( var command = manager.Connection.CreateCommand( ) ) {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "csla_project.delete_person";
                        command.Parameters.AddWithValue( "p_id", personId );
                        command.Transaction = transaction;
                        command.ExecuteNonQuery( );
                    }
                    transaction.Commit( );
                }
            }
        }

        private OracleParameter[] GetPersonParameters( ) {
            return new[] {
                             new OracleParameter( "p_id", Id ) {Direction = ParameterDirection.InputOutput},
                             new OracleParameter( "p_first_name", FirstName ),
                             new OracleParameter( "p_second_name", SecondName ),
                             new OracleParameter( "p_age", Age ),
                             new OracleParameter( "p_comment", Comment ),
                             new OracleParameter( "p_last_changed", OracleType.Int32 ) {Direction = ParameterDirection.Output}
                         };
        }
    }
}
