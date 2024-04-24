using System.Xml.Linq;

namespace Streams
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Reading and writing
            using (Stream stream = new FileStream("test.text", FileMode.Create))
            {
                Console.WriteLine(stream.CanRead);
                Console.WriteLine(stream.CanWrite);
                Console.WriteLine(stream.CanSeek);
                stream.WriteByte(102);
                stream.WriteByte(101);
                Console.WriteLine(stream.Position);
                Console.WriteLine(stream.Length);
                byte[] block = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                stream.Write(block, 0, block.Length);
                Console.WriteLine(stream.Position);
                Console.WriteLine(stream.Length);
                stream.Position = 0;
                Console.WriteLine(stream.ReadByte());
                Console.WriteLine(stream.ReadByte());
                Console.WriteLine(stream.ReadByte());
                Console.WriteLine(stream.Read(block, 0, block.Length));
            }
            #endregion
            #region binaryreader andbinary writer
            person person = new person();
            person.Name = "Yousef mohamed mahfouz";
            person.Age = 10;
            person.Height = 40;
            using (Stream s = new FileStream("test1.text", FileMode.Create))
            {
                person.SaveData(s);
                
                #endregion
            }

        }
    }
    class person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Double Height { get; set; }
        public void SaveData(Stream s)
        {
            var w = new BinaryWriter(s);
            w.Write(Name);
            w.Write(Age);
            w.Write(Height);
            w.Flush();
        }
        public void LoadData(Stream s)
        {
            var r = new BinaryReader(s);
            Name = r.ReadString();
            Age = r.ReadInt32();
            Height = r.ReadDouble();
        }
    }
    
}
