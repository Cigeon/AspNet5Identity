using AspNet5Identity.DAL.Entities;
using System;

namespace AspNet5Identity.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
