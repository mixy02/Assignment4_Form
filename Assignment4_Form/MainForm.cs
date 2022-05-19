using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Assignment4_Form
{

    /// <summary>
    /// The only GUI needed
    /// </summary>
    public partial class MainForm : Form
    {
        BoundedBuffer boundedBuffer;
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The create destination file button, starts a copy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Menu exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ev. stop running threads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        /// <summary>
        /// Menu file open source file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileToLoad();
        }

        private void OpenFileToLoad()
        {
            OpenFileDialog openFileDialog = dlgOpen;
            DialogResult dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    rtxtSource.Text = File.ReadAllText(dlgOpen.FileName);
                    lblSource.Text = "Source file: " + dlgOpen.SafeFileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Could not open source file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// Clear the destination textbox and remove markers in source textbox
        /// Disable the result and this button again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// The final step is to create output
        /// </summary>
        private void CreateDestinatinFile()
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileToLoad();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            rtxtTarget.Clear();

            string findWord = txtFind.Text;
            string replaceWord = txtReplace.Text;

            var logFile = File.ReadAllLines(dlgOpen.FileName);
            var logList = new List<string>(logFile);

            //string file = $@"C:\Users\molli\Downloads\{dlgOpen.FileName}";

            //Debug.WriteLine(file);

            //rtxtSource.Find("att");
            //Debug.WriteLine(rtxtSource.Find("to"));

            Debug.WriteLine(findWord);

            string[] stringArr = rtxtSource.Text.Split(); //Splittar texten till en array.
            Debug.WriteLine(stringArr.Length);

            boundedBuffer = new BoundedBuffer(stringArr.Length, rtxtSource, rtxtTarget, true, findWord, replaceWord, stringArr.ToList());

        }
    }
}
