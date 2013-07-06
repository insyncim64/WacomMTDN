///////////////////////////////////////////////////////////////////////////////
// QueryDataForm.cs - Windows Forms test dialog for WintabDN
//
// Copyright (c) 2010, Wacom Technology Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WintabDN
{
    public partial class QueryDataForm : Form
    {
        private CWintabContext m_logContext = null; 
        private CWintabData m_wtData = null;

        public QueryDataForm()
        {
            InitializeComponent();

            try
            {
                // Open a Wintab context that does not send Wintab data events.
                m_logContext = OpenQueryDigitizerContext();

                // Create a data object.
                m_wtData = new CWintabData(m_logContext);

                TraceMsg("Press \"Test\" and touch pen to tablet.\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private CWintabContext OpenQueryDigitizerContext()
        {
            bool status = false;
            CWintabContext logContext = null;

            try
            {
                // Get the default digitizing context.  Turn off events.  Control system cursor.
                logContext = CWintabInfo.GetDefaultDigitizingContext(ECTXOptionValues.CXO_SYSTEM);

                if (logContext == null)
                {
                    TraceMsg("OpenQueryDigitizerContext: FAILED to get default digitizing context.\n");
                    //System.Diagnostics.Debug.WriteLine("FAILED to get default digitizing context.");
                    return null;
                }

                // Modify the digitizing region.
                logContext.Name = "WintabDN Query Data Context";

                // output in a 5000 x 5000 grid
                logContext.OutOrgX = logContext.OutOrgY = 0;
                logContext.OutExtX = 5000;
                logContext.OutExtY = 5000;

                // Open the context, which will also tell Wintab to send data packets.
                status = logContext.Open();

                TraceMsg("Context Open: " + (status ? "PASSED [ctx=" + logContext.HCtx + "]" : "FAILED") + "\n");
                //System.Diagnostics.Debug.WriteLine("Context Open: " + (status ? "PASSED [ctx=" + logContext.HCtx + "]" : "FAILED"));
            }
            catch (Exception ex)
            {
                TraceMsg("OpenQueryDigitizerContext: ERROR : " + ex.ToString());
            }

            return logContext;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            // Poll for data queued up from the last pen press.

            // get up to m_maxPkts packets at a time
            const bool REMOVE = true;
            const bool PEEK = false;

            try
            {
                // Test remove from queue.
                TraceMsg("Testing remove from queue:\n");
                TestGetDataPackets(REMOVE, 1, 200);

                // Test peek at queue.
                TraceMsg("Testing peek in queue:\n");
                TestGetDataPackets(PEEK, 3, 5);
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetDataPackets ERROR: " + ex.ToString());
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Test get or peek data packets from queue.
        /// </summary>
        /// <param name="remove_I">true = remove; false = peek</param>
        /// <param name="maxPkts_I">max num pkts to capture/display at a time</param>
        /// <param name="loopCount_I">loop count for looking at queue</param>
        private void TestGetDataPackets(bool remove_I, uint maxPkts_I, uint loopCount_I)
        {
            UInt32 numPkts = 0;     // numPkts actually returned
            
            while (--loopCount_I > 0)
            {
                WintabPacket[] packets = m_wtData.GetDataPackets(maxPkts_I, remove_I, ref numPkts);

                if (numPkts == 0)
                {
                    //System.Diagnostics.Debug.WriteLine("testButton_Click: numPkts is 0");
                    continue;
                }

                TraceMsg("Received #packets: " + numPkts + "\n");

                for (int idx = 0; idx < packets.Length; idx++)
                {
                    TraceMsg("[" + packets[idx].pkSerialNumber + "]  X / Y / PA / PR = " +
                         packets[idx].pkX + " / " + packets[idx].pkY + " / " +
                         packets[idx].pkNormalPressure.pkAbsoluteNormalPressure + " / " +
                         packets[idx].pkNormalPressure.pkRelativeNormalPressure + "\n");
                }

                System.Threading.Thread.Sleep(100);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private void clearButton_Click(object sender, EventArgs e)
        {
            testTextBox.Clear();
        }

        ///////////////////////////////////////////////////////////////////////
        void TraceMsg(string msg)
        {
            testTextBox.AppendText(msg);

            // Scroll to bottom of list.
            testTextBox.SelectionLength = 0;
            testTextBox.SelectionStart = testTextBox.Text.Length;
            testTextBox.ScrollToCaret();
        }
    }
}
