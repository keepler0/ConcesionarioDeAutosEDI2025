using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concesionario.Application.Dtos.Transmision
{
	public class TransmisionRequestDto
	{
        public int Id { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; } = string.Empty;
    }
}
