using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment4_Form
{
    /// <summary>
    /// Modifies a list of text with a list of replacement text.
    /// </summary>
    class Modifier
    {
        BoundedBuffer boundedBuffer;
        int amountLines;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="boundedBuffer">The buffer that will be modified.</param>
        /// <param name="amountLines">The amount of lines that the text contains.</param>
        /// <param name="findList">The text that will be replaced if found.</param>
        /// <param name="replaceList">The text that will be replaced with.</param>
        public Modifier(BoundedBuffer boundedBuffer, int amountLines)
        {
            this.boundedBuffer = boundedBuffer;
            this.amountLines = amountLines;

            Thread t = new Thread(new ThreadStart(Modify));
            t.Start();
        }

        /// <summary>
        /// Modifies the string in the buffer.
        /// </summary>
        public void Modify()
        {
            for (int i = 0; i < amountLines; i++)
            {
                boundedBuffer.Modify();
            }
        }
    }
}
