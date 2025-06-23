namespace fiskaltrust.ifPOS.v2.Cases;

public enum ChargeItemCaseTypeOfService
{
    /// <value><c>0x00</c></value>
    UnknownService = 0x00,
    /// <value><c>0x10</c></value>
    Delivery = 0x10,
    /// <value><c>0x20</c></value>
    OtherService = 0x20,
    /// <value><c>0x30</c></value>
    Tip = 0x30,
    /// <value><c>0x40</c></value>
    Voucher = 0x40,
    /// <value><c>0x50</c></value>
    CatalogService = 0x50,
    /// <value><c>0x60</c></value>
    NotOwnSales = 0x60,
    /// <value><c>0x70</c></value>
    OwnConsumption = 0x70,
    /// <value><c>0x80</c></value>
    Grant = 0x80,
    /// <value><c>0x90</c></value>
    Receivable = 0x90,
    /// <value><c>0xA0</c></value>
    CashTransfer = 0xA0,
}

public static class ChargeItemCaseTypeOfServiceExt
{
    public static ChargeItemCase WithTypeOfService(this ChargeItemCase self, ChargeItemCaseTypeOfService typeOfService) => (ChargeItemCase)(((ulong)self & 0xFFFF_FFFF_FFFF_FF0F) | (ulong)typeOfService);
    public static bool IsTypeOfService(this ChargeItemCase self, ChargeItemCaseTypeOfService typeOfService) => ((ulong)self & 0xF0) == (ulong)typeOfService;
    public static ChargeItemCaseTypeOfService TypeOfService(this ChargeItemCase self) => (ChargeItemCaseTypeOfService)((ulong)self & 0xF0);
}