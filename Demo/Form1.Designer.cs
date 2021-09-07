
namespace Demo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ckA = new System.Windows.Forms.CheckBox();
            this.ckB = new System.Windows.Forms.CheckBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbTrainResults = new System.Windows.Forms.Label();
            this.txtDebug = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ckA
            // 
            this.ckA.AutoSize = true;
            this.ckA.Location = new System.Drawing.Point(104, 89);
            this.ckA.Name = "ckA";
            this.ckA.Size = new System.Drawing.Size(34, 19);
            this.ckA.TabIndex = 0;
            this.ckA.Text = "A";
            this.ckA.UseVisualStyleBackColor = true;
            this.ckA.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ckB
            // 
            this.ckB.AutoSize = true;
            this.ckB.Location = new System.Drawing.Point(104, 115);
            this.ckB.Name = "ckB";
            this.ckB.Size = new System.Drawing.Size(33, 19);
            this.ckB.TabIndex = 1;
            this.ckB.Text = "B";
            this.ckB.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(104, 161);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(62, 15);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "Test Result";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(104, 202);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Perform X-OR Training";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbTrainResults
            // 
            this.lbTrainResults.AutoSize = true;
            this.lbTrainResults.Location = new System.Drawing.Point(261, 50);
            this.lbTrainResults.Name = "lbTrainResults";
            this.lbTrainResults.Size = new System.Drawing.Size(0, 15);
            this.lbTrainResults.TabIndex = 5;
            // 
            // txtDebug
            // 
            this.txtDebug.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDebug.Location = new System.Drawing.Point(284, 38);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.ReadOnly = true;
            this.txtDebug.Size = new System.Drawing.Size(504, 195);
            this.txtDebug.TabIndex = 6;
            this.txtDebug.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.lbTrainResults);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.ckB);
            this.Controls.Add(this.ckA);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckA;
        private System.Windows.Forms.CheckBox ckB;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbTrainResults;
        private System.Windows.Forms.RichTextBox txtDebug;
    }
}

