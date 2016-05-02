using HooksPOC.Lib.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Lib
{
    interface IModule
    { 
        void SetState(ref StateObj state); 
    }
}
