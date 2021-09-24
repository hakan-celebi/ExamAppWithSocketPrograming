using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Controllers
{
    public static class ObjectAndFileSerializer
    {
        public static byte[] FileSerializer(this string filePath) => File.ReadAllBytes(filePath);
        public static void FileDeserializer(this byte[] file, string filePath) { File.Create(filePath).Close(); File.WriteAllBytes(filePath, file); }
        public static byte[] ObjectSerializer(this object anyConvertingObject)
        {
            if (anyConvertingObject is null)
                return null;
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anyConvertingObject);
                return memoryStream.ToArray();
            }
        }

        public static object ObjectDeserializer(this byte[] anyConvertingByte)
        {
            if (anyConvertingByte.Length <= 0)
                return null;
            using (var memoryStream = new MemoryStream(anyConvertingByte))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }

    }
}
