using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickUtils.Utils
{
    class TaskTools
    {

        public static List<Task> GetRunningTasks (List<Task> taskList)
        {
            List<Task> runningTasks = new List<Task>();

            foreach (Task task in new List<Task>(taskList))
            {
                if (!task.IsCompleted)
                    runningTasks.Add(task);
            }

            return runningTasks;
        }

        public static List<Task> GetCompletedTasks(List<Task> taskList)
        {
            List<Task> completedTasks = new List<Task>();

            foreach (Task task in new List<Task>(taskList))
            {
                if (task.IsCompleted)
                    completedTasks.Add(task);
            }

            return completedTasks;
        }

        public static List<Task> RemoveCompletedTasks(List<Task> taskList, ref int doneAmount)
        {
            foreach (Task task in new List<Task>(taskList))
            {
                if (task.IsCompleted)
                {
                    doneAmount++;
                    taskList.Remove(task);
                }
            }

            return taskList;
        }
    }
}
