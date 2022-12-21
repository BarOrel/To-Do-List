using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accessibility;

namespace ToDoListProject.Models
{
    public class TaskModel
    {
        #region Ctor

        public TaskModel(string name) => Name = name;

        #endregion

        #region Prop

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; } 
        public DateTime CreationDate { get; set; } = DateTime.Now;

        #endregion
    }
}
