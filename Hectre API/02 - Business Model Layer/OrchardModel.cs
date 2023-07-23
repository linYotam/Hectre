using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public class OrchardModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Block is required.")]
        public int Block { get; set; }
        [Required(ErrorMessage = "SubBlock is required.")]
        public string SubBlock { get; set; }


        public OrchardModel() { }

        // Set all OrchardModel params
        public OrchardModel(Orchard orchard)
        {
            Id = orchard.Id;
            Name = orchard.Name;
            Block = orchard.Block;
            SubBlock = orchard.SubBlock;
        }
    }
}
