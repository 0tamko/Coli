using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColiTool.Database.Entities;

namespace ColiTool.Database.Repositories
{
    public interface ICanMessageRepository
    {
        Task AddCanMessageAsync(CanMessage canMessage);
        Task<IEnumerable<CanMessage>> GetCanMessagesAsync();
        Task<CanMessage> GetCanMessageAsync(int id);
        Task UpdateCanMessageAsync(CanMessage canMessage);
        Task DeleteCanMessageAsync(int id);
    }
}
