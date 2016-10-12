using System;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using Csla;


namespace CslaProject.Model.Core
{
    [Serializable]
    public class InjectableReadOnlyBase<T> : ReadOnlyBase<T> where T: ReadOnlyBase<T>
    {
        protected override void DataPortal_OnDataPortalInvoke( DataPortalEventArgs e ) {
            Inject( );
            base.DataPortal_OnDataPortalInvoke( e );
        }

        protected override void OnDeserialized( StreamingContext context ) {
            Inject( );
            base.OnDeserialized( context );
        }

        private void Inject( ) {
            Container.InjectInto( this );
        }
    }
}
