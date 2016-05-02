using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Lib.BaseTypes
{
    public class RegisteredModules
    {
        public Dictionary<string, RegisteredModule> Modules { get; set; }

        public RegisteredModules()
        {
            Modules = new Dictionary<string, RegisteredModule>();
        }
    }
}
