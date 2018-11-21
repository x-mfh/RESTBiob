using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class HallRepository : Repository, IHallRepository
    {
        public HallRepository(BiobDataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Hall>> GetAllHallsAsync()
        {
            return await _context.Halls.ToListAsync();
        }

        public async Task<Hall> GetHallAsync(int id)
        {
            return await _context.Halls.Where(hall => hall.Id == id).FirstOrDefaultAsync();
        }

        public void AddHall(Hall hallToAdd)
        {
            _context.Halls.Add(hallToAdd);
        }

        public void UpdateHall(Hall hallToUpdate)
        {
            _context.Halls.Update(hallToUpdate);
        }

        public void DeleteHall(Hall hallToDelete)
        {
            _context.Halls.Remove(hallToDelete);
        }
    }
}
