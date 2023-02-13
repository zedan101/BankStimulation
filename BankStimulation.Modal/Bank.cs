using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation.Model
{
    public class Bank
    {
       public string BankId;
       public double SameRtgsCharges;
       public double SameImpsCharges;
       public double OtherRtgsCharges;
       public double OtherImpsCharges;
       public string DefaultCurrency;
       public string CurrencyExchangeRates;

    }
}
