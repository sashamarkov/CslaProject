using System.Collections.Generic;

namespace CslaProject.DataAccess.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<OrderData> FindOrders( int personId );

        int AddOrder( int personId, OrderData orderData );

        void EditOrder( int personId, OrderData orderData );

        void RemoveOrder( int personId, int orderId );
    }
}
