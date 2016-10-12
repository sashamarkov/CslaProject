
namespace CslaProject.DataAccess.Contracts
{
    public interface IAddressRepository
    {
        AddressData FindAddress( int personId );

        int AddAddress( int personId, AddressData addressData );

        void EditAddress( int personId, AddressData addressData );

        void RemoveAddress( int personId, int addressId );
    }
}
