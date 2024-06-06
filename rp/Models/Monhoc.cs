using System;
using System.Collections.Generic;

namespace rp.Models
{
    public partial class Monhoc
    {
        public Monhoc()
        {
            Dethis = new HashSet<Dethi>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Dethi> Dethis { get; set; }
    }
}
