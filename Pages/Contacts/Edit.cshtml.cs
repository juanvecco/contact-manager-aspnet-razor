using ContactManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Contact Contact { get; set; } = new Models.Contact();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contact is null)
            {
                return NotFound();
            }

            Contact = contact;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var contactToUpdate = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            if (contactToUpdate is null)
            {
                return NotFound();
            }
            var allContacts = _context.Contacts.IgnoreQueryFilters();
            if (await allContacts.AnyAsync(c => c.ContactNumber == Contact.ContactNumber && c.Id != id))
            {
                ModelState.AddModelError("Contact.ContactNumber", "Este contato já está cadastrado.");
            }
            if (await allContacts.AnyAsync(c => c.Email == Contact.Email && c.Id != id))
            {
                ModelState.AddModelError("Contact.Email", "Este e-mail já está cadastrado.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            contactToUpdate.Name = Contact.Name;
            contactToUpdate.ContactNumber = Contact.ContactNumber;
            contactToUpdate.Email = Contact.Email;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
