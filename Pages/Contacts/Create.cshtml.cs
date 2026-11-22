using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; } = new Contact();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var allContacts = _context.Contacts.IgnoreQueryFilters();

            if (await allContacts.AnyAsync(c => c.ContactNumber == Contact.ContactNumber))
            {
                ModelState.AddModelError("Contact.ContactNumber", "Este contato já está cadastrado.");
            }
            if (await allContacts.AnyAsync(c => c.Email == Contact.Email))
            {
                ModelState.AddModelError("Contact.Email", "Este e-mail já está cadastrado.");
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}

