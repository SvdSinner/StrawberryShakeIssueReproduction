﻿using System;
using System.Collections.Generic;

namespace James.Shared.Model;
//Generated for DB

public partial class AgreementTypeDm
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public string Type { get; set; } = null!;
}
