// Copyright 2022 Exubit Ltd.

using System.Drawing;
using System.Runtime.Versioning;
using WIA;

namespace Exubit.ImageScanner;

/// <summary>
///     This class can be used to call scanner and acquire images from it using WIA interface in Windows.
/// </summary>
public class WiaImageScanner : IWiaImageScanner
{
    private const string HorizontalResolutionIndex = "6147";
    private const string VerticalResolutionIndex = "6148";

    /// <summary>
    ///     How detailed the scan is, in DPI. Default is 200.
    /// </summary>
    public int Resolution { get; set; } = 200;

    /// <summary>
    ///     Acquired images intent (Color mode).
    /// </summary>
    public WiaImageIntent ImageIntent { get; set; } = WiaImageIntent.ColorIntent;

    /// <summary>
    ///     Output image format. Default is PNG.
    /// </summary>
    public WiaImageFormatType OutputImageFormat { get; set; } = WiaImageFormatType.Png;

    /// <summary>
    ///     Device types to work with. Default is a scanner device
    /// </summary>
    public WiaDeviceType DeviceType { get; set; } = WiaDeviceType.ScannerDeviceType;

    /// <summary>
    ///     Show device selection dialog or not (true by default).
    /// </summary>
    public bool ShowDeviceSelectionDialog { get; set; } = true;

    /// <summary>
    ///     Output image quality. Maximize quality by default.
    /// </summary>
    public WiaImageBias ImageQuality { get; set; } = WiaImageBias.MaximizeQuality;

    /// <summary>
    ///     Acquire image from WIA device
    /// </summary>
    [SupportedOSPlatform("windows")]
    public Image Acquire() => AcquireImages()[0];

    [SupportedOSPlatform("windows")]
    private List<Image> AcquireImages()
    {
        List<Image> images = new();
        CommonDialog commonDialog;

        try
        {
            commonDialog = new CommonDialogClass();
        }
        catch
        {
            throw new WiaException("No WIA devices are available to work with");
        }

        Device device;
        try
        {
            device = commonDialog.ShowSelectDevice(DeviceType, ShowDeviceSelectionDialog, true);
        }
        catch (Exception ex)
        {
            throw new WiaException("WIA 2: No imaging input devices were found. Please plug in one\n" +
                                   ex.InnerException?.Message);
        }

        if (device == null)
        {
            throw new WiaException("WIA 2: no devices plugged in. Please plug-in at least one");
        }

        Items items = commonDialog.ShowSelectItems(device, ImageIntent, ImageQuality, false);
        try
        {
            if (items == null || items.Count == 0)
            {
                throw new WiaException("WIA 2: No image acquired as no image was selected");
            }

            foreach (Item item in items)
            {
                item.Properties.get_Item(HorizontalResolutionIndex).set_Value(Resolution);
                item.Properties.get_Item(VerticalResolutionIndex).set_Value(Resolution);
                ImageFile image = (ImageFile)commonDialog.ShowTransfer(item, GetFormatId(OutputImageFormat));
                byte[] imageBytes = (byte[])image.FileData.get_BinaryData();
                MemoryStream ms = new(imageBytes);
                images.Add(Image.FromStream(ms));
            }

            return images;
        }
        catch (Exception ex)
        {
            throw new WiaException("Error:" + ex.Message);
        }
    }

    private static string GetFormatId(WiaImageFormatType wiaImageFormatType) =>
        wiaImageFormatType switch
        {
            WiaImageFormatType.Bmp => FormatID.wiaFormatBMP,
            WiaImageFormatType.Gif => FormatID.wiaFormatGIF,
            WiaImageFormatType.Jpeg => FormatID.wiaFormatJPEG,
            WiaImageFormatType.Png => FormatID.wiaFormatPNG,
            WiaImageFormatType.Tiff => FormatID.wiaFormatTIFF,
            _ => "{00000000-0000-0000-0000-000000000000}"
        };
}
