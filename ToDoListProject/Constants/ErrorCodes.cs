using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListProject.Constants
{
    public class ErrorCodes
    {
        #region Constants

        static public readonly int FAILED_TO_SAVE_DATA = 404;
        static public readonly string FAIL_NAME = "FAIL !";
        static public readonly string FAILED_TO_SELECT_TASK = "Select Task First";
        static public readonly string FAILED_TASK_NAME_EMPTY = "Task Name Cannot Be Blank";

        #endregion
    }
}
