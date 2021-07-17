using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GenetecDomain_IlirG.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime? DateChanged { get; set; }
        public string Description { get; set; }
        public int EntityBookId { get; set; }

    }
}
