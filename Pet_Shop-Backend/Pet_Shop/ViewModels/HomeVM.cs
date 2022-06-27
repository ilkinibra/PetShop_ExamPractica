using Pet_Shop.Models;
using System.Collections.Generic;

namespace Pet_Shop.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Bio> Bio { get; set; }
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Team> Team { get; set; }

    }
}
