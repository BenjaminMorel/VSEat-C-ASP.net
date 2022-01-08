using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ReviewToDisplay
    {

        public int IdRestaurant { get; set; }
        public int total { get; set;  }

        public double average { get; set;  }

        public int numberOfReview { get; set;  }

        public List<string> Comment { get; set;  }
    }
}
