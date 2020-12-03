using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple
{
    /*
     * The Dependency Inversion Principle (DIP) states that high-level modules/classes should not depend on low-level modules/classes. 
     * Both should depend upon abstractions. Secondly, abstractions should not depend upon details. 
     * Details should depend upon abstractions
     */

    /*****************************************Without DIP* ITERRATION 1*******************************************************************************************/

    //Suppose we need to work on an error logging module that logs exception stack traces into a file.Simple, 
    //isn't it? The following are the classes that provide the functionality to log a stack trace into a file. 

    #region "ITERATION 1"

    
    public class FileLogger
    {
        public void LogMessage(string aStackTrace)
        {
            //code to log stack trace into a file.  
        }
    }
    public class ExceptionLogger
    {
        public void LogIntoFile(Exception aException)
        {
            FileLogger objFileLogger = new FileLogger();
            objFileLogger.LogMessage(GetUserReadableMessage(aException));
        }
        private string GetUserReadableMessage(Exception ex)
        {
            string strMessage = string.Empty;  
            //code to convert Exception's stack trace and message to user readable format.  
            return strMessage;
        }
    }
    //A client class exports data from many files to a database.
    public class DataExporter
    {
        public void ExportDataFromFile()
        {
            try
            {
                //code to export data from files to database.  
            }
            catch (Exception ex)
            {
                new ExceptionLogger().LogIntoFile(ex);
            }
        }
    }

    #endregion

    /*****************************************Without DIP* ITERRATION 2*******************************************************************************************/
    //Looks good.We sent our application to the client. 
    //But our client wants to store this stack trace in a database if an IO exception occurs. Hmm...okay, no problem. 
    //We can implement that too.Here we need to add one more class that provides the functionality to log the stack trace into the database 
    //and an extra method in ExceptionLogger to interact with our new class to log the stack trace.

    #region "ITERATION 2"

   
    public class DbLogger
    {
        public void LogMessage(string aMessage)
        {
            //Code to write message in database.  
        }
    }
    public class FileLogger1
    {
        public void LogMessage(string aStackTrace)
        {
            //code to log stack trace into a file.  
        }
    }
    public class ExceptionLogger1
    {
        public void LogIntoFile(Exception aException)
        {
            FileLogger objFileLogger = new FileLogger();
            objFileLogger.LogMessage(GetUserReadableMessage(aException));
        }
        public void LogIntoDataBase(Exception aException)
        {
            DbLogger objDbLogger = new DbLogger();
            objDbLogger.LogMessage(GetUserReadableMessage(aException));
        }
        private string GetUserReadableMessage(Exception ex)
        {
            string strMessage = string.Empty;  
            //code to convert Exception's stack trace and message to user readable format.  
            return strMessage;
        }
    }
    public class DataExporter1   
    {
        public void ExportDataFromFile()
        {
            try
            {
                //code to export data from files to database.  
            }
            catch (IOException ex)
            {
                new ExceptionLogger1().LogIntoDataBase(ex);
            }
            catch (Exception ex)
            {
                new ExceptionLogger().LogIntoFile(ex);
            }
        }
    }

    #endregion

    /*****************************************With DIP* ITERRATION 3*******************************************************************************************/
    //Looks fine for now.But whenever the client wants to introduce a new logger, 
    //we need to alter ExceptionLogger by adding a new method.
    //If we continue doing this after some time then we will see a fat ExceptionLogger class 
    //with a large set of methods that provide the functionality to log a message into various targets.
    //Why does this issue occur? Because ExceptionLogger directly contacts the low-level classes FileLogger and DbLogger to log the exception.
    //We need to alter the design so that this ExceptionLogger class can be loosely coupled with those classes.
    //To do that we need to introduce an abstraction between them so that 
    //ExcetpionLogger can contact the abstraction to log the exception 
    //instead of depending on the low-level classes directly.

    #region "ITERATION 3"

    

    public interface ILogger
    {
        void LogMessage(string aString);
    }

    public class DbLogger2 : ILogger
    {
        public void LogMessage(string aMessage)
        {
            //Code to write message in database.  
        }
    }
    public class FileLogger2 : ILogger
    {
        public void LogMessage(string aStackTrace)
        {
            //code to log stack trace into a file.  
        }
    }

    //Now, we move to the low-level class's initiation from the ExcetpionLogger class to the DataExporter class 
    //to make ExceptionLogger loosely coupled with the low-level classes FileLogger and EventLogger. 
    //And by doing that we are giving provision to DataExporter class to decide what kind of Logger should be called based on the exception that occurs.

    public class ExceptionLogger2
    {
        private ILogger _logger;
        public ExceptionLogger2(ILogger aLogger)
        {
            this._logger = aLogger;
        }
        public void LogException(Exception aException)
        {
            string strMessage = GetUserReadableMessage(aException);
            this._logger.LogMessage(strMessage);
        }
        private string GetUserReadableMessage(Exception aException)
        {
            string strMessage = string.Empty;  
            return strMessage;
        }
    }
    public class DataExporter2
    {
        public void ExportDataFromFile()
        {
            ExceptionLogger2 _exceptionLogger;
            try
            {
                //code to export data from files to database.  
            }
            catch (IOException ex)
            {
                _exceptionLogger = new ExceptionLogger2(new DbLogger2());
                _exceptionLogger.LogException(ex);
            }
            catch (Exception ex)
            {
                _exceptionLogger = new ExceptionLogger2(new FileLogger2());
                _exceptionLogger.LogException(ex);
            }
        }
    }

    //We successfully removed the dependency on low-level classes.This ExceptionLogger doesn't depend 
    //on the FileLogger and EventLogger classes to log the stack trace. 
    //We don't need to change the ExceptionLogger's code anymore for any new logging functionality. 
    //We need to create a new logging class that implements the ILogger interface and 
    //must add another catch block to the DataExporter class's ExportDataFromFile method.

    public class EventLogger : ILogger
    {
        public void LogMessage(string aMessage)
        {
            //Code to write message in system's event viewer.  
        }
    }

    public class DataExporter3
    {
        public void ExportDataFromFile()
        {
            ExceptionLogger2 _exceptionLogger;
            try
            {
                //code to export data from files to database.  
            }
            catch (IOException ex)
            {
                _exceptionLogger = new ExceptionLogger2(new DbLogger2());
                _exceptionLogger.LogException(ex);
            }
            catch (SqlException ex)
            {
                _exceptionLogger = new ExceptionLogger2(new EventLogger());
                _exceptionLogger.LogException(ex);
            }
            catch (Exception ex)
            {
                _exceptionLogger = new ExceptionLogger2(new FileLogger2());
                _exceptionLogger.LogException(ex);
            }
        }
    }
    #endregion

    //There are 3 types to DI, Constructor injection, Property injection and method injection

    //Example

    #region "TYPES/WAYS OF INJECTION"

    

    public interface IMessenger
    {
        void SendMessage();
    }
    public class Email : IMessenger
    {
        public void SendMessage()
        {
            // code to send email
        }
    }

    public class SMS : IMessenger
    {
        public void SendMessage()
        {
            // code to send SMS
        }
    }
    public class Notification
    {
        private IMessenger _iMessenger;
        public Notification()
        {
            _iMessenger = new Email();
        }
        public void DoNotify()
        {
            _iMessenger.SendMessage();
        }
    }


    public class Notification_ContructorInjection
    {
        private IMessenger _iMessenger;
        public Notification_ContructorInjection(IMessenger pMessenger)
        {
            _iMessenger = pMessenger;
        }
        public void DoNotify()
        {
            _iMessenger.SendMessage();
        }
    }

    public class Notification_PropertyInjection
    {
        private IMessenger _iMessenger;

        public Notification_PropertyInjection()
        {

        }
        public IMessenger MessageService
        {
            //private get;
            set
            {
                _iMessenger = value;
            }
        }

        public void DoNotify()
        {
            _iMessenger.SendMessage();
        }
    }

    public class Notification_MethodInjection
    {
        public void DoNotify(IMessenger pMessenger)
        {
            pMessenger.SendMessage();
        }
    }

    #endregion
}
