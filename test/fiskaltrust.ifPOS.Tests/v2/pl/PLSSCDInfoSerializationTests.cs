#if !WCF
using System;
using System.Collections.Generic;
using fiskaltrust.ifPOS.v2.pl;
using NUnit.Framework;
using Newtonsoft.Json;

namespace fiskaltrust.Middleware.Interface.Tests.v2.pl
{
    [TestFixture]
    public class PLSSCDInfoSerializationTests
    {
        private PLSSCDInfo CreateTestPLSSCDInfo()
        {
            return new PLSSCDInfo
            {
                DeviceSerialNumber = "ABC1234567",
                UniqueDeviceNumber = "ZAS1234567890",
                RegistrationNumber = "1234567890",
                FiscalizationState = FiscalizationState.Fiscalized,
                VatRateTable = new List<PLVatRateTableEntry>
                {
                    new PLVatRateTableEntry { PtuSlot = "A", VatRatePercent = 23m },
                    new PLVatRateTableEntry { PtuSlot = "B", VatRatePercent = 8m },
                    new PLVatRateTableEntry { PtuSlot = "C", VatRatePercent = 5m },
                    new PLVatRateTableEntry { PtuSlot = "D", VatRatePercent = 0m },
                    new PLVatRateTableEntry { PtuSlot = "G", IsExempt = true }
                },
                CrkReachable = true,
                CrkLastTransmission = new DateTime(2026, 7, 1, 10, 30, 0, DateTimeKind.Utc),
                EReceiptCapable = true,
                CurrentZReportNumber = 42
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestPLSSCDInfo();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<PLSSCDInfo>(json);
            AssertPLSSCDInfosEqual(original, deserialized);
        }

        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestPLSSCDInfo();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<PLSSCDInfo>(json);
            AssertPLSSCDInfosEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            var item = CreateTestPLSSCDInfo();
            // Newtonsoft writes whole decimals with a trailing ".0" while System.Text.Json does not,
            // so a fractional rate is used when comparing the raw output of both serializers.
            item.VatRateTable = new List<PLVatRateTableEntry>
            {
                new PLVatRateTableEntry { PtuSlot = "A", VatRatePercent = 23.5m }
            };
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }

        [Test]
        public void EmptyInfo_SerializesCorrectly()
        {
            var original = new PLSSCDInfo();
            var json = JsonConvert.SerializeObject(original);
            var deserialized = JsonConvert.DeserializeObject<PLSSCDInfo>(json);

            Assert.IsNotNull(deserialized);
            Assert.IsNull(deserialized.DeviceSerialNumber);
            Assert.IsNull(deserialized.FiscalizationState);
            Assert.IsNull(deserialized.VatRateTable);
            Assert.IsNull(deserialized.CrkReachable);
            Assert.IsNull(deserialized.CrkLastTransmission);
            Assert.IsNull(deserialized.CurrentZReportNumber);
        }

        [Test]
        public void FiscalizationState_AllValues_RoundTrip()
        {
            foreach (FiscalizationState state in Enum.GetValues(typeof(FiscalizationState)))
            {
                var original = new PLSSCDInfo { FiscalizationState = state };
                var json = System.Text.Json.JsonSerializer.Serialize(original);
                var deserialized = System.Text.Json.JsonSerializer.Deserialize<PLSSCDInfo>(json);
                Assert.AreEqual(state, deserialized.FiscalizationState);
            }
        }

        private void AssertPLSSCDInfosEqual(PLSSCDInfo expected, PLSSCDInfo actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.DeviceSerialNumber, actual.DeviceSerialNumber);
            Assert.AreEqual(expected.UniqueDeviceNumber, actual.UniqueDeviceNumber);
            Assert.AreEqual(expected.RegistrationNumber, actual.RegistrationNumber);
            Assert.AreEqual(expected.FiscalizationState, actual.FiscalizationState);
            Assert.AreEqual(expected.CrkReachable, actual.CrkReachable);
            Assert.AreEqual(expected.CrkLastTransmission, actual.CrkLastTransmission);
            Assert.AreEqual(expected.EReceiptCapable, actual.EReceiptCapable);
            Assert.AreEqual(expected.CurrentZReportNumber, actual.CurrentZReportNumber);
            Assert.AreEqual(expected.VatRateTable?.Count, actual.VatRateTable?.Count);
            if (expected.VatRateTable != null)
            {
                for (var i = 0; i < expected.VatRateTable.Count; i++)
                {
                    Assert.AreEqual(expected.VatRateTable[i].PtuSlot, actual.VatRateTable[i].PtuSlot);
                    Assert.AreEqual(expected.VatRateTable[i].VatRatePercent, actual.VatRateTable[i].VatRatePercent);
                    Assert.AreEqual(expected.VatRateTable[i].IsExempt, actual.VatRateTable[i].IsExempt);
                }
            }
        }
    }
}
#endif
