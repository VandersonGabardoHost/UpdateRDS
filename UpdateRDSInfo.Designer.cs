namespace UpdateRDS
{
    partial class UpdateRDSInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateRDSInfo));
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblTitulodados = new System.Windows.Forms.Label();
            this.lblTitulocxt = new System.Windows.Forms.Label();
            this.txtTitulodesom = new System.Windows.Forms.TextBox();
            this.btnEnviartitulosom = new System.Windows.Forms.Button();
            this.lblInfoid = new System.Windows.Forms.Label();
            this.lblTituloemissora = new System.Windows.Forms.Label();
            this.btnApagarlogerro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.White;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(10, 159);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(460, 325);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Carregando aplicação...";
            // 
            // lblTitulodados
            // 
            this.lblTitulodados.AutoSize = true;
            this.lblTitulodados.Location = new System.Drawing.Point(10, 129);
            this.lblTitulodados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulodados.Name = "lblTitulodados";
            this.lblTitulodados.Size = new System.Drawing.Size(416, 16);
            this.lblTitulodados.TabIndex = 0;
            this.lblTitulodados.Text = "Informações de status do aplicativo e envio de informações de RDS:";
            // 
            // lblTitulocxt
            // 
            this.lblTitulocxt.AutoSize = true;
            this.lblTitulocxt.Location = new System.Drawing.Point(10, 65);
            this.lblTitulocxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulocxt.Name = "lblTitulocxt";
            this.lblTitulocxt.Size = new System.Drawing.Size(227, 16);
            this.lblTitulocxt.TabIndex = 0;
            this.lblTitulocxt.Text = "Título do som a enviar manualmente:";
            // 
            // txtTitulodesom
            // 
            this.txtTitulodesom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitulodesom.Location = new System.Drawing.Point(10, 90);
            this.txtTitulodesom.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitulodesom.Name = "txtTitulodesom";
            this.txtTitulodesom.Size = new System.Drawing.Size(225, 22);
            this.txtTitulodesom.TabIndex = 2;
            // 
            // btnEnviartitulosom
            // 
            this.btnEnviartitulosom.Location = new System.Drawing.Point(243, 86);
            this.btnEnviartitulosom.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnviartitulosom.Name = "btnEnviartitulosom";
            this.btnEnviartitulosom.Size = new System.Drawing.Size(228, 30);
            this.btnEnviartitulosom.TabIndex = 1;
            this.btnEnviartitulosom.Text = "Enviar título de som";
            this.btnEnviartitulosom.UseVisualStyleBackColor = true;
            this.btnEnviartitulosom.Click += new System.EventHandler(this.BtnEnviartitulosom_Click);
            // 
            // lblInfoid
            // 
            this.lblInfoid.AutoSize = true;
            this.lblInfoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoid.Location = new System.Drawing.Point(10, 540);
            this.lblInfoid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfoid.Name = "lblInfoid";
            this.lblInfoid.Size = new System.Drawing.Size(51, 16);
            this.lblInfoid.TabIndex = 0;
            this.lblInfoid.Text = "label1";
            // 
            // lblTituloemissora
            // 
            this.lblTituloemissora.AutoSize = true;
            this.lblTituloemissora.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloemissora.Location = new System.Drawing.Point(10, 15);
            this.lblTituloemissora.Name = "lblTituloemissora";
            this.lblTituloemissora.Size = new System.Drawing.Size(179, 29);
            this.lblTituloemissora.TabIndex = 0;
            this.lblTituloemissora.Text = "Nome da rádio:";
            // 
            // btnApagarlogerro
            // 
            this.btnApagarlogerro.Location = new System.Drawing.Point(261, 497);
            this.btnApagarlogerro.Name = "btnApagarlogerro";
            this.btnApagarlogerro.Size = new System.Drawing.Size(210, 30);
            this.btnApagarlogerro.TabIndex = 3;
            this.btnApagarlogerro.Text = "Apagar os logs de erro";
            this.btnApagarlogerro.UseVisualStyleBackColor = true;
            this.btnApagarlogerro.Click += new System.EventHandler(this.BtnApagarlogerro_Click);
            // 
            // UpdateRDSInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.btnApagarlogerro);
            this.Controls.Add(this.lblTituloemissora);
            this.Controls.Add(this.lblInfoid);
            this.Controls.Add(this.btnEnviartitulosom);
            this.Controls.Add(this.txtTitulodesom);
            this.Controls.Add(this.lblTitulocxt);
            this.Controls.Add(this.lblTitulodados);
            this.Controls.Add(this.lblInfo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 600);
            this.MinimumSize = new System.Drawing.Size(500, 600);
            this.Name = "UpdateRDSInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update RDS Informações";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateRDSInfo_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblTitulodados;
        private System.Windows.Forms.Label lblTitulocxt;
        private System.Windows.Forms.TextBox txtTitulodesom;
        private System.Windows.Forms.Button btnEnviartitulosom;
        private System.Windows.Forms.Label lblInfoid;
        private System.Windows.Forms.Label lblTituloemissora;
        private System.Windows.Forms.Button btnApagarlogerro;
    }
}