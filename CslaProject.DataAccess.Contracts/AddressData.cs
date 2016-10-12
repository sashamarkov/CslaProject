using System;

namespace CslaProject.DataAccess.Contracts
{
    [Serializable]
    public sealed class AddressData
    {
        public int Id { get; set; }

        public string FirstAddress { get; set; }

        public string SecondAddress { get; set; }
    }
}
