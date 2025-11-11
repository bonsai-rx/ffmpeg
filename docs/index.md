# Getting Started

`Bonsai.FFmpeg` is a [Bonsai](https://bonsai-rx.org/) interface for the open source [FFmpeg](https://www.ffmpeg.org/) multimedia framework.

You can use `Bonsai.FFmpeg` to flexibly encode media data using FFmpeg. The package requires a separate FFmpeg installation.

## How to install

1. Install [Bonsai](https://bonsai-rx.org/).
2. From the package manager, search and install the **Bonsai.FFmpeg** package.

## Install FFmpeg

1. Download the [latest FFmpeg build for Windows](https://ffmpeg.org/download.html#build-windows). FFmpeg only hosts source code, but they provide third-party links to already compiled executables of the full build.

2. Extract the files and place the FFmpeg binary in the `Extensions` folder of your system-wide Bonsai installation or local Bonsai [environment](https://bonsai-rx.org/docs/articles/environments.html). The path has to be exactly:

    ```
    Extensions\ffmpeg.exe
    ```

> [!TIP]
> The easiest way to find your Bonsai installation folder is to right-click on the Bonsai shortcut > Properties. The path to the folder will be shown in the "Start in" text box.


## How to use

FFmpeg arguments can be supplied to the [`OutputArguments`](xref:Bonsai.FFmpeg.VideoWriter.OutputArguments) property of the [`VideoWriter`](xref:Bonsai.FFmpeg.VideoWriter) operator. For example, to scale a video, you can enter:

`-s 640x480`

Additional output arguments can be found by referring to the [FFmpeg documentation](https://www.ffmpeg.org/documentation.html).

## Troubleshooting

FFmpeg output and error messages are displayed in the Bonsai console window and can help diagnose **Pipe is broken** issues. For example, hardware encoding may require updated graphics drivers.
