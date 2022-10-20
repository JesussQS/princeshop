using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace princeshop.Models
{
     [Table("t_producto")]
    public class Producto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public Decimal Precio { get; set; }

        public string? Temporada{ get; set;}

        public string? Sexo{ get; set;}

        public string? ImageName { get; set; }

        public string? Status { get; set; }
    }
}