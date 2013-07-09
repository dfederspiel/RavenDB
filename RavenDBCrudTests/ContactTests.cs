using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using RavenDBCRUD;
using System.Diagnostics;
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
                session.SaveChanges();  // Always call SaveChanges to commit data
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

        /// <summary>
        /// Test to show auto population of Id's on newly inserted documents.
        /// </summary>
        [TestMethod]
        public void ShowMeTheIds()
        {
            using (IDocumentSession session = Store.OpenSession())
            {
                // Get all the contacts and list their Id's
                var contacts =
                (
                    from c in session.Query<Contact>()
                    select c
                ).ToArray();

                Trace.WriteLine("Existing Id's");
                foreach (var c in contacts)
                {
                    Trace.WriteLine("Contact Id: " + c.Id);
                }

                // Insert a new contact
                var contact = new Contact()
                {
                    Age = 111,
                    Name = "Ansel Adams",
                    Email = "ansel@photog.net"
                };
                Trace.WriteLine("New Contact document id: " + contact.Id);
                session.Store(contact);
                Trace.WriteLine("New Contact document id: " + contact.Id);

                Assert.IsTrue(contact.Id != 0);
            }
        }

        [TestMethod]
        public void DataControllerTestCreate()
        {
            var contact = new Contact()
            {
                Name = "Wolfgang Mozart",
                Email = "wolfy@courtcomposers.au",
                Age = 257
            };

            using (RDataController controller = new RDataController())
            {
                Trace.WriteLine("New Contact document id: " + contact.Id);
                controller.AddDocument(contact);
                Trace.WriteLine("New Contact document id: " + contact.Id);

                contact.Name = "Bach";
            }
        }

        [TestMethod]
        public void DataControllerTestRead()
        {
            using (RDataController controller = new RDataController())
            {
                // READ
                var contacts = controller.GetDocuments<Contact>();
                foreach (var c in contacts)
                {
                    Trace.WriteLine("{0} is {1} years old today.".Replace("{0}", c.Name).Replace("{1}", c.Age.ToString()));
                }

            }
        }

        [TestMethod]
        public void DataControllerTestUpdate()
        {
            using (RDataController controller = new RDataController())
            {
                var contact = controller.GetDocuments<Contact>().FirstOrDefault();
                Trace.WriteLine("New Contact's current name is: " + contact.Name);
                contact.Name = "Wolfgang Amadeus Mozart";
                controller.UpdateDocument(contact);
                Trace.WriteLine("New Contact's new name is: " + contact.Name);
            }
        }

        [TestMethod]
        public void DataControllerTestDelete()
        {
            using (RDataController controller = new RDataController())
            {
                var contacts = controller.GetDocuments<Contact>();
                foreach (var c in contacts)
                {
                    controller.DeleteDocument(c);
                }
            }
        }
    }
}
