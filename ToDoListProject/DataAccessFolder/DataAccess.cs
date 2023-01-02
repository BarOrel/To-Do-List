using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ToDoListProject.Models;

namespace ToDoListProject.DataAccessFolder
{
    public class DataAccess
    {

        #region Methods

     
        public IMongoCollection<T> ConnectToMongo<T>(in string collection)      //Conect To Database And Make A Collection
        {
            var client = new MongoClient(Constants.MongoDB.CONNECTION_STRING); //TODO: Move line to main and pass the arguement to the ctor
            var db = client.GetDatabase(Constants.MongoDB.DATA_BASE_NAME);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<TaskModel>> GetAllTasks()         // Get All Tasks From DataBase
        {
            var taskCollection = ConnectToMongo<TaskModel>(Constants.MongoDB.TASK_COLLECTION_NAME);
            var results = await taskCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<List<TaskModel>> GetAllTasksIsDone(bool done)         // Get All Tasks that done , From DataBase
        {
            var taskCollection = ConnectToMongo<TaskModel>(Constants.MongoDB.TASK_COLLECTION_NAME);
            var results = await taskCollection.FindAsync(_ => _.IsDone == done);

            return results.ToList();
        }

        public Task CreateTask(TaskModel task)       // Create Task And Insert It To DataBase
        {
            var taskCollection = ConnectToMongo<TaskModel>(Constants.MongoDB.TASK_COLLECTION_NAME);
            if(IsDuplicatedName(task.Name))
            {
                throw new ArgumentException("Task with this name already exists!");
            }
            return taskCollection.InsertOneAsync(task);
        }

        public Task DeleteTask(TaskModel task)      // Delete Task From DataBase
        {
            var taskCollection = ConnectToMongo<TaskModel>(Constants.MongoDB.TASK_COLLECTION_NAME);
            return taskCollection.DeleteOneAsync(c => c.Id == task.Id);
        }

        public Task MadeTaskDone(TaskModel task)        // Make Task Done 
        {
            var taskCollection = ConnectToMongo<TaskModel>(Constants.MongoDB.TASK_COLLECTION_NAME);
            var filter = Builders<TaskModel>.Filter.Eq(s => s.Id,task.Id);
            
            if (task.IsDone == false)
                task.IsDone=true;
            else { task.IsDone = false; }
           
            return taskCollection.ReplaceOneAsync(filter, task);
        }

        private bool IsDuplicatedName(string name) // try
        {
            var taskCollection = ConnectToMongo<TaskModel>(Constants.MongoDB.TASK_COLLECTION_NAME);
            var filter = Builders<TaskModel>.Filter.Eq(s => s.Name, name);
            var result = filter != null ? false : true;
            return result;
        }
        #endregion
    }
}
