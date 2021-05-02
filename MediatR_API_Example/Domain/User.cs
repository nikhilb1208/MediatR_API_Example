using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR_API_Example.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public int SexId { get; set; }

        public virtual Sex Sex { get; set; }
    }
}
