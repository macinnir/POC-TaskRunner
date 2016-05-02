using HooksPOC.Lib.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC
{
    // 1. Module is not registered
    // 2. Module method is not registered
    // 3. Module is registered but not found 
    // 4. Module method is registered but not found 

    public class RegisteredModuleNotFoundException : Exception
    {

    }

    public class RegisteredModuleMethodNotFoundException : Exception
    {

    }

    public class ModuleManager
    {
        private Assembly _assembly { get; set; }
        public void SetAssembly(ref Assembly assembly)
        {
            _assembly = assembly; 
        }

        /**
         * Get the assembly that contains the code that is currently executing
         */
        public Assembly GetAssembly()
        {
            if(_assembly == null)
            {
                return Assembly.GetExecutingAssembly(); 
            }

            return _assembly; 
        }

        public object GetInstance(string className)
        {
            var type = GetAssembly().GetTypes().First(x => x.Name == className);
            return Activator.CreateInstance(type); 
        }

        public void RunModuleMethod<T>(T module, string methodName)
        {
            // Get the class instance
            var type = module.GetType();
            var methodInfo = type.GetMethod(methodName);
            var result = methodInfo.Invoke(module, null);
        }
    }
}
