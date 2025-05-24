using Microsoft.EntityFrameworkCore;
using RegistroTecnico.DAL;
using RegistroTecnico.Models;
using System.Linq.Expressions;

namespace RegistroTecnico.Services;

public class TecnicoService(IDbContextFactory<Contexto> dbFactory)
{
    public async Task<bool> Guardar(Tecnico tecnicos)
    {
        if (!await Existe(tecnicos.TecnicoId)) 
            return await Insertar(tecnicos);
        else
            return await Modificar(tecnicos);
    }

    private async Task<bool> Existe(int tecnicoId) 
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId); 
    }

    private async Task<bool> Insertar(Tecnico tecnicos)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnico tecnicos)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        contexto.Update(tecnicos);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Tecnico?> Buscar(string nombre)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .FirstOrDefaultAsync(t => t.Nombres.ToLower().Contains(nombre.ToLower()));
    }

    public async Task<Tecnico?> BuscarPorId(int tecnicoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<bool> Eliminar(int tecnicoId)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking()
            .Where(t => t.TecnicoId == tecnicoId) 
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tecnico>> Listar(Expression<Func<Tecnico, bool>> filtro)
    {
        await using var contexto = await dbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos.AsNoTracking()
            .Where(filtro)
            .ToListAsync();
    }

    public Tecnico Limpiar()
    {
        return new Tecnico();
    }
}