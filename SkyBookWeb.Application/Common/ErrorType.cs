using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Application.Common
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Database,
        Unexpected
    }
}
