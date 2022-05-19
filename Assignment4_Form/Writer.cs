using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment4_Form
{
    public class Writer
    {
        private BoundedBuffer buffer;
        private List<string> textToWrite;

        public Writer(BoundedBuffer buffer, List<string> textIn)
        {
            this.buffer = buffer;
            this.textToWrite = textIn;

            Thread t = new Thread(new ThreadStart(WriteLoop));
            t.Start();
        }
        public void WriteLoop()
        {
            foreach(string text in textToWrite)
            {
                buffer.WriteData(text);
            }

            //while (true)
            //{
            //    buffer.WriteData("s");
            //}
        }
    }
}
