using HooksPOC.Lib;
using HooksPOC.Lib.BaseTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC
{

    public class RegisteredModuleNotRegisteredException : Exception
    {
    }
    public class RegisteredModuleConfigMissingException: Exception
    {
    }
    public class RegisteredModuleMethodNotRegisteredException: Exception
    {
    }
    public class StepManagerNoStepsException : Exception
    {
    }

    public class StepManager
    {
        private RegisteredModules _registeredModules;
        private StateObj _state;
        private Stopwatch _timer { get; set; }

        public StepManager()
        {
            _state = new StateObj();
            _state.CurrentStepIndex = 0;
            _timer = new Stopwatch(); 
            _timer.Start();
            _state.DateCreated = DateTime.Now;
            _state.Steps = new List<StepState>(); 
        }

        public void SetRegisteredModules(RegisteredModules registeredModules)
        {
            _registeredModules = registeredModules;
            _state.AvailableModules = _registeredModules.Modules.Count(); 
        }

        private List<Step> _steps; 

        public void SetSteps(List<Step> steps)
        {
            _steps = steps;
            _state.TotalSteps = _steps.Count();

            var index = 0; 
            // Initialize the step states 
            foreach (var step in _steps)
            {
                _state.Steps.Add(new StepState()
                {
                    Index = index,
                    Step = step
                });
                index++; 
            }
        }
        
        public void DoStep(Step step)
        {
            // Can't run if everybody's FREAKING OUT
            if (_state.IsPanicked == true)
            {
                return;
            }

            var moduleManager = new ModuleManager(); 
            if(_registeredModules == null)
            {
                throw new RegisteredModuleConfigMissingException(); 
            }

            if(!_registeredModules.Modules.ContainsKey(step.StepClass))
            {
                throw new RegisteredModuleNotRegisteredException(); 
            }

            if(!_registeredModules.Modules[step.StepClass].Methods.Contains(step.StepMethod))
            {
                throw new RegisteredModuleMethodNotRegisteredException(); 
            }

            var stepTimer = new Stopwatch();
            var stepClassInstance = (IModule)moduleManager.GetInstance(step.StepClass);

            _state.CurrentStepIndex++;
            _state.TotalCompletedSteps++; 

            Console.WriteLine("Running step {0} of {1}", _state.CurrentStepIndex, _state.TotalSteps);

            _state.CurrentStep = _state.Steps[_state.CurrentStepIndex - 1]; 

            stepClassInstance.SetState(ref _state);
            stepTimer.Start(); 
            moduleManager.RunModuleMethod<IModule>(stepClassInstance, step.StepMethod);
            stepTimer.Stop(); 
            _state.CurrentStep.TotalTime = stepTimer.Elapsed.TotalSeconds; 
        }

        public void DoAllSteps()
        {
            if(_steps == null || !_steps.Any())
            {
                throw new StepManagerNoStepsException(); 
            }

            foreach(var step in _steps)
            {
                // Check if we're panicking!
                DoStep(step); 
            }
        }

        public void Finish()
        {
            _timer.Stop();
            _state.TotalTime = _timer.Elapsed.TotalSeconds;             
        }

        public StateObj GetState()
        {
            return _state; 
        }
    }
}
