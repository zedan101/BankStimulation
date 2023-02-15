using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation.Model
{
    public class Users
    {
        public string UserName;
        public string UserId;
        private string _password;
        public string BankId;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
