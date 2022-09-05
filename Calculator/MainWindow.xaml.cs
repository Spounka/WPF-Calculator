using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const int startingRow = 3;

        private readonly string[] buttonsStrings =
        {
            "7", "8", "9", "*",
            "4", "5", "6", "-",
            "1", "2", "3", "+",
            "+/-", "0", ".", "=",
        };

        private readonly Label operationsLabel;
        private readonly Label resultsLabel;

        private double firstNumber, secondNumber;
        private char currentOperation;
        private bool hasDot;

        private string operationsLabelText
        {
            get => (string)operationsLabel.Content;
            set => operationsLabel.Content = value;
        }

        private string resultsLabelText
        {
            set => resultsLabel.Content = value;
        }


        public MainWindow()
        {
            InitializeComponent();

            AddDisplayLabels(out operationsLabel, out resultsLabel);
            AddClearingButtons();
            AddOperationButtons();
            ClearButton_Click("CE");
        }

        private void AddDisplayLabels(out Label _operationsLabel, out Label _resultsLabel)
        {
            _operationsLabel = new Label
            {
                Content = ""
            };
            _resultsLabel = new Label
            {
                Content = "Hello There"
            };

            MainGrid.Children.Add(_operationsLabel);
            Grid.SetColumnSpan(_operationsLabel, MainGrid.ColumnDefinitions.Count);

            MainGrid.Children.Add(_resultsLabel);
            Grid.SetRow(_resultsLabel, 1);
            Grid.SetColumnSpan(_resultsLabel, MainGrid.ColumnDefinitions.Count);
        }

        private void AddClearingButtons()
        {
            var clearButton = new Button
            {
                Content = "C",
                Margin = new Thickness(2, 2, 2, 2),
            };
            clearButton.Click += NumberButton_Click;
            var clearEverythingButton = new Button
            {
                Content = "CE",
                Margin = new Thickness(2, 2, 2, 2),
            };
            clearEverythingButton.Click += NumberButton_Click;

            var divisionButton = new Button
            {
                Content = "/",
                Margin = new Thickness(2, 2, 2, 2)
            };
            divisionButton.Click += NumberButton_Click;

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
                var b = new Button
                {
                    Content = buttonsStrings[i],
                    Margin = new Thickness(2, 2, 2, 2)
                };
                b.Click += NumberButton_Click;
                MainGrid.Children.Add(b);

                var row = i / MainGrid.ColumnDefinitions.Count;
                var column = i % MainGrid.ColumnDefinitions.Count;

                Grid.SetRow(b, row + startingRow);
                Grid.SetColumn(b, column);
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            var buttonText = (string)((Button)sender).Content;

            if (int.TryParse(buttonText, out _))
            {
                operationsLabelText = string.Concat(operationsLabelText, buttonText);
                return;
            }

            if (string.CompareOrdinal(buttonText, "C") == 0 || string.CompareOrdinal(buttonText, "CE") == 0)
            {
                ClearButton_Click(buttonText);
                return;
            }

            if (string.CompareOrdinal(buttonText, "/") == 0 || string.CompareOrdinal(buttonText, "*") == 0 ||
                string.CompareOrdinal(buttonText, "-") == 0 || string.CompareOrdinal(buttonText, "+") == 0)
            {
                OperationButton_Click(buttonText);
                return;
            }

            if (string.CompareOrdinal(buttonText, "=") == 0)
            {
                CalculateResult_Click();
                return;
            }

            if (string.CompareOrdinal(buttonText, "+/-") == 0)
            {
                operationsLabelText = operationsLabelText[0] switch
                {
                    '-' => operationsLabelText.Remove(operationsLabelText[0]),
                    _ => string.Concat("-", operationsLabelText)
                };
            }

            if (string.CompareOrdinal(buttonText, ".") != 0 || hasDot) return;

            if (string.IsNullOrEmpty(operationsLabelText) || string.IsNullOrWhiteSpace(operationsLabelText))
                operationsLabelText = "0";
            operationsLabelText = string.Concat(operationsLabelText, ".");
            hasDot = true;
        }


        private void ClearButton_Click(string buttonText)
        {
            if (string.CompareOrdinal(buttonText, "C") == 0)
            {
                if (string.IsNullOrEmpty(operationsLabelText))
                    return;

                if (buttonText[^1] == '.')
                    hasDot = false;

                operationsLabelText =
                    operationsLabelText.Remove(operationsLabelText.Length - 1);
                return;
            }

            operationsLabelText = "";
            resultsLabelText = "";
            firstNumber = secondNumber = double.NaN;
            hasDot = false;
        }

        private void OperationButton_Click(string content)
        {
            CalculateResult_Click();
            currentOperation = content[0];
            if (!double.TryParse(operationsLabelText, out firstNumber))
            {
                resultsLabelText = "Error, Operation Invalid";
                return;
            }

            resultsLabelText = firstNumber.ToString(CultureInfo.InvariantCulture);
            operationsLabelText = "";
        }

        private void CalculateResult_Click()
        {
            if (double.IsNaN(firstNumber))
                return;
            if (!double.TryParse(operationsLabelText, out secondNumber))
            {
                resultsLabelText = "Error, Operation Invalid";
                return;
            }


            switch (currentOperation)
            {
                case '/':
                    if (secondNumber == 0)
                    {
                        resultsLabelText = "Cannot Divide by zero";
                        return;
                    }

                    firstNumber /= secondNumber;
                    break;
                case '*':
                    firstNumber *= secondNumber;
                    break;
                case '+':
                    firstNumber += secondNumber;
                    break;
                case '-':
                    firstNumber -= secondNumber;
                    break;
                default:
                    operationsLabelText = resultsLabelText = firstNumber.ToString(CultureInfo.CurrentCulture);
                    break;
            }

            operationsLabelText = resultsLabelText = firstNumber.ToString(CultureInfo.CurrentCulture);
            hasDot = false;

            // To Prevent calculating twice, we set current operation to a random char
            currentOperation = 'n';
        }
    }
}