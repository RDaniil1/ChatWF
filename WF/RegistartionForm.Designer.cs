namespace DotChatWF
{
  partial class RegistartionForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TBPass2 = new System.Windows.Forms.TextBox();
            this.TBPass1 = new System.Windows.Forms.TextBox();
            this.fieldUserName = new System.Windows.Forms.TextBox();
            this.btnReg2serv = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Retype password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Username:";
            // 
            // TBPass2
            // 
            this.TBPass2.Location = new System.Drawing.Point(11, 156);
            this.TBPass2.Name = "TBPass2";
            this.TBPass2.Size = new System.Drawing.Size(224, 22);
            this.TBPass2.TabIndex = 10;
            // 
            // TBPass1
            // 
            this.TBPass1.Location = new System.Drawing.Point(11, 100);
            this.TBPass1.Name = "TBPass1";
            this.TBPass1.Size = new System.Drawing.Size(224, 22);
            this.TBPass1.TabIndex = 9;
            // 
            // fieldUserName
            // 
            this.fieldUserName.Location = new System.Drawing.Point(11, 41);
            this.fieldUserName.Name = "fieldUserName";
            this.fieldUserName.Size = new System.Drawing.Size(224, 22);
            this.fieldUserName.TabIndex = 8;
            // 
            // btnReg2serv
            // 
            this.btnReg2serv.Location = new System.Drawing.Point(313, 172);
            this.btnReg2serv.Name = "btnReg2serv";
            this.btnReg2serv.Size = new System.Drawing.Size(127, 64);
            this.btnReg2serv.TabIndex = 7;
            this.btnReg2serv.Text = "Registartion";
            this.btnReg2serv.UseVisualStyleBackColor = true;
            this.btnReg2serv.Click += new System.EventHandler(this.btnReg2serv_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "IP Address:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 214);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 22);
            this.textBox1.TabIndex = 15;
            // 
            // RegistartionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 267);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBPass2);
            this.Controls.Add(this.TBPass1);
            this.Controls.Add(this.fieldUserName);
            this.Controls.Add(this.btnReg2serv);
            this.Name = "RegistartionForm";
            this.Text = "Registartion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegistartionForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegistartionForm_FormClosed);
            this.Load += new System.EventHandler(this.RegistartionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox TBPass2;
    private System.Windows.Forms.TextBox TBPass1;
    private System.Windows.Forms.TextBox fieldUserName;
    private System.Windows.Forms.Button btnReg2serv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
    }
}