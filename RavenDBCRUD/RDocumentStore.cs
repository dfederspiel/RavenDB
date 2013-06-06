using Raven.Client;
using Raven.Client.Embedded;
using System;

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