using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListProject.Constants
{
    public class MongoDB
    {
        #region Constants

        public readonly static string CONNECTION_STRING = "mongodb://localhost:27017";
        public readonly static string DATA_BASE_NAME = "ToDoListDatabase";
        public readonly static string TASK_COLLECTION_NAME = "TaskCollection";

        #endregion
    }
}
