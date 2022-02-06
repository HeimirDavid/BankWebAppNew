using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DTO
{
    public class SusTransactionsPerCountryDTO
    {
        public IEnumerable<SusSingleTransactionDTO> SusSingle { get; set; }
        public IEnumerable<SusManyTransactionsDTO> SusMany { get; set; }

    }
}
