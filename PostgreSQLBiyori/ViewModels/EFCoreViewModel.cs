using PostgreSQLBiyori.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQLBiyori.ViewModels
{
    public class EFCoreViewModel:ViewModelBase
    {
        private readonly AppConfig _config;
        public EFCoreViewModel(AppConfig config) : base()
        {
            _config = config;
        }
    }
}
