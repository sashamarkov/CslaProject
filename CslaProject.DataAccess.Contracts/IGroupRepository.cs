using System.Collections.Generic;

namespace CslaProject.DataAccess.Contracts
{
    public interface IGroupRepository
    {
        IEnumerable<GroupData> GetAllGroups( );
    }
}
