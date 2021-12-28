using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IChartDetailsManager
    {
        List<ChartDetails> GetAllChartDetailsFromLogin(int IdLogin);

        void AddNewChartDetails(ChartDetails myChartDetails);

        void DeleteOneEntry(int IdProdcut);

        void DeleteAllEntryByLogin(int IdLogin);
    }
}
