using HooksPOC.Lib.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Lib
{
    class ModuleBase : IModule
    {
        protected StateObj _state { get; set; }
        public void SetState(ref StateObj state)
        {
            _state = state; 
        }
    }
}
