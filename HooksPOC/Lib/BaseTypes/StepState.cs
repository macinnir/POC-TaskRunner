using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HooksPOC.Lib.BaseTypes
{
    public class StepState
    {
        public int Index { get; set; }
        public Step Step { get; set; }
        public double TotalTime { get; set; }
        public List<string> Notices { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }

        public StepState()
        {
            Notices = new List<string>();
            Warnings = new List<string>();
            Errors = new List<string>(); 
        }

        public void Notice(string notice)
        {
            Notices.Add(notice); 
        }

        public void Warn(string warning)
        {
            Warnings.Add(warning); 
        }

        public void Error(string error)
        {
            Errors.Add(error); 
        }
    }
}
