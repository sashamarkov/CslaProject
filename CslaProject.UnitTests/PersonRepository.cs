#region Usings

using CslaProject.DataAccess.Contracts;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

#endregion Usings


namespace CslaProject.UnitTests
{
    [Export( typeof ( IPersonRepository ) )]
    public class PersonRepository : IPersonRepository
    {
        public string GetNewId( ) {
            return Guid.NewGuid( ).ToString( "N" );
        }


        public PersonData FindPerson( int id ) {
            return new PersonData {Age = 30, Comment = "Comment", FirstName = "Test", SecondName = "Test", Id = id};
        }

        public PersonData FindPerson( string name ) {
            return new PersonData {Age = 30, Comment = "Comment", FirstName = "Test", SecondName = "Test", Id = 123};
        }

        public int AddPerson( PersonData newPerson ) {
            return 123;
        }

        public void EditPerson( PersonData existingPerson ) {
            Debug.Print( "Person {0} {1} updated", existingPerson.FirstName, existingPerson.SecondName );
        }

        public void RemovePerson( int personId ) { }

        public void DeletePerson( string personId ) {
            Debug.Print( "Person with id = {0} deleted", personId );
        }
    }

    public class Context : IContext
    {
        public ITransaction BeginTransaction( ) {
            return new Transaction(  );
        }
    }


    public class Transaction : ITransaction
    {
        public void Dispose( ) {
            
        }

        public void Commit( ) {
            
        }

        public void Rollback( ) {
            
        }
    }




}
