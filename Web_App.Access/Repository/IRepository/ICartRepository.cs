using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App.Models;

namespace Web_App.DataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        //methods for the repo ie add- to cart , remove from cart
        void Add(Cart obj);
        void Remove(Cart obj);
        void Save();
        void Update(Cart obj);

    }
}
