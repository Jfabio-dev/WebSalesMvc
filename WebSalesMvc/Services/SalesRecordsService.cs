using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace WebSalesMvc.Services
{
    public class SalesRecordsService
    {
        private readonly WebSalesMvcContext _context;
        
        public SalesRecordsService(WebSalesMvcContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecords>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                   .Include(x => x.Seller)
                   .Include(x => x.Seller.Department)
                   .OrderByDescending(x => x.Date)
                   .ToListAsync();
        }
    }
}
