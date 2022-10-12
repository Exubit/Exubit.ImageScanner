// Copyright 2022 Exubit Ltd.

using System.Drawing;
using WIA;

namespace Exubit.ImageScanner;

/// <summary>
///     Base interface for class can be used to call scanner and acquire images from it using WIA interface in Windows.
/// </summary>
public interface IWiaImageScanner
{
    /// <summary>
    ///     How detailed the scan is, in DPI.
    /// </summary>
    int Resolution { get; set; }

    /// <summary>
    ///     Acquired images intent (Color mode)
    /// </summary>
    WiaImageIntent ImageIntent { get; set; }

    /// <summary>
    ///     Output image format
    /// </summary>
    WiaImageFormatType OutputImageFormat { get; set; }

    /// <summary>
    ///     Device type to work with
    /// </summary>
    WiaDeviceType DeviceType { get; set; }

    /// <summary>
    ///     Show device selection dialog or not
    /// </summary>
    bool ShowDeviceSelectionDialog { get; set; }

    /// <summary>
    ///     Output image quality
    /// </summary>
    WiaImageBias ImageQuality { get; set; }

    /// <summary>
    ///     Acquire image from WIA device
    /// </summary>
    Image Acquire();
}
