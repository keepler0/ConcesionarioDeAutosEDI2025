using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionario.Exceptions
{
	public class NotFoundException:Exception
	{
        public NotFoundException(string mensaje):base(mensaje)
        {
        }
    }
}
