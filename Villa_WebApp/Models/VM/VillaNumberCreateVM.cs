using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_WebApp.Models.DTO;

namespace Villa_WebApp.Models.VM
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateDTO  VillaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
        public VillaNumberCreateVM()
        {
            VillaNumber = new VillaNumberCreateDTO();
        }

    }
}
