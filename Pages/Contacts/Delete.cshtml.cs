using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Contacts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Contact Contact { get; private set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

            if (contact is null)
            {
                return NotFound();
            }

            Contact = contact;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            if (contact is null)
            {
                return NotFound();
            }
            contact.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
