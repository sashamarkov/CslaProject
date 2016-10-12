using Csla;
using Csla.Data;
using Csla.Reflection;
using Csla.Server;
using CslaProject.DataAccess.Contracts;
using CslaProject.Model.ObjectFactoryPattern;
using System;
using System.ComponentModel;


namespace CslaProject.DataAccess
{
    public class OrderFactory : ObjectFactory
    {
        private IOrderRepository _orderRepository;

        //[Inject]
        [EditorBrowsable( EditorBrowsableState.Never )]
        private IOrderRepository OrderRepository {
            get { return _orderRepository; }
            set { _orderRepository = value; }
        }

        public OrderFactory( IOrderRepository orderRepository ) {
            if ( orderRepository == null ) {
                throw new ArgumentNullException( "orderRepository" );
            }
            _orderRepository = orderRepository;
        }

        [RunLocal]
        internal Order Create( ) {
            var order = CreateEmptyOrder( );
            using ( BypassPropertyChecks( order ) ) {
                CheckRules( order );
                MarkAsChild( order );
                MarkNew( order );
            }
            return order;
        }

        internal Order Fetch( OrderData orderData ) {
            var order = CreateEmptyOrder( );
            DataMapper.Map( orderData, order );
            return order;
        }

        internal Order Update( Order order, Person person ) {
            if ( order.IsDeleted ) {
                DoDelete( order, person.Id );
            } else if ( order.IsNew ) {
                DoInsert( order, person.Id );
            } else {
                DoUpdate( order, person.Id );
            }
            return order;
        }

        private void DoDelete( Order order, int personId ) {
            OrderRepository.RemoveOrder( personId, order.Id );
            MarkNew( order );
        }

        private void DoInsert( Order order, int personId ) {
            var orderData = GetOrderData( order );
            OrderRepository.AddOrder( personId, orderData );
            MarkOld( order );
        }

        private void DoUpdate( Order order, int personId ) {
            var orderData = GetOrderData( order );
            OrderRepository.EditOrder( personId, orderData );
            MarkOld( order );
        }

        private OrderData GetOrderData( Order order ) {
            var orderData = new OrderData( );
            DataMapper.Map( orderData, order );
            return orderData;
        }

        private Order CreateEmptyOrder( ) {
            return ( Order )MethodCaller.CreateInstance( typeof ( Order ) );
        }
    }
}
