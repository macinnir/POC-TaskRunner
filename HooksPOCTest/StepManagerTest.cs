using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HooksPOC;
using HooksPOC.Lib.BaseTypes;
using System.Collections.Generic;

namespace HooksPOCTest
{
    [TestClass]
    public class StepManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(RegisteredModuleConfigMissingException))]
        public void StepManagerDoStepThrowsRegisteredModuleConfigMissingException()
        {
            var step = new Step()
            {
                StepClass = "ModuleA",
                StepMethod = "DoSomethod"
            };

            var stepManager = new StepManager();
            stepManager.DoStep(step); 
        }
        [TestMethod]
        [ExpectedException(typeof(RegisteredModuleNotRegisteredException))]
        public void StepManagerDoStepThrowsRegisteredModuleNotFoundException()
        {
            var stepManager = new StepManager();
            var registeredModules = new RegisteredModules();
            registeredModules.Modules.Add("ModuleA", new RegisteredModule()
            {
                Name = "Awesome Module A",
                ClassName = "ModuleA",
                Methods = new List<string> { "DoSomething" }
            });
            stepManager.SetRegisteredModules(registeredModules);
            var step = new Step()
            {
                StepClass = "Foo",
                StepMethod = "Bar"
            };
            stepManager.DoStep(step);    
        }

        [TestMethod]
        [ExpectedException(typeof(RegisteredModuleMethodNotRegisteredException))]
        public void StepManagerDoStepThrowsRegisteredModuleMethodNotFoundException()
        {
            var stepManager = new StepManager();
            var registeredModules = new RegisteredModules();
            registeredModules.Modules.Add("ModuleA", new RegisteredModule()
            {
                Name = "Awesome Module A",
                ClassName = "ModuleA",
                Methods = new List<string> { "DoSomething" }
            });

            stepManager.SetRegisteredModules(registeredModules);

            var step = new Step()
            {
                StepClass = "ModuleA", // Correct Module name 
                StepMethod = "BadMethodName"      // Bad Method name 
            };

            stepManager.DoStep(step);
        }

        [TestMethod]
        [ExpectedException(typeof(StepManagerNoStepsException))]
        public void RunAllStepsThrowsStepManagerNoStepsException()
        {
            var stepManager = new StepManager();
            stepManager.DoAllSteps(); 
        }
    }
}
