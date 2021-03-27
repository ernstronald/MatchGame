﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondselapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondselapsed++;
            timeTextBlock.Text = (tenthsOfSecondselapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> foodEmoji = new List<string>()
            {
                "🍕", "🍕",
                "🍔","🍔",
                "🍟","🍟",
                "🥞","🥞",
                "🧀","🧀",
                "🥐","🥐",
                "🌭","🌭",
                "🥨","🥨"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in maingGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(foodEmoji.Count);
                    string nextEmoji = foodEmoji[index];
                    textBlock.Text = nextEmoji;
                    textBlock.Visibility = Visibility.Visible;
                    foodEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsOfSecondselapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBoxClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textblock = sender as TextBlock;
            if (findingMatch == false)
            {
                // Start Search
                textblock.Visibility = Visibility.Hidden;
                lastTextBoxClicked = textblock;
                findingMatch = true;
            }
            else if (textblock.Text == lastTextBoxClicked.Text)
            {
                // Match found
                matchesFound++;
                textblock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                // No Match
                lastTextBoxClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
