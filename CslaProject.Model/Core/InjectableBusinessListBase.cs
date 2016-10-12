using System;
using System.ComponentModel.Composition;
using Csla;


namespace CslaProject.Model.Core
{
    [Serializable]
    public abstract class InjectableBusinessListBase<TList, TItem> : BusinessListBase<TList, TItem> where TList : BusinessListBase<TList, TItem>
                                                                                                    where TItem : BusinessBase<TItem>
    {
        protected InjectableBusinessListBase( ) { }

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
    }
}
