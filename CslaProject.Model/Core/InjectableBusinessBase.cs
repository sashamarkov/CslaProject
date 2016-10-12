using System;
using System.ComponentModel.Composition;
using Csla;

namespace CslaProject.Model.Core
{
    [Serializable]
    public abstract class InjectableBusinessBase<T> : BusinessBase<T> where T : BusinessBase<T>
    {
        protected override void DataPortal_OnDataPortalInvoke( DataPortalEventArgs e ) {
            Inject( );
            base.DataPortal_OnDataPortalInvoke( e );
        }

        protected override void Child_OnDataPortalInvoke( DataPortalEventArgs e ) {
            Inject( );
            base.Child_OnDataPortalInvoke( e );
        }

        protected override void OnDeserialized( System.Runtime.Serialization.StreamingContext context ) {
            Inject( );
            base.OnDeserialized( context );
        }

        private void Inject( ) {
            // Здесь должен быть код для разрешения зависимостей данного экземпляра. 
            Container.InjectInto( this );
        }
    }
}