using Csla;
using Csla.Data;
using CslaProject.DataAccess.Contracts;


namespace CslaProject.Model.RepositoryPattern
{
    public partial class Order
    {
        internal static Order GetOrder( OrderData orderData ) {
            return DataPortal.FetchChild<Order>( orderData );
        }

        protected void Child_Fetch( OrderData orderData ) {
            using ( BypassPropertyChecks ) {
                DataMapper.Map( orderData, this );
            }
        }

        protected void Child_Insert( PersonData person, IOrderRepository orderRepository ) {
            var data = GetOrderData( );
            Id = orderRepository.AddOrder( person.Id, data );
        }

        protected void Child_Update( PersonData person, IOrderRepository orderRepository ) {
            var data = GetOrderData( );
            orderRepository.EditOrder( person.Id, data );
        }

        protected void Child_DeleteSelf( PersonData person, IOrderRepository orderRepository ) {
            orderRepository.RemoveOrder( person.Id, Id );
        }

        private OrderData GetOrderData( ) {
            var orderData = new OrderData( );
            DataMapper.Map( this, orderData );
            return orderData;
        }
    }
}
