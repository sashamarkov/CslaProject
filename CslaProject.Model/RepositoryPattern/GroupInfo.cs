using Csla;
using CslaProject.Model.Core;
using System;


namespace CslaProject.Model.RepositoryPattern
{
    [Serializable]
    public partial class GroupInfo : ReadOnlyBase<GroupInfo>
    {
        private GroupInfo( ) { }

        private static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        internal int Id {
            get { return GetProperty( IdProperty ); }
            set { LoadProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>( c => c.Name );

        public string Name {
            get { return GetProperty( NameProperty ); }
            protected set { LoadProperty( NameProperty, value ); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>( c => c.Description );

        public string Description {
            get { return GetProperty( DescriptionProperty ); }
            protected set { LoadProperty( DescriptionProperty, value ); }
        }
    }
}
