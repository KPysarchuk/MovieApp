using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoviesApp
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        FileSystemWatcher watcher;
        private MovieModel selectedMovie;
        public ICommand ResetCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public List<MovieModel> Movies { get; set; }
        public ObservableCollection<MovieModel> FilteredMovies { get; set; }
        public List<IFilterable> Plugins { get; set; }
        public ObservableCollection<FilterModel> Filters { get; set; }

        public MovieModel SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                selectedMovie = value;
                OnPropertyChanged("SelectedMovie");
            }
        }

        public void FilterMovies()
        {
            var selectedFilters = Filters.Where(f=>!string.IsNullOrWhiteSpace(f.SelectedItem));
            var filteredResult = Movies.ToList();
            foreach (var filter in selectedFilters)
            {
                var plagin = Plugins.FirstOrDefault(pl=>pl.GetFilterName() == filter.Title);
                filteredResult = plagin.Filter(filteredResult, filter.SelectedItem);
            }

            FilteredMovies = new ObservableCollection<MovieModel>(filteredResult);
            OnPropertyChanged(nameof(FilteredMovies));
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Init();
        }

        public ApplicationViewModel()
        {
            Init();
        }

        private void Init()
        {
            watcher = new FileSystemWatcher(Constants.PluginsPath);
            watcher.EnableRaisingEvents = true;
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);

            SearchCommand = new RelayCommand(FilterMovies);
            ResetCommand = new RelayCommand(() => Init());


            var data = ParserXML.Parse();
            var oc = new ObservableCollection<MovieModel>();
            foreach (var item in data)
            {
                oc.Add(item);
            }
            FilteredMovies = oc;
            Movies = data;

            var allAssemblies = new List<Assembly>();
            var path = Path.GetDirectoryName(Constants.PluginsPath);

            var temp = new List<IFilterable>();
            foreach (var dll in Directory.GetFiles(path, "*.dll"))
            {
                allAssemblies.Add(Assembly.LoadFile(dll));
            }
            foreach (var assembly in allAssemblies)
            {
                var cl = assembly.GetTypes().FirstOrDefault(t => t.IsClass);

                var instance = assembly.CreateInstance(cl.FullName) as IFilterable;
                temp.Add(instance);
            }

            Plugins = temp;
            var tempFilters = new List<FilterModel>();
            foreach (var dll in temp)
            {
                var filter = new FilterModel
                {
                    Title = dll.GetFilterName(),
                    Values = dll.GetSelectionValues(data).Distinct().ToList()
                };
                tempFilters.Add(filter);
            }

            Filters = new ObservableCollection<FilterModel>(tempFilters);
            FilteredMovies = new ObservableCollection<MovieModel>(Movies);
            OnPropertyChanged(nameof(FilteredMovies));
            OnPropertyChanged(nameof(Filters));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}