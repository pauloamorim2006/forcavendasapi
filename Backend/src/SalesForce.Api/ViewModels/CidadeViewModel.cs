using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.ViewModels
{
    public class CidadeViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Código IBGE")]
        public int CodigoIbge { get; set; }
        [Display(Name = "Cidade")]
        public string Descricao { get; set; }
        [Display(Name = "UF")]
        public string Uf { get; set; }
    }
}
