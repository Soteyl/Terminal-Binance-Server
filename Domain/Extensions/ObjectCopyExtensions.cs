using System.Runtime.Serialization;

namespace Ixcent.CryptoTerminal.Domain.Extensions
{
    public static class ObjectCopyExtensions
    {
        public static T? CopyWithSerialize<T>(this T obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
