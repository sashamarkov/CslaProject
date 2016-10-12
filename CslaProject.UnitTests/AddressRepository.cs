using CslaProject.DataAccess.Contracts;
using System.ComponentModel.Composition;


namespace CslaProject.UnitTests
{
    [Export( typeof ( IAddressRepository ) )]
    public class AddressRepository : IAddressRepository
    {
        public AddressData FindAddress( int personId ) {
            return new AddressData( );
        }

        public int AddAddress( int personId, AddressData addressData ) {
            return 123;
        }

        public void EditAddress( int personId, AddressData addressData ) { }

        public void RemoveAddress( int personId, int addressId ) { }
    }
}
