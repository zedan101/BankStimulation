using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation.Model
{
    public class BankEmployee
    {
        public string EmpId;
        string _password;
        public string EmpName;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
