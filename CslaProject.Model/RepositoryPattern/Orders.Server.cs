using System;
using System.ComponentModel;
using Csla;
using CslaProject.DataAccess.Contracts;
using System.Linq;
using Ninject;


namespace CslaProject.Model.RepositoryPattern
{
    public partial class Orders
    {
        [NonSerialized]
        [NotUndoable]
        private IOrderRepository _orderRepository;

        [Inject]
        [EditorBrowsable( EditorBrowsableState.Never )]
        protected IOrderRepository OrderRepository {
            get { return _orderRepository; }
            set { _orderRepository = value; }
        }

        internal static Address GetAddress( Person person ) {
            return DataPortal.FetchChild<Address>( person );
        }

        protected void Child_Fetch( PersonData person) {
            var data = OrderRepository.FindOrders( person.Id );
            RaiseListChangedEvents = false;
            AddRange( data.Select( Order.GetOrder ) );
            RaiseListChangedEvents = true;
        }

        protected void Child_Update( PersonData person ) {
            base.Child_Update( person, OrderRepository );
        }
    }
}
