using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Movie {
    public sealed record MovieModel {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Review")]
        public List<long> Reviews { get; set; }
    }
}