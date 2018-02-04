using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MyNote
{
    public partial class Attention
    {
        [Key]
        public Int64 Id { get; set; }

        [StringLength(50)]
        public string Content { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime Start { get; set; }

        public bool State { get; set; }

        public DateTime? Warning { get; set; }
    }
}
