﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualReality.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
