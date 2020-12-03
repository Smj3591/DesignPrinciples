using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple
{
    /*
     * The Liskov Substitution Principle (LSP) states that "you should be able to use any derived class 
     * instead of a parent class and have it behave in the same manner without modification". 
     * It ensures that a derived class does not affect the behavior of the parent class, in other words, 
     * that a derived class must be substitutable for its base class.
     */

    public class SqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
        public string LoadText()
        {
            /* Code to read text from sql file */
            return "";
        }
        public void SaveText()
        {
            /* Code to save text into sql file */
        }
    }

    public class SqlFileManager
    {
        public List<SqlFile> lstSqlFiles { get; set; }

        public string GetTextFromFiles()
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in lstSqlFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }
        public void SaveTextIntoFiles()
        {
            foreach (var objFile in lstSqlFiles)
            {
                objFile.SaveText();
            }
        }
    }


    /***********************************************************************************************************/

    public class SqlFile1
    {
        public virtual string LoadText()
        {
            /* Code to read text from sql file */
            return "";
        }
        public virtual void SaveText()
        {
            /* Code to save text into sql file */
        }
    }
    public class ReadOnlySqlFile : SqlFile1
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
        public override string LoadText()
        {
            /* Code to read text from sql file */
            return "";
        }
        public override void SaveText()
        {
            /* Throw an exception when app flow tries to do save. */
            throw new IOException("Can't Save");
        }
    }

    public class SqlFileManager1
    {
        public List<SqlFile1> lstSqlFiles { get; set; }

        public string GetTextFromFiles()
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in lstSqlFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }
        public void SaveTextIntoFiles()
        {
            foreach (SqlFile1 objFile1 in lstSqlFiles)
            {
                //Check whether the current file object is read-only or not.If yes, skip calling it's  
                // SaveText() method to skip the exception.  

                if (objFile1 is ReadOnlySqlFile)
                    return;
                objFile1.SaveText();
            }
        }
    }

    /***********************************************************************************************************/

    public interface IReadableSqlFile
    {
        string LoadText();
    }
    public interface IWritableSqlFile
    {
        void SaveText();
    }

    public class ReadOnlySqlFileLSP : IReadableSqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
        public string LoadText()
        {
            /* Code to read text from sql file */
            return "";
        }
    }

    public class SqlFileLSP : IWritableSqlFile, IReadableSqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
        public string LoadText()
        {
            /* Code to read text from sql file */
            return "";
        }
        public void SaveText()
        {
            /* Code to save text into sql file */
        }
    }

    public class SqlFileManagerLSP
    {
        public string GetTextFromFiles(List<IReadableSqlFile> aLstReadableFiles)
        {
            StringBuilder objStrBuilder = new StringBuilder();
            foreach (var objFile in aLstReadableFiles)
            {
                objStrBuilder.Append(objFile.LoadText());
            }
            return objStrBuilder.ToString();
        }
        public void SaveTextIntoFiles(List<IWritableSqlFile> aLstWritableFiles)
        {
            foreach (var objFile in aLstWritableFiles)
            {
                objFile.SaveText();
            }
        }
    }
}


