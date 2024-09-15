using MyToDo1.Common.Models;
using MyToDo1.Extentions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace MyToDo1.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public MainViewModel(IRegionManager regionManager)
        {
            menuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;

            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });
        }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            // 如果导航到 RemoteCaptureView，则传递 ActionSource 参数
            if (obj.NameSpace == "RemoteCaptureView")
            {
                
                var parameters = new NavigationParameters
{
                    { "CameraController", _controller },
                    { "ActionSource", _actionSource }
            };
                regionManager.RequestNavigate("MainRegion", "RemoteCaptureView", parameters);
            }
            else
            {
                // 普通导航逻辑
                regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
                {
                    journal = back.Context.NavigationService.Journal;
                });
            }
        }

        private ObservableCollection<MenuBar> menuBars;
        private object _controller;
        private object _actionSource;

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "Camera", Title = "Remote Capture", NameSpace = "RemoteCaptureView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "ToDoView" });
            MenuBars.Add(new MenuBar() { Icon = "Notebook", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "Control", NameSpace = "Control" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "Control2", NameSpace = "Control2" });
        }
    }
}
