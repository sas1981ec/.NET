using System;
using System.Collections.Generic;
using System.Text;
using TestXamarin2022.Models;
using TestXamarin2022.Repository;
using Xamarin.Forms;

namespace TestXamarin2022.ViewModels
{
    public class TaskModel : BaseViewModel
    {
        private RepositoryTask _repositoryTask;
        private Task _task;
        public TaskModel()
        {
            _repositoryTask = new RepositoryTask();
            _task = new Task();
        }
     
        public Task Task
        {
            get
            {
                return this._task;
            }

            set
            {
                _task = value;
                OnPropertyChanged("Task");
            }
        }

        public Command InsertTask
        {
            get 
            {
                return new Command(() => 
                {
                    _repositoryTask.CreateTask(Task.Name);
                });
            }
        }
        public Command UpdateTask
        {
            get
            {
                return new Command(() =>
                {
                    _repositoryTask.UpdateTask(Task.Id);
                });
            }
        }

        public Command DeleteTask
        {
            get
            {
                return new Command(() =>
                {
                    _repositoryTask.RemoveTask(Task.Id);
                });
            }
        }
    }
}
