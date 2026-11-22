using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IList<Contact> Contacts { get; private set; } = new List<Contact>();

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Contacts = await _context.Contacts.OrderBy(contact => contact.Name).ToListAsync();
        }
    } 
}
