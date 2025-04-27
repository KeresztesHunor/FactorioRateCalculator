namespace FactorioRateCalculator
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
            components = new System.ComponentModel.Container();
            recipesListBox = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            lementToolStripMenuItem = new ToolStripMenuItem();
            CalcButton = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            checkedListBox1 = new CheckedListBox();
            resultListBox = new ListBox();
            label2 = new Label();
            label3 = new Label();
            numericUpDown1 = new NumericUpDown();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // recipesListBox
            // 
            recipesListBox.FormattingEnabled = true;
            recipesListBox.ItemHeight = 15;
            recipesListBox.Location = new Point(12, 56);
            recipesListBox.Name = "recipesListBox";
            recipesListBox.Size = new Size(657, 514);
            recipesListBox.TabIndex = 0;
            recipesListBox.SelectedIndexChanged += RecipesListBox_SelectedIndexChanged;
            recipesListBox.Format += RecipesListBox_Format;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { lementToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1493, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // lementToolStripMenuItem
            // 
            lementToolStripMenuItem.Name = "lementToolStripMenuItem";
            lementToolStripMenuItem.Size = new Size(59, 20);
            lementToolStripMenuItem.Text = "Lement";
            lementToolStripMenuItem.Click += LementToolStripMenuItem_Click;
            // 
            // CalcButton
            // 
            CalcButton.Location = new Point(711, 56);
            CalcButton.Name = "CalcButton";
            CalcButton.Size = new Size(125, 69);
            CalcButton.TabIndex = 2;
            CalcButton.Text = "Calculate";
            CalcButton.UseVisualStyleBackColor = true;
            CalcButton.Click += CalcButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(63, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(606, 23);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += TextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 5;
            label1.Text = "Search:";
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(711, 170);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(125, 400);
            checkedListBox1.TabIndex = 8;
            // 
            // resultListBox
            // 
            resultListBox.FormattingEnabled = true;
            resultListBox.ItemHeight = 15;
            resultListBox.Location = new Point(875, 56);
            resultListBox.Name = "resultListBox";
            resultListBox.Size = new Size(594, 514);
            resultListBox.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(875, 35);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 11;
            label2.Text = "Result:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(711, 139);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 12;
            label3.Text = "Quantity:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(773, 137);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(63, 23);
            numericUpDown1.TabIndex = 13;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1493, 596);
            Controls.Add(numericUpDown1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(resultListBox);
            Controls.Add(checkedListBox1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(CalcButton);
            Controls.Add(menuStrip1);
            Controls.Add(recipesListBox);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox recipesListBox;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem lementToolStripMenuItem;
        private Button CalcButton;
        private Label resultLabel1;
        private TextBox textBox1;
        private Label label1;
        private CheckedListBox checkedListBox1;
        private ListBox resultListBox;
        private Label label2;
        private Label label3;
        private NumericUpDown numericUpDown1;
    }
}
