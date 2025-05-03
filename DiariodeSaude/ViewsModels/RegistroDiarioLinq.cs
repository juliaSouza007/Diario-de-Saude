using Data;
using LinqToDB;
using LinqToDB.Async;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DiariodeSaude.ViewsModels;

public class RegistroDiarioLinq
{
    public async Task<int> AdicionarRegistroDiarioAsync(RegistroDiario registro)
    {
        using var db = new BD();
        var maxId = await db.RegistroDiario.Select(r => (int?)r.Id).MaxAsync() ?? 0;
        registro.Id = maxId + 1;
        await db.InsertAsync(registro);
        return registro.Id;
    }

    public async Task<List<RegistroDiario>> ObterRegistrosAsync()
    {
        using var db = new BD();
        return await db.RegistroDiario.ToListAsync();
    }

    //-------JULINHAAAAAAA-----------------------------------
    public async Task<List<RegistroCompletoDTO>> ObterRegistrosCompletosAsync()
    {
        using var db = new BD();

        var query = from r in db.RegistroDiario
                    join h in db.Humor on r.HumorId equals h.Id
                    join s in db.QualidadeSono on r.SonoId equals s.Id
                    join a in db.Alimentacao on r.AlimentacaoId equals a.Id
                    join af in db.AtividadeFisica on r.AtividadeFisicaId equals af.Id
                    orderby r.Data descending
                    select new RegistroCompletoDTO
                    {
                        RegistroId = r.Id,
                        Data = r.Data,
                        Humor = h.Descricao ?? "",
                        Sono = s.Descricao,
                        Alimentacao = a.Descricao ?? "",
                        Atividade = af.TipoAtividade ?? "",
                        Tempo = af.DuracaoMinutos
                    };
        return await query.ToListAsync();
    }

}

