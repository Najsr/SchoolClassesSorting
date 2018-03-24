namespace SchoolClassesSorting
{
    partial class FormMain
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
            this.buttonOpenTable = new System.Windows.Forms.Button();
            this.listViewPupils = new System.Windows.Forms.ListView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPupils = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownClasses = new System.Windows.Forms.NumericUpDown();
            this.buttonSort = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownTries = new System.Windows.Forms.NumericUpDown();
            this.buttonClassViewer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPupils)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTries)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenTable
            // 
            this.buttonOpenTable.Location = new System.Drawing.Point(12, 268);
            this.buttonOpenTable.Name = "buttonOpenTable";
            this.buttonOpenTable.Size = new System.Drawing.Size(127, 23);
            this.buttonOpenTable.TabIndex = 1;
            this.buttonOpenTable.Text = "Vybrat Excel Tabulku";
            this.buttonOpenTable.UseVisualStyleBackColor = true;
            this.buttonOpenTable.Click += new System.EventHandler(this.buttonOpenTable_Click);
            // 
            // listViewPupils
            // 
            this.listViewPupils.Location = new System.Drawing.Point(12, 12);
            this.listViewPupils.Name = "listViewPupils";
            this.listViewPupils.Size = new System.Drawing.Size(285, 250);
            this.listViewPupils.TabIndex = 0;
            this.listViewPupils.UseCompatibleStateImageBehavior = false;
            this.listViewPupils.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewPupils_ColumnWidthChanging);
            this.listViewPupils.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewPupils_KeyDown);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(303, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Žáků na třídu:";
            // 
            // numericUpDownPupils
            // 
            this.numericUpDownPupils.Increment = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownPupils.Location = new System.Drawing.Point(385, 10);
            this.numericUpDownPupils.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownPupils.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownPupils.Name = "numericUpDownPupils";
            this.numericUpDownPupils.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownPupils.TabIndex = 100;
            this.numericUpDownPupils.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownPupils.ValueChanged += new System.EventHandler(this.numericUpDownPupils_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "Max. tříd:";
            // 
            // numericUpDownClasses
            // 
            this.numericUpDownClasses.Location = new System.Drawing.Point(362, 48);
            this.numericUpDownClasses.Name = "numericUpDownClasses";
            this.numericUpDownClasses.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownClasses.TabIndex = 102;
            this.numericUpDownClasses.ValueChanged += new System.EventHandler(this.numericUpDownClasses_ValueChanged);
            // 
            // buttonSort
            // 
            this.buttonSort.Enabled = false;
            this.buttonSort.Location = new System.Drawing.Point(156, 268);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(141, 23);
            this.buttonSort.TabIndex = 103;
            this.buttonSort.Text = "Rozřadit";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 104;
            this.label3.Text = "Počet pokusů:";
            // 
            // numericUpDownTries
            // 
            this.numericUpDownTries.Location = new System.Drawing.Point(385, 88);
            this.numericUpDownTries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTries.Name = "numericUpDownTries";
            this.numericUpDownTries.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownTries.TabIndex = 105;
            this.numericUpDownTries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonClassViewer
            // 
            this.buttonClassViewer.Location = new System.Drawing.Point(306, 268);
            this.buttonClassViewer.Name = "buttonClassViewer";
            this.buttonClassViewer.Size = new System.Drawing.Size(75, 23);
            this.buttonClassViewer.TabIndex = 106;
            this.buttonClassViewer.Text = "Zobrazovač";
            this.buttonClassViewer.UseVisualStyleBackColor = true;
            this.buttonClassViewer.Click += new System.EventHandler(this.buttonClassViewer_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(498, 370);
            this.Controls.Add(this.buttonClassViewer);
            this.Controls.Add(this.numericUpDownTries);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.numericUpDownClasses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownPupils);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewPupils);
            this.Controls.Add(this.buttonOpenTable);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rozřazování";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPupils)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenTable;
        private System.Windows.Forms.ListView listViewPupils;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownPupils;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownClasses;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownTries;
        private System.Windows.Forms.Button buttonClassViewer;
    }
}

