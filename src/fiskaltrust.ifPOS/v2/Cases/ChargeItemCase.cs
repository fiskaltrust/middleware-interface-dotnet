namespace fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;

public enum ChargeItemCase : long
{
    UnknownService = 0x0,  // Unknown type of service for IT (1.3.45)
    DiscountedVatRate1 = 0x1,  // Discounted-1 VAT rate (as of 1.1.2022, this is 10%) (1.3.45)
    DiscountedVatRate2 = 0x2,  // Discounted 2 VAT rate (as of 1.1.2022, this is 5%) (1.3.45)
    NormalVatRate = 0x3,  // Normal VAT rate (as of 1.1.2022, this is 22%) (1.3.45)
    SuperReducedVatRate1 = 0x4,  // Super reduced 1 VAT rate (1.3.45)
    SuperReducedVatRate2 = 0x5,  // Super reduced 2 VAT rate (1.3.45)
    ParkingVatRate = 0x6,  // Parking VAT rate, Reversal of tax liability (1.3.45)
    ZeroVatRate = 0x7,  // Zero VAT rate (1.3.45)
    NotTaxable = 0x8  // Not taxable (for processing, see 0x4954000000000001) (1.3.45)
}
