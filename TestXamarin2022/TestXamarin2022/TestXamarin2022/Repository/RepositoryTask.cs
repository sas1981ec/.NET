using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using TestXamarin2022.Dependencies;
using TestXamarin2022.Models;
using Xamarin.Forms;

namespace TestXamarin2022.Repository
{
    public class RepositoryTask
    {
        private SQLiteConnection _conecction;
        public RepositoryTask()
        {
            _conecction = DependencyService.Get<IDataBase>().GetConecction();
        }

        public void CreateDataBase()
        {
            _conecction.DropTable<Task>();
            _conecction.CreateTable<Task>();
        }

        public List<Task> GetTasks()
        {
            var tasks = from t in _conecction.Table<Task>() select t;
            return tasks == null || tasks.Count() == 0 ? new List<Task>() : tasks.ToList();
        }

        public void CreateTask(string name)
        {
            var task = new Task { Name = name };
            _conecction.Insert(task);
        }

        public void UpdateTask(int taskId)
        {
            var task = GetTask(taskId);
            _conecction.Update(task);
        }

        private Task GetTask(int taskId)
        {
            return (from t in _conecction.Table<Task>() where t.Id == taskId select t).FirstOrDefault();
        }

        public void RemoveTask(int taskId)
        {
            var task = GetTask(taskId);
            _conecction.Delete(task);
        }
    }
}
