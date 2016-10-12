using Csla;
using Csla.Data;
using CslaProject.DataAccess.Contracts;
using Ninject;
using System;
using System.ComponentModel;


namespace CslaProject.Model.RepositoryPattern
{
    public partial class Person
    {
        public static readonly PropertyInfo<object> LastChangedProperty = RegisterProperty<object>( c => c.LastChanged );

        public object LastChanged {
            get { return ReadProperty( LastChangedProperty ); }
            private set { LoadProperty( LastChangedProperty, value ); }
        }

        [NonSerialized] [NotUndoable] 
        private IPersonRepository _personRepository;

        [Inject]
        [EditorBrowsable( EditorBrowsableState.Never )]
        protected IPersonRepository PersonRepository {
            get { return _personRepository; }
            set { _personRepository = value; }
        }
        
        [NonSerialized][NotUndoable]
        private IContext _context;

        [Inject]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected IContext Context {
            get { return _context; }
            set { _context = value; }
        }

        protected override void DataPortal_Create( ) {
            BusinessRules.CheckRules( );
        }

        protected void DataPortal_Fetch( SingleCriteria<Person, int> idCriteria ) {
            var personData = PersonRepository.FindPerson( idCriteria.Value );
            if ( personData != null ) {
                CopyValuesFrom( personData );
                LoadProperty( OrdersProperty, Orders.GetOrders( personData ) );
                LoadProperty( AddressProperty, Address.GetAddress( personData ) );
            }
        }

        protected void DataPortal_Fetch( SingleCriteria<Person, string> nameCriteria ) {
            var personData = PersonRepository.FindPerson( nameCriteria.Value );
            if ( personData != null ) {
                CopyValuesFrom( personData );
                LoadProperty( OrdersProperty, Orders.GetOrders( personData ) );
                LoadProperty( AddressProperty, personData );
            }
        }

        private void CopyValuesFrom( PersonData personData ) {
            using ( BypassPropertyChecks ) {
                DataMapper.Map( personData, this );
            }
        }

        protected override void DataPortal_Insert( ) {
            using ( var transaction = Context.BeginTransaction( ) ) {
                try {
                    var personData = GetPersonData( );
                    Id = PersonRepository.AddPerson( personData );
                    LastChanged = personData.LastChanged;
                    FieldManager.UpdateChildren( personData);
                    transaction.Commit( );
                } catch {
                    transaction.Rollback( );
                    throw;
                }
            }
        }

        protected override void DataPortal_Update( ) {
            using ( var transaction = Context.BeginTransaction( ) ) {
                try {
                    var personData = GetPersonData( );
                    PersonRepository.EditPerson( personData );
                    LastChanged = personData.LastChanged;
                    FieldManager.UpdateChildren( personData );
                    transaction.Commit( );
                } catch {
                    transaction.Rollback( );
                    throw;
                }
            }
        }

        protected void DataPortal_Delete( SingleCriteria<Person, int> idCriteria ) {
            PersonRepository.RemovePerson( idCriteria.Value );
        }

        protected override void DataPortal_DeleteSelf( ) {
            PersonRepository.RemovePerson( Id );
        }

        private PersonData GetPersonData( ) {
            var personData = new PersonData( );
            DataMapper.Map( this, personData, OrdersProperty.Name, AddressProperty.Name );
            return personData;
        }
    }
}
