using HooksPOC.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Modules
{
    class ModuleA : ModuleBase
    {
        public void DoSomething()
        {
            _state.CurrentStep.Notice("This is a test notice!"); 
            _state.CurrentStep.Warn("This is a test warning!"); 
            _state.SetVal("foo", "bar"); 
        }

        public void DoSomethingElse()
        {
            _state.CurrentStep.Error("This is a test error.");
            //_state.Panic("And NOW WE'RE PANICKING!"); 
            Console.WriteLine("Foo is {0}", _state.GetVal("foo"));
        }
    }
}
