using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albedo_Surface_Ops.Commands
{
    public interface IUnitCommand
    {
        public void Execute();
        bool IsComplete();
    }
}
