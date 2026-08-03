using fiskaltrust.ifPOS.v2.Cases;

namespace fiskaltrust.ifPOS.v2.fr.Cases;

public enum ChargeItemCaseNatureOfVatFR : long
{

}

public static class ChargeItemCaseNatureOfVatFRExt
{
    public static bool IsNatureOfVat(this ChargeItemCase self, ChargeItemCaseNatureOfVatFR natureOfVatFR) => ((long)self & 0xFF00) == (long)natureOfVatFR;
    public static ChargeItemCase WithNatureOfVat(this ChargeItemCase self, ChargeItemCaseNatureOfVatFR state) => (ChargeItemCase)(((ulong)self & 0xFFFF_FFFF_FFFF_00FF) | (ulong)state);
    public static ChargeItemCaseNatureOfVatFR NatureOfVat(this ChargeItemCase self) => (ChargeItemCaseNatureOfVatFR)((long)self & 0xFF00);
}
