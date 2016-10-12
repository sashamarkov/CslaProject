using Csla.Server;
using Ninject;
using System;


namespace CslaProject.DataAccess
{
    public class FactoryLoader : IObjectFactoryLoader
    {
        // Внедрение в существующий экземляр.
        //public object GetFactory( string factoryName ) {
        //    var factoryTypeName = GetTypeName( factoryName );
        //    var factoryType = Type.GetType( factoryTypeName );
        //    var factory = MethodCaller.CreateInstance( factoryType );
        //    Container.Kernel.Inject( factory );
        //    return factory;
        //}

        //Внедрение через конструктор
        public object GetFactory( string factoryName ) {
            var factoryTypeName = GetTypeName( factoryName );
            var factoryType = Type.GetType( factoryTypeName );
            var factory = Model.Core.Container.Kernel.Get( factoryType );
            return factory;
        }

        public Type GetFactoryType( string factoryName ) {
            var typeName = GetTypeName( factoryName );
            return Type.GetType( typeName );
        }

        private string GetTypeName( string factoryName ) {
            return String.Format("CslaProject.DataAccess.{0}, CslaProject.DataAccess", factoryName);
        }
    }
}
