using System;
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

using BookmakerProject;
using System.ComponentModel;
using System.Globalization;

namespace Client
{

    /// <summary>
    /// Interaction logic for InputDataGrid.xaml
    /// </summary>
    public partial class InputDataGrid : UserControl
    {
        public InputDataGrid()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();

            dataGrid.ItemsSource = Matches;

            dataGrid.CellEditEnding += dataGrid_CellEditEnding;

            Matches.ListChanged += FootballMatches_ListChanged;
        }

        void FootballMatches_ListChanged(object sender, ListChangedEventArgs e)
        {
            dataGrid.Items.Refresh();
        }

        void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // If we're editing the Coef cell do some validations
            if (e.Column.Header.ToString() == "Коефициент")
            {
                var coefTextBox = e.EditingElement as TextBox;
                try
                {
                    if (decimal.Parse(coefTextBox.Text) <= 0)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Невалидна стойност на коефициента");
                    e.Cancel = true;
                    coefTextBox.Text = "1";
                    dataGrid.CancelEdit();
                }
            }
            else if (e.Column.Header.ToString() == "Час")
            {
                var hourTextBox = e.EditingElement as TextBox;
                DateTime currentDateTime = Matches[dataGrid.SelectedIndex].DateAndTime;
                Matches[dataGrid.SelectedIndex].DateAndTime = new DateTime(
                       year: currentDateTime.Year,
                       month: currentDateTime.Month,
                       day: currentDateTime.Day,
                       hour: int.Parse(hourTextBox.Text),
                       minute: currentDateTime.Minute,
                       second: currentDateTime.Second
                    );
            }

            else if (e.Column.Header.ToString() == "Минута")
            {
                var minuteTextBox = e.EditingElement as TextBox;
                DateTime currentDateTime = Matches[dataGrid.SelectedIndex].DateAndTime;
                Matches[dataGrid.SelectedIndex].DateAndTime = new DateTime(
                       year: currentDateTime.Year,
                       month: currentDateTime.Month,
                       day: currentDateTime.Day,
                       hour: currentDateTime.Hour,
                       minute: currentDateTime.Minute,
                       second: currentDateTime.Second
                    );
            }
        }

        public static BindingList<Game> Matches = new BindingList<Game>()
        {

        };

        private void GetInputData_Click(object sender, RoutedEventArgs e)
        {
            Game nextGame = GameFactory.CreateGame(nextGameType.Text, 1, Countries.None, DateTime.Now, new Match(null, null));

            Matches.Add(nextGame);
            bool editResult = dataGrid.CommitEdit();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DataGridRow.GetRowContainingElement(sender as FrameworkElement).GetIndex();

            Matches.RemoveAt(selectedIndex);

            dataGrid.CancelEdit();
        }

        private void Hour_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int newHour = int.Parse((sender as TextBox).Text);

                if (newHour > 23 || newHour < 0)
                {
                    MessageBox.Show("Не валидна стойност на часа");
                }
                else if (dataGrid.SelectedIndex >= 0)
                {
                    int currentHour = Matches[dataGrid.SelectedIndex].DateAndTime.Hour;
                    Matches[dataGrid.SelectedIndex].DateAndTime = Matches[dataGrid.SelectedIndex].DateAndTime.AddHours(newHour - currentHour);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не валидна стойност на часа");
            }
        }

        private void Minute_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int newMinute = int.Parse((sender as TextBox).Text);

                if (newMinute > 59 || newMinute < 0)
                {
                    MessageBox.Show("Не валидна стойност на минутите");
                }
                else if (dataGrid.SelectedIndex >= 0)
                {
                    int currentHour = Matches[dataGrid.SelectedIndex].DateAndTime.Minute;
                    Matches[dataGrid.SelectedIndex].DateAndTime = Matches[dataGrid.SelectedIndex].DateAndTime.AddMinutes(newMinute - currentHour);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не валидна стойност на минутите");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string decimalSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            try
            {
                decimal bet = decimal.Parse(System.Text.RegularExpressions.Regex.Replace(BetText.Text, @"[\,\.]", decimalSeparator));
                IList<decimal> matches = InputDataGrid.Matches.Select(x => x.Coef).ToList();
                ProfitText.Text = string.Format("{0:F2}", Calculator<decimal>.CalcProfitFromSingleCol(matches, bet));
            }
            catch
            {
                MessageBox.Show("Невалиден залог!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string decimalSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            IList<decimal> matches = InputDataGrid.Matches.Select(x => x.Coef).ToList();
            decimal bet = decimal.Parse(System.Text.RegularExpressions.Regex.Replace(BetText.Text, @"[\,\.]", decimalSeparator));

            int[] systems;
            
            if (AllCombinations.IsEnabled)
            {
                int matchCounts = InputDataGrid.Matches.Count;
                int combinationsParsedText = int.Parse(Combinations.Text);
                systems = new int[matchCounts - combinationsParsedText + 1];

                for (int i = 0; i < systems.Length; i++)
                {
                    systems[i] = combinationsParsedText++;
                }
            }
            else
            {
                string[] textCombinations = System.Text.RegularExpressions.Regex.Split(Combinations.Text, @"\D");
                systems = new int[textCombinations.Length];

                for (int i = 0; i < systems.Length; i++)
                {
                    systems[i] = int.Parse(textCombinations[i]);
                }
            }

            ProfitText.Text = string.Format("{0:F2}", Calculator<decimal>.CalcProfitFromCombination(matches, systems, bet));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllCombinations.IsEnabled = (Combinations.Text.Length != 1) ? false : true;
        }
    }
}
