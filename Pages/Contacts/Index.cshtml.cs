using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
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

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Contact> query = _context.Contacts.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var termo = SearchTerm.Trim();

                query = query.Where(contact => contact.Name.Contains(SearchTerm) || contact.ContactNumber.Contains(SearchTerm) || contact.Email.Contains(SearchTerm));
            }
            Contacts = await query.OrderBy(contact => contact.Name).ToListAsync();
        }
    }
}
