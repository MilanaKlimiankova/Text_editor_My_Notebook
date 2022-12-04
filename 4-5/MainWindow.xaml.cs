using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;

namespace _4_5
{
    public partial class MainWindow : Window
    {

        private List<string> lastFilesList; //Список недавних файлов
        public MainWindow()
        {
            InitializeComponent();
            this.FontTypes.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/RU.xaml") };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(textArea.Document.ContentStart, textArea.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }

        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(textArea.Document.ContentStart, textArea.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);

            }

        }

        private void FontTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontTypes.SelectedItem != null)
            {
                textArea.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, this.FontTypes.SelectedItem);
            }
            
        }

        private string GetLength(RichTextBox rtb)
        {
            int count_of_symbols = 0;
            int count_of_lines = 0;
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            foreach (char c in textRange.Text)
            {
                if (!c.Equals('\n') && (int)c != 13)
                {
                    count_of_symbols++;
                }
                else if (c.Equals('\n'))
                {
                    count_of_lines++;
                }
            }
            return "count of symbols " + count_of_symbols + ", count of lines " + count_of_lines;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.Log.Content = GetLength(this.textArea);
        }

        private void SetRussian(object sender, RoutedEventArgs e) //Локализация
        {
            try
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/RU.xaml")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void SetEnglish(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/EN.xaml")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

    }
}
