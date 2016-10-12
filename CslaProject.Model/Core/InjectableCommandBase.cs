#region Usings

using System;
using System.ComponentModel.Composition;
using Csla;

#endregion Usings


namespace CslaProject.Model.Core
{
    [Serializable]
    public abstract class InjectableCommandBase<T> : CommandBase<T> where T : CommandBase<T>
    {
        protected override void DataPortal_OnDataPortalInvoke( Csla.DataPortalEventArgs e ) {
            Inject( );
            base.DataPortal_OnDataPortalInvoke( e );
        }

        protected override void OnDeserialized( System.Runtime.Serialization.StreamingContext context ) {
            Inject( );
            base.OnDeserialized( context );
        }

        private void Inject( ) {
            Container.InjectInto(this);
        }
    }
}
