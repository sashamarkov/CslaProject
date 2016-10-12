using Csla;
using Csla.Data;
using System.Data;
using System.Data.OracleClient;

#pragma warning disable 618

namespace CslaProject.Model.Old
{
    public partial class Orders
    {
        internal static Orders GetOrders( Person person ) {
            return DataPortal.FetchChild<Orders>( person );
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Fetch( Person person ) {
            const string query = @"SELECT id 
                                         ,description
                                    FROM All_orders
                                   WHERE person_id = :p_person_id";
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue( "p_person_id", person.Id );
                    using ( var reader = new SafeDataReader( command.ExecuteReader( CommandBehavior.SingleRow ) ) ) {
                        RaiseListChangedEvents = false;
                        while ( reader.Read( ) ) {
                            Add( Order.GetOrder( reader ) );
                        }
                        RaiseListChangedEvents = true;
                    }
                }
            }
        }

// ReSharper disable once UnusedMember.Local
        private void Child_Update( Person person, OracleTransaction transaction ) {
            base.Child_Update( person, transaction );
        }
    }
}
