﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using UpdateRDS.Properties;

namespace UpdateRDS
{
    public partial class UpdateRDS : Form
    {
        private void InfoErroAplic(string errogeral, string errogeraltrace, bool weberro)
        {
            try
            {
                Process proc = Process.GetCurrentProcess();

                string datadeagora = DateTime.Now.ToString().Replace(":", "").Replace("/", "");
                string identificadorproc = proc.Id.ToString();
                string caminhoarquivologapp = $@"{diretoriodoaplicativo}LOGS\ERRO {datadeagora} LOG.html";

                if (!Directory.Exists($@"{diretoriodoaplicativo}LOGS"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}LOGS");

                if (btnVerificardadosderds.Visible == false)
                    Updrdsfcar.CarregaInfo(errogeral);

                if (erroconta < 1 && errocontanext < 1)
                {
                    if (erroconta == -3)
                    {
                        erroconta = -1;
                    }

                    if (erroconta < -1)
                    {
                        erroconta = 250;
                        errfilec = errogeral;
                    }

                    if (errocontanext < -1)
                    {
                        errocontanext = 250;
                        errfilecnext = errogeral;
                    }

                    if (chkNaonotificarsomtray.Checked == false)
                    {
                        if (weberro == true)
                        {
                            ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro de conexão", errogeral, ToolTipIcon.Warning);
                        }
                        else
                        {
                            ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro", errogeral, ToolTipIcon.Error);
                        }
                    }

                    using (XmlTextWriter xtwerr = new XmlTextWriter(caminhoarquivologapp, Encoding.Default)
                    {
                        Formatting = Formatting.Indented
                    })
                    {
                        xtwerr.WriteStartElement("html");
                        xtwerr.WriteStartElement("head");
                        xtwerr.WriteElementString("title", null, "ERRO DO APLICATIVO EM EXECUÇÃO");
                        xtwerr.WriteEndElement();
                        xtwerr.WriteStartElement("body");
                        xtwerr.WriteElementString("h1", null, "ERRO DO APLICATIVO EM EXECUÇÃO:");
                        xtwerr.WriteElementString("p", $"Versão do software que está utilizando: {versaoappcurrent}");
                        xtwerr.WriteElementString("p", $"Data e hora do erro: {DateTime.Now.ToString()}");
                        xtwerr.WriteElementString("p", $"Mensagem de erro: {errogeral}");
                        xtwerr.WriteElementString("p", $"Mensagem de erro técnico: {errogeraltrace}");
                        xtwerr.WriteElementString("p", $"Nome da emissora: {txtNomeemi.Text}");
                        xtwerr.WriteElementString("p", $"Codificação de caracteres: {cbCaracteres.SelectedItem}. Item selecionado: {cbCaracteres.SelectedIndex}");
                        xtwerr.WriteElementString("p", $"Tipo de servidor: {cbTiposervidor.SelectedItem}. Item selecionado: {cbTiposervidor.SelectedIndex}");
                        VerTrueFalse(chkNaominimsystray.Checked);
                        xtwerr.WriteElementString("p", "Não minimizar no system tray: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkNaonotificarsomtray.Checked);
                        xtwerr.WriteElementString("p", "Não notificar no system tray: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkAcentospalavras.Checked);
                        xtwerr.WriteElementString("p", "Remover acentos das palavras: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkCaracteresespeciais.Checked);
                        xtwerr.WriteElementString("p", "Remover caracteres especiais: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkDadossensiveis.Checked);
                        xtwerr.WriteElementString("p", "Exibir dados sensíveis como senhas: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkTransmproxsom.Checked);
                        xtwerr.WriteElementString("p", "Transmitir próximo som: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkUsoproxy.Checked);
                        xtwerr.WriteElementString("p", "Uso um servidor proxy para acesso a internet: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkAutenticaproxy.Checked);
                        xtwerr.WriteElementString("p", "Uso autenticação para o servidor proxy: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        xtwerr.WriteElementString("p", $"Domínio ou endereço de IP do servidor proxy: {txtDoproxy.Text}");
                        xtwerr.WriteElementString("p", $"Porta do servidor proxy: {txtPortaproxy.Text}");
                        xtwerr.WriteElementString("p", $"Login do servidor proxy: {txtLoginproxy.Text}");
                        xtwerr.WriteElementString("p", $"Senha do servidor proxy: {txtSenhaproxy.Text}");
                        xtwerr.WriteElementString("p", $"Tempo de execução para verificação de arquivo ou URL: {txtTempoexec.Text}");
                        xtwerr.WriteElementString("p", $"Caminho do arquivo de texto do som: {lblArquivotextosom.Text}");
                        xtwerr.WriteElementString("p", $"Caminho do arquivo de texto do próximo som: {lblArquivotextosomnext.Text}");
                        VerTrueFalse(chkUrlsom.Checked);
                        xtwerr.WriteElementString("p", "Atualizar título de som através de uma URL: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        xtwerr.WriteElementString("p", $"URL Informada para captura da informação do nome de som: {txtUrlsom.Text}");
                        VerTrueFalse(chkUrlsomnext.Checked);
                        xtwerr.WriteElementString("p", "Atualizar título do próximo som através de uma URL: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        xtwerr.WriteElementString("p", $"URL Informada para captura da informação do nome do próximo som: {txtUrlsomnext.Text}");
                        xtwerr.WriteElementString("p", $"Domínio ou endereço de IP informado para o servidor: {txtDominioip.Text}");
                        xtwerr.WriteElementString("p", $"Porta informada para o servidor: {txtPorta.Text}");
                        xtwerr.WriteElementString("p", $"Ponto de montagem ou ID informado para o servidor: {txtIdoumont.Text}");
                        xtwerr.WriteElementString("p", $"Login do servidor: {txtLoginserver.Text}");
                        xtwerr.WriteElementString("p", $"Senha do servidor: {txtSenhaserver.Text}");
                        xtwerr.WriteElementString("p", $"ID do processo em execução: {identificadorproc}");
                        xtwerr.WriteEndElement();
                        xtwerr.WriteEndElement();
                    }
                }
            }
            catch (Exception ex)
            {
                qualquerlixoaqui = ex.Message;
            }
        }

        private void ErroWebConServ(string weberrogeral, string weberrogeralcode)
        {
            string weberroexplic = null;

            string caracteresaanalisar = @"(?i)[^0-9]";

            Regex rgx = new Regex(caracteresaanalisar);

            string coderro = rgx.Replace(weberrogeral, "");

            if (cbTiposervidor.SelectedIndex == 0 && weberrogeral == "Impossível conectar-se ao servidor remoto")
            {
                weberroexplic = $"Este erro indica que o servidor não está no ar. \nVerifique se o servidor http://{txtDominioip.Text}:{txtPorta.Text}/ está funcionando!";
            }

            if (coderro == "400")
                weberroexplic = "Este erro indica que o encoder que transmite a rádio pode não estar no ar \nOu o ponto de montagem informado não está correto!";

            if (coderro == "401")
                weberroexplic = "Este erro indica que você errou a senha ou o ID \nOu o ponto de montagem do servidor não aceita o login e senha informados!";

            if (coderro == "403")
            {
                weberroexplic = "Este erro indica que o servidor proibiu o acesso aos dados \nOu o ponto de montagem do servidor não aceita o acesso!";
                if (chkUsoproxy.Checked == true)
                {
                    weberroexplic = $"Este erro indica que o servidor proxy {txtDoproxy.Text}:{txtPortaproxy.Text} proibiu o acesso! Será necessário solicitar desbloqueio para o endereço http://{txtDominioip.Text}:{txtPorta.Text}/ para que os dados sejam enviados!";
                }
            }

            if (coderro == "503")
            {
                weberroexplic = "Este erro indica que o servidor que você está se conectando está com problemas! \nOu o erro pode ser um retorno de erro do servidor proxy, checar se o servidor proxy está funcionando corretamente!";
            }

            if (weberrogeralcode == "ConnectFailure")
            {
                if (chkUsoproxy.Checked == true)
                {
                    weberroexplic = $"Este erro indica que o servidor ou o servidor proxy não está no ar. \nVerifique se o servidor http://{txtDominioip.Text}:{txtPorta.Text}/ está funcionando e se o proxy {txtDoproxy.Text}:{txtPortaproxy.Text} está funcionando!";
                }
                else
                    weberroexplic = $"Este erro indica que o servidor não está no ar. \nVerifique se o servidor http://{txtDominioip.Text}:{txtPorta.Text}/ está funcionando!";
            }

            if (weberrogeralcode == "NameResolutionFailure")
                weberroexplic = $"Verifique se não há erros de digitação do domínio informado!";


            if (weberrogeralcode == "ProxyNameResolutionFailure")
                weberroexplic = $"Verifique se não há erros de digitação na caixa de texto de domínio do servidor proxy informado!";

            if (coderro == "407")
            {
                if (chkAutenticaproxy.Checked == true)
                {
                    weberroexplic = $"Verifique se o servidor proxy: {txtDoproxy.Text}:{txtPortaproxy.Text}, o Login: {txtLoginproxy.Text} e a senha: {txtSenhaproxy.Text} do servidor estão corretos e se o servidor está funcionando e se há acesso nesse servidor!";
                }
                else
                    weberroexplic = $"Verifique se o servidor proxy: {txtDoproxy.Text}:{txtPortaproxy.Text} não requer autenticação adicional para acessar o servidor, se for o caso marque a opção 'Meu servidor requer autenticação de proxy' acima!";
            }

            string mensagemerro = $"Título não atualizado devido a um erro ao conectar no servidor: \n{weberrogeral} \n{weberroexplic}";

            if (string.IsNullOrEmpty(weberroexplic))
            {
                mensagemerro = $"Título não atualizado devido a um erro ao conectar no servidor: \n{weberrogeral}";
            }

            if (btnVerificardadosderds.Visible == false)
            {
                mensagemerro = $"{mensagemerro} \nData e hora do erro: {DateTime.Now.ToString()} - Por favor, Verifique a conexão com o servidor! ";
            }
            else
            {
                mensagemerro = $"Houve um erro ao conectar no servidor: \n{weberrogeral} \n{weberroexplic}";
            }
            erroconta = -3;
            throw new Exception(mensagemerro);
        }

        private void CarregarInterfacesPersonalizadas()
        {
            try
            {
                InitializeComponent();

                Process proc = Process.GetCurrentProcess();
                string identificadorproc = proc.Id.ToString();

                ntfIcone.ShowBalloonTip(60000, "Update RDS - Bem vindo!", "Aqui você pode receber notificações se quiser!", ToolTipIcon.Info);

                cbCaracteres.SelectedIndex = 1;
                cbTiposervidor.SelectedIndex = 1;

                lblTextotitulo.Text = "Update RDS By GabardoHost";
                lblInformacaoid.Text = $"Abertura do aplicativo: {DateTime.Now.ToString()} - ID do Processo do aplicativo em execução: {identificadorproc}";
                chkEnviatitulosom.Text = "Enviar título\nde som\nSOMENTE de\nforma manual";

                qualquerlixoaqui = "Para prosseguir com o envio dos dados, preencha corretamente a tela de cadastro e clique no botão verificar dados da mesma tela";

                Updrdsfcar.CarregaInfo(qualquerlixoaqui);

                try
                {
                    UpdateAppRDS(null);
                }
                catch (Exception exup)
                {
                    InfoErroAplic(exup.Message, exup.StackTrace, true);

                    MessageBox.Show($"Infelizmente não foi possível verificar se o aplicativo precisa de atualização!\nNão foi possível verificar devido ao seguinte problema:\n{exup.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                VerifArqConfig(null);
            }
            catch (Exception ex)
            {
                InfoErroAplic(ex.Message, ex.StackTrace, false);

                MessageBox.Show($"Infelizmente não foi possível carregar corretamente o aplicativo!\nNão foi possível carregar devido ao seguinte problema:\n{ex.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void VerifArqConfig(string diretoriodoxml)
        {
            string[] comandosdados = Environment.GetCommandLineArgs();

            foreach (string comando in comandosdados)
            {
                if (!comando.Contains("Update RDS.exe"))
                    diretoriodoxml = comando.ToString();
            }

            if (!string.IsNullOrEmpty(diretoriodoxml))
            {
                if (File.Exists(diretoriodoxml))
                {
                    CarregaXml(diretoriodoxml);
                }
                else
                {
                    throw new Exception("Aviso! O arquivo XML configurado na inicialização desse aplicativo não foi encontrado! Verifique se o arquivo existe no diretório configurado!");
                }
            }
        }

        private void CarregaXml(string diretoriodoarquivoxml)
        {
            string versaoparacomparacao = "Ver-XML-1.0";
            string versaoxmlcarregado;

            XmlDocument oXML = new XmlDocument();
            oXML.Load(diretoriodoarquivoxml);
            XmlElement root = oXML.DocumentElement;
            XmlNodeList lst = root.GetElementsByTagName("SOFTWARE-UPDATE-RDS-XML-VERSION");

            if (lst.Count == 0)
                throw new Exception("Aviso! O arquivo XML carregado é inválido! Preencha novamente os dados da tela e salve um XML novo, ou procure o arquivo XML salvo pelo aplicativo!");

            versaoxmlcarregado = oXML.SelectSingleNode("Configuracao").ChildNodes[0].InnerText;

            if (versaoparacomparacao != versaoxmlcarregado)
            {
                throw new Exception("Aviso! A versão do XML carregada é incompatível com a versão desse software! Preencha novamente os dados da tela para esta versão e salve um XML novo!");
            }

            cbCaracteres.SelectedIndex = int.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[1].InnerText);
            cbTiposervidor.SelectedIndex = int.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[2].InnerText);
            chkEnviatitulosom.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[3].InnerText);
            chkNaominimsystray.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[4].InnerText);
            chkNaonotificarsomtray.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[5].InnerText);
            chkAcentospalavras.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[6].InnerText);
            chkCaracteresespeciais.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[7].InnerText);
            chkDadossensiveis.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[8].InnerText);
            chkTransmproxsom.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[9].InnerText);
            chkUsoproxy.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[10].InnerText);
            chkAutenticaproxy.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[11].InnerText);
            txtDoproxy.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[12].InnerText;
            txtPortaproxy.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[13].InnerText;
            txtLoginproxy.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[14].InnerText;
            txtSenhaproxy.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[15].InnerText;
            txtTempoexec.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[16].InnerText;
            lblArquivotextosom.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[17].InnerText;
            chkUrlsom.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[18].InnerText);
            txtUrlsom.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[19].InnerText;
            lblArquivotextosomnext.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[20].InnerText;
            chkUrlsomnext.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[21].InnerText);
            txtUrlsomnext.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[22].InnerText;
            txtDominioip.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[23].InnerText;
            txtPorta.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[24].InnerText;
            txtIdoumont.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[25].InnerText;
            txtLoginserver.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[26].InnerText;
            txtSenhaserver.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[27].InnerText;
            txtNomeemi.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[28].InnerText;

            if (!string.IsNullOrEmpty(txtNomeemi.Text))
            {
                lblTextotitulo.Text = txtNomeemi.Text;
                Updrdsfcar.InfoEmiNome(txtNomeemi.Text);
                btnNomeemi.Visible = false;
                btnNomeemialt.Visible = true;
                txtNomeemi.Enabled = false;
            }
        }

        private void VerTrueFalse(bool infofalseoutrue)
        {
            try
            {
                if (infofalseoutrue == true)
                {
                    htmltestado = "disabled checked";
                }
                else
                {
                    htmltestado = "disabled";
                }
            }
            catch (Exception ex)
            {
                qualquerlixoaqui = ex.Message;
            }
        }

        private void UpdateAppRDS(string versaonovadoapp)
        {
            if (!Directory.Exists(diretoriodoaplicativo))
                Directory.CreateDirectory(diretoriodoaplicativo);

            lblVersaoapp.Text = "Versão 0.0.7 Alfa\n(Sem verificar nova versão)";
            lblVersaoapp.ForeColor = Color.Yellow;

            // string urlcompletaversao = "http://localhost/versao.txt";
            string urlcompletaversao = "http://www.vanderson.net.br/updaterds/versao.txt";

            string urlcompletadownload = "http://www.vanderson.net.br/updaterds/UpdateRDSInstaller.exe";

            try
            {
                using (WebClient wcurlcompletaversao = new WebClient())
                {
                    wcurlcompletaversao.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                    if (chkUsoproxy.Checked == true)
                    {
                        DadosProxy();
                        wcurlcompletaversao.Proxy = servidorproxydoaplicativo;
                    }

                    Stream strurlcompleta = wcurlcompletaversao.OpenRead(urlcompletaversao);

                    using (StreamReader rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.Default))
                    {
                        versaonovadoapp = rdrurlcompleta.ReadLine();
                    }
                }
            }
            catch (WebException excwebupdate)
            {
                throw new Exception($"A conexão retornou um erro: {excwebupdate.Message}");
            }

            if (versaonovadoapp != versaoappcurrent)
            {
                versaonova = true;

                lblVersaoapp.Text = "Versão 0.0.7 Alfa\n(DESATUALIZADO)";
                lblVersaoapp.ForeColor = Color.Red;

                if (MessageBox.Show($"Há uma nova versão do aplicativo disponível para download, gostaria de baixar a nova versão do aplicativo? a sua versão de aplicativo instalada atualmente é {versaoappcurrent} e a nova versão do aplicativo para baixar é {versaonovadoapp} sendo a nova versão com correções de problemas e outras correções de interface.", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!File.Exists($"{diretoriodoaplicativo}UpdateRDSInstaller.exe"))
                    {
                        using (WebClient wcurlcompletadownload = new WebClient())
                        {
                            wcurlcompletadownload.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                            if (chkUsoproxy.Checked == true)
                            {
                                DadosProxy();
                                wcurlcompletadownload.Proxy = servidorproxydoaplicativo;
                            }

                            wcurlcompletadownload.DownloadFile(urlcompletadownload, $"{diretoriodoaplicativo}UpdateRDSInstaller.exe");
                        }
                    }

                    if (MessageBox.Show($"O aplicativo foi baixado com sucesso! Gostaria de instalar agora?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start($"{diretoriodoaplicativo}UpdateRDSInstaller.exe");

                        foreach (Process processodoaplicativo in Process.GetProcessesByName("Update RDS"))
                        {
                            // processodoaplicativo.Kill();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"O Aplicativo não foi instalado automáticamente!\nPara instalar manualmente a nova versão do aplicativo, entre no diretório {diretoriodoaplicativo} e execute o aplicativo 'UpdateRDSInstaller.exe' para instalar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("O Aplicativo permanecerá desatualizado!\nPara evitar problemas de execução, ter mais novidades de atualização etc desse aplicativo, clique em 'Verificar por atualizações' mais tarde se preferir!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                lblVersaoapp.Text = "Versão 0.0.7 Alfa\n(ATUALIZADO)";
                lblVersaoapp.ForeColor = Color.Green;
                versaonova = false;
            }
        }

        private void DadosProxy()
        {
            string enderecoservidorproxy = $"http://{txtDoproxy.Text}:{txtPortaproxy.Text}";

            Uri uridoproxyserver = new Uri(enderecoservidorproxy);

            servidorproxydoaplicativo.Address = uridoproxyserver;

            // Não bypassa dados para proxy local
            // servidorproxydoaplicativo.BypassProxyOnLocal = true;

            if (chkAutenticaproxy.Checked == true)
                servidorproxydoaplicativo.Credentials = new NetworkCredential(txtLoginproxy.Text, txtSenhaproxy.Text);
        }

        private void ValidarInformacoes()
        {
            string ipserver = txtDominioip.Text;
            string portaserver = txtPorta.Text;
            string idoupontomont = txtIdoumont.Text;

            string caminhoarquivo = $@"{lblArquivotextosom.Text}";
            string caminhoarquivonext = $@"{lblArquivotextosomnext.Text}";

            if (cbTiposervidor.SelectedIndex == 2 & string.IsNullOrEmpty(idoupontomont))
            {
                txtIdoumont.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto ID com números para Shoutcast Server ou ponto de montagem para Icecast Server!");
            }

            if (cbTiposervidor.SelectedIndex == 1 & !Regex.IsMatch(idoupontomont, @"^[0-9]+$"))
            {
                txtIdoumont.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto ID ou ponto de montagem apenas com números para Shoutcast V2!");
            }

            if (string.IsNullOrEmpty(caminhoarquivo) && chkUrlsom.Checked == false)
            {
                lblArquivotextosom.BackColor = Color.Red;
                throw new Exception("Selecione o arquivo no Caminho do arquivo texto gerado pelo automatizador com o nome do áudio!");
            }

            if (chkUrlsom.Checked == true && string.IsNullOrEmpty(txtUrlsom.Text))
            {
                txtUrlsom.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto URL com o link que leva ao arquivo texto ou a URL do currentsong do servidor shoutcast!");
            }

            if (chkUrlsom.Checked == true && !Uri.IsWellFormedUriString(txtUrlsom.Text, UriKind.Absolute))
            {
                txtUrlsom.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto URL com o link VÁLIDO http://link/arquivotexto.txt que leva ao arquivo texto ou a URL do currentsong do servidor shoutcast!");
            }

            if (chkTransmproxsom.Checked == true && cbTiposervidor.SelectedIndex == 1)
            {
                if (chkUrlsomnext.Checked == true && string.IsNullOrEmpty(txtUrlsomnext.Text))
                {
                    txtUrlsomnext.BackColor = Color.Red;
                    throw new Exception("Preencha a caixa de texto URL com o link que leva ao arquivo texto de próximo som ou a URL do próximo som do servidor shoutcast!");
                }

                if (chkUrlsomnext.Checked == true && !Uri.IsWellFormedUriString(txtUrlsomnext.Text, UriKind.Absolute))
                {
                    txtUrlsomnext.BackColor = Color.Red;
                    throw new Exception("Preencha a caixa de texto URL com um link VÁLIDO http://link/arquivotexto.txt que leva ao arquivo texto de próximo som ou a URL do próximo som do servidor shoutcast!");
                }

                if (string.IsNullOrEmpty(caminhoarquivonext) && chkUrlsomnext.Checked == false)
                {
                    lblArquivotextosomnext.BackColor = Color.Red;
                    throw new Exception("Selecione o arquivo de Caminho do arquivo texto de próximo audio gerado pelo automatizador com o nome do áudio!");
                }

                if (!File.Exists(caminhoarquivonext) && chkUrlsomnext.Checked == false)
                    throw new Exception("O Caminho selecionado para o arquivo de texto com o nome do próximo áudio está incorreto! verifique se o arquivo realmente existe!");
            }

            if (txtTempoexec.Text == "0" || !Regex.IsMatch(txtTempoexec.Text, @"^[0-9]+$"))
            {
                txtTempoexec.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de tempo de verificação de arquivo APENAS COM NÚMEROS para verificar uma atualização de arquivo! NÃO PODE SER VAZIO OU ZERO!");
            }

            if (string.IsNullOrEmpty(ipserver))
            {
                txtDominioip.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto endereço de IP ou nome de domínio!");
            }

            if (string.IsNullOrEmpty(portaserver))
            {
                txtPorta.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto porta!");
            }

            if (!Regex.IsMatch(portaserver, @"^[0-9]+$"))
                throw new Exception("Preencha a caixa de texto porta apenas com números!");

            if (string.IsNullOrEmpty(txtLoginserver.Text))
            {
                txtLoginserver.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto login!");
            }

            if (string.IsNullOrEmpty(txtSenhaserver.Text))
            {
                txtSenhaserver.BackColor = Color.Red;
                throw new Exception("Preencha a caixa de texto senha!");
            }

            if (!File.Exists(caminhoarquivo) && chkUrlsom.Checked == false)
                throw new Exception("O Caminho selecionado para o arquivo de texto com o nome do áudio está incorreto! verifique se o arquivo realmente existe!");

            if (chkUrlsom.Checked == false && chkUrlsomnext.Checked == false && caminhoarquivo == caminhoarquivonext)
            {
                throw new Exception("O Caminho selecionado para o arquivo de texto com o nome do áudio é o mesmo do arquivo texto de proximo som! Você não pode colocar o mesmo arquivo, precisa ser necessariamente dois arquivos diferentes!");
            }

            if (chkUrlsom.Checked == true && chkUrlsomnext.Checked == true && txtUrlsom.Text == txtUrlsomnext.Text)
                throw new Exception("A URL do próximo som é a mesma URL do som atual, as duas URLs não podem ser as mesmas! use URLs com textos diferentes para cadastrar no sistema!");

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
        }

        private void TratamentoURLNowNext(bool eumurlnext)
        {
            string infolabel;

            Process proc = Process.GetCurrentProcess();

            string identificadorproc = proc.Id.ToString();

            string arquivotextoantigo = $@"{diretoriodoaplicativo}{identificadorproc}OLD.txt";
            string urlcompleta = txtUrlsom.Text;
            string dadoscapturadosdaurl = null;

            if (eumurlnext == true)
            {
                urlcompleta = txtUrlsomnext.Text;
                arquivotextoantigo = $@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt";
            }

            if (errocontanext > 0)
            {
                errocontanext = errocontanext - 1;
                throw new Exception(errfilecnext + " Nova tentativa de verificar a URL: " + errocontanext);
            }

            if (erroconta > 0)
            {
                erroconta = erroconta - 1;
                throw new Exception(errfilec + " Nova tentativa de verificar a URL: " + erroconta);
            }

            Stream strurlcompleta = null;
            StreamReader rdrurlcompleta;

            try
            {
                using (WebClient wcurlcompleta = new WebClient())
                {
                    wcurlcompleta.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                    if (chkUsoproxy.Checked == true)
                    {
                        DadosProxy();
                        wcurlcompleta.Proxy = servidorproxydoaplicativo;
                    }

                    strurlcompleta = wcurlcompleta.OpenRead(urlcompleta);
                }
            }
            catch (WebException excecaointerna)
            {
                if (eumurlnext == true)
                {
                    string erroconexaowebexc1 = $"A URL do próximo som informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta e se o servidor está funcionando!";

                    if (chkUsoproxy.Checked == true)
                    {
                        erroconexaowebexc1 = $"A URL do próximo som informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta, se o servidor proxy está funcionando e se o servidor está funcionando!";
                    }
                    errocontanext = -2;
                    throw new Exception(erroconexaowebexc1);
                }
                else
                {
                    string erroconexaowebexc2 = $"A URL informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta e se o servidor está funcionando!";

                    if (chkUsoproxy.Checked == true)
                    {
                        erroconexaowebexc2 = $"A URL informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta, se o servidor proxy está funcionando e se o servidor está funcionando!";
                    }
                    erroconta = -2;
                    throw new Exception(erroconexaowebexc2);
                }
            }

            if (cbCaracteres.SelectedIndex == 0)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres.SelectedIndex == 1)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.Default))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres.SelectedIndex == 2)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.UTF8))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres.SelectedIndex == 3)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.UTF7))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres.SelectedIndex == 4)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.ASCII))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (erroconta == 0)
            {
                infolabel = "Nome do som conectado no servidor! Aguarde atualização de título...";

                Updrdsfcar.CarregaInfo(infolabel);

                erroconta = -1;
            }

            if (errocontanext == 0)
            {
                infolabel = "Nome do próximo som conectado no servidor! Aguarde atualização de título...";

                Updrdsfcar.CarregaInfo(infolabel);

                errocontanext = -1;
            }

            if (string.IsNullOrEmpty(dadoscapturadosdaurl))
            {
                if (eumurlnext == true)
                {
                    errocontanext = -2;
                    throw new Exception("A URL do próximo som informada anteriormente está com problemas! verificar se o texto da URL do próximo som não está vazio!");
                }
                else
                {
                    erroconta = -2;
                    throw new Exception("A URL informada anteriormente está com problemas! verificar se o texto da URL não está vazio!");
                }
            }

            conteudotexto = dadoscapturadosdaurl;

            if (!File.Exists(arquivotextoantigo))
            {
                if (!Directory.Exists($@"{diretoriodoaplicativo}"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}");

                using (FileStream fs = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(dadoscapturadosdaurl);
                    }
                }
            }

            using (StreamReader srOld = new StreamReader(arquivotextoantigo))
            {
                conteudotextoantigo = srOld.ReadLine().ToString();
            }

            if (conteudotexto != conteudotextoantigo || btnVerificardadosderds.Visible == true)
            {
                try
                {
                    File.Delete(arquivotextoantigo);

                    using (FileStream fs = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(dadoscapturadosdaurl);
                        }
                    }
                }
                catch (IOException errfile)
                {
                    qualquerlixoaqui = errfile.Message;

                    System.Threading.Thread.Sleep(1500);

                    File.Delete(arquivotextoantigo);

                    System.Threading.Thread.Sleep(1500);

                    using (FileStream fs = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(dadoscapturadosdaurl);
                        }
                    }
                }
            }
        }

        private void TratamentoTextoNowNext(bool eumarquivonext)
        {
            string informacoes;

            Process proc = Process.GetCurrentProcess();

            string identificadorproc = proc.Id.ToString();

            string arquivotexto = $@"{lblArquivotextosom.Text}";
            string arquivotextoantigo = $@"{lblArquivotextosom.Text}{identificadorproc}.txt";

            if (eumarquivonext == true)
            {
                arquivotexto = $@"{lblArquivotextosomnext.Text}";
                arquivotextoantigo = $@"{lblArquivotextosomnext.Text}{identificadorproc}.txt";
            }

            if (errocontanext > 0)
            {
                errocontanext = errocontanext - 1;
                throw new Exception(errfilecnext + " Nova tentativa de verificar o arquivo: " + errocontanext);
            }

            if (erroconta > 0)
            {
                erroconta = erroconta - 1;
                throw new Exception(errfilec + " Nova tentativa de verificar o arquivo: " + erroconta);
            }

            //if (File.Exists(arquivotexto))
            //{
            //    if (eumnext == true && errocontanext == 1)
            //        errocontanext = 0;

            //    if (eumnext == false && erroconta == 1)
            //        erroconta = 0;
            //}

            if (erroconta == 0)
            {
                informacoes = "Arquivo de nome do som corrigido com sucesso! Aguarde atualização de título...";

                Updrdsfcar.CarregaInfo(informacoes);

                erroconta = -1;
            }

            if (errocontanext == 0)
            {
                informacoes = "Arquivo de nome do próximo som corrigido com sucesso! Aguarde atualização de título...";

                Updrdsfcar.CarregaInfo(informacoes);

                errocontanext = -1;
            }

            if (!File.Exists(arquivotexto))
            {
                if (eumarquivonext == true)
                {
                    errocontanext = -2;

                    throw new Exception("O Caminho informado anteriormente para o arquivo de texto de próximo som está com problemas! \nVerificar se o arquivo ainda existe!");
                }

                erroconta = -2;

                throw new Exception("O Caminho informado anteriormente para o arquivo de texto está com problemas! \nVerificar se o arquivo ainda existe!");
            }

            try
            {
                try
                {
                    if (cbCaracteres.SelectedIndex == 0)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 1)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.Default))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 2)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF8))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 3)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF7))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 4)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.ASCII))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }
                }
                catch (Exception errofile)
                {
                    qualquerlixoaqui = errofile.Message;

                    System.Threading.Thread.Sleep(1500);

                    if (cbCaracteres.SelectedIndex == 0)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 1)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.Default))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 2)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF8))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 3)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF7))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres.SelectedIndex == 4)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.ASCII))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }
                }
            }
            catch (Exception errofilegeral)
            {
                if (errofilegeral.Source == "mscorlib")
                {
                    if (eumarquivonext == true)
                    {
                        errocontanext = -2;

                        throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está em uso por outro aplicativo ou processo do sistema!");
                    }
                    else
                    {
                        erroconta = -2;

                        throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está em uso por outro aplicativo ou processo do sistema!");
                    }
                }

                FileInfo arquivotextomusica = new FileInfo(arquivotexto);

                if (arquivotextomusica.Length == 0)
                {
                    if (eumarquivonext == true)
                    {
                        errocontanext = -2;

                        throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio!");
                    }
                    else
                    {
                        erroconta = -2;

                        throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio!");
                    }
                }
            }

            if (!File.Exists(arquivotextoantigo))
                File.Copy(arquivotexto, arquivotextoantigo);

            if (cbCaracteres.SelectedIndex == 0)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres.SelectedIndex == 1)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.Default))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres.SelectedIndex == 2)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.UTF8))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres.SelectedIndex == 3)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.UTF7))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres.SelectedIndex == 4)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.ASCII))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (conteudotexto.Length > 2000)
            {
                if (eumarquivonext == true)
                {
                    errocontanext = -2;

                    throw new Exception("O arquivo texto de próximo som contém mais de 2000 caracteres \nO servidor não é capaz de receber essa quantidade de caracteres! \nTente apagar algumas palavras do arquivo!");
                }
                else
                {
                    erroconta = -2;

                    throw new Exception("O arquivo texto de som contém mais de 2000 caracteres \nO servidor não é capaz de receber essa quantidade de caracteres! \nTente apagar algumas palavras do arquivo!");
                }
            }

            if (conteudotexto.Length < 1)
            {
                if (eumarquivonext == true)
                {
                    errocontanext = -2;

                    throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio ou falta a primeira linha!");
                }
                else
                {
                    erroconta = -2;

                    throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio ou falta a primeira linha!");
                }
            }

            if (conteudotexto != conteudotextoantigo || btnVerificardadosderds.Visible == true)
            {
                try
                {
                    File.WriteAllText(arquivotextoantigo, string.Empty);

                    if (cbCaracteres.SelectedIndex == 0)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto);
                    }

                    if (cbCaracteres.SelectedIndex == 1)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.Default);
                    }

                    if (cbCaracteres.SelectedIndex == 2)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF8);
                    }

                    if (cbCaracteres.SelectedIndex == 3)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF7);
                    }

                    if (cbCaracteres.SelectedIndex == 4)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.ASCII);
                    }
                }
                catch (IOException errfile)
                {
                    qualquerlixoaqui = errfile.Message;

                    System.Threading.Thread.Sleep(1000);

                    File.WriteAllText(arquivotextoantigo, string.Empty);

                    if (cbCaracteres.SelectedIndex == 0)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto);
                    }

                    if (cbCaracteres.SelectedIndex == 1)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.Default);
                    }

                    if (cbCaracteres.SelectedIndex == 2)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF8);
                    }

                    if (cbCaracteres.SelectedIndex == 3)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF7);
                    }

                    if (cbCaracteres.SelectedIndex == 4)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.ASCII);
                    }
                }
            }
        }

        private void RecInfoDosDadosCad()
        {
            Process proc = Process.GetCurrentProcess();

            string identificadorproc = proc.Id.ToString();
            string labeldeinformacao;
            string urlparacarregar;
            string dadosadicionais;
            string conteudoarquivotextonextsong = "Update RDS By GabardoHost - Vanderson Gabardo";
            string dadosarquivotexto = null;
            string ipserver = txtDominioip.Text;
            string portaserver = txtPorta.Text;
            string senhaserver = $"{txtLoginserver.Text}:{txtSenhaserver.Text}";
            string idoupontomont = txtIdoumont.Text;
            string arquivodelog = $@"{diretoriodoaplicativo}LOGS\SOM{identificadorproc}LOG.csv";
            string urlshoutcastv1 = $"http://{ipserver}:{portaserver}/admin.cgi?mode=updinfo&song=";
            string urlshoutcastv2 = $"http://{ipserver}:{portaserver}/admin.cgi?sid={idoupontomont}&mode=updinfo&song=";
            string urlicecast = $"http://{ipserver}:{portaserver}/admin/metadata?mount=/{idoupontomont}&mode=updinfo&song=";

            lblInformacaoid.Text = $"Última checagem de atualização: {DateTime.Now.ToString()} - ID do Processo do aplicativo em execução: {identificadorproc}";

            if (chkTransmproxsom.Checked == true)
            {
                if (chkUrlsomnext.Checked == true)
                    TratamentoURLNowNext(true);

                else
                    TratamentoTextoNowNext(true);

                conteudoarquivotextonextsong = conteudotexto;
            }

            if (chkUrlsom.Checked == true)
            {
                TratamentoURLNowNext(false);
            }
            else
            {
                TratamentoTextoNowNext(false);
            }

            string conteudoarquivotexto = conteudotexto;
            string conteudoarquivotextoantigo = conteudotextoantigo;

            if (conteudoarquivotexto == conteudoarquivotextonextsong)
            {
                System.Threading.Thread.Sleep(1500);

                if (chkTransmproxsom.Checked == true)
                {
                    if (chkUrlsomnext.Checked == true)
                        TratamentoURLNowNext(true);

                    else
                        TratamentoTextoNowNext(true);

                    conteudoarquivotextonextsong = conteudotexto;
                }

                if (chkUrlsom.Checked == true)
                {
                    TratamentoURLNowNext(false);
                }
                else
                {
                    TratamentoTextoNowNext(false);
                }

                conteudoarquivotexto = conteudotexto;
            }

            if (conteudoarquivotexto != conteudoarquivotextoantigo || btnVerificardadosderds.Visible == true)
            {
                conteudoarquivotexto = conteudoarquivotexto.Replace("&", "e").Replace("_", " ");
                conteudoarquivotextonextsong = conteudoarquivotextonextsong.Replace("&", "e").Replace("_", " ");

                if (chkCaracteresespeciais.Checked == true)
                {
                    string caracteresaanalisar = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\-\s]";

                    Regex rgx = new Regex(caracteresaanalisar);

                    string resultado = rgx.Replace(conteudoarquivotexto, " ");
                    conteudoarquivotexto = resultado;

                    string resultadonext = rgx.Replace(conteudoarquivotextonextsong, " ");
                    conteudoarquivotextonextsong = resultadonext;
                }

                if (chkAcentospalavras.Checked == true)
                {
                    string RemoveAcentos(string textopuroacentuado)
                    {
                        return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(textopuroacentuado));
                    }

                    conteudoarquivotexto = RemoveAcentos(conteudoarquivotexto);

                    string RemoveAcentosNext(string textopuroacentuadonext)
                    {
                        return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(textopuroacentuadonext));
                    }

                    conteudoarquivotextonextsong = RemoveAcentosNext(conteudoarquivotextonextsong);
                }

                urlparacarregar = urlshoutcastv1 + conteudoarquivotexto;

                Icon = Resources.shoutcast;
                ntfIcone.Icon = Resources.shoutcast;
                pbFront.Image = Resources.shoutcast1;

                if (cbTiposervidor.SelectedIndex == 2)
                {
                    urlparacarregar = urlicecast + conteudoarquivotexto;

                    Icon = Resources.icecast;
                    ntfIcone.Icon = Resources.icecast;
                    pbFront.Image = Resources.icecast1;
                }

                if (cbTiposervidor.SelectedIndex == 1)
                {
                    urlparacarregar = urlshoutcastv2 + conteudoarquivotexto;

                    if (chkTransmproxsom.Checked == true)
                        urlparacarregar = urlshoutcastv2 + conteudoarquivotexto + "&next=" + conteudoarquivotextonextsong;
                }
                try
                {
                    HttpWebRequest webreqshouticecast = (HttpWebRequest)WebRequest.Create(urlparacarregar);
                    webreqshouticecast.UserAgent = useragentdef;
                    senhaserver = Convert.ToBase64String(Encoding.Default.GetBytes(senhaserver));
                    webreqshouticecast.Headers.Add("Authorization", "Basic " + senhaserver);
                    webreqshouticecast.Credentials = new NetworkCredential("username", "password");
                    webreqshouticecast.Method = WebRequestMethods.Http.Get;
                    webreqshouticecast.AllowAutoRedirect = true;

                    if (chkUsoproxy.Checked == true)
                    {
                        DadosProxy();
                        webreqshouticecast.Proxy = servidorproxydoaplicativo;
                    }
                    else
                        webreqshouticecast.Proxy = null;

                    if (cbTiposervidor.SelectedIndex == 0)
                    {
                        try
                        {
                            HttpWebResponse webrespshouticecast = (HttpWebResponse)webreqshouticecast.GetResponse();
                            webrespshouticecast.Close();
                        }
                        catch (WebException erroverificar)
                        {
                            if (chkUsoproxy.Checked == true)
                            {
                                if (erroverificar.Message != "O servidor remoto retornou um erro: (502) Gateway Incorreto.")
                                    throw new WebException(erroverificar.Message);
                            }
                            else
                            {
                                if (erroverificar.Message != "A conexão subjacente estava fechada: A conexão foi fechada de modo inesperado.")
                                    throw new WebException(erroverificar.Message);
                            }
                        }
                    }
                    else
                    {
                        HttpWebResponse webrespshouticecast = (HttpWebResponse)webreqshouticecast.GetResponse();
                        webrespshouticecast.Close();
                    }
                }
                catch (WebException excecaodoservidor)
                {
                    ErroWebConServ(excecaodoservidor.Message, excecaodoservidor.Status.ToString());
                }

                string novosdadosarquivotexto = $"{DateTime.Now.ToString()};{conteudoarquivotexto}";

                if (chkTransmproxsom.Checked == true)
                    novosdadosarquivotexto = $"{novosdadosarquivotexto};{conteudoarquivotextonextsong}";

                if (!Directory.Exists($@"{diretoriodoaplicativo}LOGS"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}LOGS");

                if (File.Exists(arquivodelog))
                {
                    using (StreamReader sr = new StreamReader(arquivodelog, Encoding.Default))
                    {
                        dadosarquivotexto = sr.ReadToEnd();
                        // dadosarquivotexto = dadosarquivotexto.Substring(0, dadosarquivotexto.Length - 2);
                    }
                }

                if (string.IsNullOrEmpty(dadosarquivotexto))
                    dadosarquivotexto = "Data e Hora:;Nome do som:;Nome do próximo som:";

                File.WriteAllText(arquivodelog, $"{dadosarquivotexto}\n{novosdadosarquivotexto}", Encoding.Default);

                FileInfo arquivotextolog = new FileInfo(arquivodelog);

                if (arquivotextolog.Length > 10485760)
                    File.Move(arquivodelog, $"{arquivodelog}{DateTime.Now.ToString().Replace(":", "").Replace("/", "")}.csv");

                if (btnVerificardadosderds.Visible == true)
                {
                    labeldeinformacao = $"O RDS Transmitiu agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()} \nSe estiver tudo certo com o cadastro, clique no botão abaixo para começar a transmitir os dados:";

                    if (chkTransmproxsom.Checked == true)
                        labeldeinformacao = $"O RDS Transmitiu agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nA Informação de próximo som é: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()} \nSe estiver tudo certo com o cadastro, clique no botão abaixo para começar a transmitir os dados:";

                    lblInformacaoid.Text = "";
                }
                else
                {
                    labeldeinformacao = $"O RDS Está transmitindo agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()}";

                    if (chkTransmproxsom.Checked == true)
                        labeldeinformacao = $"O RDS Está transmitindo agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nPróximo som: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()}";
                }

                Updrdsfcar.CarregaInfo(labeldeinformacao);

                dadosadicionais = $"No ar o som: {conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()}";

                if (chkTransmproxsom.Checked == true)
                    dadosadicionais = $"No ar o som: {conteudoarquivotexto} \nPróximo som: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()}";

                if (chkNaonotificarsomtray.Checked == false)
                    ntfIcone.ShowBalloonTip(60000, "Update RDS - Atualização de título de som", dadosadicionais, ToolTipIcon.Info);
            }
        }
    }
}