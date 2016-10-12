using CslaProject.Model.Old;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Person = CslaProject.Model.Old.Person;


namespace CslaProject.UnitTests.Productivity
{
    [TestClass]
    public class InjectTest
    {
        [TestMethod]
        public void SaveNewPerson_Test( ) {
            var oldPerson = Person.NewPerson( );
            oldPerson.FirstName = "first_name";
            oldPerson.SecondName = "second_name";
            oldPerson.Age = 20;
            var watch = Stopwatch.StartNew( );
            oldPerson = oldPerson.Save( );
            var oldPersonInsert = watch.Elapsed;

            watch.Restart(  );
            var person = Model.RepositoryPattern.Person.NewPerson( );
            person.FirstName = "first_name";
            person.SecondName = "second_name";
            person.Age = 20;
            watch.Restart( );
            person = person.Save( );
            var personInsert = watch.Elapsed;
            watch.Stop(  );

            Debug.WriteLine("Original person insert: {0}:{1}:{2}", oldPersonInsert.Minutes, oldPersonInsert.Seconds, oldPersonInsert.Milliseconds);
            Debug.WriteLine( "Injected person insert: {0}:{1}:{2}", personInsert.Minutes, personInsert.Seconds, personInsert.Milliseconds );
        }

        [TestMethod]
        public void SaveNewPerson_OneThousand_Test( ) {
            var watch = new Stopwatch( );
            watch.Start( );
            for ( int i = 0; i < 1000; i++ ) {
                var oldPerson = Person.NewPerson( );
                oldPerson.FirstName = "first_name";
                oldPerson.SecondName = "second_name";
                oldPerson.Age = 20;
                oldPerson = oldPerson.Save( );
            }
            var oldPersonInsert = watch.Elapsed;

            watch.Restart( );
            for ( int i = 0; i < 1000; i++ ) {
                var person = Model.RepositoryPattern.Person.NewPerson( );
                person.FirstName = "first_name";
                person.SecondName = "second_name";
                person.Age = 20;
                person = person.Save( );
            }
            var personInsert = watch.Elapsed;
            watch.Stop( );

            Debug.WriteLine("Original person insert: {0}:{1}:{2}", oldPersonInsert.Minutes, oldPersonInsert.Seconds, oldPersonInsert.Milliseconds);
            Debug.WriteLine("Injected person insert: {0}:{1}:{2}", personInsert.Minutes, personInsert.Seconds, personInsert.Milliseconds);
        }

        [TestMethod]
        public void TSaveNewperson_TenThousands_Test( ) {
            var watch = new Stopwatch( );
            watch.Start( );
            for ( int i = 0; i < 10000; i++ ) {
                var oldPerson = Person.NewPerson( );
                oldPerson.FirstName = "first_name";
                oldPerson.SecondName = "second_name";
                oldPerson.Age = 20;
                oldPerson = oldPerson.Save( );
            }
            var oldPersonInsert = watch.Elapsed;

            watch.Restart( );
            for ( int i = 0; i < 10000; i++ ) {
                var person = Model.RepositoryPattern.Person.NewPerson( );
                person.FirstName = "first_name";
                person.SecondName = "second_name";
                person.Age = 20;
                person = person.Save( );
            }
            var personInsert = watch.Elapsed;
            watch.Stop( );

            Debug.WriteLine("Original person insert: {0}:{1}:{2}", oldPersonInsert.Minutes, oldPersonInsert.Seconds, oldPersonInsert.Milliseconds);
            Debug.WriteLine("Injected person insert: {0}:{1}:{2}", personInsert.Minutes, personInsert.Seconds, personInsert.Milliseconds);
        }

        [TestMethod]
        public void OneThousandWith10Orders_Old_Test( ) {
            var watch = new Stopwatch( );
            watch.Start( );
            for ( int i = 0; i < 1000; i++ ) {
                var oldPerson = Person.NewPerson( );
                oldPerson.FirstName = "first_name";
                oldPerson.SecondName = "second_name";
                oldPerson.Age = 20;
                for ( int j = 0; j < 10; j++ ) {
                    oldPerson.Orders.Add( Order.NewOrder( ) );
                }
                oldPerson = oldPerson.Save( );
            }
            var oldPersonInsert = watch.Elapsed;
            watch.Restart( );
            watch.Start( );
            for ( int i = 0; i < 10000; i++ ) {
                var person = Model.RepositoryPattern.Person.NewPerson( );
                person.FirstName = "first_name";
                person.SecondName = "second_name";
                person.Age = 20;
                for ( int j = 0; j < 10; j++ ) {
                    person.Orders.Add( Model.RepositoryPattern.Order.NewOrder( ) );
                }
                person = person.Save( );
            }
            watch.Stop( );
            var personInsert = watch.Elapsed;

            Debug.WriteLine( "Original person insert: {0}:{1}:{2}", oldPersonInsert.Minutes, oldPersonInsert.Seconds, oldPersonInsert.Milliseconds );
            Debug.WriteLine( "Injected person insert: {0}:{1}:{2}", personInsert.Minutes, personInsert.Seconds, personInsert.Milliseconds );
        }
    }
}
