# File Hash Generator and Validator

A simple Windows desktop application for generating file hashes and verifying files against an expected checksum.

File Hash Generator and Validator allows you to calculate the hash of any file using a selected hashing algorithm, or verify a file against a checksum provided by the file publisher.

The application processes files incrementally instead of loading the entire file into memory, making it suitable for large files as well.

## Features

* Generate hashes for any file.
* Verify a file against an expected hash.
* Supports multiple hashing algorithms.
* Supports drag and drop file selection.
* Displays the generated hash after processing.
* Copy the generated hash to the clipboard.
* Progress reporting based on the amount of data processed.
* Cancel hash generation or verification while the operation is running.
* Validates the expected hash length before starting verification.
* Suggests compatible algorithms when the expected hash length does not match the selected algorithm.
* Processes files incrementally without loading the entire file into memory.
* Works with large files.
* Files are processed locally and are never uploaded or transmitted.

## Supported Algorithms

The application currently supports:

* MD5
* SHA-1
* SHA-256
* SHA-384
* SHA-512
* SHA3-256
* SHA3-384
* SHA3-512

## Why This Project?

When downloading software or other files from the internet, publishers often provide a checksum for the file.

For example, a software website may provide a SHA-256 checksum alongside a download.

After downloading the file, you can use this application to calculate the hash of the downloaded file and compare it with the checksum provided by the publisher.

If the hashes match, the file content matches the expected checksum.

This can help detect accidental corruption or unexpected modifications to a downloaded file.

> **Important:** Hash verification confirms that the calculated hash matches the expected hash. It does not by itself prove that the file or the published checksum came from a trusted source.

## How It Works

The application has two main modes.

### Generate Hash

When the expected hash field is empty:

1. Select a file.
2. Select the hashing algorithm.
3. Click **Calculate Hash**.
4. The application reads the file incrementally and calculates its hash.
5. The generated hash is displayed and can be copied to the clipboard.

The expected hash is not required when generating a hash.

### Verify Hash

When an expected hash is entered:

1. Select a file.
2. Select the hashing algorithm.
3. Enter the expected hash.
4. Click **Start Check**.
5. The application first validates the expected hash length against the selected algorithm.
6. If the length is valid, the file is processed and its hash is calculated.
7. The generated hash is compared with the expected hash.
8. The result is displayed to the user.

The comparison is case-insensitive, so uppercase and lowercase hexadecimal characters are treated as equivalent.

Whitespace and prefixes such as `0x` are not normalized.

## Hash Length Validation

Before calculating a hash during verification, the application checks whether the expected hash has the correct length for the selected algorithm.

For example, if a SHA-384 algorithm is selected but the entered hash has a length corresponding to SHA-512, the verification does not start.

Instead, the application informs the user about the expected length and suggests algorithms that generate hashes of the same length.

This helps prevent accidentally selecting the wrong hashing algorithm when verifying a downloaded file.

## Large File Support

The application does not load the entire file into memory.

File data is read incrementally in chunks of **80 KB** and passed to the hashing process as it is read.

The application uses .NET's `IncrementalHash` API to calculate the hash incrementally.

This approach allows the application to process large files without requiring memory proportional to the file size.

The application has no application-defined maximum file size.

For example, a 3 GB file has been successfully processed without freezing the application.

Actual processing time depends on factors such as file size and storage performance.

## Progress Reporting

While generating or verifying a hash, the application displays a progress indicator based on:

```text
Bytes processed / Total file size
```

The progress represents how much of the file has already been read and processed.

The progress indicator disappears when the operation finishes or is cancelled.

## Cancellation

Hash generation and verification can be cancelled while an operation is in progress.

Cancellation is implemented using `CancellationToken`, which is passed to the asynchronous file-reading operation.

Once the operation is cancelled, the application stops processing and displays a cancellation message.

Cancellation is only available while an operation is running.

## Privacy

All file processing is performed locally on the user's computer.

The application:

* Does not upload files.
* Does not send file contents to a server.
* Does not require an online service to calculate hashes.
* Does not modify the file being processed.

The application only reads the selected file to calculate its hash.

## Requirements

* Windows
* .NET 10 SDK

## Getting Started

Clone the repository:

```bash
git clone https://github.com/Kareem-Zein/FileHashGeneratorAndValidator.git
```

Navigate to the project directory:

```bash
cd FileHashGeneratorAndValidator
```

Run the application:

```bash
dotnet run
```

The project can also be opened and run using Visual Studio with the .NET 10 SDK installed.

## Project Structure

The project uses a lightweight MVVM-based structure.

```text
FileHashGeneratorAndValidator/
│
├── Converters/
│   └──InverseBooleanConverter.cs
|
├── Core/
│   ├── Bindable.cs
│   └── RelayCommand.cs
│
├── Enums/
│   └── HashAlgorithm.cs
│
├── Helpers/
│   └── AlgorithmsHelper.cs
│
├── Models/
│   └── HashOperationResult.cs
│
├── Services/
│   └── FileHashService.cs
│
├── ViewModels/
│   └── MainWindowViewModel.cs
│
├── App.xaml
├── App.xaml.cs
├── AssemblyInfo.cs
├── MainWindow.xaml
└── MainWindow.xaml.cs
```

### Main Components

**MainWindow.xaml**

Contains the application's user interface.

**MainWindowViewModel**

Handles the UI state, commands, user interactions, and application workflow.

**FileHashService**

Responsible for reading files and generating hashes.

The service processes files incrementally using `IncrementalHash` rather than loading the complete file into memory.

**HashAlgorithm**

Defines the hashing algorithms supported by the application.

## Technology Stack

* C# 14
* .NET 10
* WPF
* WPF-UI
* MVVM
* `System.Security.Cryptography`
* `IncrementalHash`
* `CancellationToken`
* Asynchronous file I/O

## Releases

Pre-built Windows releases will be available through the repository's **Releases** page.

For users who do not need the source code, downloading a release is the recommended way to use the application.

## Contributing

Contributions are welcome.

If you find a bug, have an improvement, or want to propose a new feature:

1. Open an issue describing the problem or proposal.
2. For code changes, create a fork of the repository.
3. Create a branch for your changes.
4. Make your changes.
5. Submit a pull request with a clear description of what was changed.

Please keep pull requests focused on a specific change whenever possible.

## Roadmap

Potential future improvements may include:

* Additional hashing algorithms.
* Additional verification workflows.
* Improved release and distribution options.
* Further usability improvements.

The roadmap may change as the project evolves.

## License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for the full license text.

## Disclaimer

This application is provided as an integrity verification utility.

A matching hash means that the calculated hash of the file matches the expected hash provided by the user.

The reliability of the verification ultimately depends on the trustworthiness of the expected checksum and the source from which it was obtained.
