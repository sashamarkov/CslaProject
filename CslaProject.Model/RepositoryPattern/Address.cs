using Csla;
using CslaProject.DataAccess.Contracts;
using CslaProject.Model.Core;
using System;


namespace CslaProject.Model.RepositoryPattern
{
    [Serializable]
    public partial class Address : InjectableBusinessBase<Address>
    {
        private Address() { }

        public static Address NewAddress( ) {
            return DataPortal.CreateChild<Address>( );
        }

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        public int Id {
            get { return GetProperty( IdProperty ); }
            set { LoadProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> FirstAddressProperty = RegisterProperty<string>(c => c.FirstAddress);

        public string FirstAddress {
            get { return GetProperty( FirstAddressProperty ); }
            set { SetProperty( FirstAddressProperty, value ); }
        }

        public static readonly PropertyInfo<string> SecondAddressProperty = RegisterProperty<string>(c => c.SecondAddress);

        public string SecondAddress {
            get { return GetProperty( SecondAddressProperty ); }
            set { SetProperty( SecondAddressProperty, value ); }
        }
    }
}