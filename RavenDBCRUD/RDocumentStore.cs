using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenDBCRUD
{
    public class RDocumentStore
    {
        public IDocumentStore Store
        {
            get { return LazyDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new EmbeddableDocumentStore
            {
                DataDirectory = "Data"
            };

            docStore.Initialize();
            return docStore;
        });
    }
}