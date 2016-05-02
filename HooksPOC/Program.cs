using HooksPOC.Lib.BaseTypes;
using HooksPOC.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HooksPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var registeredModules = new RegisteredModules();
            registeredModules.Modules.Add("ModuleA", new RegisteredModule()
            {
                Name = "Awesome Module A", 
                ClassName = "ModuleA", 
                Methods = new List<string>{ "DoSomething", "DoSomethingElse" }
            });
            registeredModules.Modules.Add("ModuleB", new RegisteredModule()
            {
                Name = "Awesome Module B",
                ClassName = "ModuleA",
                Methods = new List<string> { "DoSomething" }
            });
            registeredModules.Modules.Add("ModuleC", new RegisteredModule()
            {
                Name = "Awesome Module C",
                ClassName = "ModuleA",
                Methods = new List<string> { "DoSomething" }
            });
            registeredModules.Modules.Add("ModuleD", new RegisteredModule()
            {
                Name = "Awesome Module D",
                ClassName = "ModuleA",
                Methods = new List<string> { "DoSomething" }
            });

            var steps = new List<Step>();


            steps.Add(new Step()
            {
                StepClass = "ModuleA",
                StepMethod = "DoSomething"
            });

            steps.Add(new Step()
            {
                StepClass = "ModuleA",
                StepMethod = "DoSomethingElse"
            });

            steps.Add(new Step()
            {
                StepClass = "ModuleB",
                StepMethod = "DoSomething"
            });

            steps.Add(new Step()
            {
                StepClass = "ModuleC",
                StepMethod = "DoSomething"
            });

            steps.Add(new Step()
            {
                StepClass = "ModuleD",
                StepMethod = "DoSomething"
            });

            var stepManager = new StepManager();
            stepManager.SetRegisteredModules(registeredModules);
            stepManager.SetSteps(steps);
            Console.WriteLine("Running " + stepManager.GetState().TotalSteps + " steps"); 
            stepManager.DoAllSteps();
            stepManager.Finish();

            if (stepManager.GetState().IsPanicked)
            {
                Console.WriteLine("Failed on step {0}", stepManager.GetState().PanickedOnIndex); 
            }

            foreach(var stepState in stepManager.GetState().Steps)
            {
                Console.WriteLine("Step #{0} took {1} with {2} notices, {3} warnings and {4} errors.", (stepState.Index + 1), stepState.TotalTime, stepState.Notices.Count(), stepState.Warnings.Count(), stepState.Errors.Count()); 
            }

            Console.WriteLine("Ran {0} of {1} steps in {2} seconds", stepManager.GetState().TotalCompletedSteps, stepManager.GetState().TotalSteps, stepManager.GetState().TotalTime);

            var theState = stepManager.GetState();

            var serializer = new JavaScriptSerializer();
            var serializedState = serializer.Serialize(theState);

            Console.WriteLine(serializedState); 

            Console.ReadLine(); 
        }
    }
}
