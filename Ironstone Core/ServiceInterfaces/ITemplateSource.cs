﻿using System;
using System.Collections.Generic;
using Jpp.Ironstone.Core.Autocad;

namespace Jpp.Ironstone.Core.ServiceInterfaces
{
    public interface ITemplateSource
    {
        IEnumerable<Guid> GetAllTemplates();

        DrawingObject GetTemplate(Guid id);

        bool Contains(Guid id);
    }
}
