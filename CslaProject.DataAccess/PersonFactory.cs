#region Usings

using System.ComponentModel;
using Csla;
using Csla.Data;
using Csla.Reflection;
using Csla.Server;
using CslaProject.DataAccess.Contracts;
using CslaProject.Model.CommonCriteries;
using CslaProject.Model.ObjectFactoryPattern;
using System;
using System.ComponentModel.Composition;
using Ninject;

#endregion Usings

namespace CslaProject.DataAccess
{
    public class PersonFactory : ObjectFactory
    {
        private IPersonRepository _personRepository;

        //[Inject]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private IPersonRepository PersonRepository
        {
            get { return _personRepository; }
            set { _personRepository = value; }
        }

        public PersonFactory( IPersonRepository personRepository ) {
            if ( personRepository == null ) {
                throw new ArgumentNullException( "personRepository" );
            }
            _personRepository = personRepository;
        }

        [RunLocal]
        public object Create( ) {
            var person = GetEmptyPerson( );
            using ( BypassPropertyChecks( person ) ) {
                MarkNew( person );
                CheckRules( person );
            }
            return person;
        }

        public object Fetch( object criteria ) {
            if ( criteria is IdCriteria ) {
                return Fetch( ( IdCriteria )criteria );
            }
            if ( criteria is NameCriteria ) {
                return Fetch( ( NameCriteria )criteria );
            }
            throw new ArgumentException( string.Format( "{0} criteria is not supported", criteria.GetType( ).Name ) );
        }

        private Person Fetch( IdCriteria idCriteria ) {
            var person = GetEmptyPerson( );
            using ( BypassPropertyChecks( person ) ) {
                var data = PersonRepository.FindPerson( idCriteria.Id );
                DataMapper.Map( data, person, Person.OrdersProperty.Name );
            }
            MarkOld( person );
            return person;
        }

        private Person Fetch( NameCriteria nameCriteria ) {
            var person = GetEmptyPerson( );
            using ( BypassPropertyChecks( person ) ) {
                var data = PersonRepository.FindPerson( nameCriteria.Name );
                DataMapper.Map( data, person, Person.OrdersProperty.Name );
            }
            MarkOld( person );
            return person;
        }

        private Person GetEmptyPerson( ) {
            return ( Person )MethodCaller.CreateInstance( typeof ( Person ) );
        }

        internal Person Update( Person person ) {
            var personData = new PersonData( );
                DataMapper.Map( person, personData, Person.OrdersProperty.Name );
                if ( person.IsNew ) {
                    PersonRepository.EditPerson( personData );
                    LoadProperty( person, Person.IdProperty, personData.Id );
                    LoadProperty( person, Person.LastChangedProperty, personData.LastChanged );
                    UpdateChildren( person );
                    MarkOld( person );
                } else if ( person.IsDeleted ) {
                    PersonRepository.RemovePerson( person.Id );
                } else {
                    PersonRepository.EditPerson( personData );
                    LoadProperty( person, Person.LastChangedProperty, personData.LastChanged );
                    UpdateChildren( person );
                    MarkOld( person );
                }
              
            
            MarkOld( person );
            return person;
        }

        private void UpdateChildren( Person person ) {
            var factoryLoader = new FactoryLoader( );
            var ordersFactory = (OrdersFactory)factoryLoader.GetFactory("OrdersFactory");
            ordersFactory.Update( person.Orders, person );
            var addressFactory = ( AddressFactory )factoryLoader.GetFactory( "AddressFactory" );
            addressFactory.Update( person.Address, person );
        }
    }
}