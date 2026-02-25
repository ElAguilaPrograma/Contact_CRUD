using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact.Data.Entities
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumeber { get; set; } = null!;
        public int CategoryId { get; set; }

        //NAVEGATION Properties.
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

    }
}
