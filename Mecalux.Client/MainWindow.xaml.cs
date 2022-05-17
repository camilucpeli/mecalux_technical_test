using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mecalux.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ApiClient _client;
        public MainWindow()
        {
            _client = new ApiClient();
            InitializeComponent();
            FillOrderOptionsComboBox();
            InputTextBox.Focus();
        }

        private void FillOrderOptionsComboBox()
        {
            var orderOptions = _client.GetOrderOptions();

            OrderOptionsComboBox.ItemsSource = orderOptions.Result;

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

            var response = _client.GetStatistics(text);

            ResultText.Text = response.Result;
        }

        private void OrderText()
        {
            if (InputTextBox.Text == null || InputTextBox.Text == string.Empty) return;
            if(OrderOptionsComboBox.SelectedValue == null) return;

            var orderOption = OrderOptionsComboBox.SelectedValue.ToString();
            var text = InputTextBox.Text;

            var response = _client.GetOrderedText(text, orderOption);

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
    }
}
