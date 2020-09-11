using CollectSmartHomeData.Models;
using CollectSmartHomeData.Services;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CollectSmartHomeData.ViewModels {
    /// <summary>
    /// The View Model for the edit window
    /// </summary>
    public class EditSensorViewModel : BaseViewModel {

        #region Private Members

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

        #endregion Private Members

        #region Window Toolbar

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 700;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 590;

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
        public int TitleHeight { get; set; } = 40;

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength {
            get {
                return new GridLength(TitleHeight + ResizeBorder);
            }
        }

        #endregion Window Toolbar

        #region General variables

        /// <summary>
        /// The descriptor for a specific sensor
        /// </summary>
        public Sensor SensorInfo { get; set; } 


        /// <summary>
        /// The collection of sensors for live updating items
        /// </summary>
        public ObservableCollection<Sensor> Sensors { get; set; }

        /// <summary>
        /// The collection of sensors to display items for
        /// </summary>
        public ICollectionView SensorsView { get; set; }


        /// <summary>
        /// The collection of rooms for live updating items
        /// </summary>
        public ObservableCollection<Room> Rooms { get; set; }


        /// <summary>
        /// The collection of rooms to display items for
        /// </summary>
        public ICollectionView RoomsView { get; set; }

        /// <summary>
        /// The collection of sensor types for live updating items
        /// </summary>
        public ObservableCollection<SensorType> SensorTypes { get; set; }

        /// <summary>
        /// The collection of sensor types to display items for
        /// </summary>
        public ICollectionView SensorTypesView { get; set; }

        #endregion General variables

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

        #region Constructors

        public EditSensorViewModel(Window window) {
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

            // Load rooms data
            List<Room> mRooms = new List<Room>();
            Task.Run(async () => mRooms = await GetRoomsAsync()).Wait();

            Rooms = new ObservableCollection<Room>(mRooms);
            Rooms.CollectionChanged += (s, e) => {
                File.WriteAllText("RoomsData.json", JsonConvert.SerializeObject(Rooms));
            };
            BindingOperations.EnableCollectionSynchronization(Rooms, new object());
            RoomsView = CollectionViewSource.GetDefaultView(Rooms);

            // Load sensor types data
            List<SensorType> mSensorTypes = new List<SensorType>();
            Task.Run(async () => mSensorTypes = await GetSensorTypesAsync()).Wait();

            SensorTypes = new ObservableCollection<SensorType>(mSensorTypes);
            SensorTypes.CollectionChanged += (s, e) => {
                File.WriteAllText("SensorTypesData.json", JsonConvert.SerializeObject(Rooms));
            };
            BindingOperations.EnableCollectionSynchronization(SensorTypes, new object());
            SensorTypesView = CollectionViewSource.GetDefaultView(SensorTypes);

            // Load CSV File

            // Load Image

        }

        #endregion Constructors

        #region API methods

        public async Task<List<Sensor>> GetSensorsAsync() {
            var service = new SensorsService(new Uri("https://inmovery.ru"));
            var results = await service.getSensors();
            return results;
        }

        public async Task<List<Room>> GetRoomsAsync() {
            var service = new SensorsService(new Uri("https://inmovery.ru"));
            var results = await service.getRooms();
            return results;
        }

        public async Task<List<SensorType>> GetSensorTypesAsync() {
            var service = new SensorsService(new Uri("https://inmovery.ru"));
            var results = await service.getSensorTypes();
            return results;
        }

        #endregion

        #region Active methods

        public ICommand AddRoom {
            get
        }

        public ICommand AddSensorType {
            
        }

        public ICommand CheckTopic {
            
        }
        
        public

        #endregion Active methods

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
