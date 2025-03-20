using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColiTool.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColiTool.Database.Repositories
{
    public class CanMessageRepository : ICanMessageRepository
    {
        private readonly AppDbContext _context;

        public CanMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        //Add new CAN message into the db and save changes
        public async Task AddCanMessageAsync(CanMessage canMessage)
        {
            await _context.CanMessages.AddAsync(canMessage);
            await _context.SaveChangesAsync();
        }

        //Finds a CAN message by id and removes it from the db
        public async Task DeleteCanMessageAsync(int id)
        {
            var canMessage = await _context.CanMessages.FindAsync(id);
            if (canMessage != null)
            {
                _context.CanMessages.Remove(canMessage);
                await _context.SaveChangesAsync();
            }
        }

        //Finds a CAN message by id
        public async Task<CanMessage> GetCanMessageAsync(int id)
        {
            return await _context.CanMessages.FindAsync(id);
        }

        //Returns all CAN messages from the db
        public async Task<IEnumerable<CanMessage>> GetCanMessagesAsync()
        {
            return await _context.CanMessages.ToListAsync();
        }

        //Updates a CAN message in the db
        public async Task UpdateCanMessageAsync(CanMessage canMessage)
        {
            _context.CanMessages.Update(canMessage);
            await _context.SaveChangesAsync();
        }
    }
}
