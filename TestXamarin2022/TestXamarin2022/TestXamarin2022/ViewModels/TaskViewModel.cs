using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestXamarin2022.Models;
using TestXamarin2022.Repository;

namespace TestXamarin2022.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private ObservableCollection<Task> _tasks;

        public ObservableCollection<Task> Tasks
        {
            get 
            {
                return this._tasks;
            }

            set 
            {
                _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }
        //public ObservableCollection<Item> Items { get; }
        //public Command LoadItemsCommand { get; }
        //public Command AddItemCommand { get; }
        //public Command<Item> ItemTapped { get; }

        public TaskViewModel()
        {
            var repository = new RepositoryTask();
            var list = repository.GetTasks();
            Tasks = new ObservableCollection<Task>(list);
        }


    }
}
