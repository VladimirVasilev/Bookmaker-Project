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
using BookmakerProject.IO;

namespace Client
{
    /// <summary>
    /// Interaction logic for TopMenu.xaml
    /// </summary>
    public partial class TopMenu : UserControl
    {
        public TopMenu()
        {
            InitializeComponent();
            
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = "text"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    var newItems = ReadWriteFile.ReadFile(dialog.FileName);

                    InputDataGrid.Matches.Clear();

                    foreach (var item in newItems)
                    {
                        InputDataGrid.Matches.Add(item);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ReadWriteFile.CurrentFile != null)
            {
                try
                {
                    ReadWriteFile.SaveFile(InputDataGrid.Matches, ReadWriteFile.CurrentFile);
                }
                catch (ArgumentException ex)
                {
                    if (ex.ParamName == "AwayTeam")
                    {
                        MessageBox.Show("Грешка: невалидна стойност на отбора гост");
                    }
                    else if (ex.ParamName == "HomeTeam")
                    {
                         MessageBox.Show("Грешка: невалидна стойност на отбора домакин");
                    }
                    else
                    {
                        MessageBox.Show("Грешка: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: develop translating method for future releases
                    MessageBox.Show("Грешка: " + ex.Message);
                }
            }
            else
            {
                SaveAs_Click(sender, e);
            }

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = "text"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {

                try
                {
                    ReadWriteFile.SaveFile(InputDataGrid.Matches, dialog.FileName);
                }
                catch (ArgumentException ex)
                {
                    if (ex.ParamName == "AwayTeam")
                    {
                        MessageBox.Show("Грешка: невалидна стойност на отбора гост");
                    }
                    else if (ex.ParamName == "HomeTeam")
                    {
                        MessageBox.Show("Грешка: невалидна стойност на отбора домакин");
                    }
                    else
                    {
                        MessageBox.Show("Грешка: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: develop translating method for future releases
                    MessageBox.Show("Грешка: " + ex.Message);
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
