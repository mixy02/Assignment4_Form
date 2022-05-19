using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment4_Form
{
    public class BoundedBuffer
    {
        private string[] strArr;
        private BufferStatus[] status;

        private int elements, max = 10, writePos, readPos, findPos, start, nbrReplacements;
        private RichTextBox rtxtBox, rtxtTarget;
        private string findString, replaceString;
        private bool notify;
        private object lockObj;

        private delegate void Marker();
        private delegate void Selector();

        public Reader reader;
        private Writer writer;
        private Modifier modifier;

        private int readerCount = 0;  //number of concurrent readers

        public enum BufferStatus
        {
            Empty,
            Full,
            New
        }

        public int GetNrReplace { get; }

        public BoundedBuffer(int elements, RichTextBox rtxtBox, RichTextBox rtxtTarget, bool notify, string find, string replace, List<string> textIn)
        {
            this.elements = elements;
            this.rtxtBox = rtxtBox;
            this.rtxtTarget = rtxtTarget;
            this.notify = notify;
            findString = find;
            replaceString = replace;

            strArr = new string[max]; //not same size as sourceText
            status = new BufferStatus[max];

            writePos = 0;
            readPos = 0;

            lockObj = new object();

            writer = new Writer(this, textIn);
            reader = new Reader(this, elements, rtxtTarget);
            modifier = new Modifier(this, elements);
        }
        public void Mark()
        {

        }
        public void Modify()
        {
            Monitor.Enter(lockObj);

            while (status[findPos] != BufferStatus.Full)
            {
                Monitor.Wait(lockObj);  //condition synchronization
            }

            //replace the word if it match
            if (strArr[findPos] == findString)
            {
                strArr[findPos] = replaceString;
            }

            Thread.Sleep(10);
            status[findPos] = BufferStatus.New;

            Debug.WriteLine($"{Thread.CurrentThread.ToString():-15} :{strArr[findPos]} at pos [{findPos}]");

            findPos = (findPos + 1) % strArr.Length;


            Monitor.PulseAll(lockObj);  //awake all threads
        }
        public string ReadData()
        {
            string data = string.Empty;

            Monitor.Enter(lockObj);

            readerCount++;

            //Condition Sych - if the readerPos is not full (no data)
            //block (go to sleep inside the monitor)
            while (status[readPos] != BufferStatus.New)
                Monitor.Wait(lockObj);

            //read data and mark the position
            data = strArr[readPos];

            readerCount--;
            Thread.Sleep(10);
            status[readPos] = BufferStatus.Empty;
            readPos = (readPos + 1) % strArr.Length;

            Debug.WriteLine($"{Thread.CurrentThread.Name:10} :{data}! at pos [{readPos}]");

            Monitor.PulseAll(lockObj); //awake all waiting threds

            return data;
        }
        public string ReplaceAt(string strSource, string strReplace, int pos, int size)
        {
            return "";
        }
        public void Select()
        {

        }
        public void WriteData(string s)
        {
            Monitor.Enter(lockObj);

            while (status[writePos] != BufferStatus.Empty)
            {
                Monitor.Wait(lockObj);  //condition synchronization
            }

            //write data, mark the position as full
            strArr[writePos] = s;
            Thread.Sleep(10);
            status[writePos] = BufferStatus.Full;

            writePos = (writePos + 1) % strArr.Length;

            Debug.WriteLine($"{Thread.CurrentThread.ToString():-15} :{s} at pos [{writePos}]");

            Monitor.PulseAll(lockObj);  //awake all threads

        }
    }
}
