using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR_API_Example.Features.User
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
    }

    public class Sex
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public String Name { get; set; }
    }
}
