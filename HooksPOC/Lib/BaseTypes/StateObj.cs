using System;
using System.Diagnostics;
using System.Threading; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Lib.BaseTypes
{
    public class StateObjValNotExistsException : Exception { }

    public class StateObj
    {
        public DateTime DateCreated { get; set; }
        public double TotalTime { get; set; } 
        public int CurrentStepIndex { get; set; }
        public int TotalSteps { get; set; }   
        public int AvailableModules { get; set; }
        public Dictionary<string, string> Vals { get; set; }
        public bool IsPanicked = false;
        public int PanickedOnIndex { get; set; }
        public int TotalCompletedSteps { get; set; }
        public StepState CurrentStep { get; set; }
        public void Panic(string reason = "")
        {
            IsPanicked = true;
            PanickedOnIndex = CurrentStepIndex;
            CurrentStep.Error(reason); 
        }
        public List<StepState> Steps { get; set; }

        public StateObj()
        {
            Vals = new Dictionary<string, string>(); 
        }    
        public void SetVal(string name, string val)
        {
            if(!Vals.ContainsKey(name))
            {
                Vals.Add(name, val); 
            }
            else
            {
                Vals[name] = val; 
            }
        }
        public string GetVal(string name)
        {
            if(!Vals.ContainsKey(name))
            {
                throw new StateObjValNotExistsException(); 
            }

            return Vals[name]; 
        }
    }
}
