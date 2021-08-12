using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.BLL.Interfaces;
using ToDoListEnhanced.PL.Models;
using ToDoListEnhanced.PL.Util;
using ToDoListEnhanced.PL.ViewModels.Commands;

namespace ToDoListEnhanced.PL.ViewModels
{
    public class MainWorkspaceViewModel : BaseViewModel
    {
        private IDataService<ProjectDTO> _projectService;
        private IDataService<SubTaskDTO> _subTaskService;

        public MainWorkspaceViewModel(IDataService<ProjectDTO> projectService, IDataService<SubTaskDTO> subTaskService)
        {
            _projectService = projectService;
            _subTaskService = subTaskService;
            this.PropertyChanged += MainWindowViewModel_PropertyChanged;
            LogOffCommand = new DelegateCommand(LogOff);
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedProject) && SelectedProject != null)
                LoadSubTasks();
        }

        private string access_token;

        private Guid _loggedInUserId;

        public void SetLoggedInUser(Dictionary<string, string> loggedInUserInfo)
        {
            access_token = loggedInUserInfo["access_token"];
            _loggedInUserId = Guid.Parse(loggedInUserInfo["userId"]);
            Username = loggedInUserInfo["username"];
            LoadProjects();
        }

        private void LogOff(object obj)
        {
            var injector = new DInjector();
            Application.Current.MainWindow.Content = injector.GetLoginVM();
        }

        public ICommand LogOffCommand { get; private set; }

        private string _userName;

        public string Username
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (_selectedProject == value)
                    return;
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        private ObservableCollection<Project> _projectsCollection = new ObservableCollection<Project>();

        public ObservableCollection<Project> ProjectsCollection
        {
            get { return _projectsCollection; }
            set
            {
                if (_projectsCollection == value)
                    return;
                _projectsCollection.CollectionChanged -= ProjectsCollection_CollectionChanged;
                foreach (var item in _projectsCollection)
                    item.PropertyChanged -= ProjectsCollectionItem_PropertyChanged;
                _projectsCollection = value;
                foreach (var item in _projectsCollection)
                    item.PropertyChanged += ProjectsCollectionItem_PropertyChanged;
                _projectsCollection.CollectionChanged += ProjectsCollection_CollectionChanged;
                OnPropertyChanged(nameof(ProjectsCollection));
            }
        }

        private async void LoadProjects()
        {
            ICollection<ProjectDTO> projectDTOs = await _projectService.Get(_loggedInUserId, access_token);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();
            ProjectsCollection = mapper.Map<ICollection<ProjectDTO>, ObservableCollection<Project>>(projectDTOs);
        }

        private async void ProjectsCollectionItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (Project)sender;
            await _projectService.Update(new ProjectDTO { Id = item.Id, ProjectName = item.ProjectName, Description = item.Description, Status = item.Status, UserId = _loggedInUserId }, access_token);
        }

        private async void ProjectsCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Project item in e.NewItems)
                    {
                        await _projectService.Create(new ProjectDTO { Id = item.Id, ProjectName = item.ProjectName, Description = item.Description, Status = item.Status, UserId = _loggedInUserId }, access_token);
                        item.PropertyChanged += ProjectsCollectionItem_PropertyChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (Project item in e.OldItems)
                    {
                        item.PropertyChanged -= ProjectsCollectionItem_PropertyChanged;
                        await _projectService.Delete(new ProjectDTO { Id = item.Id }, access_token);
                    }
                    break;
                default:
                    break;
            }
        }

        private ObservableCollection<SubTask> _subTaskCollection = new ObservableCollection<SubTask>();

        public ObservableCollection<SubTask> SubTaskCollection
        {
            get { return _subTaskCollection; }
            set
            {
                if (_subTaskCollection == value)
                    return;
                _subTaskCollection.CollectionChanged -= SubTasksCollection_CollectionChanged;
                foreach (var item in _subTaskCollection)
                    item.PropertyChanged -= SubTasksItem_PropertyChanged;
                _subTaskCollection = value;
                foreach (var item in _subTaskCollection)
                    item.PropertyChanged += SubTasksItem_PropertyChanged;
                _subTaskCollection.CollectionChanged += SubTasksCollection_CollectionChanged;
                OnPropertyChanged(nameof(SubTaskCollection));
            }
        }

        public async void LoadSubTasks()
        {
            ICollection<SubTaskDTO> subTaskDTOs = await _subTaskService.Get(_selectedProject.Id, access_token);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubTaskDTO, SubTask>()).CreateMapper();
            SubTaskCollection = mapper.Map<ICollection<SubTaskDTO>, ObservableCollection<SubTask>>(subTaskDTOs);
        }

        private async void SubTasksItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (SubTask)sender;
            await _subTaskService.Update(new SubTaskDTO { Id = item.Id, SubTaskName = item.SubTaskName, Description = item.Description, Status = item.Status, ProjectId = _selectedProject.Id }, access_token);
        }

        private async void SubTasksCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (SubTask item in e.NewItems)
                    {
                        await _subTaskService.Create(new SubTaskDTO { Id = item.Id, SubTaskName = item.SubTaskName, Description = item.Description, Status = item.Status, ProjectId = _selectedProject.Id }, access_token);
                        item.PropertyChanged += SubTasksItem_PropertyChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (SubTask item in e.OldItems)
                    {
                        item.PropertyChanged -= SubTasksItem_PropertyChanged;
                        await _subTaskService.Delete(new SubTaskDTO { Id = item.Id }, access_token);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
