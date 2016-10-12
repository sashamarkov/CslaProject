using System;
using Csla;


namespace CslaProject.Model.Old
{
    [Serializable]
    public partial class GroupInfos : ReadOnlyListBase<GroupInfos, GroupInfo>
    {
        private GroupInfos( ) { }

        private static GroupInfos _groupInfos;

        public static GroupInfos GetGroupInfos( ) {
            return _groupInfos ?? ( _groupInfos = DataPortal.Fetch<GroupInfos>( ) );
        }

        public static void InvalidateLocalCache( ) {
            _groupInfos = null;
        }
    }
}
