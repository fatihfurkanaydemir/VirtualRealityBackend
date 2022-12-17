using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualReality.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
