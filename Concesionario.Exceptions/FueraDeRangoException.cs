using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionario.Exceptions
{
	public class FueraDeRangoException:Exception
	{
        public FueraDeRangoException(string mensaje):base(mensaje)
        {
        }
    }
}
