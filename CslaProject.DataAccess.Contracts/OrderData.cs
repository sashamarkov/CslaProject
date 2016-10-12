using System;


namespace CslaProject.DataAccess.Contracts
{
    [Serializable]
    public sealed class OrderData
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
