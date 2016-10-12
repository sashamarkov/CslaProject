using System;


namespace CslaProject.DataAccess.Contracts
{
    [Serializable]
    public sealed class GroupData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
