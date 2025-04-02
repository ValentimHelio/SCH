using Microsoft.EntityFrameworkCore;
using SCH.Context;
using SCH.Models;
using SCH.ViewModels;

namespace SCH.Services;
public class RelatorioServices
{
    private readonly AppDbContext _context;

    public RelatorioServices(AppDbContext context)
    {
        _context = context;
    }

    public List<Movimento> RelDemonstrativos(Cliente cliente,DateTime? mimDate, DateTime? maxDate)
    {
        var result = _context.Movimentos.AsQueryable();

        if (mimDate.HasValue)
        {
            result = result.Where(x => x.Data >= mimDate.Value);
        }

        if (maxDate.HasValue)
        {
            result = result.Where(x => x.Data <= maxDate.Value);
        }

        if (cliente is not null)
        {
            result = result.Where(x => x.ClienteId == cliente.ClienteId);
        }

        return result.Include(x => x.servico).Include(y => y.cliente).OrderBy(d => d.Data).ToList();
    }

    public async Task<List<RelatorioDemonstrativoViewModel>> ObterRelatorioDemonstrativo(DateTime dataInicio, DateTime dataFim)
    {
        var query = @"
            SELECT 
                C.NomeCliente,
                M1.Valor_Hora AS 'GPS_INSS',
                M2.Valor_Hora AS 'GPS_Autonomo',
                M3.Valor_Hora AS 'Domestica',
                M4.Valor_Hora AS 'FGTS',
                M5.Valor_Hora AS 'CONTR_CONF',
                M6.Valor_Hora AS 'CNA_CCIR_SEG',
                M7.Valor_Hora AS 'SST_ESOCIAL',
                M8.Valor_Hora AS 'IRPF',
                M9.Valor_Hora AS 'GTA_ISSQN_CA',
                M10.Valor_Hora AS 'XEROX_FIRMA',
                M11.Valor_Hora AS 'ITR_PIS_FUNR',
                M12.Valor_Hora AS 'MENSALIDADE',

                COALESCE(M1.Valor_Hora, 0) + COALESCE(M2.Valor_Hora, 0) + 
                COALESCE(M3.Valor_Hora, 0) + COALESCE(M4.Valor_Hora, 0) + 
                COALESCE(M5.Valor_Hora, 0) + COALESCE(M6.Valor_Hora, 0) + 
                COALESCE(M7.Valor_Hora, 0) + COALESCE(M8.Valor_Hora, 0) + 
                COALESCE(M9.Valor_Hora, 0) + COALESCE(M10.Valor_Hora, 0) + 
                COALESCE(M11.Valor_Hora, 0) + COALESCE(M12.Valor_Hora, 0) AS 'Total'

            FROM CLIENTE C
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 8 
                AND Data BETWEEN {0} AND {1}
            ) M1
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 9 
                AND Data BETWEEN {0} AND {1}
            ) M2
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 10 
                AND Data BETWEEN {0} AND {1}
            ) M3
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 11 
                AND Data BETWEEN {0} AND {1}
            ) M4
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 12 
                AND Data BETWEEN {0} AND {1}
            ) M5
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 13 
                AND Data BETWEEN {0} AND {1}
            ) M6
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 14 
                AND Data BETWEEN {0} AND {1}
            ) M7
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 15 
                AND Data BETWEEN {0} AND {1}
            ) M8
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 16 
                AND Data BETWEEN {0} AND {1}
            ) M9
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 17 
                AND Data BETWEEN {0} AND {1}
            ) M10
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 18 
                AND Data BETWEEN {0} AND {1}
            ) M11
            OUTER APPLY (
                SELECT Sum(Valor_Hora) Valor_Hora FROM MOVIMENTO
                WHERE ClienteId = C.ClienteId AND ServicoId = 19 
                AND Data BETWEEN {0} AND {1}
            ) M12
            WHERE 
                COALESCE(M1.Valor_Hora, M2.Valor_Hora, M3.Valor_Hora, M4.Valor_Hora, 
                    M5.Valor_Hora, M6.Valor_Hora, M7.Valor_Hora, M8.Valor_Hora, 
                    M9.Valor_Hora, M10.Valor_Hora, M11.Valor_Hora, M12.Valor_Hora) IS NOT NULL;
        ";

        return await _context.RelatorioDemonstrativo
            .FromSqlRaw(query, dataInicio, dataFim)
            .ToListAsync();
    }

}
