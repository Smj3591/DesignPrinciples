using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple
{
    /*
     * The Interface Segregation Principle states "that clients should not be forced to implement interfaces they don't use. 
     * Instead of one fat interface, many small interfaces are preferred based on groups of methods, each one serving one submodule.".
     */

    /*****************************************Without ISP********************************************************************************************/

    public interface ILead
    {  
        void CreateSubTask();  
        void AssignTask();  
        void WorkOnTask();
    }
    public class TeamLead : ILead
    {
        public void AssignTask()
        {
            //Code to assign a task.  
        }
        public void CreateSubTask()
        {
            //Code to create a sub task  
        }
        public void WorkOnTask()
        {
            //Code to implement perform assigned task.  
        }
    }

    public class Manager : ILead
    {
        public void AssignTask()
        {
            //Code to assign a task.  
        }
        public void CreateSubTask()
        {
            //Code to create a sub task.  
        }
        public void WorkOnTask() // *************** This is unnecessary for manager 
        {
            throw new Exception("Manager can't work on Task");
        }
    }

    /*****************************************With ISP********************************************************************************************/

    public interface IProgrammer
    {
        void WorkOnTask();
    }
    public interface ILeadISP
    {
        void AssignTask();
        void CreateSubTask();
    }

    public class Programmer : IProgrammer
    {
        public void WorkOnTask()
        {
            //code to implement to work on the Task.  
        }
    }
    public class ManagerLSP : ILeadISP
    {
        public void AssignTask()
        {
            //Code to assign a Task  
        }
        public void CreateSubTask()
        {
            //Code to create a sub taks from a task.  
        }
    }

    public class TeamLeadISP : IProgrammer, ILeadISP
    {
        public void AssignTask()
        {
            //Code to assign a Task  
        }
        public void CreateSubTask()
        {
            //Code to create a sub task from a task.  
        }
        public void WorkOnTask()
        {
            //code to implement to work on the Task.  
        }
    }
}
