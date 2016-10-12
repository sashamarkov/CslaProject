using System;
using Csla;
using Csla.Server;
using DataPortal = Csla.DataPortal;


namespace CslaProject.Model.ObjectFactoryPattern
{
    [Serializable]
    [ObjectFactory("OrderFactory")]
    public class Order : BusinessBase<Order>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        public int Id {
            get { return GetProperty( IdProperty ); }
            set { SetProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>( c => c.Description );

        public string Description {
            get { return GetProperty( DescriptionProperty ); }
            set { SetProperty( DescriptionProperty, value ); }
        }

        private Order( ) { }

        public static Order NewOrder( ) {
            return DataPortal.Create<Order>( );
        }
    }
}
