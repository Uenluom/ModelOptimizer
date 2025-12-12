# ModelOptimizer

ModelOptimizer is a Live2D Model texture optimizer that heavily uses the [OptiPNG](https://optipng.sourceforge.net/) project.

Essentially, we call OptiPNG on all .png files in the VTube Studio data folder.

## Installation and Usage

To install ModelOptimizer, simply extract it into a folder along with the latest copy of OptiPNG (which is not included in this repository).

You may need to install the [.NET Desktop 9.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) ([direct link](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-9.0.11-windows-x64-installer)).

Then, run ModelOptimizer.exe. It will ask if you want to locate VTube Studio automatically or manually. 

The automatic search really only is a shortcut to `C:/Program Files (x86)/Steam/steamapps/common/VTube Studio/VTube Studio_Data/StreamingAssets/Live2DModels` and does not search your drive (I got a little lazy if I'm being honest)

After that, just sit back and relax as ModelOptimizer runs OptiPNG on all of your models' textures!

An emergency backup is made by copying your folder path `f` to `f.modopt-backup`. This should be eventually removed if the operation does not cause any issues, and this backup is made solely at the assumption you won't actually back it up yourself.

## Building

This app should build with no problems on a standard Visual Studio installation with the .NET Development package installed. We used NET 9 in this project but you should be able to port it around since the only thing we actually even used was `Process.Start`.

## Why is the Code so Wacky

I wrote this while sleep deprived [on stream](https://www.youtube.com/watch?v=DGuBqDylBxk). I was going to clean it up a bit but I forgot.

## What are all of these \x1b[ things?

Those are ANSI escape sequences. They make the terminal change the formatting of the text.

## Shameless Promotion

If you want a custom app (that is written slightly better than this) check me out at my [VGen page](https://vgen.co/Uenluom)!
