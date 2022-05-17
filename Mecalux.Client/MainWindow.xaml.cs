using System.Linq;
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
        ApiClient _client = new ApiClient();
        public MainWindow()
        {
            InitializeComponent();
            InputTextBox.Focus();
        }
        

        private async Task FillOrderOptionsComboBoxAsync()
        {
            var orderOptions = await _client.GetOrderOptions();

            OrderOptionsComboBox.ItemsSource = orderOptions;

            OrderOptionsComboBox.IsEnabled = true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var srcButton = e.Source as Button;
            switch (srcButton.Name)
            {
                case "OrderTextButton":
                    await OrderTextAsync();
                    break;
                case "GetStatisticsButton":
                    await GetStatisticsAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task GetStatisticsAsync()
        {
            if (InputTextBox.Text == null || InputTextBox.Text == string.Empty) return;
            var text = InputTextBox.Text;

            var response = await _client.GetStatistics(text);

            ResultText.Text = response;
        }

        private async Task OrderTextAsync()
        {
            if (InputTextBox.Text == null || InputTextBox.Text == string.Empty) return;
            if(OrderOptionsComboBox.SelectedValue == null) return;

            var orderOption = OrderOptionsComboBox.SelectedValue.ToString();
            var text = InputTextBox.Text;

            var response = await _client.GetOrderedText( text, orderOption);

            ResultText.Text = response;
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
