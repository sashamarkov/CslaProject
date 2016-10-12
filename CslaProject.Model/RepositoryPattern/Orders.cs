using Csla;
using CslaProject.DataAccess.Contracts;
using CslaProject.Model.Core;
using System;


namespace CslaProject.Model.RepositoryPattern
{
    [Serializable]
    public partial class Orders : InjectableBusinessListBase<Orders, Order>
    {
        private Orders( ) { }

        public static Orders NewOrders( ) {
            return DataPortal.CreateChild<Orders>( );
        }

        internal static Orders GetOrders( PersonData person ) {
            return DataPortal.FetchChild<Orders>( person );
        }
    }
}
