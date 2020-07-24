using System;
using System.Linq;

namespace fiskaltrust.Middleware.Interface.Client.Helpers.ModelConverters
{
    static class Helper
    {
        public static T[] ConvertArray<U, T>(U[] from, Func<U, T> converter)
        {
            if(from == null) return null;

            return from.Select(item => converter(item)).ToArray();
        }
    }
    public static class v0
    {
        public static ifPOS.v0.ReceiptRequest ReceiptRequest(ifPOS.v1.ReceiptRequest from)
        {
            return new ifPOS.v0.ReceiptRequest()
            {
                ftCashBoxID = from.ftCashBoxID,
                ftQueueID = from.ftQueueID,
                ftPosSystemId = from.ftPosSystemId,
                cbTerminalID = from.cbTerminalID,
                cbReceiptReference = from.cbReceiptReference,
                cbReceiptMoment = from.cbReceiptMoment,
                cbChargeItems = Helper.ConvertArray(from.cbChargeItems, ChargeItem),
                cbPayItems = Helper.ConvertArray(from.cbPayItems, PayItem),
                ftReceiptCase = from.ftReceiptCase,
                ftReceiptCaseData = from.ftReceiptCaseData,
                cbReceiptAmount = from.cbReceiptAmount,
                cbUser = from.cbUser,
                cbArea = from.cbArea,
                cbCustomer = from.cbCustomer,
                cbSettlement = from.cbSettlement,
                cbPreviousReceiptReference = from.cbPreviousReceiptReference,
            };
        }

        public static ifPOS.v0.ChargeItem ChargeItem(ifPOS.v1.ChargeItem from)
        {
            return new ifPOS.v0.ChargeItem()
            {
                Position = from.Position,
                Quantity = from.Quantity,
                Description = from.Description,
                Amount = from.Amount,
                VATRate = from.VATRate,
                ftChargeItemCase = from.ftChargeItemCase,
                ftChargeItemCaseData = from.ftChargeItemCaseData,
                VATAmount = from.VATAmount,
                AccountNumber = from.AccountNumber,
                CostCenter = from.CostCenter,
                ProductGroup = from.ProductGroup,
                ProductNumber = from.ProductNumber,
                ProductBarcode = from.ProductBarcode,
                Unit = from.Unit,
                UnitQuantity = from.UnitQuantity,
                UnitPrice = from.UnitPrice,
                Moment = from.Moment,
            };
        }

        public static ifPOS.v0.PayItem PayItem(ifPOS.v1.PayItem from)
        {
            return new ifPOS.v0.PayItem()
            {
                Position = from.Position,
                Quantity = from.Quantity,
                Description = from.Description,
                Amount = from.Amount,
                ftPayItemCase = from.ftPayItemCase,
                ftPayItemCaseData = from.ftPayItemCaseData,
                AccountNumber = from.AccountNumber,
                CostCenter = from.CostCenter,
                MoneyGroup = from.MoneyGroup,
                MoneyNumber = from.MoneyNumber,
                Moment = from.Moment,
            };
        }
    }

    public static class v1
    {
        public static ifPOS.v1.ReceiptResponse ReceiptResponse(ifPOS.v0.ReceiptResponse from)
        {
            return new ifPOS.v1.ReceiptResponse()
            {
                ftCashBoxID = from.ftCashBoxID,
                ftQueueID = from.ftQueueID,
                ftQueueItemID = from.ftQueueItemID,
                ftQueueRow = from.ftQueueRow,
                cbTerminalID = from.cbTerminalID,
                cbReceiptReference = from.cbReceiptReference,
                ftCashBoxIdentification = from.ftCashBoxIdentification,
                ftReceiptIdentification = from.ftReceiptIdentification,
                ftReceiptMoment = from.ftReceiptMoment,
                ftReceiptHeader = from.ftReceiptHeader,
                ftChargeItems = Helper.ConvertArray(from.ftChargeItems, ChargeItem),
                ftChargeLines = from.ftChargeLines,
                ftPayItems = Helper.ConvertArray(from.ftPayItems, PayItem),
                ftPayLines = from.ftPayLines,
                ftSignatures = Helper.ConvertArray(from.ftSignatures, SignatureItem),
                ftReceiptFooter = from.ftReceiptFooter,
                ftState = from.ftState,
                ftStateData = from.ftStateData,
            };
        }

        public static ifPOS.v1.ChargeItem ChargeItem(ifPOS.v0.ChargeItem from)
        {
            return new ifPOS.v1.ChargeItem
            {
                Position = from.Position,
                Quantity = from.Quantity,
                Description = from.Description,
                Amount = from.Amount,
                VATRate = from.VATRate,
                ftChargeItemCase = from.ftChargeItemCase,
                ftChargeItemCaseData = from.ftChargeItemCaseData,
                VATAmount = from.VATAmount,
                AccountNumber = from.AccountNumber,
                CostCenter = from.CostCenter,
                ProductGroup = from.ProductGroup,
                ProductNumber = from.ProductNumber,
                ProductBarcode = from.ProductBarcode,
                Unit = from.Unit,
                UnitQuantity = from.UnitQuantity,
                UnitPrice = from.UnitPrice,
                Moment = from.Moment,
            };
        }

        public static ifPOS.v1.PayItem PayItem(ifPOS.v0.PayItem from)
        {
            return new ifPOS.v1.PayItem()
            {
                Position = from.Position,
                Quantity = from.Quantity,
                Description = from.Description,
                Amount = from.Amount,
                ftPayItemCase = from.ftPayItemCase,
                ftPayItemCaseData = from.ftPayItemCaseData,
                AccountNumber = from.AccountNumber,
                CostCenter = from.CostCenter,
                MoneyGroup = from.MoneyGroup,
                MoneyNumber = from.MoneyNumber,
                Moment = from.Moment,
            };
        }

        public static ifPOS.v1.SignaturItem SignatureItem(ifPOS.v0.SignaturItem from)
        {
            return new ifPOS.v1.SignaturItem()
            {
                ftSignatureFormat = from.ftSignatureFormat,
                ftSignatureType = from.ftSignatureType,
                Caption = from.Caption,
                Data = from.Data,
            };
        }
    }
}
