
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Address:SharedModel
    {
        [Required(ErrorMessage ="Adress Is Requierd")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Code is Requierd")]
  
        public int Code { get; set; }


        public virtual Address Parent { get; set; }
        
        public virtual List<Address> Child { get; set; }
        public string AppUserId { get; set; }
        public virtual AppUser AppUsers { get; set; }

    }
}
