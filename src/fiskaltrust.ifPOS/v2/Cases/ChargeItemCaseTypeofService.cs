namespace fiskaltrust.ifPOS.v2.Cases;

public enum ChargeItemCaseTypeOfService
{
    /// <remarks>
    /// Value: 0x00
    /// </remarks>
    UnknownService = 0x00,
    /// <remarks>
    /// Value: 0x10
    /// </remarks>
    Delivery = 0x10,
    /// <remarks>
    /// Value: 0x20
    /// </remarks>
    OtherService = 0x20,
    /// <remarks>
    /// Value: 0x30
    /// </remarks>
    Tip = 0x30,
    /// <remarks>
    /// Value: 0x40
    /// </remarks>
    Voucher = 0x40,
    /// <remarks>
    /// Value: 0x50
    /// </remarks>
    CatalogService = 0x50,
    /// <remarks>
    /// Value: 0x60
    /// </remarks>
    NotOwnSales = 0x60,
    /// <remarks>
    /// Value: 0x70
    /// </remarks>
    OwnConsumption = 0x70,
    /// <remarks>
    /// Value: 0x80
    /// </remarks>
    Grant = 0x80,
    /// <remarks>
    /// Value: 0x90
    /// </remarks>
    Receivable = 0x90,
    /// <remarks>
    /// Value: 0xA0
    /// </remarks>
    CashTransfer = 0xA0,
}

public static class ChargeItemCaseTypeOfServiceExt
{
    public static ChargeItemCase WithTypeOfService(this ChargeItemCase self, ChargeItemCaseTypeOfService typeOfService) => (ChargeItemCase)(((ulong)self & 0xFFFF_FFFF_FFFF_FF0F) | (ulong)typeOfService);
    public static bool IsTypeOfService(this ChargeItemCase self, ChargeItemCaseTypeOfService typeOfService) => ((long)self & 0xF0) == (long)typeOfService;
    public static ChargeItemCaseTypeOfService TypeOfService(this ChargeItemCase self) => (ChargeItemCaseTypeOfService)((long)self & 0xF0);
}