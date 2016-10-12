using Csla;
using Csla.Rules.CommonRules;
using System;
using System.ComponentModel.DataAnnotations;


namespace CslaProject.Model.Old
{
    [Serializable]
    public sealed partial class Person : BusinessBase<Person>
    {
        private Person() { }

        public static Person NewPerson( ) {
            return DataPortal.Create<Person>( );
        }

        public static Person GetPerson( int personId ) {
            return DataPortal.Fetch<Person>( new SingleCriteria<Person, int>( personId ) );
        }

        public static Person GetPerson( string personName ) {
            return DataPortal.Fetch<Person>( new SingleCriteria<Person, string>( personName ) );
        }

        public static void RemovePerson( int personId ) {
            DataPortal.Delete<Person>( new SingleCriteria<Person, int>( personId ) );
        }

        private static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>( c => c.Id );

        internal int Id {
            get { return GetProperty( IdProperty ); }
            set { LoadProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>( c => c.FirstName );

        [Required]
        public string FirstName {
            get { return GetProperty( FirstNameProperty ); }
            set { SetProperty( FirstNameProperty, value ); }
        }

        public static readonly PropertyInfo<string> SecondNameProperty = RegisterProperty<string>( c => c.SecondName );

        [Required]
        public string SecondName {
            get { return GetProperty( SecondNameProperty ); }
            set { SetProperty( SecondNameProperty, value ); }
        }

        public static readonly PropertyInfo<int> AgeProperty = RegisterProperty<int>( c => c.Age );

        [Range( 18, 65 )]
        public int Age {
            get { return GetProperty( AgeProperty ); }
            set { SetProperty( AgeProperty, value ); }
        }

        public static readonly PropertyInfo<string> CommentProperty = RegisterProperty<string>( c => c.Comment );

        public string Comment {
            get { return GetProperty( CommentProperty ); }
            set { SetProperty( CommentProperty, value ); }
        }


        public static readonly PropertyInfo<Orders> OrdersProperty = RegisterProperty<Orders>(
                                                                                              c => c.Orders,
                                                                                              RelationshipTypes.Child | RelationshipTypes.LazyLoad );

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

        public static readonly PropertyInfo<Address> AddressProperty = RegisterProperty<Address>(
                                                                                         c => c.Address,
                                                                                         RelationshipTypes.Child | RelationshipTypes.LazyLoad);

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

        protected override void AddBusinessRules( ) {
            base.AddBusinessRules( );
            BusinessRules.AddRule( new MaxLength( FirstNameProperty, 20 ) );
            BusinessRules.AddRule( new MaxLength( SecondNameProperty, 40 ) );
            BusinessRules.AddRule( new MaxLength( CommentProperty, 1000 ) );
        }
    }
}