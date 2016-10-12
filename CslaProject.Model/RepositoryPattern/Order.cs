using Csla;
using System;


namespace CslaProject.Model.RepositoryPattern
{
    [Serializable]
    public partial class Order : BusinessBase<Order>
    {
        private Order( ) { }

        public static Order NewOrder( ) {
            return DataPortal.CreateChild<Order>( );
        }

        private static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        private int Id {
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
