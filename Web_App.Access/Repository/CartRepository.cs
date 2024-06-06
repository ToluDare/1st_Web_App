using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App.Data;
using Web_App.DataAccess.Repository.IRepository;
using Web_App.Models;

namespace Web_App.DataAccess.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private Data.ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Cart obj)
        {
            _db.Carts.Update(obj);
        }
    }
}
