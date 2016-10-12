using System;
using Csla;
using Csla.Server;
using CslaProject.Model.CommonCriteries;
using DataPortal = Csla.DataPortal;

namespace CslaProject.Model.ObjectFactoryPattern
{
    [Serializable]
    [ObjectFactory("PersonFactory")]
    public class Person : BusinessBase<Person>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        public int Id {
            get { return GetProperty( IdProperty ); }
            set { SetProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>( c => c.FirstName );

        public string FirstName {
            get { return GetProperty( FirstNameProperty ); }
            set { SetProperty( FirstNameProperty, value ); }
        }

        public static readonly PropertyInfo<string> SecondNameProperty = RegisterProperty<string>( c => c.SecondName );

        public string SecondName {
            get { return GetProperty( SecondNameProperty ); }
            set { SetProperty( SecondNameProperty, value ); }
        }

        public static readonly PropertyInfo<int> AgeProperty = RegisterProperty<int>( c => c.Age );

        public int Age {
            get { return GetProperty( AgeProperty ); }
            set { SetProperty( AgeProperty, value ); }
        }

        public static readonly PropertyInfo<string> CommentProperty = RegisterProperty<string>( c => c.Comment );

        public string Comment {
            get { return GetProperty( CommentProperty ); }
            set { SetProperty( CommentProperty, value ); }
        }

        public static readonly PropertyInfo<Orders> OrdersProperty = RegisterProperty<Orders>(c => c.Orders, RelationshipTypes.Child | RelationshipTypes.LazyLoad);

        public Orders Orders {
            get {
                if ( !FieldManager.FieldExists( OrdersProperty ) ) {
                    Orders = Orders.NewOrders( );
                }
                return GetProperty( OrdersProperty );
            }
            private set {
                LoadProperty( OrdersProperty, value );
                OnPropertyChanged( OrdersProperty );
            }
        }

        public static readonly PropertyInfo<Address> AddressProperty = RegisterProperty<Address>(c => c.Address, RelationshipTypes.Child | RelationshipTypes.LazyLoad);

        public Address Address {
            get {
                if ( !FieldManager.FieldExists( AddressProperty ) ) {
                    Address = Address.NewAddress( );
                }
                return GetProperty( AddressProperty );
            }
            private set {
                LoadProperty( AddressProperty, value );
                OnPropertyChanged( AddressProperty );
            }
        }

        public static readonly PropertyInfo<object> LastChangedProperty = RegisterProperty<object>(c => c.LastChanged);

        public object LastChanged {
            get { return ReadProperty( LastChangedProperty ); }
            set { LoadProperty( LastChangedProperty, value ); }
        }

        private Person( ) { }

        public static Person NewPerson( ) {
            return DataPortal.Create<Person>( );
        }

        public static Person GetPersonById( int personId ) {
            return DataPortal.Fetch<Person>( new IdCriteria( personId ) );
        }

        public static Person GetPersonByName( string personName ) {
            return DataPortal.Fetch<Person>( new NameCriteria( personName ) );
        }
    }
}