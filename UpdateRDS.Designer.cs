namespace UpdateRDS
{
    partial class UpdateRDS
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateRDS));
            this.lblTextotitulo = new System.Windows.Forms.Label();
            this.lblTextotipo = new System.Windows.Forms.Label();
            this.rbtShoutcastv1 = new System.Windows.Forms.RadioButton();
            this.rbtShoutcastv2 = new System.Windows.Forms.RadioButton();
            this.rbtIcecast = new System.Windows.Forms.RadioButton();
            this.lblTextoselec = new System.Windows.Forms.Label();
            this.chkNaominimsystray = new System.Windows.Forms.CheckBox();
            this.chkNaonotificarsomtray = new System.Windows.Forms.CheckBox();
            this.chkAcentospalavras = new System.Windows.Forms.CheckBox();
            this.chkCaracteresespeciais = new System.Windows.Forms.CheckBox();
            this.lblTextotempo = new System.Windows.Forms.Label();
            this.txtTempoexec = new System.Windows.Forms.TextBox();
            this.lblTextoarqdado = new System.Windows.Forms.Label();
            this.txtCadastrodados = new System.Windows.Forms.TextBox();
            this.btnSalvadados = new System.Windows.Forms.Button();
            this.btnCarregadados = new System.Windows.Forms.Button();
            this.lblTextoinfoseg = new System.Windows.Forms.Label();
            this.lblTextoccsom = new System.Windows.Forms.Label();
            this.txtArquivotextosom = new System.Windows.Forms.TextBox();
            this.btnLocalizatxtsom = new System.Windows.Forms.Button();
            this.lblTextourl = new System.Windows.Forms.Label();
            this.chkUrlsom = new System.Windows.Forms.CheckBox();
            this.txtUrlsom = new System.Windows.Forms.TextBox();
            this.lblTextosomnext = new System.Windows.Forms.Label();
            this.txtArquivotextosomnext = new System.Windows.Forms.TextBox();
            this.btnLocalizatxtsomnext = new System.Windows.Forms.Button();
            this.lblTextourlnext = new System.Windows.Forms.Label();
            this.chkUrlsomnext = new System.Windows.Forms.CheckBox();
            this.txtUrlsomnext = new System.Windows.Forms.TextBox();
            this.chkTransmproxsom = new System.Windows.Forms.CheckBox();
            this.lblTextoendipdom = new System.Windows.Forms.Label();
            this.txtDominioip = new System.Windows.Forms.TextBox();
            this.btnResolvernomeip = new System.Windows.Forms.Button();
            this.lblTextoporta = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.lblTextoidmon = new System.Windows.Forms.Label();
            this.txtIdoumont = new System.Windows.Forms.TextBox();
            this.lblTextologin = new System.Windows.Forms.Label();
            this.txtLoginserver = new System.Windows.Forms.TextBox();
            this.lblTextosenha = new System.Windows.Forms.Label();
            this.txtSenhaserver = new System.Windows.Forms.TextBox();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.btnRevisarinfo = new System.Windows.Forms.Button();
            this.btnPararenviords = new System.Windows.Forms.Button();
            this.btnEnviardadosrds = new System.Windows.Forms.Button();
            this.btnVerificardadosderds = new System.Windows.Forms.Button();
            this.chkDadossensiveis = new System.Windows.Forms.CheckBox();
            this.ntfIcone = new System.Windows.Forms.NotifyIcon(this.components);
            this.ofdCca = new System.Windows.Forms.OpenFileDialog();
            this.lblInformacaoid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTextotitulo
            // 
            this.lblTextotitulo.AutoSize = true;
            this.lblTextotitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextotitulo.Location = new System.Drawing.Point(257, 9);
            this.lblTextotitulo.Name = "lblTextotitulo";
            this.lblTextotitulo.Size = new System.Drawing.Size(303, 25);
            this.lblTextotitulo.TabIndex = 0;
            this.lblTextotitulo.Text = "Título do programa a definir";
            // 
            // lblTextotipo
            // 
            this.lblTextotipo.AutoSize = true;
            this.lblTextotipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextotipo.Location = new System.Drawing.Point(12, 39);
            this.lblTextotipo.Name = "lblTextotipo";
            this.lblTextotipo.Size = new System.Drawing.Size(110, 16);
            this.lblTextotipo.TabIndex = 0;
            this.lblTextotipo.Text = "Tipo de servidor:";
            // 
            // rbtShoutcastv1
            // 
            this.rbtShoutcastv1.AutoSize = true;
            this.rbtShoutcastv1.Checked = true;
            this.rbtShoutcastv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtShoutcastv1.Location = new System.Drawing.Point(133, 37);
            this.rbtShoutcastv1.Name = "rbtShoutcastv1";
            this.rbtShoutcastv1.Size = new System.Drawing.Size(331, 20);
            this.rbtShoutcastv1.TabIndex = 1;
            this.rbtShoutcastv1.TabStop = true;
            this.rbtShoutcastv1.Text = "Shoutcast Server Versão 1.X - Padrão do aplicativo";
            this.rbtShoutcastv1.UseVisualStyleBackColor = true;
            this.rbtShoutcastv1.CheckedChanged += new System.EventHandler(this.RbtShoutcastv1_CheckedChanged);
            // 
            // rbtShoutcastv2
            // 
            this.rbtShoutcastv2.AutoSize = true;
            this.rbtShoutcastv2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtShoutcastv2.Location = new System.Drawing.Point(466, 37);
            this.rbtShoutcastv2.Name = "rbtShoutcastv2";
            this.rbtShoutcastv2.Size = new System.Drawing.Size(196, 20);
            this.rbtShoutcastv2.TabIndex = 1;
            this.rbtShoutcastv2.Text = "Shoutcast Server Versão 2.X";
            this.rbtShoutcastv2.UseVisualStyleBackColor = true;
            this.rbtShoutcastv2.CheckedChanged += new System.EventHandler(this.RbtShoutcastv2_CheckedChanged);
            // 
            // rbtIcecast
            // 
            this.rbtIcecast.AutoSize = true;
            this.rbtIcecast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtIcecast.Location = new System.Drawing.Point(668, 37);
            this.rbtIcecast.Name = "rbtIcecast";
            this.rbtIcecast.Size = new System.Drawing.Size(180, 20);
            this.rbtIcecast.TabIndex = 1;
            this.rbtIcecast.Text = "Icecast Server Versão 2.X";
            this.rbtIcecast.UseVisualStyleBackColor = true;
            // 
            // lblTextoselec
            // 
            this.lblTextoselec.AutoSize = true;
            this.lblTextoselec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoselec.Location = new System.Drawing.Point(12, 65);
            this.lblTextoselec.Name = "lblTextoselec";
            this.lblTextoselec.Size = new System.Drawing.Size(103, 16);
            this.lblTextoselec.TabIndex = 0;
            this.lblTextoselec.Text = "Selecione para:";
            // 
            // chkNaominimsystray
            // 
            this.chkNaominimsystray.AutoSize = true;
            this.chkNaominimsystray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNaominimsystray.Location = new System.Drawing.Point(136, 64);
            this.chkNaominimsystray.Name = "chkNaominimsystray";
            this.chkNaominimsystray.Size = new System.Drawing.Size(324, 20);
            this.chkNaominimsystray.TabIndex = 2;
            this.chkNaominimsystray.Text = "Não minimizar o aplicativo na bandeja do sistema";
            this.chkNaominimsystray.UseVisualStyleBackColor = true;
            // 
            // chkNaonotificarsomtray
            // 
            this.chkNaonotificarsomtray.AutoSize = true;
            this.chkNaonotificarsomtray.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNaonotificarsomtray.Location = new System.Drawing.Point(466, 64);
            this.chkNaonotificarsomtray.Name = "chkNaonotificarsomtray";
            this.chkNaonotificarsomtray.Size = new System.Drawing.Size(407, 20);
            this.chkNaonotificarsomtray.TabIndex = 3;
            this.chkNaonotificarsomtray.Text = "Não notificar sobre atualizações de som na bandeja do sistema";
            this.chkNaonotificarsomtray.UseVisualStyleBackColor = true;
            // 
            // chkAcentospalavras
            // 
            this.chkAcentospalavras.AutoSize = true;
            this.chkAcentospalavras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAcentospalavras.Location = new System.Drawing.Point(136, 90);
            this.chkAcentospalavras.Name = "chkAcentospalavras";
            this.chkAcentospalavras.Size = new System.Drawing.Size(250, 20);
            this.chkAcentospalavras.TabIndex = 4;
            this.chkAcentospalavras.Text = "Remover a acentuação das palavras";
            this.chkAcentospalavras.UseVisualStyleBackColor = true;
            // 
            // chkCaracteresespeciais
            // 
            this.chkCaracteresespeciais.AutoSize = true;
            this.chkCaracteresespeciais.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCaracteresespeciais.Location = new System.Drawing.Point(466, 90);
            this.chkCaracteresespeciais.Name = "chkCaracteresespeciais";
            this.chkCaracteresespeciais.Size = new System.Drawing.Size(319, 20);
            this.chkCaracteresespeciais.TabIndex = 5;
            this.chkCaracteresespeciais.Text = "Remover caracteres especiais menos o hífen ( - )";
            this.chkCaracteresespeciais.UseVisualStyleBackColor = true;
            // 
            // lblTextotempo
            // 
            this.lblTextotempo.AutoSize = true;
            this.lblTextotempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextotempo.Location = new System.Drawing.Point(12, 152);
            this.lblTextotempo.Name = "lblTextotempo";
            this.lblTextotempo.Size = new System.Drawing.Size(352, 16);
            this.lblTextotempo.TabIndex = 0;
            this.lblTextotempo.Text = "Tempo para verificar uma atualização de arquivo ou URL:";
            // 
            // txtTempoexec
            // 
            this.txtTempoexec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTempoexec.Location = new System.Drawing.Point(370, 149);
            this.txtTempoexec.Name = "txtTempoexec";
            this.txtTempoexec.Size = new System.Drawing.Size(100, 22);
            this.txtTempoexec.TabIndex = 8;
            // 
            // lblTextoarqdado
            // 
            this.lblTextoarqdado.AutoSize = true;
            this.lblTextoarqdado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoarqdado.Location = new System.Drawing.Point(12, 180);
            this.lblTextoarqdado.Name = "lblTextoarqdado";
            this.lblTextoarqdado.Size = new System.Drawing.Size(243, 16);
            this.lblTextoarqdado.TabIndex = 0;
            this.lblTextoarqdado.Text = "Cadastrar dados com o seguinte nome:";
            // 
            // txtCadastrodados
            // 
            this.txtCadastrodados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastrodados.Location = new System.Drawing.Point(261, 177);
            this.txtCadastrodados.Name = "txtCadastrodados";
            this.txtCadastrodados.Size = new System.Drawing.Size(209, 22);
            this.txtCadastrodados.TabIndex = 9;
            // 
            // btnSalvadados
            // 
            this.btnSalvadados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvadados.Location = new System.Drawing.Point(476, 175);
            this.btnSalvadados.Name = "btnSalvadados";
            this.btnSalvadados.Size = new System.Drawing.Size(215, 26);
            this.btnSalvadados.TabIndex = 10;
            this.btnSalvadados.Text = "Salvar dados cadastrados";
            this.btnSalvadados.UseVisualStyleBackColor = true;
            this.btnSalvadados.Click += new System.EventHandler(this.BtnSalvadados_Click);
            // 
            // btnCarregadados
            // 
            this.btnCarregadados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarregadados.Location = new System.Drawing.Point(697, 175);
            this.btnCarregadados.Name = "btnCarregadados";
            this.btnCarregadados.Size = new System.Drawing.Size(171, 26);
            this.btnCarregadados.TabIndex = 11;
            this.btnCarregadados.Text = "Carregar dados cadastrados";
            this.btnCarregadados.UseVisualStyleBackColor = true;
            this.btnCarregadados.Click += new System.EventHandler(this.BtnCarregadados_Click);
            // 
            // lblTextoinfoseg
            // 
            this.lblTextoinfoseg.AutoSize = true;
            this.lblTextoinfoseg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoinfoseg.Location = new System.Drawing.Point(476, 152);
            this.lblTextoinfoseg.Name = "lblTextoinfoseg";
            this.lblTextoinfoseg.Size = new System.Drawing.Size(328, 16);
            this.lblTextoinfoseg.TabIndex = 0;
            this.lblTextoinfoseg.Text = "Coloque apenas números de tempo em SEGUNDOS!";
            // 
            // lblTextoccsom
            // 
            this.lblTextoccsom.AutoSize = true;
            this.lblTextoccsom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoccsom.Location = new System.Drawing.Point(12, 212);
            this.lblTextoccsom.Name = "lblTextoccsom";
            this.lblTextoccsom.Size = new System.Drawing.Size(343, 16);
            this.lblTextoccsom.TabIndex = 0;
            this.lblTextoccsom.Text = "Caminho completo do arquivo texto com nome do áudio:";
            // 
            // txtArquivotextosom
            // 
            this.txtArquivotextosom.Enabled = false;
            this.txtArquivotextosom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArquivotextosom.Location = new System.Drawing.Point(361, 209);
            this.txtArquivotextosom.Name = "txtArquivotextosom";
            this.txtArquivotextosom.Size = new System.Drawing.Size(464, 22);
            this.txtArquivotextosom.TabIndex = 12;
            // 
            // btnLocalizatxtsom
            // 
            this.btnLocalizatxtsom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocalizatxtsom.Image = ((System.Drawing.Image)(resources.GetObject("btnLocalizatxtsom.Image")));
            this.btnLocalizatxtsom.Location = new System.Drawing.Point(831, 205);
            this.btnLocalizatxtsom.Name = "btnLocalizatxtsom";
            this.btnLocalizatxtsom.Size = new System.Drawing.Size(37, 30);
            this.btnLocalizatxtsom.TabIndex = 13;
            this.btnLocalizatxtsom.UseVisualStyleBackColor = true;
            this.btnLocalizatxtsom.Click += new System.EventHandler(this.BtnLocalizatxtsom_Click);
            // 
            // lblTextourl
            // 
            this.lblTextourl.AutoSize = true;
            this.lblTextourl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextourl.Location = new System.Drawing.Point(12, 247);
            this.lblTextourl.Name = "lblTextourl";
            this.lblTextourl.Size = new System.Drawing.Size(381, 16);
            this.lblTextourl.TabIndex = 0;
            this.lblTextourl.Text = "Atualizar nome do som através de uma URL com arquivo texto:";
            // 
            // chkUrlsom
            // 
            this.chkUrlsom.AutoSize = true;
            this.chkUrlsom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUrlsom.Location = new System.Drawing.Point(399, 246);
            this.chkUrlsom.Name = "chkUrlsom";
            this.chkUrlsom.Size = new System.Drawing.Size(138, 20);
            this.chkUrlsom.TabIndex = 14;
            this.chkUrlsom.Text = "Informar URL Aqui:";
            this.chkUrlsom.UseVisualStyleBackColor = true;
            this.chkUrlsom.CheckedChanged += new System.EventHandler(this.ChkUrlsom_CheckedChanged);
            // 
            // txtUrlsom
            // 
            this.txtUrlsom.Enabled = false;
            this.txtUrlsom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrlsom.Location = new System.Drawing.Point(540, 244);
            this.txtUrlsom.Name = "txtUrlsom";
            this.txtUrlsom.Size = new System.Drawing.Size(328, 22);
            this.txtUrlsom.TabIndex = 15;
            // 
            // lblTextosomnext
            // 
            this.lblTextosomnext.AutoSize = true;
            this.lblTextosomnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextosomnext.Location = new System.Drawing.Point(12, 279);
            this.lblTextosomnext.Name = "lblTextosomnext";
            this.lblTextosomnext.Size = new System.Drawing.Size(423, 16);
            this.lblTextosomnext.TabIndex = 0;
            this.lblTextosomnext.Text = "Caminho completo do arquivo texto com nome do áudio próximo som:";
            // 
            // txtArquivotextosomnext
            // 
            this.txtArquivotextosomnext.Enabled = false;
            this.txtArquivotextosomnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArquivotextosomnext.Location = new System.Drawing.Point(441, 276);
            this.txtArquivotextosomnext.Name = "txtArquivotextosomnext";
            this.txtArquivotextosomnext.Size = new System.Drawing.Size(384, 22);
            this.txtArquivotextosomnext.TabIndex = 16;
            // 
            // btnLocalizatxtsomnext
            // 
            this.btnLocalizatxtsomnext.Enabled = false;
            this.btnLocalizatxtsomnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocalizatxtsomnext.Image = ((System.Drawing.Image)(resources.GetObject("btnLocalizatxtsomnext.Image")));
            this.btnLocalizatxtsomnext.Location = new System.Drawing.Point(831, 272);
            this.btnLocalizatxtsomnext.Name = "btnLocalizatxtsomnext";
            this.btnLocalizatxtsomnext.Size = new System.Drawing.Size(37, 30);
            this.btnLocalizatxtsomnext.TabIndex = 17;
            this.btnLocalizatxtsomnext.UseVisualStyleBackColor = true;
            this.btnLocalizatxtsomnext.Click += new System.EventHandler(this.BtnLocalizatxtsomnext_Click);
            // 
            // lblTextourlnext
            // 
            this.lblTextourlnext.AutoSize = true;
            this.lblTextourlnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextourlnext.Location = new System.Drawing.Point(12, 311);
            this.lblTextourlnext.Name = "lblTextourlnext";
            this.lblTextourlnext.Size = new System.Drawing.Size(381, 16);
            this.lblTextourlnext.TabIndex = 0;
            this.lblTextourlnext.Text = "Atualizar nome do som através de uma URL com arquivo texto:";
            // 
            // chkUrlsomnext
            // 
            this.chkUrlsomnext.AutoSize = true;
            this.chkUrlsomnext.Enabled = false;
            this.chkUrlsomnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUrlsomnext.Location = new System.Drawing.Point(399, 310);
            this.chkUrlsomnext.Name = "chkUrlsomnext";
            this.chkUrlsomnext.Size = new System.Drawing.Size(138, 20);
            this.chkUrlsomnext.TabIndex = 18;
            this.chkUrlsomnext.Text = "Informar URL Aqui:";
            this.chkUrlsomnext.UseVisualStyleBackColor = true;
            this.chkUrlsomnext.CheckedChanged += new System.EventHandler(this.ChkUrlsomnext_CheckedChanged);
            // 
            // txtUrlsomnext
            // 
            this.txtUrlsomnext.Enabled = false;
            this.txtUrlsomnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrlsomnext.Location = new System.Drawing.Point(543, 308);
            this.txtUrlsomnext.Name = "txtUrlsomnext";
            this.txtUrlsomnext.Size = new System.Drawing.Size(325, 22);
            this.txtUrlsomnext.TabIndex = 19;
            // 
            // chkTransmproxsom
            // 
            this.chkTransmproxsom.AutoSize = true;
            this.chkTransmproxsom.Enabled = false;
            this.chkTransmproxsom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTransmproxsom.Location = new System.Drawing.Point(466, 116);
            this.chkTransmproxsom.Name = "chkTransmproxsom";
            this.chkTransmproxsom.Size = new System.Drawing.Size(227, 20);
            this.chkTransmproxsom.TabIndex = 7;
            this.chkTransmproxsom.Text = "Transmitir dados de próximo som";
            this.chkTransmproxsom.UseVisualStyleBackColor = true;
            this.chkTransmproxsom.CheckedChanged += new System.EventHandler(this.ChkTransmproxsom_CheckedChanged);
            // 
            // lblTextoendipdom
            // 
            this.lblTextoendipdom.AutoSize = true;
            this.lblTextoendipdom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoendipdom.Location = new System.Drawing.Point(12, 341);
            this.lblTextoendipdom.Name = "lblTextoendipdom";
            this.lblTextoendipdom.Size = new System.Drawing.Size(244, 16);
            this.lblTextoendipdom.TabIndex = 0;
            this.lblTextoendipdom.Text = "Endereço de IP ou domínio do servidor:";
            // 
            // txtDominioip
            // 
            this.txtDominioip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDominioip.Location = new System.Drawing.Point(262, 338);
            this.txtDominioip.Name = "txtDominioip";
            this.txtDominioip.Size = new System.Drawing.Size(466, 22);
            this.txtDominioip.TabIndex = 20;
            // 
            // btnResolvernomeip
            // 
            this.btnResolvernomeip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResolvernomeip.Location = new System.Drawing.Point(734, 338);
            this.btnResolvernomeip.Name = "btnResolvernomeip";
            this.btnResolvernomeip.Size = new System.Drawing.Size(134, 23);
            this.btnResolvernomeip.TabIndex = 21;
            this.btnResolvernomeip.Text = "Resolver o domínio";
            this.btnResolvernomeip.UseVisualStyleBackColor = true;
            this.btnResolvernomeip.Click += new System.EventHandler(this.BtnResolvernomeip_Click);
            // 
            // lblTextoporta
            // 
            this.lblTextoporta.AutoSize = true;
            this.lblTextoporta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoporta.Location = new System.Drawing.Point(12, 371);
            this.lblTextoporta.Name = "lblTextoporta";
            this.lblTextoporta.Size = new System.Drawing.Size(43, 16);
            this.lblTextoporta.TabIndex = 0;
            this.lblTextoporta.Text = "Porta:";
            // 
            // txtPorta
            // 
            this.txtPorta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorta.Location = new System.Drawing.Point(61, 368);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(100, 22);
            this.txtPorta.TabIndex = 22;
            // 
            // lblTextoidmon
            // 
            this.lblTextoidmon.AutoSize = true;
            this.lblTextoidmon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoidmon.Location = new System.Drawing.Point(167, 371);
            this.lblTextoidmon.Name = "lblTextoidmon";
            this.lblTextoidmon.Size = new System.Drawing.Size(378, 16);
            this.lblTextoidmon.TabIndex = 0;
            this.lblTextoidmon.Text = "ID do servidor Shoutcast v2 ou ponto de montagem Icecast v2:";
            // 
            // txtIdoumont
            // 
            this.txtIdoumont.Enabled = false;
            this.txtIdoumont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdoumont.Location = new System.Drawing.Point(551, 368);
            this.txtIdoumont.Name = "txtIdoumont";
            this.txtIdoumont.Size = new System.Drawing.Size(317, 22);
            this.txtIdoumont.TabIndex = 23;
            // 
            // lblTextologin
            // 
            this.lblTextologin.AutoSize = true;
            this.lblTextologin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextologin.Location = new System.Drawing.Point(12, 407);
            this.lblTextologin.Name = "lblTextologin";
            this.lblTextologin.Size = new System.Drawing.Size(115, 16);
            this.lblTextologin.TabIndex = 0;
            this.lblTextologin.Text = "Login do servidor:";
            // 
            // txtLoginserver
            // 
            this.txtLoginserver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginserver.Location = new System.Drawing.Point(133, 404);
            this.txtLoginserver.Name = "txtLoginserver";
            this.txtLoginserver.Size = new System.Drawing.Size(228, 22);
            this.txtLoginserver.TabIndex = 24;
            // 
            // lblTextosenha
            // 
            this.lblTextosenha.AutoSize = true;
            this.lblTextosenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextosenha.Location = new System.Drawing.Point(367, 407);
            this.lblTextosenha.Name = "lblTextosenha";
            this.lblTextosenha.Size = new System.Drawing.Size(121, 16);
            this.lblTextosenha.TabIndex = 0;
            this.lblTextosenha.Text = "Senha do servidor:";
            // 
            // txtSenhaserver
            // 
            this.txtSenhaserver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaserver.Location = new System.Drawing.Point(494, 404);
            this.txtSenhaserver.Name = "txtSenhaserver";
            this.txtSenhaserver.PasswordChar = '*';
            this.txtSenhaserver.Size = new System.Drawing.Size(374, 22);
            this.txtSenhaserver.TabIndex = 25;
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacao.Location = new System.Drawing.Point(11, 429);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(199, 20);
            this.lblInformacao.TabIndex = 0;
            this.lblInformacao.Text = "Carregando aplicação...";
            // 
            // btnRevisarinfo
            // 
            this.btnRevisarinfo.Enabled = false;
            this.btnRevisarinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisarinfo.Location = new System.Drawing.Point(17, 556);
            this.btnRevisarinfo.Name = "btnRevisarinfo";
            this.btnRevisarinfo.Size = new System.Drawing.Size(138, 40);
            this.btnRevisarinfo.TabIndex = 26;
            this.btnRevisarinfo.Text = "Revisar informações";
            this.btnRevisarinfo.UseVisualStyleBackColor = true;
            this.btnRevisarinfo.Click += new System.EventHandler(this.BtnRevisarinfo_Click);
            // 
            // btnPararenviords
            // 
            this.btnPararenviords.Enabled = false;
            this.btnPararenviords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPararenviords.Location = new System.Drawing.Point(161, 563);
            this.btnPararenviords.Name = "btnPararenviords";
            this.btnPararenviords.Size = new System.Drawing.Size(212, 33);
            this.btnPararenviords.TabIndex = 27;
            this.btnPararenviords.Text = "Parar o envio de dados de RDS";
            this.btnPararenviords.UseVisualStyleBackColor = true;
            this.btnPararenviords.Click += new System.EventHandler(this.BtnPararenviords_Click);
            // 
            // btnEnviardadosrds
            // 
            this.btnEnviardadosrds.Enabled = false;
            this.btnEnviardadosrds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviardadosrds.Location = new System.Drawing.Point(379, 563);
            this.btnEnviardadosrds.Name = "btnEnviardadosrds";
            this.btnEnviardadosrds.Size = new System.Drawing.Size(249, 33);
            this.btnEnviardadosrds.TabIndex = 28;
            this.btnEnviardadosrds.Text = "Enviar dados de RDS para o servidor";
            this.btnEnviardadosrds.UseVisualStyleBackColor = true;
            this.btnEnviardadosrds.Click += new System.EventHandler(this.BtnEnviardadosrds_Click);
            // 
            // btnVerificardadosderds
            // 
            this.btnVerificardadosderds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerificardadosderds.Location = new System.Drawing.Point(634, 563);
            this.btnVerificardadosderds.Name = "btnVerificardadosderds";
            this.btnVerificardadosderds.Size = new System.Drawing.Size(236, 33);
            this.btnVerificardadosderds.TabIndex = 29;
            this.btnVerificardadosderds.Text = "Verificar dados de RDS para enviar";
            this.btnVerificardadosderds.UseVisualStyleBackColor = true;
            this.btnVerificardadosderds.Click += new System.EventHandler(this.BtnVerificardadosderds_Click);
            // 
            // chkDadossensiveis
            // 
            this.chkDadossensiveis.AutoSize = true;
            this.chkDadossensiveis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDadossensiveis.Location = new System.Drawing.Point(136, 116);
            this.chkDadossensiveis.Name = "chkDadossensiveis";
            this.chkDadossensiveis.Size = new System.Drawing.Size(325, 20);
            this.chkDadossensiveis.TabIndex = 6;
            this.chkDadossensiveis.Text = "Exibir no aplicativo dados sensiveis como senhas";
            this.chkDadossensiveis.UseVisualStyleBackColor = true;
            this.chkDadossensiveis.CheckedChanged += new System.EventHandler(this.ChkDadossensiveis_CheckedChanged);
            // 
            // ntfIcone
            // 
            this.ntfIcone.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfIcone.Icon")));
            this.ntfIcone.Text = "Update RDS";
            this.ntfIcone.Visible = true;
            this.ntfIcone.Click += new System.EventHandler(this.NtfIcone_Click);
            // 
            // lblInformacaoid
            // 
            this.lblInformacaoid.AutoSize = true;
            this.lblInformacaoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacaoid.Location = new System.Drawing.Point(13, 533);
            this.lblInformacaoid.Name = "lblInformacaoid";
            this.lblInformacaoid.Size = new System.Drawing.Size(373, 20);
            this.lblInformacaoid.TabIndex = 0;
            this.lblInformacaoid.Text = "Aguardando carregamento de dados do aplicativo...";
            // 
            // UpdateRDS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(880, 608);
            this.Controls.Add(this.lblInformacaoid);
            this.Controls.Add(this.chkDadossensiveis);
            this.Controls.Add(this.btnVerificardadosderds);
            this.Controls.Add(this.btnEnviardadosrds);
            this.Controls.Add(this.btnPararenviords);
            this.Controls.Add(this.btnRevisarinfo);
            this.Controls.Add(this.lblInformacao);
            this.Controls.Add(this.txtSenhaserver);
            this.Controls.Add(this.lblTextosenha);
            this.Controls.Add(this.txtLoginserver);
            this.Controls.Add(this.lblTextologin);
            this.Controls.Add(this.txtIdoumont);
            this.Controls.Add(this.lblTextoidmon);
            this.Controls.Add(this.txtPorta);
            this.Controls.Add(this.lblTextoporta);
            this.Controls.Add(this.btnResolvernomeip);
            this.Controls.Add(this.txtDominioip);
            this.Controls.Add(this.lblTextoendipdom);
            this.Controls.Add(this.chkTransmproxsom);
            this.Controls.Add(this.txtUrlsomnext);
            this.Controls.Add(this.chkUrlsomnext);
            this.Controls.Add(this.lblTextourlnext);
            this.Controls.Add(this.btnLocalizatxtsomnext);
            this.Controls.Add(this.txtArquivotextosomnext);
            this.Controls.Add(this.lblTextosomnext);
            this.Controls.Add(this.txtUrlsom);
            this.Controls.Add(this.chkUrlsom);
            this.Controls.Add(this.lblTextourl);
            this.Controls.Add(this.btnLocalizatxtsom);
            this.Controls.Add(this.txtArquivotextosom);
            this.Controls.Add(this.lblTextoccsom);
            this.Controls.Add(this.lblTextoinfoseg);
            this.Controls.Add(this.btnCarregadados);
            this.Controls.Add(this.btnSalvadados);
            this.Controls.Add(this.txtCadastrodados);
            this.Controls.Add(this.lblTextoarqdado);
            this.Controls.Add(this.txtTempoexec);
            this.Controls.Add(this.lblTextotempo);
            this.Controls.Add(this.chkCaracteresespeciais);
            this.Controls.Add(this.chkAcentospalavras);
            this.Controls.Add(this.chkNaonotificarsomtray);
            this.Controls.Add(this.chkNaominimsystray);
            this.Controls.Add(this.lblTextoselec);
            this.Controls.Add(this.rbtIcecast);
            this.Controls.Add(this.rbtShoutcastv2);
            this.Controls.Add(this.rbtShoutcastv1);
            this.Controls.Add(this.lblTextotipo);
            this.Controls.Add(this.lblTextotitulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateRDS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update RDS By GabardoHost";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateRDS_FormClosing);
            this.Resize += new System.EventHandler(this.UpdateRDS_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTextotitulo;
        private System.Windows.Forms.Label lblTextotipo;
        private System.Windows.Forms.RadioButton rbtShoutcastv1;
        private System.Windows.Forms.RadioButton rbtShoutcastv2;
        private System.Windows.Forms.RadioButton rbtIcecast;
        private System.Windows.Forms.Label lblTextoselec;
        private System.Windows.Forms.CheckBox chkNaominimsystray;
        private System.Windows.Forms.CheckBox chkNaonotificarsomtray;
        private System.Windows.Forms.CheckBox chkAcentospalavras;
        private System.Windows.Forms.CheckBox chkCaracteresespeciais;
        private System.Windows.Forms.Label lblTextotempo;
        private System.Windows.Forms.TextBox txtTempoexec;
        private System.Windows.Forms.Label lblTextoarqdado;
        private System.Windows.Forms.TextBox txtCadastrodados;
        private System.Windows.Forms.Button btnSalvadados;
        private System.Windows.Forms.Button btnCarregadados;
        private System.Windows.Forms.Label lblTextoinfoseg;
        private System.Windows.Forms.Label lblTextoccsom;
        private System.Windows.Forms.TextBox txtArquivotextosom;
        private System.Windows.Forms.Button btnLocalizatxtsom;
        private System.Windows.Forms.Label lblTextourl;
        private System.Windows.Forms.CheckBox chkUrlsom;
        private System.Windows.Forms.TextBox txtUrlsom;
        private System.Windows.Forms.Label lblTextosomnext;
        private System.Windows.Forms.TextBox txtArquivotextosomnext;
        private System.Windows.Forms.Button btnLocalizatxtsomnext;
        private System.Windows.Forms.Label lblTextourlnext;
        private System.Windows.Forms.CheckBox chkUrlsomnext;
        private System.Windows.Forms.TextBox txtUrlsomnext;
        private System.Windows.Forms.CheckBox chkTransmproxsom;
        private System.Windows.Forms.Label lblTextoendipdom;
        private System.Windows.Forms.TextBox txtDominioip;
        private System.Windows.Forms.Button btnResolvernomeip;
        private System.Windows.Forms.Label lblTextoporta;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label lblTextoidmon;
        private System.Windows.Forms.TextBox txtIdoumont;
        private System.Windows.Forms.Label lblTextologin;
        private System.Windows.Forms.TextBox txtLoginserver;
        private System.Windows.Forms.Label lblTextosenha;
        private System.Windows.Forms.TextBox txtSenhaserver;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.Button btnRevisarinfo;
        private System.Windows.Forms.Button btnPararenviords;
        private System.Windows.Forms.Button btnEnviardadosrds;
        private System.Windows.Forms.Button btnVerificardadosderds;
        private System.Windows.Forms.CheckBox chkDadossensiveis;
        private System.Windows.Forms.NotifyIcon ntfIcone;
        private System.Windows.Forms.OpenFileDialog ofdCca;
        private System.Windows.Forms.Label lblInformacaoid;
    }
}

