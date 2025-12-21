
<p align="center"><h1 align="center">LibManager</h1></p>

<p align="center">WPFで開発された簡易書籍管理システム</p>

<p align="center"><a href="./LICENSE"><img src="https://img.shields.io/github/license/icchon/LibManager" alt="license"></a></p>

<br>

## Contents

- [Overview](#overview)

- [Features](#features)

- [Getting Started](#getting-started)

  - [Prerequisites](#prerequisites)

  - [Installation](#installation)

  - [Usage](#usage)

- [License](#license)

---

## Overview

`LibManager`は、WPF（Windows Presentation Foundation）アプリケーションとして開発された簡易的な書籍管理システムである。バーコードスキャンを利用して、本の追加、貸出、返却、ユーザーの識別などの管理を容易に行う。また、外部API（Google Books API, OpenBD API, NDL API）から書籍情報を取得し、表示する。

---

## Features

- **Barcode Scan:** ISBNバーコードとユーザーコードのスキャンによる高速な操作を提供する。

- **Book Management:**

    - 書籍の追加（ISBNスキャンによる情報自動取得）が可能である。

    - 書籍の貸出状況や貸出履歴を表示し、管理する。

- **Rental/Return:** ユーザーと本のバーコードをスキャンすることで、貸出・返却処理を実行する。

- **User Management:** ユーザーバーコードをスキャンしてログインおよびログアウトを行う。

- **External API Integration:** Google Books API、OpenBD API、NDL APIを利用し、書籍情報の自動取得と詳細表示を実現する。

---

## Getting Started

### Prerequisites

- **.NET 7.0 SDK** (for Windows)

### Installation

```sh

❯ git clone https://github.com/icchon/LibManager
❯ cd LibManager
# Visual Studio 2022 または互換性のあるIDEで LibManager.sln を開いてビルドする
```

### Usage

アプリケーションを起動し、メインウィンドウから各機能へアクセスする。バーコードスキャン入力フィールドにバーコードを入力（またはスキャン）することで操作を実行する。

---

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

© 2025 icchon






