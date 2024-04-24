using System.IO.Compression;
using System.Security.AccessControl;

namespace ComperessionStreams
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileCompressionModeExampleByGZip.Run();
            FileCompressionModeExampleByDefelta.Run();
            #region Compressing in Memory
            byte[] data = new byte[1000]; 
            var ms = new MemoryStream();
            using (Stream ds = new DeflateStream(ms, CompressionMode.Compress))
                ds.Write(data, 0, data.Length);
            byte[] compressed = ms.ToArray();
            Console.WriteLine(compressed.Length);                                 
            ms = new MemoryStream(compressed);
            using (Stream ds = new DeflateStream(ms, CompressionMode.Decompress))
            {
                for (int i = 0; i < 1000; i += ds.Read(data, i, 1000 - i)) ;
                foreach (byte b in compressed)
                {
                    Console.WriteLine(b);
                }
            }

            #endregion
            #region ZipFiles
            ZipFile.CreateFromDirectory(@"D:\myfolder", @"D:\archive.zip");
            ZipFile.ExtractToDirectory(@"D:\archive.zip",@"D:\myfolder");

            #endregion
        }
    }
    public class FileCompressionModeExampleByGZip
    {
        private const string Message = "Two general-purpose compression streams are provided in" +
            "\r\nthe System.IO.Compression namespace: DeflateStream and\r\nGZipStream. " +
            "Both use a popular compression algorithm\r\nsimilar to that of the ZIP format." +
            " They differ in that\r\nGZipStream writes an additional protocol at the start" +
            " and end\r\n—including a CRC to detect errors. GZipStream also conforms\r\nto a" +
            " standard recognized by other software.";
        private const string OriginalFileName = "original.txt";
        private const string CompressedFileName = "compressed.gz";
        private const string DecompressedFileName = "decompressed.txt";

        public static void Run()
        {
            CreateFileToCompress();
            CompressFile();
            DecompressFile();
            PrintResults();
           
        }

        private static void CreateFileToCompress() => File.WriteAllText(OriginalFileName, Message);

        private static void CompressFile()
        {
            using FileStream originalFileStream = File.Open(OriginalFileName, FileMode.Open);
            using FileStream compressedFileStream = File.Create(CompressedFileName);
            using var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);
        }

        private static void DecompressFile()
        {
            using FileStream compressedFileStream = File.Open(CompressedFileName, FileMode.Open);
            using FileStream outputFileStream = File.Create(DecompressedFileName);
            using var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
        }

        private static void PrintResults()
        {
            long originalSize = new FileInfo(OriginalFileName).Length;
            long compressedSize = new FileInfo(CompressedFileName).Length;
            long decompressedSize = new FileInfo(DecompressedFileName).Length;

            Console.WriteLine($"The original file '{OriginalFileName}' is {originalSize} bytes. Contents: \"{File.ReadAllText(OriginalFileName)}\"");
            Console.WriteLine($"The compressed file '{CompressedFileName}' is {compressedSize} bytes.");
            Console.WriteLine($"The decompressed file '{DecompressedFileName}' is {decompressedSize} bytes. Contents: \"{File.ReadAllText(DecompressedFileName)}\"");
        }

       
    }
    public class FileCompressionModeExampleByDefelta
    {
        private const string Message = "Two general-purpose compression streams are provided in" +
            "\r\nthe System.IO.Compression namespace: DeflateStream and\r\nGZipStream. " +
            "Both use a popular compression algorithm\r\nsimilar to that of the ZIP format." +
            " They differ in that\r\nGZipStream writes an additional protocol at the start" +
            " and end\r\n—including a CRC to detect errors. GZipStream also conforms\r\nto a" +
            " standard recognized by other software.";
        private const string OriginalFileName = "original1.txt";
        private const string CompressedFileName = "compressed1.dfl";
        private const string DecompressedFileName = "decompressed1.txt";

        public static void Run()
        {
            CreateFileToCompress();
            CompressFile();
            DecompressFile();
            PrintResults();
        }

        private static void CreateFileToCompress() => File.WriteAllText(OriginalFileName, Message);

        private static void CompressFile()
        {
            using FileStream originalFileStream = File.Open(OriginalFileName, FileMode.Open);
            using FileStream compressedFileStream = File.Create(CompressedFileName);
            using var compressor = new DeflateStream(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);
        }
        private static void DecompressFile()
        {
            using FileStream compressedFileStream = File.Open(CompressedFileName, FileMode.Open);
            using FileStream outputFileStream = File.Create(DecompressedFileName);
            using var decompressor = new DeflateStream(compressedFileStream, CompressionMode.Decompress);
            decompressor.CopyTo(outputFileStream);
        }

        private static void PrintResults()
        {
            long originalSize = new FileInfo(OriginalFileName).Length;
            long compressedSize = new FileInfo(CompressedFileName).Length;
            long decompressedSize = new FileInfo(DecompressedFileName).Length;
            Console.WriteLine($"The original file '{OriginalFileName}' is {originalSize} bytes. Contents: \"{File.ReadAllText(OriginalFileName)}\"");
            Console.WriteLine($"The compressed file '{CompressedFileName}' is {compressedSize} bytes.");
            Console.WriteLine($"The decompressed file '{DecompressedFileName}' is {decompressedSize} bytes. Contents: \"{File.ReadAllText(DecompressedFileName)}\"");
        }

        }
    }
