namespace Simple_PC_GSC_Injector
{
    partial class MW2019GscInjector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.textboxExtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxProcessName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxOverwriteGSC = new System.Windows.Forms.TextBox();
            this.buttonInjectGSC = new System.Windows.Forms.Button();
            this.buttonClearList = new System.Windows.Forms.Button();
            this.richtextboxLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textboxExtName
            // 
            this.textboxExtName.Location = new System.Drawing.Point(112, 25);
            this.textboxExtName.Margin = new System.Windows.Forms.Padding(4);
            this.textboxExtName.Name = "textboxExtName";
            this.textboxExtName.Size = new System.Drawing.Size(328, 23);
            this.textboxExtName.TabIndex = 0;
            this.textboxExtName.Text = "gscbin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Extension name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Process name";
            // 
            // textboxProcessName
            // 
            this.textboxProcessName.Location = new System.Drawing.Point(112, 73);
            this.textboxProcessName.Margin = new System.Windows.Forms.Padding(4);
            this.textboxProcessName.Name = "textboxProcessName";
            this.textboxProcessName.Size = new System.Drawing.Size(328, 23);
            this.textboxProcessName.TabIndex = 3;
            this.textboxProcessName.Text = "game_dx12_ship_replay";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Overwrite GSC";
            // 
            // textboxOverwriteGSC
            // 
            this.textboxOverwriteGSC.Location = new System.Drawing.Point(112, 127);
            this.textboxOverwriteGSC.Margin = new System.Windows.Forms.Padding(4);
            this.textboxOverwriteGSC.Name = "textboxOverwriteGSC";
            this.textboxOverwriteGSC.Size = new System.Drawing.Size(328, 23);
            this.textboxOverwriteGSC.TabIndex = 5;
            this.textboxOverwriteGSC.Text = "scripts/asm/shared/utility.gsc";
            // 
            // buttonInjectGSC
            // 
            this.buttonInjectGSC.Location = new System.Drawing.Point(14, 280);
            this.buttonInjectGSC.Margin = new System.Windows.Forms.Padding(4);
            this.buttonInjectGSC.Name = "buttonInjectGSC";
            this.buttonInjectGSC.Size = new System.Drawing.Size(141, 55);
            this.buttonInjectGSC.TabIndex = 6;
            this.buttonInjectGSC.Text = "Inject GSC";
            this.buttonInjectGSC.UseVisualStyleBackColor = true;
            this.buttonInjectGSC.Click += new System.EventHandler(this.buttonInjectGSC_Click);
            // 
            // buttonClearList
            // 
            this.buttonClearList.Location = new System.Drawing.Point(299, 280);
            this.buttonClearList.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClearList.Name = "buttonClearList";
            this.buttonClearList.Size = new System.Drawing.Size(141, 55);
            this.buttonClearList.TabIndex = 7;
            this.buttonClearList.Text = "Clear list";
            this.buttonClearList.UseVisualStyleBackColor = true;
            this.buttonClearList.Click += new System.EventHandler(this.buttonClearList_Click);
            // 
            // richtextboxLog
            // 
            this.richtextboxLog.Location = new System.Drawing.Point(448, 24);
            this.richtextboxLog.Margin = new System.Windows.Forms.Padding(4);
            this.richtextboxLog.Name = "richtextboxLog";
            this.richtextboxLog.Size = new System.Drawing.Size(374, 310);
            this.richtextboxLog.TabIndex = 8;
            this.richtextboxLog.Text = "";
            // 
            // MW2019GscInjector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 362);
            this.Controls.Add(this.richtextboxLog);
            this.Controls.Add(this.buttonClearList);
            this.Controls.Add(this.buttonInjectGSC);
            this.Controls.Add(this.textboxOverwriteGSC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textboxProcessName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxExtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MW2019GscInjector";
            this.Text = "[CoD:MW2019] Donetsk GSC Injector";
            this.Load += new System.EventHandler(this.MW2019GscInjector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxExtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxProcessName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textboxOverwriteGSC;
        private System.Windows.Forms.Button buttonInjectGSC;
        private System.Windows.Forms.Button buttonClearList;
        private System.Windows.Forms.RichTextBox richtextboxLog;
    }
}