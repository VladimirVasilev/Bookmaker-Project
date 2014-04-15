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

namespace Client
{
    /// <summary>
    /// A UserControl for reading input
    /// </summary>
    public partial class InputControl : UserControl
    {
        public static List<TextBox> MatchTextBoxes { get; private set; }
        public static List<TextBox> CoefTextBoxes { get; private set; }
        public static List<CheckBox> CombBoxes { get; private set; }
        public static List<DatePicker> MatchDatePickers { get; private set; }

        public InputControl()
        {
            InitializeComponent();
            InitializeLists();
        }


        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) }); // add a new row
            int lastRow = grid.RowDefinitions.Count - 1;  // get the index of the row

            #region Add TextBox for the match
            TextBox matchTextBox = new TextBox();
            Grid.SetColumn(matchTextBox, 0);
            Grid.SetRow(matchTextBox, lastRow);
            grid.Children.Add(matchTextBox);
            MatchTextBoxes.Add(matchTextBox);
            #endregion

            #region Add TextBox for coefs on the new row
            TextBox coefTextBox = new TextBox();
            Grid.SetColumn(coefTextBox, 1);
            Grid.SetRow(coefTextBox, lastRow);
            grid.Children.Add(coefTextBox);
            CoefTextBoxes.Add(coefTextBox);
            #endregion

            #region Add CheckBox for combinations on the new row
            CheckBox combBox = new CheckBox()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };
            Grid.SetColumn(combBox, 2);
            Grid.SetRow(combBox, lastRow);
            grid.Children.Add(combBox);
            CombBoxes.Add(combBox);
            #endregion

            #region Add DatePicker for match date on the new row
            DatePicker matchDatePicker = new DatePicker();
            Grid.SetColumn(matchDatePicker, 3);
            Grid.SetRow(matchDatePicker, lastRow);
            grid.Children.Add(matchDatePicker);
            MatchDatePickers.Add(matchDatePicker);
            #endregion
        }


        public void InitializeLists()
        {
            MatchTextBoxes = new List<TextBox>();
            CoefTextBoxes = new List<TextBox>();
            CombBoxes = new List<CheckBox>();
            MatchDatePickers = new List<DatePicker>();
        }


    }
}
