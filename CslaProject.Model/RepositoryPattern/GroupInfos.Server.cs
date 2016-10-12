using CslaProject.DataAccess.Contracts;
using Ninject;
using System;
using System.ComponentModel;
using System.Linq;


namespace CslaProject.Model.RepositoryPattern
{
    public partial class GroupInfos
    {
        [NonSerialized]
        private IGroupRepository _groupRepository;

        [Inject]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected IGroupRepository GroupRepository {
            get { return _groupRepository; }
            set { _groupRepository = value; }
        }

        protected void DataPortal_Fetch( ) {
            var data = GroupRepository.GetAllGroups( );
            AddRangeWithoutEvents( data.Select( GroupInfo.GetGroupInfo ) );
        }
    }
}
