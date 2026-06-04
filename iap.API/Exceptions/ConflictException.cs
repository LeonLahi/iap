using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Exceptions
{
    public class ConflictException(string message) : Exception(message);
}