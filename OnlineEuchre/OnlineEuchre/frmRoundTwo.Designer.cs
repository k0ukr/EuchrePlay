
namespace OnlineEuchre
{
    partial class frmRoundTwo
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
            this.btnCall01 = new System.Windows.Forms.Button();
            this.btnPass = new System.Windows.Forms.Button();
            this.btnCall02 = new System.Windows.Forms.Button();
            this.btnCall03 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCall01
            // 
            this.btnCall01.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCall01.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall01.Location = new System.Drawing.Point(83, 7);
            this.btnCall01.Name = "btnCall01";
            this.btnCall01.Size = new System.Drawing.Size(99, 34);
            this.btnCall01.TabIndex = 1;
            this.btnCall01.Text = "Call";
            this.btnCall01.UseVisualStyleBackColor = true;
            // 
            // btnPass
            // 
            this.btnPass.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPass.Location = new System.Drawing.Point(7, 7);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(74, 34);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = true;
            // 
            // btnCall02
            // 
            this.btnCall02.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCall02.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall02.Location = new System.Drawing.Point(184, 7);
            this.btnCall02.Name = "btnCall02";
            this.btnCall02.Size = new System.Drawing.Size(99, 34);
            this.btnCall02.TabIndex = 2;
            this.btnCall02.Text = "Call";
            this.btnCall02.UseVisualStyleBackColor = true;
            // 
            // btnCall03
            // 
            this.btnCall03.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCall03.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCall03.Location = new System.Drawing.Point(285, 7);
            this.btnCall03.Name = "btnCall03";
            this.btnCall03.Size = new System.Drawing.Size(99, 34);
            this.btnCall03.TabIndex = 3;
            this.btnCall03.Text = "Call";
            this.btnCall03.UseVisualStyleBackColor = true;
            // 
            // frmRoundTwo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 48);
            this.Controls.Add(this.btnCall03);
            this.Controls.Add(this.btnCall02);
            this.Controls.Add(this.btnCall01);
            this.Controls.Add(this.btnPass);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmRoundTwo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRoundTwo";
            this.Load += new System.EventHandler(this.frmRoundTwo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCall01;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnCall02;
        private System.Windows.Forms.Button btnCall03;
    }
}