using System.Collections.Generic;
using AutoFixture;
using fiskaltrust.ifPOS.v2.Cases;
using fiskaltrust.ifPOS.v2.es.Cases;
using FluentAssertions;
using NUnit.Framework;

namespace fiskaltrust.ifPOS.Tests.v2.Cases
{
    public class SignatureTypeTests
    {
        private readonly IFixture _fixture;

        public SignatureTypeTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void FuzzTest_WithTypeES()
        {
            for (var i = 0; i < 1000; i++)
            {
                var signatureType = _fixture.Create<SignatureType>();
                var signatureTypeCase = _fixture.Create<SignatureTypeES>();

                var result = signatureType.WithType(signatureTypeCase);

                SignatureTypeESExt.Type(result).Should().Be((SignatureTypeES)((long)signatureTypeCase & 0xFFFF));
                result.IsType(signatureTypeCase).Should().BeTrue();
            }
        }

        [Test]
        public void FuzzTest_WithFlag()
        {
            for (var i = 0; i < 1000; i++)
            {
                var signatureType = _fixture.Create<SignatureType>();
                var signatureTypeFlag = _fixture.Create<SignatureTypeFlags>();

                var result = signatureType.WithFlag(signatureTypeFlag);

                result.IsFlag(signatureTypeFlag).Should().BeTrue();
            }
        }

        [Test]
        public void FuzzTest_Reset()
        {
            for (var i = 0; i < 1000; i++)
            {
                var signatureType = _fixture.Create<SignatureType>();

                var result = signatureType.Reset();

                result.Should().Be((SignatureType)(0xFFFF_F000_0000_0000 & (ulong)signatureType));
            }
        }

        [Test]
        public void FuzzTest_WithVersion()
        {
            for (var i = 0; i < 1000; i++)
            {
                var signatureType = _fixture.Create<SignatureType>();
                var version = (byte)(_fixture.Create<byte>() >> 4);

                var result = signatureType.WithVersion(version);

                result.Version().Should().Be(version);
            }
        }

        [Test]
        public void FuzzTest_WithCountry()
        {
            foreach (var (country, code) in new List<(string, ulong)> {
                ("AT", 0x4154),
                ("DE", 0x4445),
                ("FR", 0x4652),
                ("IT", 0x4954),
                ("ME", 0x4D45),
                ("ES", 0x4752),
                ("GR", 0x4752),
                ("PT", 0x5054),
                })
            {
                var signatureType = _fixture.Create<SignatureType>();

                var resultCode = signatureType.WithCountry(code);
                resultCode.CountryCode().Should().Be(code);

                var resultString = signatureType.WithCountry(country);
                resultString.Country().Should().Be(country);
            }
        }
    }
}
