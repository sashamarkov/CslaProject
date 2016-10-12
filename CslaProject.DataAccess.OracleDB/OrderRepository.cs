using Csla.Data;
using CslaProject.DataAccess.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;


namespace CslaProject.DataAccess.OracleDB
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        protected OrderRepository( ) : base( "PDM" ) { }

        public IEnumerable<OrderData> FindOrders( int personId ) {
            const string query = @"SELECT id 
                                         ,description
                                    FROM All_orders
                                   WHERE person_id = :p_person_id";
            return GetRows( query, FetchFromReader, new OracleParameter( "p_person_id", personId ) );
        }

        public int AddOrder( int personId, OrderData orderData ) {
            var parameters = GetOrderParameters( personId, orderData );
            ExecuteProcedure( "csla_project.add_order", parameters );
            return ( int )parameters.First( ).Value;
        }

        public void EditOrder( int personId, OrderData orderData ) {
            var parameters = GetOrderParameters( personId, orderData );
            ExecuteProcedure( "csla_project.edit_order", parameters );
        }

        public void RemoveOrder( int personId, int orderId ) {
            ExecuteProcedure( "csla_project.remove_order", new OracleParameter( "p_person_id", personId ), new OracleParameter( "p_id", orderId ) );
        }

        private OrderData FetchFromReader( SafeDataReader reader ) {
            return new OrderData {
                                     Id = reader.GetInt32( "id" ),
                                     Description = reader.GetString( "description" )
                                 };
        }

        private OracleParameter[] GetOrderParameters( int personId, OrderData orderData ) {
            return new[] {
                             new OracleParameter( "p_id", orderData.Id ) {Direction = ParameterDirection.InputOutput},
                             new OracleParameter( "p_person_id", personId ),
                             new OracleParameter( "p_description", orderData.Description )
                         };
        }
    }
}
