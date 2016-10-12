using System;
using Csla;
using Csla.Data;


namespace CslaProject.Model.Old
{
    [Serializable]
    public sealed partial class Order : BusinessBase<Order>
    {
        private Order( ) { }

        public static Order NewOrder( ) {
            return DataPortal.CreateChild<Order>( );
        }

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        public int Id {
            get { return GetProperty( IdProperty ); }
            set { LoadProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>( c => c.Description );

        public string Description {
            get { return GetProperty( DescriptionProperty ); }
            set { SetProperty( DescriptionProperty, value ); }
        }
    }
}
