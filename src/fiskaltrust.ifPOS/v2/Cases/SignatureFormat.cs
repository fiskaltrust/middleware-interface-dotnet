namespace fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;

public enum SignatureFormat : long
{
    Unknown = 0x0000,
    Text = 0x0001,
    Link = 0x0002,
    QRCode = 0x0003,
    Code128 = 0x0004,
    OcrA = 0x0005,
    Pdf417 = 0x0006,
    DataMatrix = 0x0007,
    Aztec = 0x0008,
    Ean8Barcode = 0x0009,

    Ean13 = 0x000A,
    UPCA = 0x000B,
    Code39 = 0x000C,
    Base64 = 0x000D
}