using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChartDetailsManager : IChartDetailsManager
    {
        private IChartDetailsDB ChartDetailsDB { get; }

        
        public ChartDetailsManager(IChartDetailsDB ChartDetailsDB)
        {
            this.ChartDetailsDB = ChartDetailsDB; 
        }
    
        public List<ChartDetails> GetAllChartDetailsFromLogin(int IdLogin)
        {
           return  ChartDetailsDB.GetAllChartDetailsFromLogin(IdLogin); 
        }

        public void AddNewChartDetails(ChartDetails myChartDetails)
        {
            ChartDetailsDB.AddNewChartDetails(myChartDetails); 
        }

        public void DeleteOneEntry(int IdProdcut)
        {
            ChartDetailsDB.DeleteOneEntry(IdProdcut); 
        }

        public void DeleteAllEntryByLogin(int IdLogin)
        {
            ChartDetailsDB.DeleteAllEntryByLogin(IdLogin); 
        }
    }

}
