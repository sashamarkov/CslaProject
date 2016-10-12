using Csla.Data;
using System.Data;
using System.Data.OracleClient;

#pragma warning disable 618

namespace CslaProject.Model.Old
{
    public partial class GroupInfos
    {
        protected void DataPortal_Fetch( ) {
            const string query = @"SELECT id
                                         ,name
                                         ,description
                                    FROM Group
                                ORDER BY name";
            using ( var manager = ConnectionManager<OracleConnection>.GetManager( "PDM" ) ) {
                using ( var command = manager.Connection.CreateCommand( ) ) {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    using ( var reader = new SafeDataReader( command.ExecuteReader( ) ) ) {
                        RaiseListChangedEvents = false;
                        IsReadOnly = false;
                        while ( reader.Read(  ) ) {
                            Add( GroupInfo.GetGroupInfo( reader ) );
                        }
                        RaiseListChangedEvents = true;
                        IsReadOnly = true;
                    }
                }
            }
        }
    }
}
