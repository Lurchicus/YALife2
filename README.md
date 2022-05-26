# YALife

## Description

Yet Another Life Program (based on Conway's Game of Life).

![Y A Life Screenshot 2021 11 20 153427](YALife Screenshot 2021-11-20 153427.png)

## Details

Designed and authored by Dan Rhea.

I wrote this to try out WinForms on the VS 2022 Preview
using .NET 6. Note that Visual Studio 2022 has been released as has
.NET 6.

I added the ability to scale a life "cell" from 1 to 32. This means
that a scale of 4 will draw a live cell that is 4 pixels high and 4
pixels wide. This can be thought of as a zoom, you see less cells 
but the ones you see are much easier to see.

## Licensing

I'm licensing this program under the MIT License. 

The two classes I mention below that are from StackOverflow.com 
are covered under the MIT license as well.

## Attributions

I don't have copyright information (dates) for the items below so I will
go with the following attributions as suggested by StackOverflow.com.

I use the following to apply color to pixels based on how many passes 
the pixel remains unchanged by the Game of Life rules. It's fun!

ColorHeatMap class by Davide Dolla 
(https://stackoverflow.com/users/1332133/davide-dolla) and shared on 
(https://stackoverflow.com/questions/17821828/calculating-heat-map-colours)

I use the following to greatly speed up drawing the Game of Life bitmap
each pass. I was seeing 1 to 1.5 seconds per pass before with an improvement
to 0.01 seconds between passes. Before I was using standard bitmap.setpixel
operations.

DirectBitmap by A.Konzel 
(https://stackoverflow.com/users/3117338/a-konzel) via StackOverflow 
(https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f)

## Change history

Detailed program change history is at the top of the Form1.cs code which is 
where I prefer to keep change history. This is an overview:

* 1.0.1.0  08/12/2021  Initial version
* 1.0.4.0  08/29/2021  Added a color gradient to color persistant cells. The
                       gradient is pretty bland. Need something better.
* 1.0.6.0  09/25/2021  Implemented pixel coloring (continous cycle). This new
                       method is from Davide Dolla on StackOverflow and works
                       exactly the way I want.
* 1.0.7.0  09/28/2021  Added a checkbox to control if persistance colors cycle
                       only once or continously.
* 1.0.10.0 10/16/2021  First compile using VS 2022 RC 1
* 1.0.11.0 10/19/2021  Added a file reader form to display the license and a 
                       button on the UI panel to load the reader form with the
                       GPL 3 license.
* 1.0.12.0 10/25/2021  Added a pass timer to show how long (in seconds) it takes
                       to get through both DoLife() and DrawLife().
                      - Added an about/spalsh screen (preliminary).
* 1.0.13.0 11/05/2021  Removed the reset debounce as it blocked a proper response 
                       to screen size changes. With the debounce, it was seeing and
                       responding to the vertical changes but not the horizontal.
                       - Set frame (holds the bitmap) to a minimum of 1, 1 so we 
                       never try to create a 0x0 bitmap when minimized. For now 
                       coming back from a minimize forces a reset. In the future, 
                       if I can detect a minimize and restore, I could try to 
                       save the bitmap where the size of Frame doesn't affect it 
                       and restore it when we un-minimize the form. 
* 1.0.16.0 11/09/2021  Implemented DirectBitmap which greatly improved my draw
                       times. More optimizations by moving the bitmap create and
                       dispose into the reset logic so we only recreate the 
                       bitmap if we change size or the blocksize (or click Stop
                       or Reset).
* 1.0.18.0 11/26/2021  Added a "mode" dropdown that will control color cycling
                       or a single non cycling color.
* 1.0.20.0 12/08/2021  Did some refactoring in DoLife with the code that 
                       checks for any friends living around us. It's simplified
                       in DoLife but the new 8 functions are a bit jank for now.
* 1.0.22.0 01/27/2022  Added attributions to comments, readme and splash screen 
                       for the code used in Paper.cs and Gradient.cs that came
                       from StackOverflow.com.
                       - Added a file extention checker for FileReader to limit
                       what files the reader can display (experimenting on some
                       code to use at work)
* 1.0.24.0 03/13/2022  Moved cardnal direction checkers after DoLife() but before 
                       DrawLife(). It just seemed to make more sense that way.
* 1.0.26.0 03/31/2022  Changed to the MIT license and started working on a file
                       format to manage pre-defined "GOL" patterns (for example a
                       glider factory). WIP.

## Installation

I do not (currently) have a package and install for this program. For now,
it needs to be loaded into Visual Studio 2022 and run. I find it difficult
to pay for an expensive code signing license for what is in effect a free
program I wrote as a hobby/fun/learning/testing project. 