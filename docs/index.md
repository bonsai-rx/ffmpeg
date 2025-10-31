# Introduction

The FFmpeg package is a [Bonsai](https://bonsai-rx.org/) interface for the open source [FFmpeg](https://www.ffmpeg.org/) multimedia framework.

You can use the FFmpeg package to flexibly encode media data using FFmpeg. The package requires a separate FFmpeg installation.

## How to install

1. Install [Bonsai](https://bonsai-rx.org/).
2. From the package manager, search and install the **Bonsai - FFmpeg** package.

## Install FFmpeg

In addition to the FFmpeg package you need to have a version of [FFmpeg](https://www.ffmpeg.org/) installed in your system. Below are suggested installation steps for Windows.

1. Download and install the [latest FFmpeg build for Windows](https://ffmpeg.org/download.html#build-windows). FFmpeg only hosts source code, but they provide third-party links to already compiled executables of the full build.

2. Extract the files and place the FFmpeg binary in the `Extensions` folder of your Bonsai installation. The easiest way to find your Bonsai install folder is to right-click on the Bonsai shortcut > Properties. The path to the folder will be shown in the "Start in" textbox. The path has to be exactly `Extensions\ffmpeg.exe`.


## How to use

To set the VideWriter(FFmpeg) node's `OutputArguments` property, refer to the [FFmpeg documentation](https://ffmpeg.org/ffmpeg.html).
