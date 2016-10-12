using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Csla;


namespace CslaProject.Model.Core
{
    [Serializable]
    public class InjectableReadOnlyListBase<TList, TItem> : ReadOnlyListBase<TList, TItem> where TList : ReadOnlyListBase<TList, TItem>
                                                                                           where TItem : ReadOnlyBase<TItem>
    {
        protected override void DataPortal_OnDataPortalInvoke( DataPortalEventArgs e ) {
            Inject( );
            base.DataPortal_OnDataPortalInvoke( e );
        }

        protected override void Child_OnDataPortalInvoke( DataPortalEventArgs e ) {
            Inject( );
            base.Child_OnDataPortalInvoke( e );
        }

        protected override void OnDeserialized( ) {
            Inject( );
            base.OnDeserialized( );
        }

        private void Inject( ) {
            Container.InjectInto( this );
        }

        protected void AddRangeWithoutEvents( IEnumerable<TItem> range ) {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            AddRange( range );
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}
