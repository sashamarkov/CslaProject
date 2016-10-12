using Csla.Data;


namespace CslaProject.Model.Old
{
    public partial class GroupInfo
    {
        internal static GroupInfo GetGroupInfo( SafeDataReader reader ) {
            return new GroupInfo {Id = reader.GetInt32( "id" ), Name = reader.GetString( "name" ), Description = reader.GetString( "description" )};
        }
    }
}
