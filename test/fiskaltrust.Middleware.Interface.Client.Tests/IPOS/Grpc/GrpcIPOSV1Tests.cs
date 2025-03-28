﻿using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Grpc;
using fiskaltrust.Middleware.Interface.Client.Tests.Helpers;
using fiskaltrust.Middleware.Interface.Tests.Helpers.Grpc;
using FluentAssertions;
using Grpc.Core;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Tests.IPOS.Grpc
{
    public class GrpcIPOSV1Tests : IPOSV1Tests
    {
        private string _host = "localhost";
        private int _port = 40142;
        private static Server _server;

        ~GrpcIPOSV1Tests()
        {
            Task.Run(() => _server.ShutdownAsync()).Wait();
            _server = null;
        }

        protected override ifPOS.v1.IPOS CreateClient() => GrpcPosFactory.CreatePosAsync(new GrpcClientOptions { Url = new Uri($"http://{_host}:{_port}") }).Result;

        protected override void StartHost()
        {
            if (_server == null)
                _server = GrpcHelper.StartHost(_host, _port, new DummyPOSV1());
        }

        protected override void StopHost()
        {
            Task.Run(() => _server.ShutdownAsync()).Wait();
            _server = null;
        }

        [Test]
        public async Task Journal()
        {
            var client = CreateClient();

            using (var stream = File.OpenWrite("JournalData.json"))
            {
                await foreach (var chunk in client.JournalAsync(new JournalRequest()))
                {
                    var write = chunk.Chunk.ToArray();
                    await stream.WriteAsync(write, 0, write.Length);
                }
            }

            var chunkSize = 4096;
            byte[] expectedBuffer = new byte[chunkSize];
            byte[] actualBuffer = new byte[chunkSize];
            int bytesRead;
            using (var actualStream = File.OpenRead("JournalData.json"))
            {
                using (var expectedStream = File.OpenRead("TestData.json"))
                {
                    while ((bytesRead = await expectedStream.ReadAsync(expectedBuffer, 0, expectedBuffer.Length)) > 0)
                    {
                        await actualStream.ReadAsync(actualBuffer, 0, actualBuffer.Length);
                        actualBuffer.Should().BeEquivalentTo(expectedBuffer);

                        expectedBuffer = new byte[chunkSize];
                        actualBuffer = new byte[chunkSize];
                    }
                }
            }
        }
    }
}
