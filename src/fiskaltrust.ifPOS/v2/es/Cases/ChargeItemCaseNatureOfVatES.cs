using fiskaltrust.ifPOS.v2.Cases;

namespace fiskaltrust.ifPOS.v2.es.Cases;

public enum ChargeItemCaseNatureOfVatES : long
{
    /// <value><c>0x0000</c></value>
    UsualVatApplies = 0x0000,

    /// <value><c>0x2000</c></value>
    NotSubjectArticle7and14 = 0x2000,
    /// <value><c>0x2100</c></value>
    NotSubjectLocationRules = 0x2100,

    /// <value><c>0x3000</c></value>
    ExteptArticle20 = 0x3000,
    /// <value><c>0x3100</c></value>
    ExteptArticle21 = 0x3100,
    /// <value><c>0x3200</c></value>
    ExteptArticle22 = 0x3200,
    /// <value><c>0x3300</c></value>
    ExteptArticle23And24 = 0x3300,
    /// <value><c>0x3400</c></value>
    ExteptArticle25 = 0x3400,
    /// <value><c>0x3500</c></value>
    ExteptOthers = 0x3500,

    /// <value><c>0x5000</c></value>
    ReverseCharge = 0x5000
}

public static class ChargeItemCaseNatureOfVatESExt
{
    public static bool IsNatureOfVat(this ChargeItemCase self, ChargeItemCaseNatureOfVatES natureOfVatES) => ((long)self & 0xFF00) == (long)natureOfVatES;
    public static ChargeItemCase WithNatureOfVat(this ChargeItemCase self, ChargeItemCaseNatureOfVatES state) => (ChargeItemCase)(((ulong)self & 0xFFFF_FFFF_FFFF_00FF) | (ulong)state);
    public static ChargeItemCaseNatureOfVatES NatureOfVat(this ChargeItemCase self) => (ChargeItemCaseNatureOfVatES)((long)self & 0xFF00);
}
