using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace UpdateRDS
{
    public partial class UpdateRDS
    {
        static readonly WebProxy servidorproxydoaplicativo = new WebProxy();
        static bool errodoaplicativo = false;
        static readonly string useragentdef = "Update RDS By GabardoHost v0.3 Beta - Mozilla/50MIL.0 (Windows NeanderThal) KHTML like Gecko Chrome Opera Safari Netscape Internet Exploit Firefox Godzilla Giroflex Alex Marques Print";
        static bool versaonova = false;
        static readonly string versaoappcurrent = "Versao " + Application.ProductVersion;
        static string conteudotexto;
        static string conteudotextoantigo;
        static int arquivoerrocontanext = -1;
        static int arquivoerroconta = -1;
        static int errocontanext = -1;
        static int erroconta = -1;
        static string errfilecnext = null;
        static string errfilec = null;
        static readonly string diretoriodoaplicativo = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Update RDS\";
        static string htmltestado;
        static readonly Process processodoaplicativo = Process.GetCurrentProcess();
        static readonly UpdateRDSManutencao manutencaodoaplicativo = new UpdateRDSManutencao();

        public UpdateRDS()
        {
            try
            {
                InitializeComponent();

                string identificadorproc = processodoaplicativo.Id.ToString();

                ntfIcone.ShowBalloonTip(60000, "Update RDS - Bem vindo!", "Aqui você pode receber notificações se quiser!", ToolTipIcon.Info);

                cbCaracteres.SelectedIndex = 1;
                cbTiposervidor.SelectedIndex = 1;

                lblTextotitulo.Text = "Update RDS By GabardoHost";
                lblInformacaoid.Text = "Para prosseguir com o envio dos dados, preencha corretamente a tela de cadastro e clique em enviar RDS!";
                chkEnviatitulosom.Text = "Enviar título\nde som\nSOMENTE de\nforma manual";

                try
                {
                    UpdateAppRDS(null);
                }
                catch (Exception exup)
                {
                    InfoErroAplic(exup.Message, exup.StackTrace, true);

                    MessageBox.Show($"Infelizmente não foi possível verificar se o aplicativo precisa de atualização!\nNão foi possível verificar devido ao seguinte problema:\n{exup.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                CarregaInfoTelaCadBal(true);

                VerifArqConfig(null);
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);

                MessageBox.Show($"Infelizmente não foi possível carregar corretamente o aplicativo!\nNão foi possível carregar devido ao seguinte problema:\n{ex.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Temporizacao_Tick(object Sender, EventArgs e)
        {
            try
            {
                RecInfoDosDadosCad();
            }
            catch (Exception ex)
            {
                if (erroconta == -3 || errocontanext == -2 || erroconta == -2)
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, true);
                }
                else
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, false);
                }
            }
        }

        private void BtnNomeemi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNomeemi.Text))
                {
                    throw new Exception("O nome da emissora não pode ser em branco!");
                }

                lblTextotitulo.Text = txtNomeemi.Text;
                ntfIcone.Text = $"Update RDS - Nome da rádio: {txtNomeemi.Text}";
                btnNomeemi.Visible = false;
                btnNomeemialt.Visible = true;
                txtNomeemi.Enabled = false;
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnNomeemialt_Click(object sender, EventArgs e)
        {
            try
            {
                btnNomeemialt.Visible = false;
                btnNomeemi.Visible = true;
                txtNomeemi.Enabled = true;
                ntfIcone.Text = "Update RDS By GabardoHost";
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnVerupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkUsoproxy.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtDoproxy.Text))
                    {
                        txtDoproxy.BackColor = Color.Red;
                        throw new Exception("O endereço de IP ou domínio do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                    }

                    if (string.IsNullOrEmpty(txtPortaproxy.Text))
                    {
                        txtPortaproxy.BackColor = Color.Red;
                        throw new Exception("A porta do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                    }

                    if (!Regex.IsMatch(txtPortaproxy.Text, @"^[0-9]+$"))
                        throw new Exception("Preencha a caixa de texto porta do servidor proxy apenas com números!");

                    if (chkAutenticaproxy.Checked == true)
                    {
                        if (string.IsNullOrEmpty(txtLoginproxy.Text))
                        {
                            txtLoginproxy.BackColor = Color.Red;
                            throw new Exception("O login do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                        }

                        if (string.IsNullOrEmpty(txtSenhaproxy.Text))
                        {
                            txtSenhaproxy.BackColor = Color.Red;
                            throw new Exception("A senha do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                        }
                    }
                }

                UpdateAppRDS(null);

                if (versaonova == false)
                {
                    MessageBox.Show($"O Aplicativo instalado nesse sistema está atualizado!\nVerifique se existe uma nova versão do aplicativo novamente mais tarde!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, true);
                MessageBox.Show($"Infelizmente não foi possível verificar a atualização do aplicativo!\nNão foi possível verificar devido ao seguinte problema:\n{ex.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnApagalogerro_Click(object sender, EventArgs e)
        {
            try
            {
                bool arquivosdeletados = false;
                bool apagasomtambem = false;
                string indexnome;

                if (MessageBox.Show("Gostaria de apagar os arquivos de log de som também?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    indexnome = "";
                    apagasomtambem = true;
                }
                else
                {
                    indexnome = $"ERRO";
                    apagasomtambem = false;
                }

                if (Directory.Exists($"{diretoriodoaplicativo}LOGS"))
                {
                    DirectoryInfo dir = new DirectoryInfo(diretoriodoaplicativo + @"LOGS\");
                    FileInfo[] arquivostexto = dir.GetFiles();
                    foreach (FileInfo file in arquivostexto)
                    {
                        if (file.Name.IndexOf(indexnome) > -1)
                        {
                            file.Delete();
                            arquivosdeletados = true;
                        }
                    }

                    if (arquivosdeletados == true)
                    {
                        if (apagasomtambem == true)
                        {
                            MessageBox.Show("Os arquivos de log de som e log de erro foram apagados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Os arquivos de log de erro foram apagados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (apagasomtambem == true)
                        {
                            MessageBox.Show("Não existem arquivos de log de som e log de erro para apagar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Não existem arquivos de erro para apagar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAbrirappdata_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Explorer", diretoriodoaplicativo);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEnviardadosrds_Click(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;

                if (chkEnviatitulosom.Checked == true)
                {
                    if (!File.Exists(diretoriodoaplicativo + "CT.txt"))
                    {
                        if (!string.IsNullOrEmpty(txtNomeemi.Text))
                        {
                            File.WriteAllText(diretoriodoaplicativo + "CT.txt", txtNomeemi.Text);
                        }
                        else
                            File.WriteAllText(diretoriodoaplicativo + "CT.txt", "Update RDS - Enviando dados para o servidor");
                    }

                    lblArquivotextosom.Text = diretoriodoaplicativo + "CT.txt";
                }

                ValidarInformacoes();

                RecInfoDosDadosCad();
                tsmiParar.Enabled = true;
                tsmiIniciar.Enabled = false;
                btnEnviardadosrds.Visible = false;
                cbCaracteres.Enabled = false;
                cbTiposervidor.Enabled = false;
                chkEnviatitulosom.Enabled = false;
                chkTransmproxsom.Enabled = false;
                chkUsoproxy.Enabled = false;
                chkAutenticaproxy.Enabled = false;
                chkUrlsom.Enabled = false;
                chkUrlsomnext.Enabled = false;
                btnCarregadados.Enabled = false;
                btnSalvadados.Enabled = false;
                btnResolvernomeip.Enabled = false;
                btnLocalizatxtsomnext.Enabled = false;
                btnLocalizatxtsom.Enabled = false;
                btnPararenviords.Visible = true;
                txtDoproxy.Enabled = false;
                txtPortaproxy.Enabled = false;
                txtLoginproxy.Enabled = false;
                txtSenhaproxy.Enabled = false;
                txtUrlsom.Enabled = false;
                txtUrlsomnext.Enabled = false;
                txtTempoexec.Enabled = false;
                txtDominioip.Enabled = false;
                txtPorta.Enabled = false;
                txtIdoumont.Enabled = false;
                txtLoginserver.Enabled = false;
                txtSenhaserver.Enabled = false;
                lblInfo.Visible = true;

                if (chkUrlsom.Checked == false)
                {
                    btnEnviatitulosom.Enabled = true;
                    txtTitulodesom.Enabled = true;
                }

                int tempoescolhido = Convert.ToInt32(txtTempoexec.Text + "000");

                lblTextodobotao.Text = "Parar o envio:";

                temporizadorgeral.Interval = tempoescolhido;
                temporizadorgeral.Tick += new EventHandler(Temporizacao_Tick);
                temporizadorgeral.Start();
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                if (erroconta == -3 || errocontanext == -2 || erroconta == -2)
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, true);
                }
                else
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, false);
                }
                if (e == null)
                {
                    errodoaplicativo = true;
                }
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPararenviords_Click(object sender, EventArgs e)
        {
            try
            {
                string identificadorproc = processodoaplicativo.Id.ToString();
                string caminhoarquivoantigo = $@"{lblArquivotextosom.Text}{identificadorproc}.txt";
                string caminhoarquivoantigonext = $@"{lblArquivotextosomnext.Text}{identificadorproc}.txt";

                if (MessageBox.Show("Você gostaria de parar o envio de dados? ao parar, os dados de RDS não serão enviados para o servidor!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Os dados de RDS pararam de ser enviados para o servidor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    temporizadorgeral.Stop();
                    tsmiParar.Enabled = false;
                    tsmiIniciar.Enabled = true;
                    lblInfo.Visible = false;
                    btnPararenviords.Visible = false;
                    cbCaracteres.Enabled = true;
                    cbTiposervidor.Enabled = true;
                    chkEnviatitulosom.Enabled = true;
                    chkUsoproxy.Enabled = true;
                    txtTempoexec.Enabled = true;
                    btnSalvadados.Enabled = true;
                    btnCarregadados.Enabled = true;
                    chkUrlsom.Enabled = true;
                    txtDominioip.Enabled = true;
                    btnResolvernomeip.Enabled = true;
                    txtPorta.Enabled = true;
                    txtLoginserver.Enabled = true;
                    txtSenhaserver.Enabled = true;
                    btnEnviatitulosom.Enabled = false;
                    txtTitulodesom.Enabled = false;

                    txtTitulodesom.Text = "";

                    if (chkUsoproxy.Checked == true)
                    {
                        chkAutenticaproxy.Enabled = true;
                        txtDoproxy.Enabled = true;
                        txtPortaproxy.Enabled = true;
                        if (chkAutenticaproxy.Checked == true)
                        {
                            txtLoginproxy.Enabled = true;
                            txtSenhaproxy.Enabled = true;
                        }
                    }

                    if (chkUrlsom.Checked == false)
                        btnLocalizatxtsom.Enabled = true;

                    if (chkUrlsom.Checked == true)
                        txtUrlsom.Enabled = true;

                    if (cbTiposervidor.SelectedIndex != 0)
                        txtIdoumont.Enabled = true;

                    if (cbTiposervidor.SelectedIndex == 1)
                    {
                        chkTransmproxsom.Enabled = true;

                        if (chkTransmproxsom.Checked == true)
                        {
                            if (chkUrlsomnext.Checked == false)
                                btnLocalizatxtsomnext.Enabled = true;

                            if (chkUrlsomnext.Checked == true)
                                txtUrlsomnext.Enabled = true;

                            chkUrlsomnext.Enabled = true;
                        }
                    }

                    btnEnviardadosrds.Visible = true;

                    lblTextodobotao.Text = "Enviar RDS:";
                    lblInformacaoid.Text = "O RDS parou de ser transmitido para o servidor! Última checagem do arquivo: " + DateTime.Now.ToString();

                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
                    File.Delete(caminhoarquivoantigo);
                    File.Delete(caminhoarquivoantigonext);

                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Informação", "O RDS Não está sendo transmitido para o servidor!", ToolTipIcon.Info);
                }
                else
                    MessageBox.Show("Os dados de RDS continuarão sendo enviados para o servidor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSalvadados_Click(object sender, EventArgs e)
        {
            try
            {
                Stream nomedoarquivoxml;
                erroconta = -1;
                errocontanext = -1;

                ValidarInformacoes();

                using (SaveFileDialog salvardadosdexml = new SaveFileDialog
                {
                    Filter = "Arquivos XML (*.XML)|*.XML|Arquivos XML (*.xml)|*.xml",
                    FilterIndex = 2,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                })
                {
                    if (salvardadosdexml.ShowDialog() == DialogResult.OK)
                    {
                        if ((nomedoarquivoxml = salvardadosdexml.OpenFile()) != null)
                        {
                            using (XmlTextWriter xtw = new XmlTextWriter(nomedoarquivoxml, Encoding.Default)
                            {
                                Formatting = Formatting.Indented
                            })
                            {
                                xtw.WriteStartDocument();
                                xtw.WriteStartElement("Configuracao");
                                xtw.WriteElementString("SOFTWARE-UPDATE-RDS-XML-VERSION", "Ver-XML-1.0");
                                xtw.WriteElementString("CARACTERESELECIONADO", cbCaracteres.SelectedIndex.ToString());
                                xtw.WriteElementString("TIPOSERVERSELECIONADO", cbTiposervidor.SelectedIndex.ToString());
                                xtw.WriteElementString("ENVTITLEMANUAL", chkEnviatitulosom.Checked.ToString());
                                xtw.WriteElementString("NAOMINIMIZARSYSTRAY", chkNaominimsystray.Checked.ToString());
                                xtw.WriteElementString("NAONOTIFICARSYSTRAY", chkNaonotificarsomtray.Checked.ToString());
                                xtw.WriteElementString("REMOVERACENTOSPALAVRAS", chkAcentospalavras.Checked.ToString());
                                xtw.WriteElementString("REMOVERCARACTERESESPECIAIS", chkCaracteresespeciais.Checked.ToString());
                                xtw.WriteElementString("DADOSSENSIVEISEXIBIR", chkDadossensiveis.Checked.ToString());
                                xtw.WriteElementString("TRANSMITIRDADOSPROXSOM", chkTransmproxsom.Checked.ToString());
                                xtw.WriteElementString("USOSERVIDORPROXY", chkUsoproxy.Checked.ToString());
                                xtw.WriteElementString("USOAUTENTICACAOSERVIDORPROXY", chkAutenticaproxy.Checked.ToString());
                                xtw.WriteElementString("IPDOMINIOPROXYSERVER", txtDoproxy.Text);
                                xtw.WriteElementString("PORTASERVIDORPROXY", txtPortaproxy.Text);
                                xtw.WriteElementString("LOGINPROXY", txtLoginproxy.Text);
                                xtw.WriteElementString("SENHAPROXY", txtSenhaproxy.Text);
                                xtw.WriteElementString("TEMPOCHECAGEMTEXTOURL", txtTempoexec.Text);
                                xtw.WriteElementString("TXTARQUIVODETEXTOSOM", lblArquivotextosom.Text);
                                xtw.WriteElementString("ATUALIZARSOMPORURL", chkUrlsom.Checked.ToString());
                                xtw.WriteElementString("TXTURLSOM", txtUrlsom.Text);
                                xtw.WriteElementString("TXTARQUIVOPROXIMOSOM", lblArquivotextosomnext.Text);
                                xtw.WriteElementString("URLPROXIMOSOM", chkUrlsomnext.Checked.ToString());
                                xtw.WriteElementString("TXTURLPROXIMOSOM", txtUrlsomnext.Text);
                                xtw.WriteElementString("IPOUDOMINIO", txtDominioip.Text);
                                xtw.WriteElementString("PORTA", txtPorta.Text);
                                xtw.WriteElementString("IDOUPONTODEMONTAGEM", txtIdoumont.Text);
                                xtw.WriteElementString("LOGINDOSERVER", txtLoginserver.Text);
                                xtw.WriteElementString("SENHADOSERVER", txtSenhaserver.Text);
                                xtw.WriteElementString("NOMEDAEMISSORA", txtNomeemi.Text);
                                xtw.WriteElementString("APLICATIVOGERADORPID", $"PID do Aplicativo gerador do arquivo: {processodoaplicativo.Id.ToString()}");
                                xtw.WriteElementString("DATAEHORA", DateTime.Now.ToString());
                                xtw.WriteElementString("CAMINHOCOMPLETODOARQUIVOGERADO", salvardadosdexml.FileName);
                                xtw.WriteEndElement();
                                xtw.WriteEndDocument();
                            }
                            MessageBox.Show("As informações preenchidas aqui foram salvas com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (MessageBox.Show("Você gostaria de criar um atalho na área de trabalho para essa configuração?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string nomedoarquivo = Path.GetFileName(salvardadosdexml.FileName.Replace(".xml", ""));
                                IWshRuntimeLibrary.WshShell wsh = new IWshRuntimeLibrary.WshShell();
                                IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Update RDS " + nomedoarquivo + ".lnk") as IWshRuntimeLibrary.IWshShortcut;
                                shortcut.Arguments = $"\"{salvardadosdexml.FileName}\"";
                                shortcut.TargetPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Update RDS.exe";
                                shortcut.Description = "Update RDS - Configurações Personalizadas";
                                shortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                                shortcut.Save();

                                MessageBox.Show("O atalho foi criado na área de trabalho conforme solicitado!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("O atalho não foi criado, se preferir, pode criar um atalho manualmente com as configurações", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("As informações preenchidas aqui não foram salvas!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCarregadados_Click(object sender, EventArgs e)
        {
            try
            {
                ofdCca.Multiselect = false;
                ofdCca.Title = "Selecionar arquivo XML";
                ofdCca.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofdCca.Filter = "Arquivos de configuração XML (*.XML;*.xml)|*.XML;*.xml";
                ofdCca.CheckFileExists = true;
                ofdCca.FileName = "";

                DialogResult dr = ofdCca.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    CarregaXml(ofdCca.FileName);
                    MessageBox.Show("As informações foram carregadas com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Não foi possível carregar a configuração pois houve o cancelamento da abertura do arquivo XML!");
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAbretelainfo_Click(object sender, EventArgs e)
        {
            try
            {
                string caminhoarquivo = lblArquivotextosom.Text;
                string dadosdoarquivotexto;

                if (chkUrlsom.Checked == true)
                {
                    throw new Exception("Não é possível enviar título de som manualmente!\nOs títulos de som estão sendo enviados via URL, não é possível atualizar título de som se está sendo atualizado via URL!");
                }
                if (File.Exists(caminhoarquivo))
                {
                    using (StreamReader srManual = new StreamReader(caminhoarquivo, Encoding.Default))
                    {
                        dadosdoarquivotexto = srManual.ReadLine();
                    }

                    if (string.IsNullOrEmpty(txtTitulodesom.Text))
                    {
                        throw new Exception("O título de som informado não pode ser vazio! Será necessário preencher a caixa de texto antes de enviar os dados!");
                    }

                    if (txtTitulodesom.Text.Length > 2000)
                    {
                        throw new Exception("O título de som informado ultrapassa os 2000 caracteres! Será necessário apagar alguns caracteres do texto antes de enviar os dados!");
                    }

                    if (dadosdoarquivotexto == txtTitulodesom.Text)
                    {
                        throw new Exception("O título de som informado já foi enviado! Será necessário preencher a caixa de texto com outro título de som!");
                    }

                    File.WriteAllText(caminhoarquivo, string.Empty);
                    File.WriteAllText(caminhoarquivo, txtTitulodesom.Text.Replace("&", "e"));
                }
                else
                    throw new Exception("Ainda não é possível enviar título de som manualmente!\nExperimente iniciar a transmissão de dados de RDS primeiro! ou verifique se o arquivo texto existe!");
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnLocalizatxtsom_Click(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;

                ofdCca.Multiselect = false;
                ofdCca.Title = "Selecionar arquivo";
                ofdCca.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofdCca.Filter = "Arquivos de texto (*.TXT;*.txt)|*.TXT;*.txt";
                ofdCca.CheckFileExists = true;
                ofdCca.FileName = "";

                DialogResult dr = ofdCca.ShowDialog();

                if (dr == DialogResult.OK)
                    lblArquivotextosom.Text = ofdCca.FileName;
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLocalizatxtsomnext_Click(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;

                ofdCca.Multiselect = false;
                ofdCca.Title = "Selecionar arquivo";
                ofdCca.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofdCca.Filter = "Arquivos de texto (*.TXT;*.txt)|*.TXT;*.txt";
                ofdCca.CheckFileExists = true;
                ofdCca.FileName = "";

                DialogResult dr = ofdCca.ShowDialog();

                if (dr == DialogResult.OK)
                    lblArquivotextosomnext.Text = ofdCca.FileName;
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnResolvernomeip_Click(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;

                string nomeouip = txtDominioip.Text;
                bool endipvalido = IPAddress.TryParse(nomeouip, out IPAddress IP);

                if (endipvalido == true)
                    throw new Exception("Você digitou um endereço de IP! \nAqui você não consegue resolver endereço IP em nome de domínio, somente nome para endereço de IP! \nDigite um nome de domínio válido, como no exemplo: seuservidor.suaurl.com.br");
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(nomeouip);

                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                            txtDominioip.Text = ip.ToString();
                    }
                }
                catch (SocketException sexept)
                {
                    if (sexept.SocketErrorCode.ToString() == "HostNotFound")
                        throw new Exception($"Ocorreu um erro ao tentar resolver o nome para o IP: \nO domínio {txtDominioip.Text} não pode ser resolvido, verifique se não errou a digitação do endereço!");

                    throw new Exception($"Ocorreu um erro ao tentar resolver o nome para o IP: {sexept.Message}");
                }
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, true);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NtfIcone_Click(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;
                if (ShowInTaskbar == false)
                {
                    ShowInTaskbar = true;
                }
                if (WindowState != FormWindowState.Normal)
                {
                    Show();
                    WindowState = FormWindowState.Normal;

                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Você clicou em mim", "E eu abri :D", ToolTipIcon.Info);
                }
                else
                {
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Você clicou em mim", "E eu já estou aberto, não está me vendo? :D", ToolTipIcon.Info);

                    if (chkNaominimsystray.Checked == false && MessageBox.Show("Você gostaria de minimizar na bandeja do sistema?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        WindowState = FormWindowState.Minimized;
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CbTiposervidor_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbTiposervidor.SelectedIndex == 0)
                {
                    txtIdoumont.Enabled = false;
                    txtIdoumont.Text = "";
                    txtIdoumont.BackColor = Color.Empty;
                }
                else
                {
                    txtIdoumont.Enabled = true;
                    txtIdoumont.BackColor = Color.LightYellow;
                }

                if (cbTiposervidor.SelectedIndex == 1)
                {
                    chkTransmproxsom.Enabled = true;
                    if (!string.IsNullOrEmpty(txtIdoumont.Text) && !Regex.IsMatch(txtIdoumont.Text, @"^[0-9]+$"))
                    {
                        txtIdoumont.BackColor = Color.Red;
                    }
                }
                else
                {
                    chkTransmproxsom.Checked = false;
                    chkTransmproxsom.Enabled = false;
                    btnLocalizatxtsomnext.Enabled = false;
                    lblArquivotextosomnext.Text = "";
                }

                if (cbTiposervidor.SelectedIndex == 2 && !string.IsNullOrEmpty(txtIdoumont.Text))
                {
                    txtIdoumont.BackColor = Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkTransmproxsom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTransmproxsom.Checked == true)
                {
                    btnLocalizatxtsomnext.Enabled = true;
                    chkUrlsomnext.Enabled = true;
                    lblArquivotextosomnext.BackColor = Color.LightYellow;
                }
                else
                {
                    btnLocalizatxtsomnext.Enabled = false;
                    lblArquivotextosomnext.Text = "";
                    lblArquivotextosomnext.BackColor = Color.Empty;
                    txtUrlsomnext.Text = "";
                    chkUrlsomnext.Enabled = false;
                    chkUrlsomnext.Checked = false;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkUrlsom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUrlsom.Checked == true)
                {
                    txtUrlsom.Enabled = true;
                    btnLocalizatxtsom.Enabled = false;
                    lblArquivotextosom.Text = "";
                    lblArquivotextosom.BackColor = Color.Empty;
                    txtUrlsom.BackColor = Color.LightYellow;
                    btnEnviatitulosom.Enabled = false;
                    txtTitulodesom.Enabled = false;
                    txtTitulodesom.Text = "";
                }
                else
                {
                    btnLocalizatxtsom.Enabled = true;
                    txtUrlsom.Enabled = false;
                    txtUrlsom.Text = "";
                    lblArquivotextosom.BackColor = Color.LightYellow;

                    if (btnEnviardadosrds.Visible == false)
                    {
                        btnEnviatitulosom.Enabled = true;
                        txtTitulodesom.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(lblArquivotextosom.Text))
                    {
                        lblArquivotextosom.BackColor = Color.LightGreen;
                    }

                    txtUrlsom.BackColor = Color.Empty;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkUrlsomnext_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUrlsomnext.Checked == true)
                {
                    txtUrlsomnext.Enabled = true;
                    btnLocalizatxtsomnext.Enabled = false;
                    lblArquivotextosomnext.Text = "";
                    lblArquivotextosomnext.BackColor = Color.Empty;
                    txtUrlsomnext.BackColor = Color.LightYellow;
                }
                else
                {
                    if (chkTransmproxsom.Checked == true)
                    {
                        lblArquivotextosomnext.BackColor = Color.LightYellow;

                        if (!string.IsNullOrEmpty(lblArquivotextosomnext.Text))
                        {
                            lblArquivotextosomnext.BackColor = Color.LightGreen;
                        }

                        btnLocalizatxtsomnext.Enabled = true;
                    }

                    txtUrlsomnext.Enabled = false;
                    txtUrlsomnext.Text = "";
                    txtUrlsomnext.BackColor = Color.Empty;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkDadossensiveis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDadossensiveis.Checked == true)
                {
                    txtSenhaserver.PasswordChar = Convert.ToChar(0);
                    txtSenhaproxy.PasswordChar = Convert.ToChar(0);
                }
                else
                {
                    txtSenhaserver.PasswordChar = Convert.ToChar("*");
                    txtSenhaproxy.PasswordChar = Convert.ToChar("*");
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkUsoproxy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUsoproxy.Checked == true)
                {
                    txtDoproxy.Enabled = true;
                    txtPortaproxy.Enabled = true;
                    chkAutenticaproxy.Enabled = true;
                    txtDoproxy.BackColor = Color.LightYellow;
                    txtPortaproxy.BackColor = Color.LightYellow;
                }
                else
                {
                    txtDoproxy.BackColor = Color.Empty;
                    txtPortaproxy.BackColor = Color.Empty;
                    txtDoproxy.Enabled = false;
                    txtPortaproxy.Enabled = false;
                    chkAutenticaproxy.Enabled = false;
                    chkAutenticaproxy.Checked = false;
                    txtDoproxy.Text = null;
                    txtPortaproxy.Text = null;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkAutenticaproxy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutenticaproxy.Checked == true)
                {
                    txtLoginproxy.Enabled = true;
                    txtSenhaproxy.Enabled = true;
                    txtLoginproxy.BackColor = Color.LightYellow;
                    txtSenhaproxy.BackColor = Color.LightYellow;
                }
                else
                {
                    txtLoginproxy.Enabled = false;
                    txtSenhaproxy.Enabled = false;
                    txtLoginproxy.Text = null;
                    txtSenhaproxy.Text = null;
                    txtLoginproxy.BackColor = Color.Empty;
                    txtSenhaproxy.BackColor = Color.Empty;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkEnviatitulosom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEnviatitulosom.Checked == true)
                {
                    btnLocalizatxtsom.Visible = false;
                    chkUrlsom.Visible = false;
                    chkUrlsom.Checked = false;
                    txtUrlsom.Text = "";
                    lblArquivotextosom.Text = "";
                    lblArquivotextosom.BackColor = Color.Empty;
                }
                else
                {
                    btnLocalizatxtsom.Visible = true;
                    chkUrlsom.Visible = true;
                    lblArquivotextosom.BackColor = Color.LightYellow;
                    txtUrlsom.Text = "";
                    lblArquivotextosom.Text = "";
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ChkNaominimsystray_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkNaominimsystray.Checked == true)
                {
                    tsmiOcultar.Enabled = false;
                }
                else
                {
                    tsmiOcultar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtUrlsom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUrlsom.Text) && txtUrlsomnext.Text != txtUrlsom.Text && Uri.IsWellFormedUriString(txtUrlsom.Text, UriKind.Absolute))
                {
                    txtUrlsom.BackColor = Color.LightGreen;
                }
                else
                {
                    txtUrlsom.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtUrlsomnext_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUrlsomnext.Text) && txtUrlsomnext.Text != txtUrlsom.Text && Uri.IsWellFormedUriString(txtUrlsomnext.Text, UriKind.Absolute))
                {
                    txtUrlsomnext.BackColor = Color.LightGreen;
                }
                else
                {
                    txtUrlsomnext.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtDominioip_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDominioip.Text))
                {
                    txtDominioip.BackColor = Color.LightGreen;
                }
                else
                {
                    txtDominioip.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtPorta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtPorta.Text, @"^[0-9]+$") && Convert.ToInt32(txtPorta.Text) < 65535 && Convert.ToInt32(txtPorta.Text) > 0)
                {
                    txtPorta.BackColor = Color.LightGreen;
                }
                else
                {
                    txtPorta.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtTempoexec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtTempoexec.Text, @"^[0-9]+$") && Convert.ToInt32(txtTempoexec.Text) > 0)
                {
                    txtTempoexec.BackColor = Color.LightGreen;
                }
                else
                {
                    txtTempoexec.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtLoginserver_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLoginserver.Text))
                {
                    txtLoginserver.BackColor = Color.LightGreen;
                }
                else
                {
                    txtLoginserver.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtSenhaserver_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSenhaserver.Text))
                {
                    txtSenhaserver.BackColor = Color.LightGreen;
                }
                else
                {
                    txtSenhaserver.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtIdoumont_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIdoumont.Text))
                {
                    if (cbTiposervidor.SelectedIndex == 1 && Regex.IsMatch(txtIdoumont.Text, @"^[0-9]+$"))
                    {
                        txtIdoumont.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        txtIdoumont.BackColor = Color.Red;
                    }

                    if (cbTiposervidor.SelectedIndex == 2)
                    {
                        txtIdoumont.BackColor = Color.LightGreen;
                    }
                }
                else
                {
                    txtIdoumont.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtDoproxy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDoproxy.Text))
                {
                    txtDoproxy.BackColor = Color.LightGreen;
                }
                else
                {
                    if (chkUsoproxy.Checked == true)
                        txtDoproxy.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtPortaproxy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtPortaproxy.Text, @"^[0-9]+$") && Convert.ToInt32(txtPortaproxy.Text) < 65535 && Convert.ToInt32(txtPortaproxy.Text) > 0)
                {
                    txtPortaproxy.BackColor = Color.LightGreen;
                }
                else
                {
                    if (chkUsoproxy.Checked == true)
                        txtPortaproxy.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtLoginproxy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLoginproxy.Text))
                {
                    txtLoginproxy.BackColor = Color.LightGreen;
                }
                else
                {
                    txtLoginproxy.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void TxtSenhaproxy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSenhaproxy.Text))
                {
                    txtSenhaproxy.BackColor = Color.LightGreen;
                }
                else
                {
                    txtSenhaproxy.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void LblArquivotextosom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(lblArquivotextosom.Text) && lblArquivotextosom.Text != lblArquivotextosomnext.Text)
                {
                    lblArquivotextosom.BackColor = Color.LightGreen;
                }
                else
                {
                    lblArquivotextosom.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void LblArquivotextosomnext_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(lblArquivotextosomnext.Text) && lblArquivotextosom.Text != lblArquivotextosomnext.Text)
                {
                    lblArquivotextosomnext.BackColor = Color.LightGreen;
                }
                else
                {
                    lblArquivotextosomnext.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void UpdateRDS_Load(object sender, EventArgs e)
        {
            try
            {
                string[] comandosdados = Environment.GetCommandLineArgs();

                foreach (string comando in comandosdados)
                {
                    if (!comando.Contains("Update RDS.exe"))
                    {
                        if (comando.Contains("-O"))
                        {
                            BtnEnviardadosrds_Click(null, null);
                            if (errodoaplicativo != true)
                            {
                                Hide();
                                ShowInTaskbar = false;
                                TsmiOcultar_Click(null, null);
                            }
                        }
                        if (comando.Contains("-o"))
                        {
                            if (errodoaplicativo != true)
                            {
                                Hide();
                                ShowInTaskbar = false;
                                TsmiOcultar_Click(null, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRDS_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized & chkNaominimsystray.Checked == false)
                {
                    Hide();
                    tsmiExibir.Enabled = true;
                    tsmiOcultar.Enabled = false;
                }
                if (WindowState == FormWindowState.Normal & chkNaominimsystray.Checked == false)
                {
                    tsmiExibir.Enabled = false;
                    tsmiOcultar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRDS_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bool canceloufechamento = false;

                if (btnEnviardadosrds.Visible == false)
                {
                    if (MessageBox.Show("Você gostaria MESMO de fechar esse programa? ao fechar o aplicativo, os dados de RDS não serão enviados para o servidor!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        e.Cancel = true;

                        MessageBox.Show("Os dados de RDS continuam sendo enviados para o servidor!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        canceloufechamento = true;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000);

                        MessageBox.Show("O Aplicativo encerrou a execução, verifique se existe mais algum arquivo temporário na pasta do texto, caso tenha é só apagar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                string identificadorproc = processodoaplicativo.Id.ToString();
                string caminhoarquivoantigo = $@"{lblArquivotextosom.Text}{identificadorproc}.txt";

                if (File.Exists(caminhoarquivoantigo) && canceloufechamento == false)
                    File.Delete(caminhoarquivoantigo);

                string caminhoarquivoantigonext = $@"{lblArquivotextosomnext.Text}{identificadorproc}.txt";

                if (File.Exists(caminhoarquivoantigonext) && canceloufechamento == false)
                    File.Delete(caminhoarquivoantigonext);

                if (File.Exists($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt") && canceloufechamento == false)
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");

                if (File.Exists($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt") && canceloufechamento == false)
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiExibir_Click(object sender, EventArgs e)
        {
            try
            {
                Show();
                WindowState = FormWindowState.Normal;
                tsmiExibir.Enabled = false;
                tsmiOcultar.Enabled = true;
                if (ShowInTaskbar != true)
                {
                    ShowInTaskbar = true;
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiOcultar_Click(object sender, EventArgs e)
        {
            try
            {
                Hide();
                WindowState = FormWindowState.Minimized;
                tsmiOcultar.Enabled = false;
                tsmiExibir.Enabled = true;
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiFechar_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiParar_Click(object sender, EventArgs e)
        {
            try
            {
                BtnPararenviords_Click(null, null);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                BtnEnviardadosrds_Click(null, null);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiAppdata_Click(object sender, EventArgs e)
        {
            try
            {
                BtnAbrirappdata_Click(null, null);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TsmiApagalog_Click(object sender, EventArgs e)
        {
            try
            {
                BtnApagalogerro_Click(null, null);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}