using Data;
using LinqToDB;
using LinqToDB.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DiariodeSaude.ViewsModels;

public class SonoLinq
{
    public async Task<int> UltimoIdAsync()
    {
        using var db = new BD();
        return await db.QualidadeSono.Select(r => r.Id).DefaultIfEmpty(0).MaxAsync();
    }

    public async Task<List<QualidadeSono>> ObterQualidadeSonoAsync()
    {
        using var db = new BD();
        var registros = await db.GetTable<QualidadeSono>().ToListAsync();
            // Verifique se os dados foram recuperados
            Console.WriteLine($"Registros encontrados: {registros.Count}");
        return registros;
    }


    public async Task<int> AdicionarSonoAsync(QualidadeSono sono)
    {
        using var db = new BD();
        var maxId = await db.QualidadeSono.Select(s => (int?)s.Id).MaxAsync() ?? 0;
        sono.Id = maxId + 1;
        await db.InsertAsync(sono);
        return sono.Id;
    }

}

