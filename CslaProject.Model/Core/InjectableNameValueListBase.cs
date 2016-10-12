using System;
using System.ComponentModel.Composition;
using Csla;


namespace CslaProject.Model.Core
{
    [Serializable]
    public class InjectableNameValueListBase<TKey, TItem> : NameValueListBase<TKey, TItem>
    {
        protected InjectableNameValueListBase( ) { }

        protected override void DataPortal_OnDataPortalInvoke( DataPortalEventArgs e ) {
            Inject( );
            base.DataPortal_OnDataPortalInvoke( e );
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
