using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IChartDetailsDB
    {

        List<ChartDetails> GetAllChartDetailsFromLogin(int Idlogin);
        void AddNewChartDetails(ChartDetails myChartDetails);

        void DeleteOneEntry(int IdProduct);

        void DeleteAllEntryByLogin(int Idlogin); 
    }
}
