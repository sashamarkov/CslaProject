using Csla;
using Csla.Data;
using CslaProject.DataAccess.Contracts;
using Ninject;
using System;
using System.ComponentModel;


namespace CslaProject.Model.RepositoryPattern
{
    public partial class Address
    {
        [NonSerialized, NotUndoable]
        private IAddressRepository _addressRepository;

        [Inject]
        [EditorBrowsable( EditorBrowsableState.Never )]
        protected IAddressRepository AddressRepository {
            get { return _addressRepository; }
            set { _addressRepository = value; }
        }

        internal static Address GetAddress( PersonData person ) {
            return DataPortal.FetchChild<Address>( person );
        }

        protected void Child_Fetch( PersonData personData ) {
            using ( BypassPropertyChecks ) {
                var addressData = AddressRepository.FindAddress( personData.Id );
                DataMapper.Map( addressData, this );
            }
        }

        protected void Child_Insert( PersonData person ) {
            var data = GetAddressData( );
            Id = AddressRepository.AddAddress( person.Id, data );
        }

        protected void Child_Update( PersonData person ) {
            var data = GetAddressData( );
            AddressRepository.EditAddress( person.Id, data );
        }

        protected void Child_DeleteSelf( PersonData person ) {
            AddressRepository.RemoveAddress( person.Id, Id );
        }

        private AddressData GetAddressData( ) {
            var addressData = new AddressData( );
            DataMapper.Map( this, addressData );
            return addressData;
        }
    }
}
