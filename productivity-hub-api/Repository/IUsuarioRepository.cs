﻿using productivity_hub_api.Models;

namespace productivity_hub_api.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> Validate(string email, string password);

        Task<Usuario?> GetByIdAsync(int id);

        Task AddAsync(Usuario usuario);

        void Update(Usuario usuario);

        Task SaveAsync();
    }
}