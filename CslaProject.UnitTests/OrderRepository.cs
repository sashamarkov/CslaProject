using System.ComponentModel.Composition;
using CslaProject.DataAccess.Contracts;
using System.Collections.Generic;


namespace CslaProject.UnitTests
{
    [Export(typeof(IOrderRepository))]
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<OrderData> FindOrders( int personId ) { 
            return new[] {new OrderData( )};
        }

        public int AddOrder( int personId, OrderData orderData ) {
            return 123;
        }

        public void EditOrder( int personId, OrderData orderData ) { }

        public void RemoveOrder( int personId, int orderId ) { }
    }
}
