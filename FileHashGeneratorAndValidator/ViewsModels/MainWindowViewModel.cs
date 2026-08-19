using FileHashGeneratorAndValidator.Core;
using FileHashGeneratorAndValidator.Models;
using FileHashGeneratorAndValidator.Services;
using KZ.FileHash.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace FileHashGeneratorAndValidator.ViewsModels
{
    public class MainWindowViewModel : Bindable
    {
        private CancellationTokenSource? _cancellationTokenSource;

        #region Properties
        public ICommand StartCheckCommand { get; }
        public ICommand CalculateHashCommand { get; }
        public ICommand CancelCommand { get; }

        public string FilePath
        {
            get => field ?? string.Empty;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(CanCalculateHash));
                OnPropertyChanged(nameof(CanStartCheck));
            }
        }
        
        public string GeneratedFileHash
        {
            get => field ?? string.Empty;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(IsGeneratedFileHashVisible));
            }
        }
        
        public string ExpectedHash
        {
            get => field ?? string.Empty;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(CanCalculateHash));
                OnPropertyChanged(nameof(CanStartCheck));
            }
        }
        
        public string Message
        {
            get => field ?? string.Empty;
            set => Set(ref field, value);
        }

        public string MessageTitle
        {
            get => field ?? string.Empty;
            set => Set(ref field, value);
        }

        public HashAlgorithmType SelectedAlgorithm
        {
            get => field;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(CanCalculateHash));
                OnPropertyChanged(nameof(CanStartCheck));
            }
        } = HashAlgorithmType.None;

        public ObservableCollection<HashAlgorithmType> SupportedAlgorithms
        {
            get => field;
        } = [];
        
        public bool IsBusy
        {
            get => field;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(CanCalculateHash));
                OnPropertyChanged(nameof(CanStartCheck));
            }
        }

        public bool IsGeneratedFileHashVisible
            => !string.IsNullOrEmpty(GeneratedFileHash);

        public bool IsMessageVisible
        {
            get => field;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(MessageBackground));
                OnPropertyChanged(nameof(MessageBorderBrush));
            }
        }

        public bool IsTaskFinishedSuccessfully
        {
            get => field;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(MessageTitle));
            }
        }

        private bool AllowStartTask
            => !string.IsNullOrEmpty(FilePath) && SelectedAlgorithm != HashAlgorithmType.None && !IsBusy;

        public bool CanCalculateHash
            => AllowStartTask && string.IsNullOrEmpty(ExpectedHash);

        public bool CanStartCheck
            => AllowStartTask && !string.IsNullOrEmpty(ExpectedHash);

        public Brush MessageBackground
            => IsTaskFinishedSuccessfully ? Brushes.DarkGreen : Brushes.DarkRed;
        
        public Brush MessageBorderBrush
            => IsTaskFinishedSuccessfully ? Brushes.Green : Brushes.Red;

        public double ProgressValue
        {
            get => field;
            set
            {
                Set(ref field, value);
                OnPropertyChanged(nameof(ProgressValueString));
            }
        }

        public string ProgressValueString
            => $"{Math.Round(ProgressValue, 2)}%";
        #endregion

        public MainWindowViewModel()
        {
            StartCheckCommand = new RelayCommand(async (obj) =>
            {
                await CheckFileAsync();
            });

            CalculateHashCommand = new RelayCommand(async (obj) => 
            {
                await CalculateFileHashAsync();
            });
            CancelCommand = new RelayCommand(CancelTask);

            FillAlgorithms();
        }

        private void FillAlgorithms()
        {
            foreach (var value in Enum.GetValues<HashAlgorithmType>())
                SupportedAlgorithms.Add(value);
        }

        public void CancelTask(object? obj)
            => _cancellationTokenSource?.Cancel();

        private async Task CalculateFileHashAsync()
        {
            StartTask();

            using (_cancellationTokenSource = new CancellationTokenSource())
            {
                var hashData = await FileHashService.CalculateHashAsync(FilePath, SelectedAlgorithm, new Progress<double>((newVal) =>
                {
                    ProgressValue = newVal;
                }), _cancellationTokenSource.Token);
                SetResult(hashData);
            }

            EndTask();
        }

        private void SetResult(HashOperationResult result)
        {
            IsTaskFinishedSuccessfully = result.IsSuccess;
            GeneratedFileHash = result.HashData;
            Message = result.Message;
            MessageTitle = result.Title;
            IsMessageVisible = true;
        }

        private async Task CheckFileAsync()
        {
            StartTask();

            using (_cancellationTokenSource = new CancellationTokenSource())
            {
                var result = await FileHashService.CheckAsync(FilePath, ExpectedHash, SelectedAlgorithm, new Progress<double>((newVal) =>
                {
                    ProgressValue = newVal;
                }), _cancellationTokenSource.Token);

                SetResult(result);
            }

            EndTask();
        }

        private void StartTask()
        {
            IsMessageVisible = false;
            GeneratedFileHash = string.Empty;
            IsBusy = true;
        }

        private void EndTask()
        {
            _cancellationTokenSource = null;
            IsBusy = false;
        }
    }
}
