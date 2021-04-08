using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace C
{

    public class CaesarStream {
         public Stream file;
        int shift;

        public CaesarStream(Stream f, int shif)
        {
            this.file = f;
            this.shift = shif;


        }
        public int Read(byte[] buffer, int size)
        {
            int res = this.file.Read(buffer,0,size);
            for(int i =0; i <size; i++)
            {
                buffer[i] = (byte)(buffer[i] + this.shift);
            }

            return res;


        }
        

        public void Write(byte[] buffer, int size)
        {
            
            for(int i =0; i <size; i++)
            {
                buffer[i] = (byte)(buffer[i] + this.shift);
            }
            this.file.Write(buffer,0,size);
            this.file.Close();


        }
        


    }

    class Program
    {
        static void Main(string[] args)
        {
           FileStream fileToWrite = File.Create("w.txt");
           FileStream fileToRead = File.Open("read.txt",FileMode.Open, FileAccess.Read);
            CaesarStream caeToRead = new CaesarStream(fileToRead, 5);
            CaesarStream caeToWrite = new CaesarStream(fileToWrite, -5);
            int size = (int)fileToRead.Length;
            byte[] buffer = new byte[size];

            caeToRead.Read(buffer,size);
            caeToWrite.Write(buffer,size);

        


    
    
        }
    }
}
