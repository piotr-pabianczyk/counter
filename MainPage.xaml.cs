using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        List<CounterItem> counters = new List<CounterItem>();

        string filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.txt");

        public MainPage()
        {
            InitializeComponent();

            LoadCounters();
        }

        private void AddCounter_Clicked(object sender, EventArgs e)
        {
            if(nameEntry.Text == "")
            {
                return;
            }

            CounterItem counter = new CounterItem();

            counter.Name = nameEntry.Text;
            counter.Value = 0;

            counters.Add(counter);
            nameEntry.Text = "";
            ShowCounters();
        }

        private void ShowCounters()
        {
            countersLayout.Children.Clear();

            foreach (CounterItem counter in counters)
            {
                Label nameLabel = new Label {

                    Text = counter.Name,
                    FontSize = 22,
                    HorizontalOptions = LayoutOptions.Center
                };

                Label valueLabel = new Label
                {

                    Text = counter.Value.ToString(),
                    FontSize = 40,
                    HorizontalOptions = LayoutOptions.Center
                };

                Button minusButton = new Button
                {

                    Text = "-"
                };

                Button plusButton = new Button
                {

                    Text = "+"
                };

                minusButton.Clicked += (sender, e) =>
                {
                    counter.Value--;
                    valueLabel.Text = counter.Value.ToString();
                };

                plusButton.Clicked += (sender, e) =>
                {
                    counter.Value++;
                    valueLabel.Text = counter.Value.ToString();
                };

                HorizontalStackLayout buttons = new HorizontalStackLayout
                {
                    Spacing = 10,
                    HorizontalOptions = LayoutOptions.Center
                };

                buttons.Children.Add(minusButton);
                buttons.Children.Add(plusButton);

                VerticalStackLayout counterBox = new VerticalStackLayout
                {
                    Spacing = 5
                };

                counterBox.Children.Add(nameLabel);
                counterBox.Children.Add(valueLabel);
                counterBox.Children.Add(buttons);
                countersLayout.Children.Add(counterBox);

            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();

            foreach (CounterItem counter in counters)
            {
                lines.Add(counter.Name + ";" + counter.Value);
            }
            File.WriteAllLines(filePath, lines);

            DisplayAlert("Zapis", "Liczniki zostały zapisane", "Ok");
        }
        private void LoadCounters()
        {
            if(!File.Exists(filePath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] data = line.Split(';');

                if (data.Length == 2)
                {
                    CounterItem counter = new CounterItem
                    {
                        Name = data[0],
                        Value = int.Parse(data[1])
                    };

                    counters.Add(counter);
                }
            }
            ShowCounters();
        }
    }
}
