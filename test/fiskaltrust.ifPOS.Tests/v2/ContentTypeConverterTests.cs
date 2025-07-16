using System.Net.Mime;
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.Converters;
using NUnit.Framework;
using Newtonsoft.Json;

#if !WCF
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
  [TestFixture]
  public class ContentTypeConverterTests
  {
    [Test]
    public void NewtonsoftConverter_SerializeContentType_AsString()
    {
      // Arrange
      var contentType = new ContentType("application/json; charset=utf-8");

      // Act
      var json = JsonConvert.SerializeObject(contentType, new ContentTypeNewtonsoftConverter());

      // Assert
      Assert.AreEqual("\"application/json; charset=utf-8\"", json);
    }

    [Test]
    public void NewtonsoftConverter_DeserializeString_AsContentType()
    {
      // Arrange
      var json = "\"application/json; charset=utf-8\"";

      // Act
      var contentType = JsonConvert.DeserializeObject<ContentType>(json, new ContentTypeNewtonsoftConverter());

      // Assert
      Assert.IsNotNull(contentType);
      Assert.AreEqual("application/json", contentType.MediaType);
      Assert.AreEqual("utf-8", contentType.CharSet);
    }

    [Test]
    public void NewtonsoftConverter_SerializeNull_AsNull()
    {
      // Arrange
      ContentType contentType = null;

      // Act
      var json = JsonConvert.SerializeObject(contentType, new ContentTypeNewtonsoftConverter());

      // Assert
      Assert.AreEqual("null", json);
    }

    [Test]
    public void NewtonsoftConverter_DeserializeNull_AsNull()
    {
      // Arrange
      var json = "null";

      // Act
      var contentType = JsonConvert.DeserializeObject<ContentType>(json, new ContentTypeNewtonsoftConverter());

      // Assert
      Assert.IsNull(contentType);
    }

#if !WCF
        [Test]
        public void SystemTextJsonConverter_SerializeContentType_AsString()
        {
            // Arrange
            var contentType = new ContentType("application/json; charset=utf-8");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ContentTypeSystemTextJsonConverter());
            
            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(contentType, options);
            
            // Assert
            Assert.AreEqual("\"application/json; charset=utf-8\"", json);
        }

        [Test]
        public void SystemTextJsonConverter_DeserializeString_AsContentType()
        {
            // Arrange
            var json = "\"application/json; charset=utf-8\"";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ContentTypeSystemTextJsonConverter());
            
            // Act
            var contentType = System.Text.Json.JsonSerializer.Deserialize<ContentType>(json, options);
            
            // Assert
            Assert.IsNotNull(contentType);
            Assert.AreEqual("application/json", contentType.MediaType);
            Assert.AreEqual("utf-8", contentType.CharSet);
        }

        [Test]
        public void SystemTextJsonConverter_SerializeNull_AsNull()
        {
            // Arrange
            ContentType contentType = null;
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ContentTypeSystemTextJsonConverter());
            
            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(contentType, options);
            
            // Assert
            Assert.AreEqual("null", json);
        }

        [Test]
        public void SystemTextJsonConverter_DeserializeNull_AsNull()
        {
            // Arrange
            var json = "null";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ContentTypeSystemTextJsonConverter());
            
            // Act
            var contentType = System.Text.Json.JsonSerializer.Deserialize<ContentType>(json, options);
            
            // Assert
            Assert.IsNull(contentType);
        }
#endif
  }
}
