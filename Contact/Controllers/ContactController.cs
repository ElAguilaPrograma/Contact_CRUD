using Contact.Data;
using Contact.Data.Entities;
using Contact.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;
        public ContactController(ContactContext context)
        {
            _context = context;
        }

        [HttpGet("insertdata")]
        public async Task<IActionResult> InsertCategotyData()
        {
            List<string> names = new List<string>() { "Trabajo", "Familia", "Amigos" };

            for (int i = 1; i < names.Count; i++)
            {
                var categoryData = new Category
                {
                    Name = names[i]
                };

                _context.Categories.Add(categoryData);
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.Contacts.ToListAsync());
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateContactDto createContactDto)
        {
            try
            {
                var newContact = new Contact.Data.Entities.Contact
                {
                    Name = createContactDto.Name,
                    PhoneNumeber = createContactDto.PhoneNumber,
                    CategoryId = createContactDto.categoryId
                };
                _context.Contacts.Add(newContact);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CreateContactDto createContactDto, Guid contactId)
        {
            try
            {
                var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);
                if (contact == null)
                    return NotFound();

                contact.Name = createContactDto.Name;
                contact.PhoneNumeber = createContactDto.PhoneNumber;

                _context.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);
            if ( contact == null )
                return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return Ok();
        } 
    }
}
