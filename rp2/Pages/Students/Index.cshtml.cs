using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rp2.Models;

namespace rp2.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly PRN211_1Context _context;
        public IndexModel(PRN211_1Context context)
        {
            _context = context;
        }

        public List<Student> Std;
        public List<Department> Dept { get; set; }
        public void OnGet()
        {
            // load(select);
            ViewData["select"] = new List<string>();
            Std=_context.Students.Include(x=>x.Depart).ToList();
            Dept = _context.Departments.Select(x => x).ToList();
        }

        public IActionResult OnPost(IFormCollection f)
        {
            List<string> select = new List<string>();
            if (f.ContainsKey("All"))
            {
                Std = _context.Students.Include(x => x.Depart).ToList();
                select.Add("All");
            }
            else
            {
                foreach (var de in _context.Departments.ToList())
                {
                    // if (Request.Form[de.Id] != string.IsNullOrEmpty) select.Add(de.Id.ToString());
                    if (f.ContainsKey(de.Id)) select.Add(de.Id);

                }
                Std = _context.Students.Include(x => x.Depart).Where(x => select.Contains(x.DepartId)).ToList();

            }

            //Std = _context.Students.Include(x => x.Depart).Where(x => select.Contains(x.DepartId)).Select(x => x).ToList();
            //  OnGet(select);
            ViewData["select"] = select;
            Dept = _context.Departments.Select(x=>x).ToList();
            return Page();
        }



        private void load(List<string> select)
        {
            if (select.Count == 0)
                _context.Students.Include(x => x.Depart).ToList();
            else
                Std = _context.Students.Include(x => x.Depart).Where(x => select.Contains(x.DepartId)).Select(x => x).ToList();
        }
    }
}
