using Csla;
using Csla.Data;
using Csla.Server;
using CslaProject.DataAccess.Contracts;
using CslaProject.Model.ObjectFactoryPattern;
using System;
using System.ComponentModel;


namespace CslaProject.DataAccess
{
    public class AddressFactory : ObjectFactory
    {
        private IAddressRepository _addressRepository;

        //[Inject]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private IAddressRepository AddressRepository {
            get { return _addressRepository; }
            set { _addressRepository = value; }
        }

        public AddressFactory( IAddressRepository addressRepository ) {
            if ( addressRepository == null ) {
                throw new ArgumentNullException("addressRepository");
            }
            _addressRepository = addressRepository;
        }

        [RunLocal]
        internal Address Create( ) {
            var address = CreateEmptyAddress( );
            using ( BypassPropertyChecks( address ) ) {
                CheckRules( address );
                MarkAsChild( address );
                MarkNew( address );
            }
            return address;
        }

        internal Address Fetch( Person person ) {
            var address = CreateEmptyAddress( );
            var addressData = AddressRepository.FindAddress( person.Id );
            DataMapper.Map( addressData, address );
            return address;
        }

        internal Address Update( Address address, Person person ) {
            if ( address.IsDeleted ) {
                DoDelete( address, person.Id );
            } else if ( address.IsNew ) {
                DoInsert( address, person.Id );
            } else {
                DoUpdate( address, person.Id );
            }
            return address;
        }

        private void DoDelete( Address address, int personId ) {
            AddressRepository.RemoveAddress( personId, address.Id );
            MarkNew( address );
        }

        private void DoInsert( Address address, int personId ) {
            var addressData = GetAddressData( address );
            AddressRepository.AddAddress( personId, addressData );
            MarkOld( address );
        }

        private void DoUpdate( Address address, int personId ) {
            var addressData = GetAddressData(address);
            AddressRepository.RemoveAddress( personId, address.Id );
            MarkOld(address);
        }

        private Address CreateEmptyAddress( ) {
            return ( Address )Activator.CreateInstance( typeof ( Address ), true );
        }

        private AddressData GetAddressData( Address address ) {
            var addressData = new AddressData( );
            DataMapper.Map( address, addressData );
            return addressData;
        }
    }
}
