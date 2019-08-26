namespace UpdateRDS
{
    partial class Avisodeversaobeta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Avisodeversaobeta));
            this.lblAviso = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAviso
            // 
            this.lblAviso.Location = new System.Drawing.Point(12, 7);
            this.lblAviso.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(558, 372);
            this.lblAviso.TabIndex = 0;
            this.lblAviso.Text = resources.GetString("lblAviso.Text");
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(70, 410);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(437, 44);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Tudo bem, pode deixar que, qualquer coisa eu aviso";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // Avisodeversaobeta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 466);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblAviso);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximumSize = new System.Drawing.Size(600, 500);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Avisodeversaobeta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aviso de versão Beta - Update RDS By GabardoHost";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Button btnOk;
    }
}