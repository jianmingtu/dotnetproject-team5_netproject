using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using DotNetProject_Team5_Armoire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetProject_Team5_Armoire.Pages.ItemAddedNotification
{
    public class IndexModel : PageModel
    {
        //Access database
        protected readonly ClothDbContext db;
        public IQueryable<Clothing> Clothes { get; set; }
        public List<Clothing> isDirty = new List<Clothing>();

        public string msg = "";

        public IndexModel(ClothDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            string userId;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Clothes = db.Clothes
                    .Where(c => c.OwnerId == userId);
                // filter
                foreach (var item in Clothes)
                {
                    if (!item.IsClean)
                    {
                        isDirty.Add(item);

                    }
                }
                if (isDirty.Count > 3)
                {
                    msg = $"You have {isDirty.Count} items in your dirty pile. Time to do laundry!";
                }
                else
                {
                    msg = "No new notifications at this time";
                }
            }
        }
    }
}
