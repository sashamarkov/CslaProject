using Csla;
using Csla.Data;
using System.Data;
using System.Data.OracleClient;

#pragma warning disable 618

namespace CslaProject.Model.Old
{
    public sealed partial class Address
    {
        internal static Address GetAddress( SafeDataReader reader ) {
            return DataPortal.Fetch<Address>( reader );
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Fetch( SafeDataReader reader ) {
            using ( BypassPropertyChecks ) {
                LoadProperty( IdProperty, reader.GetInt32( "address_id" ) );
                LoadProperty( FirstAddressProperty, reader.GetString( "first_address" ) );
                LoadProperty( SecondAddressProperty, reader.GetString( "second_address" ) );
            }
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Insert( Person person, OracleTransaction transaction ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "csla_project.add_address";
                    command.Parameters.AddRange( GetParameters( ) );
                    command.Parameters.AddWithValue( "p_person_id", person.Id );
                    command.Transaction = transaction;
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
                    command.CommandText = "csla_project.edit_address";
                    command.Parameters.AddRange( GetParameters( ) );
                    command.Parameters.AddWithValue( "p_person_id", person.Id );
                    command.Transaction = transaction;
                    command.ExecuteNonQuery( );
                }
            }
        }

// ReSharper disable once UnusedMember.Local
        private void Child_DeleteSelf( Person person, OracleTransaction transaction ) {
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "csla_project.remove_address";
                    command.Parameters.AddWithValue( "p_address_id", Id );
                    command.Parameters.AddWithValue( "p_person_id", person.Id );
                    command.Transaction = transaction;
                    command.ExecuteNonQuery( );
                }
            }
        }

        private OracleParameter[] GetParameters( ) {
            return new[] {
                             new OracleParameter( "p_id", Id ) {Direction = ParameterDirection.InputOutput},
                             new OracleParameter( "p_first_address", FirstAddress ),
                             new OracleParameter( "p_second_address", SecondAddress )
                         };
        }
    }
}