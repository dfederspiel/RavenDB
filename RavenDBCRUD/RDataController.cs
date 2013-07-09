using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenDBCRUD
{
    public class RDataController : RDocumentStore, IDisposable
    {
        private IDocumentSession session;
        public IDocumentSession Session
        {
            get
            {
                if (session == null)
                {
                    session = Store.OpenSession();

                }
                return session;
            }
        }

        /// <summary>
        /// Gets all documents of the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetDocuments<T>()
        {
            return Session.Query<T>();
        }

        /// <summary>
        /// Gets a document of the specified type from the document store.
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="id">Document Id</param>
        /// <returns>Document of Type T</returns>
        public T GetDocument<T>(int id)
        {
            return Session.Load<T>(id);
        }

        /// <summary>
        /// Gets all documents by the specified ids
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IQueryable<T> GetDocuments<T>(IEnumerable<string> ids)
        {
            return Session.Load<T>(ids).AsQueryable();
        }

        /// <summary>
        /// Adds a document to the databse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void AddDocument<T>(T entity)
        {
            Session.Store(entity);
        }

        /// <summary>
        /// Updates a document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public void UpdateDocument<T>(T entity)
        {
            Session.Store(entity);
        }

        /// <summary>
        /// Deletes a document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void DeleteDocument<T>(T entity)
        {
            Session.Delete<T>(entity);
        }

        public void Dispose()
        {
            Session.SaveChanges();
            Session.Dispose();
        }
    }
}