using FileHashGeneratorAndValidator.ViewsModels;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace FileHashGeneratorAndValidator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Wpf.Ui.Controls.FluentWindow
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new MainWindowViewModel();
        }

        private void FileUploadArea_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
            => FileUploadArea.BorderBrush = Brushes.Blue;

        private void FileUploadArea_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
            => FileUploadArea.BorderBrush = Brushes.AliceBlue;

        private void FileUploadArea_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
                _viewModel.FilePath = dlg.FileName;
        }

        private void CopyButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => Clipboard.SetText(_viewModel.GeneratedFileHash);

        private void FileUploadArea_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files is not null && files.Length > 0)
                _viewModel.FilePath = files[0];
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_viewModel.IsBusy)
            {
                var messageResult = MessageBox.Show("There is a process currently in progress. If you want to close the app, the process will be canceled.",
                    "Are you sure you want to cancel the operation?", MessageBoxButton.OKCancel);

                if (messageResult == MessageBoxResult.OK)
                {
                    _viewModel.CancelTask(null);
                }
                else
                    e.Cancel = true;
            }
        }

        private void FileUploadArea_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null && files.Length > 0 && File.Exists(files[0]))
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.None;
            }
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }

        private void OnVisitGitHubRepositoryClick(object sender, RoutedEventArgs e)
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/Kareem-Zein/FileHashGeneratorAndValidator",
                UseShellExecute = true
            };

            System.Diagnostics.Process.Start(psi);
        }
    }
}