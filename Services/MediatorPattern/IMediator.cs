using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MediatorPattern
{
    public interface IMediator
    {
        void LogInformationAction(object sender, string ev);
    }
}
