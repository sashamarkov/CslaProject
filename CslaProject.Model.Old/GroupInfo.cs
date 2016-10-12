using System;
using Csla;


namespace CslaProject.Model.Old
{
    [Serializable]
    public sealed partial class GroupInfo : ReadOnlyBase<GroupInfo>
    {
        private GroupInfo( ) { }

        private static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        internal int Id {
            get { return GetProperty( IdProperty ); }
            private set { LoadProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>( c => c.Name );

        public string Name {
            get { return GetProperty( NameProperty ); }
            private set { LoadProperty( NameProperty, value ); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>( c => c.Description );

        public string Description {
            get { return GetProperty( DescriptionProperty ); }
            private set { LoadProperty( DescriptionProperty, value ); }
        }
    }
}
