namespace FormTestApp
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.testButton = new System.Windows.Forms.Button();
            this.testTextBox = new System.Windows.Forms.RichTextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.scribblePanel = new System.Windows.Forms.Panel();
            this.scribbleButton = new System.Windows.Forms.Button();
            this.scribbleLabel = new System.Windows.Forms.Label();
            this.testLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.BackColor = System.Drawing.Color.Lime;
            this.testButton.Location = new System.Drawing.Point(12, 12);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Test...";
            this.testButton.UseVisualStyleBackColor = false;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // testTextBox
            // 
            this.testTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.testTextBox.BackColor = System.Drawing.Color.Black;
            this.testTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testTextBox.ForeColor = System.Drawing.Color.Lime;
            this.testTextBox.Location = new System.Drawing.Point(12, 43);
            this.testTextBox.Name = "testTextBox";
            this.testTextBox.Size = new System.Drawing.Size(528, 517);
            this.testTextBox.TabIndex = 1;
            this.testTextBox.Text = "";
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(1106, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // scribblePanel
            // 
            this.scribblePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scribblePanel.BackColor = System.Drawing.Color.Gainsboro;
            this.scribblePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scribblePanel.Location = new System.Drawing.Point(546, 43);
            this.scribblePanel.Name = "scribblePanel";
            this.scribblePanel.Size = new System.Drawing.Size(635, 517);
            this.scribblePanel.TabIndex = 3;
            // 
            // scribbleButton
            // 
            this.scribbleButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.scribbleButton.Location = new System.Drawing.Point(546, 11);
            this.scribbleButton.Name = "scribbleButton";
            this.scribbleButton.Size = new System.Drawing.Size(77, 24);
            this.scribbleButton.TabIndex = 4;
            this.scribbleButton.Text = "Scribble";
            this.scribbleButton.UseVisualStyleBackColor = false;
            this.scribbleButton.Click += new System.EventHandler(this.scribbleButton_Click);
            // 
            // scribbleLabel
            // 
            this.scribbleLabel.AutoSize = true;
            this.scribbleLabel.Location = new System.Drawing.Point(634, 15);
            this.scribbleLabel.Name = "scribbleLabel";
            this.scribbleLabel.Size = new System.Drawing.Size(0, 13);
            this.scribbleLabel.TabIndex = 5;
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.Location = new System.Drawing.Point(93, 17);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(162, 13);
            this.testLabel.TabIndex = 6;
            this.testLabel.Text = "Press Test button to start testing.";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1194, 572);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.scribbleLabel);
            this.Controls.Add(this.scribbleButton);
            this.Controls.Add(this.scribblePanel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.testTextBox);
            this.Controls.Add(this.testButton);
            this.Name = "TestForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        public System.Windows.Forms.RichTextBox testTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel scribblePanel;
        private System.Windows.Forms.Button scribbleButton;
        private System.Windows.Forms.Label scribbleLabel;
        private System.Windows.Forms.Label testLabel;
    }
}

