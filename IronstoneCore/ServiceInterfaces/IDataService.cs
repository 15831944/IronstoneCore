﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jpp.Ironstone.Core.Autocad;
using Jpp.Ironstone.Core.ServiceInterfaces.Template;

namespace Jpp.Ironstone.Core.ServiceInterfaces
{
    public interface IDataService
    {
        void CreateStoresFromAppDocumentManager();
        void PopulateStoreTypes();

        Type[] GetManagerTypes();

        T GetStore<T>(string ID) where T : DocumentStore;

        IEnumerable<DocumentStore> GetExistingStores(string ID);

        ITemplateSource GetTemplateSource(Guid id);

        void RegisterSource(ITemplateSource source);
    }
}
