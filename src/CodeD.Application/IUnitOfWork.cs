using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeD.Application
{
    public interface IUnitOfWork : IDisposable
    {

        Task SaveAsync();
    }
}
