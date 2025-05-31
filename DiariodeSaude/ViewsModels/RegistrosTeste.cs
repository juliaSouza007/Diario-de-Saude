using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using LinqToDB;

public class RegistrosTeste
{
    public static void InserirRegistros()
    {
        using var db = new BD();

        int idBase = 90;

        int humorId = Math.Max(idBase, db.Humor.Select(h => h.Id).DefaultIfEmpty(0).Max() + 1);
        int sonoId = Math.Max(idBase, db.QualidadeSono.Select(s => s.Id).DefaultIfEmpty(0).Max() + 1);
        int alimentacaoId = Math.Max(idBase, db.Alimentacao.Select(a => a.Id).DefaultIfEmpty(0).Max() + 1);
        int atividadeId = Math.Max(idBase, db.AtividadeFisica.Select(a => a.Id).DefaultIfEmpty(0).Max() + 1);
        int registroId = Math.Max(idBase, db.RegistroDiario.Select(r => r.Id).DefaultIfEmpty(0).Max() + 1);

        var registros = new List<(DateTime data, string humor, string sono, string alimentacao, string atividade, int tempo)>
        {
            (new DateTime(2024,12,1), "HumorBom", "Ruim", "iogurte whey", "corrida", 20),
            (new DateTime(2024,12,5), "HumorOtimo", "Bom", "almoço e janta", "academia", 120),
            (new DateTime(2024,12,10), "HumorBom", "Ruim", "biscoito, limonada", "andei", 15),
            (new DateTime(2024,12,15), "HumorRuim", "Média", "carne moida e rap10", "academia", 20),
            (new DateTime(2024,12,20), "HumorBom", "Média", "sopa de legumes", "corrida", 30),

            //mês passado
            (new DateTime(2025,4,2), "HumorOtimo", "Bom", "iogurte whey", "corrida", 20),
            (new DateTime(2025,4,7), "HumorRegular", "Ruim", "almoço, fruta e janta", "academia", 120),
            (new DateTime(2025,4,14), "HumorBom", "Média", "biscoito, limonada", "andei", 15),
            (new DateTime(2025,4,20), "HumorBom", "Ruim", "frango assado e coca 0", "academia", 20),
            (new DateTime(2025,4,28), "HumorRuim", "Bom", "sopa de legumes", "corrida", 30),

            //semana passada
            (new DateTime(2025,5,7), "HumorBom", "Ruim", "iogurte whey", "corrida", 20),
            (new DateTime(2025,5,6), "HumorRuim", "Media", "fruta e janta", "academia", 120),
            (new DateTime(2025,5,5), "HumorBom", "Bom", "biscoito, limonada", "andei", 15),
            (new DateTime(2025,5,4), "HumorOtimo", "Ruim", "frango e legumes", "academia", 20),
            (new DateTime(2025,5,3), "HumorRegular", "Media", "sopa de legumes", "corrida", 30),

            //ano passado
            (new DateTime(2023,12,1), "HumorBom", "Ruim", "carne e arroz", "corrida", 20),
            (new DateTime(2023,12,5), "HumorOtimo", "Bom", "almoço e janta", "academia", 90),
            (new DateTime(2023,12,10), "HumorBom", "Ruim", "biscoito", "andei", 15),
            (new DateTime(2023,12,15), "HumorRuim", "Média", "salada", "academia", 70),
            (new DateTime(2023,12,20), "HumorRuim", "Média", "sopa de legumes", "corrida", 30),
        };

        foreach (var (data, humor, sono, alimentacao, atividade, tempo) in registros)
        {
            var humorObj = new Humor { Id = humorId++, Descricao = humor };
            var sonoObj = new QualidadeSono { Id = sonoId++, Descricao = sono };
            var alimentacaoObj = new Alimentacao { Id = alimentacaoId++, Descricao = alimentacao };
            var atividadeObj = new AtividadeFisica { Id = atividadeId++, TipoAtividade = atividade, DuracaoMinutos = tempo };

            db.Insert(humorObj);
            db.Insert(sonoObj);
            db.Insert(alimentacaoObj);
            db.Insert(atividadeObj);

            var registro = new RegistroDiario
            {
                Id = registroId++,
                Data = data,
                HumorId = humorObj.Id,
                SonoId = sonoObj.Id,
                AlimentacaoId = alimentacaoObj.Id,
                AtividadeFisicaId = atividadeObj.Id
            };

            db.Insert(registro);
        }
    }
}
