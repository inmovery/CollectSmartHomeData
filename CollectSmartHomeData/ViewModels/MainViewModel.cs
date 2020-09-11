using CollectSmartHomeData.Services;
using CollectSmartHomeData.Models;
using Refit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Data;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using CollectSmartHomeData.ViewModels;
using System.Linq;
using System.Reflection;

namespace CollectSmartHomeData {

    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class MainViewModel : BaseViewModel {

        #region Private Member

        /// <summary>
        /// The Window this view model controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 5;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 3;

        /// <summary>
        /// The command to execute Window for edit specific sensor
        /// </summary>
        private ICommand mEditSensor { get; set; }

        /// <summary>
        /// The command to show window for add a new sensor
        /// </summary>
        private ICommand mAddSensor { get; set; }

        #endregion Private Member

        #region Window Toolbar

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 1120;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 600;

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The size of the resize border, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness {
            get {
                return new Thickness(ResizeBorder + OuterMarginSize);
            }
        }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding {
            get {
                return new Thickness(ResizeBorder);
            }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize {
            get {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set {
                mOuterMarginSize = value;
            }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness {
            get {
                return new Thickness(OuterMarginSize);
            }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius {
            get {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set {
                mWindowRadius = value;
            }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius {
            get {
                return new CornerRadius(WindowRadius);
            }
        }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 54;

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength {
            get {
                return new GridLength(TitleHeight + ResizeBorder);
            }
        }

        #endregion

        #region General variables

        /// <summary>
        /// The collection of sensors for live updating items
        /// </summary>
        public ObservableCollection<Sensor> Sensors { get; set; }

        /// <summary>
        /// The collection of sensors to display items for
        /// </summary>
        public ICollectionView SensorsView { get; set; }

        #endregion

        #region Commands for Controls

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion Commands for Controls

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public MainViewModel(Window window) {
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) => {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));

                OnPropertyChanged(nameof(WindowMinimumWidth));
                OnPropertyChanged(nameof(WindowMinimumHeight));

            };

            // Create commands for controls
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, getMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);
            
            // Load sensor data
            List<Sensor> test = new List<Sensor>();
            Task.Run(async () => test = await GetSensorsAsync()).Wait();

            Sensors = new ObservableCollection<Sensor>(test);

            Sensors.CollectionChanged += (s, e) => {
                File.WriteAllText("SensorsData.json", JsonConvert.SerializeObject(Sensors));
            };
            BindingOperations.EnableCollectionSynchronization(Sensors, new object());
            SensorsView = CollectionViewSource.GetDefaultView(Sensors);

            //string json = JsonConvert.SerializeObject(SensorsView, Formatting.Indented);
            //MessageBox.Show(json);

        }

        #endregion

        #region

        public ICommand EditSensor {
            get {
                if(mEditSensor == null)
                    mEditSensor = new RelayCommand<Sensor>(OpenEditWindow);
                return mEditSensor;
            }
        }

        private void OpenEditWindow(Sensor sensor) {
            var EditSensorWindow = new EditSensorWindow();
            var EditViewModel = new EditSensorViewModel(EditSensorWindow) {
                SensorInfo = sensor,
            };
            EditSensorWindow.DataContext = EditViewModel;
            EditSensorWindow.Show();
        }

        public ICommand AddSensor {
            get {
                if(mAddSensor == null)
                    mAddSensor = new RelayCommand(OpenAddSensorWindow);
                return mAddSensor;
            }
        }

        private void OpenAddSensorWindow() {
            var AddSensorWindow = new AddSensorWindow();
            var AddViewModel = new AddSensorViewModel(AddSensorWindow);
            AddSensorWindow.DataContext = AddViewModel;
            AddSensorWindow.Show();
        }

        #endregion

        #region API methods

        public async Task<List<Sensor>> GetSensorsAsync() {
            var service = new SensorsService(new Uri("https://inmovery.ru"));
            var results = await service.getSensors();
            return results;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point getMousePosition() {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the  window position
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion

    }
}
