using Csla.Data;
using CslaProject.DataAccess.Contracts;


namespace CslaProject.Model.RepositoryPattern
{
    public partial class GroupInfo
    {
        internal static GroupInfo GetGroupInfo( GroupData groupData ) {
            var groupInfo = new GroupInfo( );
            DataMapper.Map( groupData, groupInfo );
            return groupInfo;
        }
    }
}
