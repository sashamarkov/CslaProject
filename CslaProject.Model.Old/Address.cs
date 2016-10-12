using System;
using System.ComponentModel.DataAnnotations;
using Csla;


namespace CslaProject.Model.Old
{
    [Serializable]
    public partial class Address : BusinessBase<Address>
    {
        private Address() { }

        public static Address NewAddress( ) {
            return DataPortal.CreateChild<Address>( );
        }

        private static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        private int Id {
            get { return GetProperty( IdProperty ); }
            set { LoadProperty( IdProperty, value ); }
        }

        public static readonly PropertyInfo<string> FirstAddressProperty = RegisterProperty<string>(c => c.FirstAddress);

        [Required]
        public string FirstAddress {
            get { return GetProperty( FirstAddressProperty ); }
            set { SetProperty( FirstAddressProperty, value ); }
        }

        public static readonly PropertyInfo<string> SecondAddressProperty = RegisterProperty<string>(c => c.SecondAddress);

        public string SecondAddress {
            get { return GetProperty( SecondAddressProperty ); }
            set { SetProperty( SecondAddressProperty, value ); }
        }
    }
}