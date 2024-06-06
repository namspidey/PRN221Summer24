using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using rp2.Models;

namespace rp2.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly PRN211_1Context _context;

        public CreateModel(PRN211_1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Student St {  get; set; }
        public List<Department> Dept { get; set; }
        public IActionResult OnGet()
        {
            Dept = _context.Departments.ToList();
            ViewData["dept"] = new SelectList(_context.Departments, "Id", "Name");
            return Page();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid || _context.Students==null || St==null) {
                return Page();
            }
            var x = _context.Students.Find(St.Id);
            if (x != null) return Page();
            _context.Students.Add(St);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
