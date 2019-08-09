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
using UpdateRDS.Properties;

/// Update RDS By GabardoHost - Versão 0.0.7 Alfa build
/// @file UpdateRDS.cs
/// <summary>
/// Este arquivo é o código principal do aplicativo
/// </summary>
/// Fiz esse programa de computador para o objetivo de usar apenas na Rádio CBS - Comunicações Brasileira de Sistemas - A Rádio dos profissionais de Tecnologia da informação!
/// Mas resolvi criar um para disponibilizar para todos, pois esse programa ou vem associado a um encoder ou não tem para download, então fiz um!
/// Minha ideia é essa, se uma coisa não existe e você precisa muito, então crie você mesmo! pode ser carro, casa, transmissor de FM, programa de PC, celular etc... CRIE VOCÊ MESMO!!!
/// @author Vanderson Gabardo <vanderson@vanderson.net.br>
/// @date 07/08/2019
/// $Id: UpdateRDS.cs, v0.0.7 2019/08/07 20:30:00 Vanderson Gabardo $

namespace UpdateRDS
{
    public partial class UpdateRDS
    {
        static readonly WebProxy servidorproxydoaplicativo = new WebProxy();
        static string qualquerlixoaqui;
        static readonly string useragentdef = "Update RDS By GabardoHost - Mozilla/50MIL.0 (Windows NeanderThal) KHTML like Gecko Chrome Opera Safari Netscape Internet Exploit Firefox Godzilla Giroflex Alex Marques Print";
        static bool versaonova = false;
        static readonly string versaoappcurrent = "Versao 0.0.7";
        static string conteudotexto;
        static string conteudotextoantigo;
        static int errocontanext = -1;
        static int erroconta = -1;
        static string errfilecnext = null;
        static string errfilec = null;
        static readonly string diretoriodoaplicativo = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Update RDS\";
        static int iconealternar = 0;
        public UpdateRDSInfo Updrdsfcar { get; } = new UpdateRDSInfo();
        static string htmltestado;

        public UpdateRDS()
        {
            CarregarInterfacesPersonalizadas();
        }

        private void Temporizacao_Tick(object Sender, EventArgs e)
        {
            try
            {
                RecInfoDosDadosCad();
            }
            catch (Exception ex)
            {
                if (erroconta == -2 || erroconta == -3)
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, true);
                }
                else
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, false);
                }
            }
        }

        private void TemporizacaoIcone_Tick(object Sender, EventArgs e)
        {
            try
            {
                Process proc = Process.GetCurrentProcess();

                string identificadorproc = proc.Id.ToString();

                DirectoryInfo dir = new DirectoryInfo(diretoriodoaplicativo + @"\LOGS\");

                int indexnum = 0;
                string indexnome = $"ERRO {DateTime.Now.Date.ToString().Replace("00:00:00", "").Replace("/", "")}";
                string indexcsv = ".csv";

                if (iconealternar == 0)
                {
                    iconealternar = 1;
                }
                else
                {
                    if (iconealternar == 1)
                    {
                        iconealternar = 0;
                    }
                }

                FileInfo[] arquivostexto = dir.GetFiles();

                foreach (FileInfo file in arquivostexto)
                {
                    if (file.Name.IndexOf(indexcsv) > 0)
                    {
                        if (iconealternar == 0)
                        {
                            btnAbretelainfo.BackColor = Color.Yellow;
                        }
                        else
                        {
                            btnAbretelainfo.BackColor = Color.LightGreen;
                        }
                    }
                    else
                    {
                        indexnum = file.Name.IndexOf(indexnome);
                        if (indexnum > -1)
                        {
                            if (iconealternar == 0)
                            {
                                btnAbrirappdata.BackColor = Color.Red;
                            }
                            else
                            {
                                btnAbrirappdata.BackColor = Color.Yellow;
                            }
                        }
                        if (indexnum < 0)
                        {
                            btnAbrirappdata.BackColor = Color.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                qualquerlixoaqui = ex.Message;
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
                Updrdsfcar.InfoEmiNome(txtNomeemi.Text);

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
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, true);
                MessageBox.Show($"Infelizmente não foi possível verificar a atualização do aplicativo!\nNão foi possível verificar devido ao seguinte problema:\n{ex.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnVerificardadosderds_Click(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;

                if (chkEnviatitulosom.Checked == true)
                {
                    if (!File.Exists(diretoriodoaplicativo + "CT.txt"))
                        File.WriteAllText(diretoriodoaplicativo + "CT.txt", "Update RDS Ativo de forma manual");

                    lblArquivotextosom.Text = diretoriodoaplicativo + "CT.txt";
                }

                ValidarInformacoes();

                RecInfoDosDadosCad();

                btnVerificardadosderds.Visible = false;
                btnRevisarinfo.Enabled = true;
                btnRevisarinfo.Visible = true;
                lblTextoinforev.Text = "Revisar informações do cadasto:";
                btnEnviardadosrds.Visible = true;
                lblTextodobotao.Text = "   Enviar RDS:";
                cbCaracteres.Enabled = false;
                cbTiposervidor.Enabled = false;
                chkEnviatitulosom.Enabled = false;
                chkTransmproxsom.Enabled = false;
                chkUsoproxy.Enabled = false;
                chkAutenticaproxy.Enabled = false;
                chkUrlsom.Enabled = false;
                chkUrlsomnext.Enabled = false;
                btnCarregadados.Visible = false;
                btnSalvadados.Enabled = false;
                btnResolvernomeip.Enabled = false;
                btnLocalizatxtsomnext.Enabled = false;
                btnLocalizatxtsom.Enabled = false;
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

                lblInformacaoid.Text = "Dados de RDS enviados com sucesso! Clique no botão Enviar RDS para fazer o envio contínuo dos dados!";

                if (chkUrlsom.Checked == true)
                {
                    Updrdsfcar.ArquivoTextoSom("", true);
                }
                else
                    Updrdsfcar.ArquivoTextoSom(lblArquivotextosom.Text, false);
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;

                if (erroconta == -2 || erroconta == -3)
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, true);
                }
                else
                {
                    InfoErroAplic(ex.Message, ex.StackTrace, false);
                }

                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEnviardadosrds_Click(object sender, EventArgs e)
        {
            try
            {
                string informacaolabel;
                erroconta = -1;
                errocontanext = -1;
                int tempoescolhido = Convert.ToInt32(txtTempoexec.Text + "000");

                informacaolabel = "O RDS está verificando e transmitindo dados! Aguarde próxima atualização de título... \nOu atualize o arquivo texto manualmente para que a informação seja atualizada!";

                Updrdsfcar.CarregaInfo(informacaolabel);

                btnEnviardadosrds.Visible = false;
                btnRevisarinfo.Enabled = false;
                btnPararenviords.Visible = true;
                lblTextodobotao.Text = "  Parar o envio:";

                temporizadorgeral.Enabled = true;
                temporizadorgeral.Interval = tempoescolhido;
                temporizadorgeral.Tick += new EventHandler(Temporizacao_Tick);
                temporizadorgeral.Start();

                temporizadoricone.Enabled = true;
                temporizadoricone.Interval = 1000;
                temporizadoricone.Tick += new EventHandler(TemporizacaoIcone_Tick);
                temporizadoricone.Start();
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPararenviords_Click(object sender, EventArgs e)
        {
            try
            {
                string informacaolabel;
                Process proc = Process.GetCurrentProcess();

                string identificadorproc = proc.Id.ToString();
                string caminhoarquivoantigo = $@"{lblArquivotextosom.Text}{identificadorproc}.txt";
                string caminhoarquivoantigonext = $@"{lblArquivotextosomnext.Text}{identificadorproc}.txt";

                if (MessageBox.Show("Você gostaria de parar o envio de dados? ao parar, os dados de RDS não serão enviados para o servidor!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Os dados de RDS pararam de ser enviados para o servidor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    temporizadorgeral.Stop();
                    temporizadoricone.Stop();

                    btnPararenviords.Visible = false;
                    btnRevisarinfo.Visible = false;
                    cbCaracteres.Enabled = true;
                    cbTiposervidor.Enabled = true;
                    chkEnviatitulosom.Enabled = true;
                    chkUsoproxy.Enabled = true;
                    txtTempoexec.Enabled = true;
                    btnSalvadados.Enabled = true;
                    btnCarregadados.Visible = true;
                    chkUrlsom.Enabled = true;
                    txtDominioip.Enabled = true;
                    btnResolvernomeip.Enabled = true;
                    txtPorta.Enabled = true;
                    txtLoginserver.Enabled = true;
                    txtSenhaserver.Enabled = true;
                    lblTextoinforev.Text = "Carregar dados de cadastro:";

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

                    btnVerificardadosderds.Visible = true;

                    lblTextodobotao.Text = "Verificar dados:";
                    informacaolabel = "O RDS Não está sendo transmitido para o servidor! Para continuar enviando dados, clique no botão 'verificar dados' da tela anterior!";
                    lblInformacaoid.Text = "Última checagem de modificação do arquivo: " + DateTime.Now.ToString();

                    Updrdsfcar.CarregaInfo(informacaolabel);

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
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRevisarinfo_Click(object sender, EventArgs e)
        {
            try
            {
                string labeldeinformacao;
                erroconta = -1;
                errocontanext = -1;

                cbCaracteres.Enabled = true;
                cbTiposervidor.Enabled = true;
                chkEnviatitulosom.Enabled = true;
                chkUsoproxy.Enabled = true;
                txtTempoexec.Enabled = true;
                btnSalvadados.Enabled = true;
                btnCarregadados.Visible = true;
                chkUrlsom.Enabled = true;
                btnResolvernomeip.Enabled = true;
                txtDominioip.Enabled = true;
                txtPorta.Enabled = true;
                txtLoginserver.Enabled = true;
                txtSenhaserver.Enabled = true;
                lblTextoinforev.Text = "Carregar dados de cadastro:";

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

                if (chkUrlsom.Checked == false)
                    btnLocalizatxtsom.Enabled = true;

                if (chkUrlsom.Checked == true)
                    txtUrlsom.Enabled = true;

                btnRevisarinfo.Visible = false;
                btnEnviardadosrds.Visible = false;
                btnVerificardadosderds.Visible = true;

                lblTextodobotao.Text = "Verificar dados:";
                lblInformacaoid.Text = "";
                labeldeinformacao = "O Processo de envio foi interrompido com sucesso! \nPara retomar o envio dos dados, preencha ou faça as correções e clique no botão abaixo:";

                Updrdsfcar.CarregaInfo(labeldeinformacao);

                Process proc = Process.GetCurrentProcess();

                string identificadorproc = proc.Id.ToString();
                string caminhoarquivoantigo = $@"{lblArquivotextosom.Text}{identificadorproc}.txt";
                string caminhoarquivoantigonext = $@"{lblArquivotextosomnext.Text}{identificadorproc}.txt";

                File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");
                File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
                File.Delete(caminhoarquivoantigo);
                File.Delete(caminhoarquivoantigonext);
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (MessageBox.Show("Você gostaria de criar um atalho na área de trabalho para essa configuração?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if ("Codigo a implementar" == qualquerlixoaqui)
                    {
                        string diretoriododesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        string nomedolink = "Update RDS ";
                        using (StreamWriter escreveoarquivo = new StreamWriter($"{diretoriododesktop}\\{nomedolink}.lnk"))
                        {
                            escreveoarquivo.WriteLine("[InternetShortcut]");
                            escreveoarquivo.WriteLine("URL=file:///" + Environment.CurrentDirectory);
                            escreveoarquivo.Flush();
                        }
                        MessageBox.Show("O atalho foi criado na área de trabalho conforme solicitado!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

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
                                xtw.WriteEndElement();
                                xtw.WriteEndDocument();
                            }
                            MessageBox.Show("As informações preenchidas aqui foram salvas com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("As informações preenchidas aqui não foram salvas!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCarregadados_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog carregardadosdexml = new OpenFileDialog
                {
                    Filter = "Arquivos XML (*.XML)|*.XML|Arquivos XML (*.xml)|*.xml",
                    FilterIndex = 2,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                })
                {
                    if (carregardadosdexml.ShowDialog() == DialogResult.OK)
                    {
                        if (carregardadosdexml.OpenFile() != null)
                        {
                            CarregaXml(carregardadosdexml.FileName);
                            MessageBox.Show("As informações foram carregadas com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        throw new Exception("Não foi possível carregar a configuração pois houve o cancelamento da abertura do arquivo XML!");
                }
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAbretelainfo_Click(object sender, EventArgs e)
        {
            try
            {
                Updrdsfcar.Show();
            }
            catch (Exception ex)
            {
                qualquerlixoaqui = ex.Message;
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
                lblInformacaoid.Text = "";

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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                }
                else
                {
                    btnLocalizatxtsom.Enabled = true;
                    txtUrlsom.Enabled = false;
                    txtUrlsom.Text = "";
                    lblArquivotextosom.BackColor = Color.LightYellow;

                    if (!string.IsNullOrEmpty(lblArquivotextosom.Text))
                    {
                        lblArquivotextosom.BackColor = Color.LightGreen;
                    }

                    txtUrlsom.BackColor = Color.Empty;
                }
            }
            catch (Exception ex)
            {
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
            }
        }

        private void TxtPorta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtPorta.Text, @"^[0-9]+$"))
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
                qualquerlixoaqui = ex.Message;
            }
        }

        private void TxtTempoexec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtTempoexec.Text, @"^[0-9]+$"))
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
            }
        }

        private void TxtPortaproxy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtPortaproxy.Text, @"^[0-9]+$"))
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
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
                qualquerlixoaqui = ex.Message;
            }
        }

        private void UpdateRDS_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized & chkNaominimsystray.Checked == false)
                    Hide();
            }
            catch (Exception ex)
            {
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRDS_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bool canceloufechamento = false;

                if (btnVerificardadosderds.Visible == false)
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

                Process proc = Process.GetCurrentProcess();

                string identificadorproc = proc.Id.ToString();
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
                lblInformacaoid.Text = "";

                InfoErroAplic(ex.Message, ex.StackTrace, false);
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}