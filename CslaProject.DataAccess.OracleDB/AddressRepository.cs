using Csla.Data;
using CslaProject.DataAccess.Contracts;
using System.Data.OracleClient;
using System.Linq;


namespace CslaProject.DataAccess.OracleDB
{
    public class AddressRepository : RepositoryBase, IAddressRepository
    {
        protected AddressRepository( ) : base( "PDM" ) { }

        public AddressData FindAddress( int personId ) {
            const string query = @"SELECT id
                                          ,first_address
                                          ,second_address
                                     FROM All_addresses
                                    WHERE person_id = :p_person_id";
            return GetRows( query, FetchFromReader, new OracleParameter( "p_person_id", personId ) ).FirstOrDefault( );
        }

        public int AddAddress( int personId, AddressData addressData ) {
            var parameters = GetAddressParameters( personId, addressData );
            ExecuteProcedure( "csla_project.add_address", parameters );
            return ( int )parameters.First( ).Value;
        }

        public void EditAddress( int personId, AddressData addressData ) {
            var parameters = GetAddressParameters( personId, addressData );
            ExecuteProcedure( "csla_project.edit_address", parameters );
        }

        public void RemoveAddress( int personId, int addressId ) {
            ExecuteProcedure( "csla_project.remove_address", new OracleParameter( "p_person_id", personId ), new OracleParameter( "p_id", addressId ) );
        }

        private AddressData FetchFromReader( SafeDataReader reader ) {
            return new AddressData {
                                       Id = reader.GetInt32( "id" ),
                                       FirstAddress = reader.GetString( "first_address" ),
                                       SecondAddress = reader.GetString( "second_address" )
                                   };
        }

        private OracleParameter[] GetAddressParameters( int personId, AddressData addressData ) {
            return new[] {
                             new OracleParameter( "p_id", addressData.Id ),
                             new OracleParameter( "p_first_address", addressData.FirstAddress ),
                             new OracleParameter( "p_second_address", addressData.SecondAddress ),
                             new OracleParameter( "p_person_id", personId )
                         };
        }
    }
}
