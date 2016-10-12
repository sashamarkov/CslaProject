using System;


namespace CslaProject.DataAccess.Contracts
{
    [Serializable]
    public sealed class PersonData
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public int Age { get; set; }

        public string Comment { get; set; }

        public object LastChanged { get; set; }
    }
}
