
namespace OnlineEuchre
{
    partial class frmAsk
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
            this.btnPass = new System.Windows.Forms.Button();
            this.btnCall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPass
            // 
            this.btnPass.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPass.Location = new System.Drawing.Point(12, 12);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(75, 28);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = true;
            // 
            // btnCall
            // 
            this.btnCall.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCall.Location = new System.Drawing.Point(93, 12);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(75, 28);
            this.btnCall.TabIndex = 1;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            // 
            // frmAsk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(183, 54);
            this.ControlBox = false;
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.btnPass);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAsk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmAsk";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnCall;
    }
}