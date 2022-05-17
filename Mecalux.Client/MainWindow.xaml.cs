using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Mecalux.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient _client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            InputTextBox.Focus();
        }
        

        private async Task FillOrderOptionsComboBoxAsync()
        {
            var orderOptions = await ApiClient.GetOrderOptions(_client);

            OrderOptionsComboBox.ItemsSource = orderOptions;

            OrderOptionsComboBox.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var srcButton = e.Source as Button;
            switch (srcButton.Name)
            {
                case "OrderTextButton":
                    OrderText();
                    break;
                case "GetStatisticsButton":
                    GetStatistics();
                    break;
                default:
                    break;
            }
        }

        private void GetStatistics()
        {
            if (InputTextBox.Text == null || InputTextBox.Text == string.Empty) return;
            var text = InputTextBox.Text;

            var response = ApiClient.GetStatistics(_client, text);

            ResultText.Text = response.Result;
        }

        private void OrderText()
        {
            if (InputTextBox.Text == null || InputTextBox.Text == string.Empty) return;
            if(OrderOptionsComboBox.SelectedValue == null) return;

            var orderOption = OrderOptionsComboBox.SelectedValue.ToString();
            var text = InputTextBox.Text;

            var response = ApiClient.GetOrderedText(_client, text, orderOption);

            ResultText.Text = response.Result;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputTextBox.Text.Any())
            {
                if(OrderOptionsComboBox.SelectedItem != null) OrderTextButton.IsEnabled = true;
                GetStatisticsButton.IsEnabled = true;
            }
            else
            {
                OrderTextButton.IsEnabled = false;
                GetStatisticsButton.IsEnabled = false;
            }
        }

        private void OrderOptionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderOptionsComboBox.SelectedItem != null) OrderTextButton.IsEnabled = true;
            else OrderTextButton.IsEnabled = false;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            _ = FillOrderOptionsComboBoxAsync();
        }
    }
}
