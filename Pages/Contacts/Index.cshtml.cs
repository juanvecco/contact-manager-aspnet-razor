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

        [BindProperty(SupportsGet = true)]
        public string OrderBy { get; set; } = "name";

        [BindProperty(SupportsGet = true)]
        public string Direction { get; set; } = "asc";

        public async Task OnGetAsync()
        {
            IQueryable<Contact> query = _context.Contacts.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var termo = SearchTerm.Trim();

                query = query.Where(contact => contact.Name.Contains(termo) || contact.ContactNumber.Contains(termo) || contact.Email.Contains(termo));
            }

            if (OrderBy == "email")
            {
                query = Direction == "desc"
                    ? query.OrderByDescending(contact => contact.Email)
                    : query.OrderBy(contact => contact.Email);
            }
            else if (OrderBy == "contactnumber")
            {
                query = Direction == "desc"
                    ? query.OrderByDescending(contact => contact.ContactNumber)
                    : query.OrderBy(contact => contact.ContactNumber);
            }
            else
            {
                query = Direction == "desc"
                    ? query.OrderByDescending(contact => contact.Name)
                    : query.OrderBy(contact => contact.Name);
            }

            Contacts = await query.ToListAsync();
        }
    }
}
