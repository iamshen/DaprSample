using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml.Serialization;

namespace System.Xml;

#region Xml助手类

/// <summary>
///     Xml助手类
/// </summary>
public class XmlHelper
{
    #region 对象转xml字符串

    /// <summary>
    ///     对象转xml字符串
    /// </summary>
    /// <param name="xmlDoc">xml文档对象</param>
    /// <returns></returns>
    public static string ConvertXmlToString(XmlDocument xmlDoc)
    {
        var stream = new MemoryStream();
        var writer = new XmlTextWriter(stream, null);
        writer.Formatting = Formatting.Indented;
        xmlDoc.Save(writer);
        var sr = new StreamReader(stream, Encoding.UTF8);
        stream.Position = 0;
        var xmlString = sr.ReadToEnd();
        sr.Close();
        stream.Close();
        return xmlString;
    }

    #endregion

    #region 对象转XmlDocument对象

    /// <summary>
    ///     对象转XmlDocument对象
    /// </summary>
    /// <param name="packet"></param>
    /// <returns></returns>
    public static XmlDocument ConvertObjectToXml(object packet)
    {
        //去除默认命名空间xmlns:xsd和xmlns:xsi
        var Stream = new MemoryStream();
        var ns = new XmlSerializerNamespaces();
        ns.Add("", ""); //把命名空间设置为空，这样就没有命名空间了

        var serialize = new XmlSerializer(packet.GetType());
        //序列化对象
        serialize.Serialize(Stream, packet, ns);
        Stream.Position = 0;
        var sr = new StreamReader(Stream);
        var xml = sr.ReadToEnd();
        sr.Dispose();
        Stream.Dispose();

        var doc = new XmlDocument
        {
            PreserveWhitespace = true
        };
        doc.LoadXml(xml);

        return doc;
    }

    #endregion

    #region Xml签名

    /// <summary>
    ///     Xml签名
    /// </summary>
    /// <param name="xmlDoc">xml文档对象</param>
    /// <param name="referenceId">引用的接点ID</param>
    /// <param name="privateKey">签名的私钥</param>
    public static XmlElement Sha1Signature(ref XmlDocument xmlDoc, string referenceId, AsymmetricAlgorithm privateKey)
    {
        var signedxml = new SignedXml(xmlDoc) { SigningKey = privateKey };
        signedxml.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
        var reference = new Reference
        {
            Uri = $"#{referenceId}",
            DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1"
        };
        var transform = new XmlDsigEnvelopedSignatureTransform();
        reference.AddTransform(transform);
        signedxml.AddReference(reference);
        signedxml.ComputeSignature();
        return signedxml.GetXml();
    }

    #endregion
}

#endregion