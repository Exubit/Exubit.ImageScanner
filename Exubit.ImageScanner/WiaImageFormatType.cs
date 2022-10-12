// Copyright 2022 Exubit Ltd.

namespace Exubit.ImageScanner;

/// <summary>
///     Image format to save acquired images to.
/// </summary>
public enum WiaImageFormatType
{
    /// <summary>
    ///     (0) BMP.
    /// </summary>
    Bmp,

    /// <summary>
    ///     (1) PNG.
    /// </summary>
    Png,

    /// <summary>
    ///     (2) GIF.
    /// </summary>
    Gif,

    /// <summary>
    ///     (3) JPG.
    /// </summary>
    Jpeg,

    /// <summary>
    ///     (4) TIFF.
    /// </summary>
    Tiff
}
