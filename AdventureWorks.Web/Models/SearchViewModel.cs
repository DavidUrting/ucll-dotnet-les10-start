using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Models
{
    public class SearchViewModel
    {
        public string Subtitle { get { return "Zoeken naar klanten"; } }

        public string Keyword { get; set; }
    }

}
