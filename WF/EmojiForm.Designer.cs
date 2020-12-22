
namespace DotChatWF
{
    partial class EmojiForm
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
            this.Music = new System.Windows.Forms.Button();
            this.Face = new System.Windows.Forms.Button();
            this.Sun = new System.Windows.Forms.Button();
            this.Heart = new System.Windows.Forms.Button();
            this.Star = new System.Windows.Forms.Button();
            this.Spades = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Music
            // 
            this.Music.Location = new System.Drawing.Point(28, 28);
            this.Music.Name = "Music";
            this.Music.Size = new System.Drawing.Size(87, 32);
            this.Music.TabIndex = 0;
            this.Music.Text = "Music";
            this.Music.UseVisualStyleBackColor = true;
            this.Music.Click += new System.EventHandler(this.musicButton_Click);
            // 
            // Face
            // 
            this.Face.Location = new System.Drawing.Point(28, 66);
            this.Face.Name = "Face";
            this.Face.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Face.Size = new System.Drawing.Size(87, 28);
            this.Face.TabIndex = 1;
            this.Face.Text = "Face";
            this.Face.UseVisualStyleBackColor = true;
            this.Face.Click += new System.EventHandler(this.faceButton_Click);
            // 
            // Sun
            // 
            this.Sun.Location = new System.Drawing.Point(28, 100);
            this.Sun.Name = "Sun";
            this.Sun.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Sun.Size = new System.Drawing.Size(87, 32);
            this.Sun.TabIndex = 2;
            this.Sun.Text = "Sun";
            this.Sun.UseVisualStyleBackColor = true;
            this.Sun.Click += new System.EventHandler(this.sunButton_Click);
            // 
            // Heart
            // 
            this.Heart.Location = new System.Drawing.Point(121, 28);
            this.Heart.Name = "Heart";
            this.Heart.Size = new System.Drawing.Size(87, 34);
            this.Heart.TabIndex = 3;
            this.Heart.Text = "Heart";
            this.Heart.UseVisualStyleBackColor = true;
            this.Heart.Click += new System.EventHandler(this.heartButton_Click);
            // 
            // Star
            // 
            this.Star.Location = new System.Drawing.Point(121, 66);
            this.Star.Name = "Star";
            this.Star.Size = new System.Drawing.Size(87, 28);
            this.Star.TabIndex = 4;
            this.Star.Text = "Star";
            this.Star.UseVisualStyleBackColor = true;
            this.Star.Click += new System.EventHandler(this.starButton_Click);
            // 
            // Spades
            // 
            this.Spades.Location = new System.Drawing.Point(121, 98);
            this.Spades.Name = "Spades";
            this.Spades.Size = new System.Drawing.Size(87, 34);
            this.Spades.TabIndex = 5;
            this.Spades.Text = "Spades";
            this.Spades.UseVisualStyleBackColor = true;
            this.Spades.Click += new System.EventHandler(this.spadesButton_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(214, 64);
            this.Cancel.Name = "Cancel";
            this.Cancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Cancel.Size = new System.Drawing.Size(87, 33);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EmojiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 157);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Spades);
            this.Controls.Add(this.Star);
            this.Controls.Add(this.Heart);
            this.Controls.Add(this.Sun);
            this.Controls.Add(this.Face);
            this.Controls.Add(this.Music);
            this.Name = "EmojiForm";
            this.Text = "Choose your emoji ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmojiForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Music;
        private System.Windows.Forms.Button Face;
        private System.Windows.Forms.Button Sun;
        private System.Windows.Forms.Button Heart;
        private System.Windows.Forms.Button Star;
        private System.Windows.Forms.Button Spades;
        private System.Windows.Forms.Button Cancel;
    }
}