using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Assignment4_Form
{
    public class Reader
    {
        private BoundedBuffer buffer;
        private int count;
        private List<string> stringList, printedList;
        private bool lastReader, isFinished = false;
        private RichTextBox rtxtTarget;

        public List<string> GetTxt { get; set; }

        public Reader(BoundedBuffer buffer, int nrOfStrings, RichTextBox rtxtTarget)
        {
            this.buffer = buffer;
            this.count = nrOfStrings;
            this.rtxtTarget = rtxtTarget;

            stringList = new List<string>();
            printedList = new List<string>();

            Thread thread = new Thread(() => ReadLoop());
            thread.Start();
        }
        public void ReadLoop()
        {
            while(!isFinished)
            {
                stringList.Add(buffer.ReadData());
                Print();

                if(stringList.Count >= count)
                {
                    isFinished = true;
                }
            }
            System.Diagnostics.Debug.WriteLine("How did we get here?");
        }
        public void Print()
        {
            for (int i = printedList.Count; i < stringList.Count; i++)
            {
                rtxtTarget.Invoke(new Action(() => rtxtTarget.Text += " " + stringList[i]));
                printedList.Add(stringList[i]);
            }
        }
    }
}
