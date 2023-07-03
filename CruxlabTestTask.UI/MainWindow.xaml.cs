using CruxlabTeatTask.BLL.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace CruxlabTestTask.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Onley text files|*.txt"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var result = PasswordAuthenticatorService.AuthenticateFile(openFileDialog.FileName);
                if (result != null)
                {
                    var fileName = new Grid()
                    {
                        Children =
                        {
                            new StackPanel() { Orientation = Orientation.Horizontal, Children =
                                {
                                    new Label () { Content = "File name: " },
                                    new Label () { Content = result.FileName },
                                }},
                        }
                    };
                    var filePath = new Grid()
                    {
                        Children =
                        {
                            new StackPanel() { Orientation = Orientation.Horizontal, Children =
                                {
                                    new Label () { Content = "File path: " },
                                    new Label () { Content = result.FilePath },
                                }},
                        }
                    };
                    var checksCount = new Grid()
                    {
                        Children =
                        {
                            new StackPanel() { Orientation = Orientation.Horizontal, Children =
                                {
                                    new Label () { Content = "Checks count: " },
                                    new Label () { Content = result.ChecksCount },
                                }},
                        }
                    };
                    var passedPasswords = new Grid()
                    {
                        Children =
                        {
                            new StackPanel() { Orientation = Orientation.Horizontal, Children =
                                {
                                    new Label () { Content = "Passed passwords: " },
                                    new Label () { Content = result.PassedPasswords },
                                }},
                        }
                    };
                    Results_StackPanel.Children.Add(fileName);
                    Results_StackPanel.Children.Add(filePath);
                    Results_StackPanel.Children.Add(checksCount);
                    Results_StackPanel.Children.Add(passedPasswords);
                }
            }
        }
    }
}
