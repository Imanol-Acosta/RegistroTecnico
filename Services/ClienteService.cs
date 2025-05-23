using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;


namespace RegistroTecnico.Services;

    public class ClienteService(IDbContextFactory<Contexto> dbFactory)
    {
        public async Task<bool> Guardar(Cliente cliente) 
        {
        if (!await Existe(cliente))
            return await Insertar(Cliente);
        else 
            return await Modificar(cliente);
        }

    private async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }






}

