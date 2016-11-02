namespace Weather_App
{
    #region Usings
    using System.Windows;
    using System.Windows.Input;
    using Client;
    using Entities; 
    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        /// <summary>
        /// An instance of the class Controller.
        /// </summary>
        private Controller controller;
        #endregion

        #region Methods
        #region Main
        /// <summary>
        /// Main method
        /// </summary>
        public MainWindow()
        {
            controller = new Controller();
            InitializeComponent();
            txtCityBox.Focus();
        }
        #endregion

        #region Button click
        /// <summary>
        /// When the button GetTemperature is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetTemperature_Click(object sender, RoutedEventArgs e)
        {
            string input = txtCityBox.Text;
            WeatherData data = controller.GetTemperatureFor(input);
            lblTemperature.Content = data.Temperature + " °C";
            txtCityBox.Focus();
        }
        #endregion

        #region KeyUpEvents
        /// <summary>
        /// When Enter is pressed, while in the city textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCityBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGetTemperature_Click((object)sender, (RoutedEventArgs)e);
            }
        }  
        #endregion
        #endregion
    }
}
