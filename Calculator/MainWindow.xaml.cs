﻿using System;
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
                var b = new Button
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