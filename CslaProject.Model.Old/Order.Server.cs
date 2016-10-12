using Csla;
using Csla.Data;
using System.Data;
using System.Data.OracleClient;

#pragma warning disable 618


namespace CslaProject.Model.Old
{
    public partial class Order
    {
        internal static Order GetOrder( SafeDataReader reader ) {
            return DataPortal.FetchChild<Order>( reader );
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Fetch( SafeDataReader reader ) {
            LoadProperty( IdProperty, reader.GetInt32( "id" ) );
            LoadProperty( DescriptionProperty, reader.GetString( "description" ) );
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Insert( Person person, OracleTransaction transaction ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "csla_project.add_order";
                    command.Transaction = transaction;
                    command.Parameters.AddRange( GetParameters( ) );
                    command.Parameters.AddWithValue( "person_id", person.Id );
                    command.ExecuteNonQuery( );
                    Id = ( int )command.Parameters[ "p_id" ].Value;
                }
            }
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Update( Person person, OracleTransaction transaction ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "csla_project.edit_order";
                    command.Transaction = transaction;
                    command.Parameters.AddRange( GetParameters( ) );
                    command.Parameters.AddWithValue( "person_id", person.Id );
                    command.ExecuteNonQuery( );
                }
            }
        }


// ReSharper disable once UnusedMember.Local
        private void Child_DeleteSelf( Person person, OracleTransaction transaction ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "csla_project.remove_order";
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue( "p_person_id", person.Id );
                    command.Parameters.AddWithValue( "p_order_id", Id );
                    command.ExecuteNonQuery( );
                }
            }
        }

        private OracleParameter[] GetParameters( ) {
            return new[] {
                             new OracleParameter( "p_id", Id ) {Direction = ParameterDirection.InputOutput},
                             new OracleParameter( "p_description", Description )
                         };
        }
    }
}
