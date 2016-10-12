using System.Linq;
using Csla;
using System;
using CslaProject.Model.Core;


namespace CslaProject.Model.RepositoryPattern
{
    [Serializable]
    public partial class GroupInfos : InjectableReadOnlyListBase<GroupInfos, GroupInfo>
    {
        private GroupInfos(){}

        private static GroupInfos _groupInfos;

        public static GroupInfos GetGroupInfos( ) {
            return _groupInfos ?? ( _groupInfos = DataPortal.Fetch<GroupInfos>( ) );
        }

        public static void InvalidateLocalCache( ) {
            _groupInfos = null;
        }
    }
}
