namespace CslaProject.DataAccess.Contracts
{
    public interface IPersonRepository
    {
        PersonData FindPerson( int id );

        PersonData FindPerson( string name );

        int AddPerson( PersonData newPerson );

        void EditPerson( PersonData existingPerson );

        void RemovePerson( int personId );
    }
}