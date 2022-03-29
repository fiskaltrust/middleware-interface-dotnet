using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class InvoiceType
    {
        public SupplyDateOrPeriodType SupplyDateOrPeriod { get; set; }
        public CorrectiveInvType CorrectiveInv { get; set; }
        public PayMethodType[] PayMethods { get; set; }
        public CurrencyType Currency { get; set; }
        public SellerType Seller { get; set; }
        public BuyerType Buyer { get; set; }
        public InvoiceItemType[] Items { get; set; }
        public SameTaxType[] SameTaxes { get; set; }
        public ApprovalType[] Approvals { get; set; }
        public FeeType[] Fees { get; set; }
        public IICRefType[] IICRefs { get; set; }
        public BadDebtInvType BadDebtInv { get; set; }
        public InvoiceTSType InvType { get; set; }
        public InvoiceSType TypeOfInv { get; set; }
        public SelfIssSType TypeOfSelfIss { get; set; }
        public bool TypeOfSelfIssSpecified { get; set; }
        public DateTime IssueDateTime { get; set; }
        public string InvNum { get; set; }
        public int InvOrdNum { get; set; }
        public string TCRCode { get; set; }
        public bool IsIssuerInVAT { get; set; }
        public decimal TaxFreeAmt { get; set; }
        public bool TaxFreeAmtSpecified { get; set; }
        public decimal MarkUpAmt { get; set; }
        public bool MarkUpAmtSpecified { get; set; }
        public decimal GoodsExAmt { get; set; }
        public bool GoodsExAmtSpecified { get; set; }
        public decimal TotPriceWoVAT { get; set; }
        public decimal TotVATAmt { get; set; }
        public bool TotVATAmtSpecified { get; set; }
        public decimal TotPrice { get; set; }
        public decimal TotPriceToPay { get; set; }
        public bool TotPriceToPaySpecified { get; set; }
        public string OperatorCode { get; set; }
        public string BusinUnitCode { get; set; }
        public string SoftCode { get; set; }
        public string IIC { get; set; }
        public string IICSignature { get; set; }
        public bool IsReverseCharge { get; set; }
        public bool IsReverseChargeSpecified { get; set; }
        public DateTime PayDeadline { get; set; }
        public bool PayDeadlineSpecified { get; set; }
        public string ParagonBlockNum { get; set; }
        public string TaxPeriod { get; set; }
        public string BankAccNum { get; set; }
        public string Note { get; set; }
    }
}