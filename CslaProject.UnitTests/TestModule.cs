using CslaProject.DataAccess.Contracts;
using Ninject.Modules;


namespace CslaProject.UnitTests
{
    public class TestModule : NinjectModule
    {
        public override void Load( ) {
            Bind<IPersonRepository>( ).To<PersonRepository>( ).InSingletonScope( );
            Bind<IOrderRepository>( ).To<OrderRepository>( ).InSingletonScope( );
            Bind<IAddressRepository>( ).To<AddressRepository>( ).InSingletonScope( );
            Bind<IContext>( ).To<Context>( ).InSingletonScope( );
        }
    }
}
