# YALife

Yet Another Life Program (based on Conway's Game of Life) by Dan Rhea
I wrote this to try out WinForms on the VS 2022 Preview (now RC 1)
using .NET 6. Note that Visual Studio 2022 has been released as has
.NET 6.

I added the ability to scale a life "cell" from 1 to 16. This means
that a scale of 4 will draw a live cell that is 4 pixels high and 4
pixels wide. It can be thought of as a zoom.

As with most of my public GitHub repositories, I'm licensing this
program under the GPL 3 license. The two classes I mention below that
are from StackOverflow.com are covered under the MIT license.

Attributions:

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

Program change history is at the top of the YALife.cs code which is where
I prefer to keep change history.

I do not (currently) have a package and install for this program. For now,
it needs to be loaded into Visual Studio 2022 and run. I find it difficult
to pay for an expensive code signing license for what is in effect a free
program I wrote as a hobby/fun/learning/testing project. 

Dan Rhea, 2/2/2022 
