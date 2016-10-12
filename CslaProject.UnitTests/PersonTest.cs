using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CslaProject.Model.ObjectFactoryPattern;
using CslaProject.Model.RepositoryPattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Order = CslaProject.Model.RepositoryPattern.Order;
using Person = CslaProject.Model.RepositoryPattern.Person;


namespace CslaProject.UnitTests
{
    [TestClass]
    public class PersonTest 
    {
        [TestMethod]
        public void Test1( ) {
            var person = Person.NewPerson( );
            person.FirstName = "FirstName";
            person.SecondName = "SecondName";
            person.Age = 18;
            person.Orders.Add( Order.NewOrder( ) );
            person.Orders.Add( Order.NewOrder(  ) );
            person = person.Save( );
        }

    }
}
