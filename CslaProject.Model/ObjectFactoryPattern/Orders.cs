using System;
using System.Linq;
using Csla;
using Csla.Server;
using DataPortal = Csla.DataPortal;


namespace CslaProject.Model.ObjectFactoryPattern
{
    [Serializable]
    [ObjectFactory( "OrdersFactory" )]
    public class Orders : BusinessListBase<Orders, Order>
    {
        private Orders( ) { }

        public Order FindOrder( int orderId ) {
            return Items.First( o => o.Id == orderId );
        }

        public static Orders NewOrders( ) {
            return DataPortal.Create<Orders>( );
        }
    }
}
