using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Lib.BaseTypes
{
    public class RegisteredModule
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public List<string> Methods { get; set; }
    }
}
