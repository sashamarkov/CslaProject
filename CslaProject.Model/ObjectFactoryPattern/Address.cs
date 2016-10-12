using Csla;
using Csla.Server;
using CslaProject.DataAccess.Contracts;
using System;
using DataPortal = Csla.DataPortal;


namespace CslaProject.Model.ObjectFactoryPattern
{
    [Serializable]
    [ObjectFactory("AddressFactory")]
    public class Address : BusinessBase<Address>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        public int Id {
            get { return GetProperty( IdProperty ); }
            set { SetProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> FirstAddressProperty = RegisterProperty<string>( c => c.FirstAddress );

        public string FirstAddress {
            get { return GetProperty( FirstAddressProperty ); }
            set { SetProperty( FirstAddressProperty, value ); }
        }

        public static readonly PropertyInfo<string> SecondAddressProperty = RegisterProperty<string>( c => c.SecondAddress );

        public string SecondAddress {
            get { return GetProperty( SecondAddressProperty ); }
            set { SetProperty( SecondAddressProperty, value ); }
        }

        private Address( ) { }

        public static Address NewAddress( ) {
            return DataPortal.CreateChild<Address>( );
        }

        internal static Address GetAddress( AddressData addressData ) {
            return DataPortal.FetchChild<Address>( addressData );
        }
    }
}
