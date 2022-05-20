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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            
            foreach (string text in textToWrite)
            {
                buffer.WriteData(text);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine(elapsedMs.ToString());
            //while (true)
            //{
            //    buffer.WriteData("s");
            //}
        }
    }
}
