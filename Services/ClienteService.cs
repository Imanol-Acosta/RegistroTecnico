using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class ClienteService
{
    private readonly IDbContextFactory<Contexto> dbFactory;

    public ClienteService(IDbContextFactory<Contexto> dbFactory)
    {
        this.dbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Cliente cliente)
    {
        if (!await Existe(cliente.ClienteId))
            return await Insertar(cliente);
        else
            return await Modificar(cliente);
    }

    private async Task<bool> Existe(int clienteId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AnyAsync(c => c.ClienteId == clienteId);
    }

    private async Task<bool> Insertar(Cliente cliente)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Clientes.Add(cliente);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Cliente cliente)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Entry(cliente).State = EntityState.Modified;
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        var eliminado = await contexto.Clientes
            .Where(c => c.ClienteId == id).ExecuteDeleteAsync();
        return eliminado > 0;
    }

    public async Task<Cliente?> Buscar(int id)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClienteId == id);
    }

    public async Task<Cliente?> BuscarNombres(string nombre)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nombres == nombre);
    }

    public async Task<Cliente?> BuscarRNC(string rnc)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Rnc == rnc);
    }

    public async Task<List<Cliente>> Listar(Expression<Func<Cliente, bool>> criterio)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Clientes.AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public Cliente Limpiar()
    {
        return new Cliente();
    }

    public async Task<List<Tecnico>> ObtenerTecnicos()
    {
        return await context.Tecnicos.ToListAsync();
    }




}
