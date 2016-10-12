using Csla;
using System;


namespace CslaProject.Model.Old
{
    [Serializable]
    public sealed partial class Orders : BusinessListBase<Orders, Order>
    {
        private Orders( ) { }

        public static Orders NewOrders( ) {
            return DataPortal.CreateChild<Orders>( );
        }
    }
}
