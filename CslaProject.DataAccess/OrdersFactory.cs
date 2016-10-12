using Csla;
using Csla.Reflection;
using Csla.Server;
using CslaProject.DataAccess.Contracts;
using CslaProject.Model.ObjectFactoryPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Ninject;


namespace CslaProject.DataAccess
{
    internal class OrdersFactory : ObjectFactory
    {
        private IOrderRepository _orderRepository;

        //[Inject]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private IOrderRepository OrderRepository {
            get { return _orderRepository; }
            set { _orderRepository = value; }
        }

        public OrdersFactory( IOrderRepository orderRepository ) {
            if ( orderRepository == null ) {
                throw new ArgumentNullException( "orderRepository" );
            }
            _orderRepository = orderRepository;
        }

        [RunLocal]
        public Orders Create( ) {
            var orders = GetEmptyOrders( );
            MarkAsChild( orders );
            MarkNew( orders );
            return orders;
        }

        internal Orders Fetch( int personId ) {
            var data = OrderRepository.FindOrders( personId );
            return GetFromData( data );
        }

        internal void Update( Orders orders, Person person ) {
            var deletedList = GetDeletedList<Order>( orders );
            if ( deletedList.Any( ) ) {
                UpdateOrders( deletedList, person );
                deletedList.Clear(  );
            }
            UpdateOrders( orders, person );
            MarkOld( orders );
        }

        private void UpdateOrders( IEnumerable<Order> orders, Person person ) {
            var orderFactory = new OrderFactory( OrderRepository );
            foreach ( var order in orders ) {
                orderFactory.Update( order, person );
            }
        }

        private Orders GetFromData( IEnumerable<OrderData> data ) {
            var orders = GetEmptyOrders( );
            orders.RaiseListChangedEvents = false;
            
            //var orderFactory = new OrderFactory( OrderRepository );
            
            var factoryLoader = new FactoryLoader();
            var orderFactory = ( OrderFactory )factoryLoader.GetFactory( "OrderFactory" );
            
            orders.AddRange( data.Select( orderFactory.Fetch ) );
            orders.RaiseListChangedEvents = true;
            return orders;
        }

        private Orders GetEmptyOrders( ) {
            return ( Orders )MethodCaller.CreateInstance( typeof ( Orders ) );
        }
    }
}
