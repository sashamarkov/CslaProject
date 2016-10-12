using System.Diagnostics.Contracts;
using Csla.Data;
using CslaProject.DataAccess.Contracts;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.OracleClient;
using System.Linq;

namespace CslaProject.DataAccess.OracleDB
{
    [Export(typeof(IPersonRepository))]
    public class PersonRepository : RepositoryBase, IPersonRepository
    {
        public PersonRepository( ) : base( "PDM" ) { }

        public PersonData FindPerson( int id ) {
            const string query = @"SELECT id 
                                         ,first_name
                                         ,second_name
                                         ,age
                                         ,comment
                                         ,last_changed
                                    FROM All_persons
                                   WHERE id = :p_id";
            return GetRows( query, FetchFromReader, new OracleParameter( "p_id", id ) ).First( );
        }

        public PersonData FindPerson( string name ) {
            const string query = @"SELECT id
                                         ,first_name
                                         ,second_name
                                         ,age
                                         ,comment
                                         ,last_changed
                                     FROM All_persons
                                    WHERE second_name = :p_second_name";
            return GetRows( query, FetchFromReader, new OracleParameter( "p_second_name", name ) ).First( );
        }

        public int AddPerson( PersonData newPerson ) {
            var parameters = GetPersonParameters(newPerson);
            ExecuteProcedure("csla_project.add_person", parameters);
            newPerson.LastChanged = parameters.Last().Value;
            return (int)parameters.First().Value;
        }

        public void EditPerson( PersonData existingPerson ) {
            var parameters = GetPersonParameters( existingPerson );
            ExecuteProcedure( "csla_project.update_person", parameters );
            existingPerson.LastChanged = parameters.Last( ).Value;
        }

        public void RemovePerson( int personId ) {
            ExecuteProcedure( "csla_project.delete_person", new OracleParameter("p_id", personId) );
        }

        private PersonData FetchFromReader( SafeDataReader reader ) {
            return new PersonData {
                                      Id = reader.GetInt32( "id" ),
                                      FirstName = reader.GetString( "first_name" ),
                                      SecondName = reader.GetString( "second_name" ),
                                      Age = reader.GetInt32( "age" ),
                                      Comment = reader.GetString( "comment" ),
                                      LastChanged = reader["last_changed"] 
                                  };
        }

        private OracleParameter[] GetPersonParameters( PersonData personData ) {
            return new[] {
                             new OracleParameter( "p_id", personData.Id ){Direction = ParameterDirection.InputOutput},
                             new OracleParameter( "p_first_name", personData.FirstName ),
                             new OracleParameter( "p_second_name", personData.SecondName ),
                             new OracleParameter( "p_age", personData.Age ),
                             new OracleParameter( "p_comment", personData.Comment ),
                             new OracleParameter( "p_last_changed", OracleType.Int32 ) {Value = personData.LastChanged, Direction = ParameterDirection.Output}
                         };
        }
    }
}
