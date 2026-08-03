# fiskaltrust.ifPOS v2 Case Builder API

The fiskaltrust.ifPOS v2 Case Builder API provides a fluent, type-safe way to create and manipulate case values used throughout the middleware interface.
These case values follow the format `0xXXXX_XXXX_XXXX_XXXX`.

The semantics of the individual case values (receipt cases, VAT rates, payment types, flags, etc.) are defined in the official [reference tables](https://docs.fiskaltrust.cloud/docs/poscreators/middleware-doc/general/reference-tables). This document describes the .NET API for building and reading these values; use it together with the reference tables, which remain the source of truth for what each value means.

## Overview

The case builder system consists of:
- **Base enums** that define fundamental case types (e.g., `ChargeItemCase`, `ReceiptCase`, `PayItemCase`, `State`)
- **Flag enums** that can be combined with base cases using bitwise operations (e.g., `ReceiptCaseFlags`, `PayItemCaseFlags`, `StateFlags`)
- **Sub-type enums** that define sub-categories within case types (e.g., `ReceiptCaseType`, `ChargeItemCaseTypeOfService`, `SignatureTypeCategory`)
- **Extension methods** that provide a fluent API for building and reading case values

Every enum member corresponds to an entry in the reference tables; the XML documentation on each member shows its raw value.

## Basic Concepts

### Case Format Structure

All cases follow the official `CCCC_vlll_gggg_xxxx` 64-bit tagging format:
```
0xCCCC_vlll_gggg_xxxx
  ││││ ││││ ││││ ││││
  ││││ ││││ ││││ └┴┴┴─ xxxx: Case-specific bits (0-15)
  ││││ ││││ └┴┴┴────── gggg: Global flags (16-31)
  ││││ │└┴┴─────────── lll:  Local, country-specific flags (32-43)
  ││││ └────────────── v:    Version of the tagging system (44-47, currently 2)
  └┴┴┴──────────────── CCCC: Country code (48-63)
```

Each section of the format has dedicated builder and reader methods:

| Section | Meaning | Builder methods | Reader methods |
|---------|---------|-----------------|----------------|
| `CCCC` | ASCII of a two-letter [ISO 3166-1](https://en.wikipedia.org/wiki/ISO_3166-1) country code (e.g. `ES` = `0x4553`) | `WithCountry(...)` | `Country()`, `CountryCode()` |
| `v` | Version of the tagging system (currently `2`) | `WithVersion(...)` | `Version()` |
| `lll` | Local, country-specific flags | – (no general API, set bits manually) | – |
| `gggg` | Global flags; change behavior but keep the semantic meaning (e.g. void, refund) | `WithFlag(...)` | `IsFlag(...)` |
| `xxxx` | Case-specific value; layout differs per case type (see below) | `WithCase(...)`, `WithVat(...)`, `WithType(...)`, … | `Case()`, `Vat()`, `Type()`, … |

For example, `0x4553_2000_0000_0001` is country `ES` (`0x4553`), version `2`, with the case-specific value `0x0001`.

> **Note:** `State` deviates from this layout. Its format is `CCCC_vlll_gggg_gggg`: the base state occupies the full lower 32 bits, and `StateFlags` are located inside that range. See [State](#state) below.

### Working with Values Not (Yet) Exposed as Enum Members

All case enums are backed by integer types, so any value from the reference tables can be built even when no named enum member exists for it. Cast the raw value and continue with the fluent API:

```csharp
var receiptCase = ((ReceiptCase)0x3011)
    .WithCountry("DE")
    .WithVersion(2);
```

The same applies to flags and to the local flags section `lll`, which has no general API:

```csharp
// Set a raw global flag
var withRawFlag = (ChargeItemCase)((ulong)chargeCase | 0x0000_0000_8000_0000);

// Set a local (country-specific) flag in the lll section (bits 32-43)
var withLocalFlag = (ReceiptCase)((ulong)receiptCase | (0x001UL << 32));
```

## API Usage Examples

### Building Cases

The fluent API allows you to chain operations to build complete case values:

```csharp
// Build a charge item case with all attributes
var chargeCase = ChargeItemCase.NormalVatRate
    .WithCountry("IT")
    .WithVersion(2)
    .WithFlag(ChargeItemCaseFlags.Refund)
    .WithTypeOfService(ChargeItemCaseTypeOfService.Tip);

// Build a receipt case with country and version
var receiptCase = ReceiptCase.PointOfSaleReceipt0x0001
    .WithCountry("AT")
    .WithVersion(2);

// Build a payment case
var paymentCase = PayItemCase.CashPayment
    .WithCountry("DE")
    .WithVersion(2)
    .WithFlag(PayItemCaseFlags.Tip);
```

### Reading Case Information

Extract information from existing cases using the reader methods:

```csharp
// Check specific conditions
if (receiptResponse.ftState.IsState(State.Error))
{
    // Handle error state
}

if (receiptResponse.ftState.IsFlag(StateFlags.MessageIsPending))
{
    // Handle pending message
}

// Check the receipt case category and specific case
if (receiptRequest.ftReceiptCase.IsType(ReceiptCaseType.DailyOperations))
{
    // Handle daily operations (zero receipt, daily closing, ...)
}

if (receiptRequest.ftReceiptCase.IsCase(ReceiptCase.PointOfSaleReceipt0x0001))
{
    // Handle point-of-sale receipt
}

// Extract specific values
string country = chargeItemCase.Country();
byte version = chargeItemCase.Version();
bool isRefund = chargeItemCase.IsFlag(ChargeItemCaseFlags.Refund);
var vatRate = chargeItemCase.Vat();
var serviceType = chargeItemCase.TypeOfService();

// Check VAT type
if (chargeItemCase.IsVat(ChargeItemCase.NormalVatRate))
{
    // Handle normal VAT rate
}

// Check service type
if (chargeItemCase.IsTypeOfService(ChargeItemCaseTypeOfService.Tip))
{
    // Handle tip service
}
```

## Fluent API Methods

### General Builder Methods (WithXxx)

These methods are available on all case types and create new case instances with additional information:

- `WithCountry(string country)` - Sets the country code using 2-letter ISO code (e.g., "DE", "AT", "IT")
- `WithCountry(ulong country)` - Sets the country code as numeric value
- `WithVersion(byte version)` - Sets the version number (0-15); use `2` for the current tagging system

### General Reader Methods

These methods extract common information from all case types:

- `Country()` - Gets the country as a string (returns null if not set)
- `CountryCode()` - Gets the country as a numeric code
- `Version()` - Gets the version number

### Case-Specific Builder Methods

#### ChargeItemCase

**Format**: `CCCC_vlll_gggg_NNSV`, where `V` is the VAT rate, `S` the type of service, and `NN` the nature of VAT.

- `WithVat(ChargeItemCase vat)` - Sets the VAT rate `V` (lowest nibble), e.g. `NormalVatRate`, `NotTaxable`
- `WithTypeOfService(ChargeItemCaseTypeOfService service)` - Sets the type of service `S` (second nibble), e.g. `Delivery`, `Tip`, `Voucher`
- `WithNatureOfVat(...)` - Sets the nature of VAT `NN` (bits 8-15); country-specific, see [Country-Specific Cases](#country-specific-cases)
- `WithFlag(ChargeItemCaseFlags flag)` - Adds flags like `Void`, `Refund`, `ExtraOrDiscount`

#### ReceiptCase

**Format**: `CCCC_vlll_gggg_txcc`, where `t` is the receipt case type (category) and `txcc` as a whole is the receipt case.

- `WithCase(ReceiptCase case)` - Sets the case portion `txcc` (lowest 16 bits)
- `WithType(ReceiptCaseType type)` - Sets the case category `t` (highest nibble of the case portion): `Receipt`, `Invoice`, `DailyOperations`, `Log`, `Lifecycle`
- `WithFlag(ReceiptCaseFlags flag)` - Adds receipt-specific flags. The reference tables group these flags by receipt category (general, POS receipt/invoice, zero receipt, lifecycle); the API exposes them all in the single `ReceiptCaseFlags` enum, so apply only flags that are meaningful for the receipt category you are building.

#### PayItemCase

**Format**: `CCCC_vlll_gggg_xxPP`, where `PP` is the payment type.

- `WithCase(PayItemCase case)` - Sets the payment type `PP` (lowest byte), e.g. `CashPayment`, `DebitCardPayment`
- `WithFlag(PayItemCaseFlags flag)` - Adds payment-specific flags

#### SignatureType

**Format**: `CCCC_vlll_gggg_tsss`, where `t` is the category and `sss` the signature case. Signature cases are market-specific and exposed as `SignatureTypeXX` enums, see [Country-Specific Cases](#country-specific-cases).

- `WithCategory(SignatureTypeCategory category)` - Sets the category `t`: `Uncategorized`, `Information`, `Alert`, `Failure`
- `WithFlag(SignatureTypeFlags flag)` - Adds flags like `ArchivingRequired`, `DontVisualize`

#### SignatureFormat

**Format**: `0x0000_0000_000p_ffff`, where `ffff` is the format and `p` where the signature is positioned on the receipt.

- `WithFormat(SignatureFormat format)` - Sets the format portion `ffff` (lowest 16 bits), e.g. `QRCode`, `Text`
- `WithPosition(SignatureFormatPosition position)` - Sets the position `p`, e.g. `AfterHeader`; the default (`0`) is `AfterPayItemBlockBeforeFooter`

#### JournalType

**Format**: `CCCC_vlll_gggg_tjjj`, where `t` is the category and `jjj` the journal case. Market-specific journal types (e.g. `JournalTypeES.VeriFactu`) are distinguished by the country code `CCCC`, see [Country-Specific Cases](#country-specific-cases).

- `WithCase(JournalType case)` - Sets the journal type portion `tjjj` (lowest 16 bits)

#### State

**Format**: `CCCC_vlll_gggg_gggg`, where the full lower 32 bits `gggg_gggg` carry the base state and flags.

- `WithState(State state)` - Sets the base state value (`Success`, `Error`, `Fail`)
- `WithFlag(StateFlags flag)` - Adds state flags like `MessageIsPending`, `DailyClosingIsDue`

### Case-Specific Reader Methods

#### ChargeItemCase
- `IsFlag(ChargeItemCaseFlags flag)` - Checks if a specific flag is set
- `IsVat(ChargeItemCase case)` - Checks if the VAT rate `V` matches the specified case
- `IsTypeOfService(ChargeItemCaseTypeOfService service)` - Checks if the type of service `S` matches
- `Vat()` - Gets the VAT rate `V` of the case
- `TypeOfService()` - Gets the type of service `S`

#### ReceiptCase
- `IsFlag(ReceiptCaseFlags flag)` - Checks if a receipt-specific flag is set
- `IsCase(ReceiptCase case)` - Checks if the case portion `txcc` (lowest 16 bits) matches
- `Case()` - Gets the case portion `txcc`
- `IsType(ReceiptCaseType type)` - Checks if the case belongs to a category `t` (e.g. `Invoice`)
- `Type()` - Gets the case category `t`

#### PayItemCase
- `IsFlag(PayItemCaseFlags flag)` - Checks if a payment-specific flag is set
- `IsCase(PayItemCase case)` - Checks if the payment type `PP` (lowest byte) matches
- `Case()` - Gets the payment type `PP`

#### SignatureType
- `IsFlag(SignatureTypeFlags flag)` - Checks if a signature flag is set
- `IsCategory(SignatureTypeCategory category)` - Checks if the category `t` matches

#### SignatureFormat
- `IsFormat(SignatureFormat format)` - Checks if the format portion `ffff` matches
- `Format()` - Gets the format portion `ffff`
- `IsPosition(SignatureFormatPosition position)` - Checks if the position `p` matches

#### JournalType
- `IsCase(JournalType case)` - Checks if the journal type portion `tjjj` (lowest 16 bits) matches
- `Case()` - Gets the journal type portion `tjjj`

#### State
- `IsFlag(StateFlags flag)` - Checks if a state flag is set
- `IsState(State state)` - Checks if the state matches the specified value
- `State()` - Gets the base state portion

> **Warning:** `StateFlags` are located inside the lower 32 bits that `IsState` compares (official format `CCCC_vlll_gggg_gggg`). A state with flags set will no longer match `State.Success` (`0x0`), so use `IsState(State.Success)` only when you also expect no flags, or check for `IsState(State.Error)` / `IsState(State.Fail)` instead.

### Utility Methods

- `Reset()` - Keeps only the country code `CCCC` and version `v`, clearing local flags, global flags, and case-specific bits
- `As<T>()` - Converts to another enum type
- `AsXxxCase()` - Converts from ulong to specific case type
