using System;
using System.Diagnostics;
using CslaProject.Model.ObjectFactoryPattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CslaProject.UnitTests.ObjectFactoryTests
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void TestMethod1() {
            var person = Person.GetPersonById( 123 );

            //var person = Person.NewPerson( );
            person.FirstName = "FirstName";
            person.SecondName = "SecondName";
            person.Age = 19;
            //person.Orders.Add( Order.NewOrder(  ) );
            //person.Orders.Add( Order.NewOrder(  ) );
            person = person.Save( );
            
        }
    }
}
