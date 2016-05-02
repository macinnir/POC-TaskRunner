using HooksPOC.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Modules
{
    class ModuleC : ModuleBase
    {
        public void DoSomething()
        {
            Console.WriteLine("ModuleC::DoSomething()");
            _state.Panic("Something went horribly wrong with ModuleC!!!"); 
        }
    }
}
