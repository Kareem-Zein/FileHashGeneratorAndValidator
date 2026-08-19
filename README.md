# File Hash Generator and Validator

[![NuGet Version](https://img.shields.io/nuget/v/KZ.FileHash.svg?style=flat-square&color=blue)](https://www.nuget.org/packages/KZ.FileHash)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg?style=flat-square)](LICENSE)

Starting from version `1.0.0.1`, we relied on my open-source library [KZ.FileHash](https://www.nuget.org/packages/KZ.FileHash)

A simple, ultra-fast Windows desktop application for generating file hashes and verifying files against expected checksums.

File Hash Generator and Validator allows you to calculate hashes using a selected algorithm or verify files against publisher checksums. Built on top of custom high-performance architecture, it processes files in a streaming, low-allocation fashion without clogging system memory.

---

## 🛠️ Powered By Internal Packages

This application relies on our standalone, custom-built open-source hashing engine:

* **[KZ.FileHash](https://www.nuget.org/packages/KZ.FileHash)** `v1.0.0` — *High-performance, zero-allocation multi-hashing core built using `ArrayPool<byte>` and single-pass streaming architecture for .NET 10

---

## Features

* **Zero-Allocation File Processing:** Incremental stream reading using zero-allocation byte buffers via `ArrayPool<byte>`.
* **Generate & Verify Hashes:** Calculate hashes or check integrity against expected publisher checksums.
* **Algorithm Auto-Suggestion:** Validates hash lengths beforehand and suggests matching algorithms if a mismatch is detected.
* **Drag and Drop UI:** Fluent Design interface with full drag-and-drop file selection.
* **Progress & Cancellation:** Real-time percentage tracking via `IProgress<double>` and full operation cancellation via `CancellationToken`.
* **100% Offline & Private:** All operations occur locally on your machine.

---

## Supported Algorithms

* MD5
* SHA-1
* SHA-256
* SHA-384
* SHA-512
* SHA3-256
* SHA3-384
* SHA3-512

---

## Why This Project?

When downloading software or large files from the internet, publishers often provide a checksum (such as SHA-256 or SHA3-256).

This application allows you to verify file integrity locally, protecting your downloads against accidental corruption or file tampering without relying on heavy third-party software or online tools.

> **Important:** Hash verification confirms that the calculated hash matches the expected hash. It does not by itself prove that the file or the published checksum came from a trusted source.

---

## How It Works

### Generate Hash
1. Select or drag-and-drop a file.
2. Choose your desired hashing algorithm.
3. Click **Calculate Hash**.
4. Copy the generated hash directly to your clipboard.

### Verify Hash
1. Select a file and enter the publisher's checksum in the **Expected Hash** field.
2. Select the hashing algorithm.
3. Click **Start Check**.
4. The system validates the hash length first. If valid, it computes the hash and performs a case-insensitive comparison.

---

## Large File & Memory Performance

The application does not load entire files into system RAM.

By leveraging **`KZ.FileHash`**, file data is processed incrementally in buffered chunks below the Large Object Heap (LOH) threshold. Memory allocations remain low and constant regardless of file size.

* Tested successfully on large files (**3 GB+**) without memory spikes or UI freezing.

---

## Technology Stack

* **Language:** C# 14
* **Target Framework:** .NET 10
* **UI Framework:** WPF with [WPF-UI](https://github.com/lepoco/wpfui) (Fluent Design)
* **Core Engine:** [KZ.FileHash](https://www.nuget.org/packages/KZ.FileHash) NuGet Package
* **Architecture:** MVVM (Model-View-ViewModel)
* **Asynchronous & Threading:** `Async/Await`, `CancellationToken`, `IProgress<double>`

---

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
├── Models/
│   └── HashOperationResult.cs
│
├── Services/
│   └── FileHashService.cs --> Integrated with KZ.FileHash Engine
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

## License & Third-Party Notices

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for the full license text.

For details regarding third-party software licenses, see the [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt)

## Acknowledgments
* Built with [WPF-UI](https://github.com/lepoco/wpfui) for modern Fluent Design UI components (MIT License).
* [KZ.FileHash](https://github.com/Kareem-Zein/KZ.FileHash) Custom open-source multi-hashing enging Built by [Kareem Zein](https://kareem-zein.com)

## Disclaimer

This application is provided as an integrity verification utility.

A matching hash means that the calculated hash of the file matches the expected hash provided by the user.

The reliability of the verification ultimately depends on the trustworthiness of the expected checksum and the source from which it was obtained.
