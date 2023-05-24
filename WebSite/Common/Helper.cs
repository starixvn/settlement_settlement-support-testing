using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WebSite.Common
{
    public static class Helper
    {
        public static bool IsNumber(object value)
        {
            if (value == null)
            {
                return false;
            }

            double number;
            return double.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out number);
        }

        public static string GeneralXmlString<T>(T t) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true
            };
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(writer, t, ns);
                    return stringWriter.ToString();
                }
            }
        }
    }
}