
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Role:SharedModel
    {
        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(20)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [ForeignKey("AppUser")]
        
        public virtual ICollection<AppUser> AppUser { get; set; }
    }
}
