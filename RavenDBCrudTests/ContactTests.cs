using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RavenDBCRUD;
using Raven.Client;
using System.Linq;

namespace RavenDBCrudTests
{
    [TestClass]
    public class ContactTests : RDocumentStore
    {
        [TestMethod]
        public void CreateContact()
        {
            var contact = new Contact()
            {
                Name = "John",
                Age = 27
            };
            using (IDocumentSession session = Store.OpenSession())
            {
                session.Store(contact);
                session.SaveChanges();
            }
        }

        [TestMethod]
        public void LoadContacts()
        {
            using (IDocumentSession session = Store.OpenSession())
            {
                var contacts =
                (
                    from c in session.Query<Contact>()
                    select c
                ).ToArray();

                Assert.IsTrue(contacts != null);
            }
        }
    }
}
