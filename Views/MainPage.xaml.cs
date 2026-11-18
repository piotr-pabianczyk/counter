using System;
using System.Collections.Generic;
using System.IO;
using Counter.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Counter.Views
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
            countersView.ItemsSource = null;
            countersView.ItemsSource = counters;
        }

        private void MinusButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CounterItem counter = (CounterItem)button.BindingContext;
            counter.Value--;

            Label label = (Label)((VerticalStackLayout)button.Parent.Parent).Children[1];
            label.Text = counter.Value.ToString();
        }

        private void PlusButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CounterItem counter = (CounterItem)button.BindingContext;
            counter.Value++;

            Label label = (Label)((VerticalStackLayout)button.Parent.Parent).Children[1];
            label.Text = counter.Value.ToString();
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
