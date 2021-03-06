﻿using Autodesk.AutoCAD.ApplicationServices;
using Jpp.Ironstone.Core.Autocad;
using Jpp.Ironstone.Core.ServiceInterfaces;
using System;

namespace Jpp.Ironstone.Core.Tests.TestObjects
{
    [Layer(Name = LAYER_NAME)]
    public class TestDocumentStoreWithSettingsDisabled : DocumentStore
    {
        public const string LAYER_NAME = "TestDocumentStoreWithSettingsLayer";

        public TestDocumentStoreWithSettingsDisabled(Document doc, Type[] managerTypes, ILogger log, LayerManager lm, IUserSettings settings) : 
            base(doc, managerTypes, log, lm, new TestLayersDisabledSettings()) { }
    }

    [Layer(Name = LAYER_NAME)]
    public class TestDocumentStoreWithSettingsEnabled : DocumentStore
    {
        public const string LAYER_NAME = "TestDocumentStoreWithSettingsLayer";

        public TestDocumentStoreWithSettingsEnabled(Document doc, Type[] managerTypes, ILogger log, LayerManager lm, IUserSettings settings) :
            base(doc, managerTypes, log, lm, new TestLayersEnabledSettings()) { }
    }
}
