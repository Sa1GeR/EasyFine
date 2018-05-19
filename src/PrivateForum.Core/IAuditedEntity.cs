﻿using System;

namespace PrivateForum.Core
{
    public interface IAuditedEntity
    {
        DateTime? Created { get; set; }
        DateTime? Modified { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
