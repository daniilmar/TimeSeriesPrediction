using OxyPlot.WindowsForms;

namespace WinFormPredictionapp
{
    partial class Form2
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
            this.plot1 = new OxyPlot.WindowsForms.PlotView();
            this.simpleMovingAverageCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.comboBoxDateStart = new System.Windows.Forms.ComboBox();
            this.comboBoxDateEnd = new System.Windows.Forms.ComboBox();
            this.calculate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plot1.Location = new System.Drawing.Point(0, -11);
            this.plot1.Margin = new System.Windows.Forms.Padding(0);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(905, 400);
            this.plot1.TabIndex = 0;
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // simpleMovingAverageCheckBox
            // 
            this.simpleMovingAverageCheckBox.AutoSize = true;
            this.simpleMovingAverageCheckBox.Location = new System.Drawing.Point(690, 48);
            this.simpleMovingAverageCheckBox.Name = "simpleMovingAverageCheckBox";
            this.simpleMovingAverageCheckBox.Size = new System.Drawing.Size(132, 17);
            this.simpleMovingAverageCheckBox.TabIndex = 1;
            this.simpleMovingAverageCheckBox.Text = "SimpleMovingAverage";
            this.simpleMovingAverageCheckBox.UseVisualStyleBackColor = true;
            this.simpleMovingAverageCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(690, 86);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(690, 122);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(80, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // comboBoxDateStart
            // 
            this.comboBoxDateStart.FormattingEnabled = true;
            this.comboBoxDateStart.Location = new System.Drawing.Point(59, 48);
            this.comboBoxDateStart.Name = "comboBoxDateStart";
            this.comboBoxDateStart.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDateStart.TabIndex = 4;
            this.comboBoxDateStart.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBoxDateEnd
            // 
            this.comboBoxDateEnd.FormattingEnabled = true;
            this.comboBoxDateEnd.Location = new System.Drawing.Point(222, 48);
            this.comboBoxDateEnd.Name = "comboBoxDateEnd";
            this.comboBoxDateEnd.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDateEnd.TabIndex = 5;
            // 
            // calculate
            // 
            this.calculate.Location = new System.Drawing.Point(695, 170);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(75, 23);
            this.calculate.TabIndex = 6;
            this.calculate.Text = "Расчет";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(905, 389);
            this.Controls.Add(this.calculate);
            this.Controls.Add(this.comboBoxDateEnd);
            this.Controls.Add(this.comboBoxDateStart);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.simpleMovingAverageCheckBox);
            this.Controls.Add(this.plot1);
            this.Name = "Form2";
            this.Text = "OxyPlot in Windows Forms";
            this.ResumeLayout(false);
            
            this.PerformLayout();

        }

        private PlotView plot1;
        #endregion

        private System.Windows.Forms.CheckBox simpleMovingAverageCheckBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox comboBoxDateStart;
        private System.Windows.Forms.ComboBox comboBoxDateEnd;
        private System.Windows.Forms.Button calculate;
    }
}