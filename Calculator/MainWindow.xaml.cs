using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int startingRow = 3;

        private readonly string[] buttonsStrings =
        {
            "7", "8", "9", "*",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "+/-", "0", ".", "=",
        };

        public MainWindow()
        {
            InitializeComponent();

            AddDisplayLabels();
            AddClearingButtons();
            AddOperationButtons();
        }

        private void AddDisplayLabels()
        {
            var operationsLabel = new Label()
            {
                Content = "Display"
            };
            var resultsLabel = new Label()
            {
                Content = "Hello There"
            };

            MainGrid.Children.Add(operationsLabel);
            Grid.SetColumnSpan(operationsLabel, MainGrid.ColumnDefinitions.Count);

            MainGrid.Children.Add(resultsLabel);
            Grid.SetRow(resultsLabel, 1);
            Grid.SetColumnSpan(resultsLabel, MainGrid.ColumnDefinitions.Count);
        }

        private void AddClearingButtons()
        {
            var clearButton = new Button()
            {
                Content = "C",
                Margin = new Thickness(2, 2, 2, 2)
            };
            var clearEverythingButton = new Button()
            {
                Content = "CE",
                Margin = new Thickness(2, 2, 2, 2)
            };
            var divisionButton = new Button()
            {
                Content = "/",
                Margin = new Thickness(2, 2, 2, 2)
            };

            MainGrid.Children.Add(clearButton);
            Grid.SetRow(clearButton, 2);
            Grid.SetColumnSpan(clearButton, 2);

            MainGrid.Children.Add(clearEverythingButton);
            Grid.SetRow(clearEverythingButton, 2);
            Grid.SetColumn(clearEverythingButton, 2);

            MainGrid.Children.Add(divisionButton);
            Grid.SetRow(divisionButton, 2);
            Grid.SetColumn(divisionButton, 3);
        }

        private void AddOperationButtons()
        {
            for (var i = 0; i < buttonsStrings.Length; i++)
            {
                var b = new Button()
                {
                    Content = buttonsStrings[i],
                    Margin = new Thickness(2, 2, 2, 2)
                };
                MainGrid.Children.Add(b);

                var row = i / MainGrid.ColumnDefinitions.Count;
                var column = i % MainGrid.ColumnDefinitions.Count;

                Grid.SetRow(b, row + startingRow);
                Grid.SetColumn(b, column);
            }
        }
    }
}