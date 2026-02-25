namespace Contact.Dtos
{
    public class CreateContactDto
    {
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int categoryId { get; set; }
    }
}
