// Copyright 2022 Exubit Ltd.

using System.Drawing;
using System.Runtime.Versioning;
using Exubit.ImageScanner;
using WIA;

namespace Sample;

internal static class Program
{
    [SupportedOSPlatform("windows")]
    private static void Main()
    {
        WiaImageScanner imageScanner = new()
        {
            Resolution = 300,
            ImageIntent = WiaImageIntent.UnspecifiedIntent,
            OutputImageFormat = WiaImageFormatType.Jpeg,
            DeviceType = WiaDeviceType.ScannerDeviceType,
            ShowDeviceSelectionDialog = true,
            ImageQuality = WiaImageBias.MaximizeQuality
        };

        try
        {
            Image image = imageScanner.Acquire();
            image.Save("image.jpeg");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
