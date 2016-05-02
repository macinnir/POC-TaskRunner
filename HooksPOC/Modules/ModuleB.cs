using HooksPOC.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Modules
{
    class ModuleB : ModuleBase
    {
        public void DoSomething()
        {
            Console.WriteLine("ModuleB::DoSomething()");
        }
    }
}
