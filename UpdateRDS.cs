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

/// Update RDS By GabardoHost - Versão 0.0.1 Alfa build
/// @file UpdateRDS.cs
/// <summary>
/// Este arquivo é o código principal do aplicativo
/// </summary>
/// Fiz esse programa de computador para o objetivo de usar apenas na Rádio CBS - Comunicações Brasileira de Sistemas - A Rádio dos profissionais de Tecnologia da informação!
/// Mas resolvi criar um para disponibilizar para todos, pois esse programa ou vem associado a um encoder ou não tem para download, então fiz um!
/// Minha ideia é essa, se uma coisa não existe e você precisa muito, então crie você mesmo! pode ser carro, casa, transmissor de FM, programa de PC, celular etc... CRIE VOCÊ MESMO!!!
/// @author Vanderson Gabardo <vanderson@vanderson.net.br>
/// @date 18/07/2019
/// $Id: UpdateRDS.cs, v0.0.1 2019/07/25 22:30:00 Vanderson Gabardo $

namespace UpdateRDS
{
    public partial class UpdateRDS : Form
    {
        /// Declaração de itens para uso pelo aplicativo de forma geral
        static readonly Timer temporizadorgeral = new Timer();
        static readonly WebProxy servidorproxydoaplicativo = new WebProxy();
        static string qualquerlixoaqui;
        static readonly string useragentdef = "Update RDS By GabardoHost - Mozilla/50MIL.0 (Windows NeanderThal) KHTML like Gecko Chrome Opera Safari Netscape Internet Exploit Firefox Godzilla Giroflex Alex Marques Print";
        static string errogeralgravado;
        static string errogeral;
        static string weberrogeralcode;
        static string weberrogeral;
        static bool eumnext = false;
        static bool versaonova = false;
        static readonly string versaoappcurrent = "Versao 0.0.1";
        static string conteudotexto;
        static string conteudotextoantigo;
        static int errocontanext = -1;
        static int erroconta = -1;
        static int errfilecnext = -1;
        static int errfilec = -1;
        static string errodaweblink = null;
        //static readonly string diretoriodoaplicativo = $@"{AppDomain.CurrentDomain.BaseDirectory.ToString()}UpdateRDS\";
        static readonly string diretoriodoaplicativo = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\UpdateRDS\";

        public UpdateRDS()
        {
            InitializeComponent();

            /// Verifica ao inicializar pela versão nova do software
            try
            {
                /// Chama o método para verificar
                UpdateAppRDS();
            }
            catch (Exception ex)
            {
                /// Caso o método retorne um erro, ele acaba aqui
                qualquerlixoaqui = ex.Message;
            }

            /// Carrega as interfaces personalizadas
            CarregarInterfacesPersonalizadas();
        }

        private void CarregarInterfacesPersonalizadas()
        {
            try
            {
                /// Verifica o processo do aplicativo
                Process proc = Process.GetCurrentProcess();

                /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
                string identificadorproc = proc.Id.ToString();

                /// Título do programa
                qualquerlixoaqui = "Update RDS By GabardoHost";

                /// Carrega label de título
                lblTextotitulo.Text = qualquerlixoaqui;

                /// Transfere dados para um balão chato haha de texto
                ntfIcone.ShowBalloonTip(60000, "Update RDS - Bem vindo!", "Aqui você pode receber notificações se quiser!", ToolTipIcon.Info);

                /// Informa o usuário quando foi a última atualização do arquivo via label
                lblInformacaoid.Text = $"Abertura do aplicativo: {DateTime.Now.ToString()} - ID do Processo do aplicativo em execução: {identificadorproc}";

                /// Informa o usuário para clicar no botão para enviar dados
                lblInformacao.Text = "Para prosseguir com o envio dos dados, preencha corretamente a tela e clique no botão abaixo:";
            }
            catch (Exception ex)
            {
                /// Qualquer erro na execução do aplicativo quando carregar, gera erro direto na label
                lblInformacao.Text = ex.Message;
            }
        }

        public void InfoErroGeral()
        {
            try
            {
                /// String para uso de explicação
                string weberroexplic = "";

                /// Verifica o processo do aplicativo
                Process proc = Process.GetCurrentProcess();

                /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
                string identificadorproc = proc.Id.ToString();

                /// Caso o diretório de logs não exista, Criamos um com o nome LOGS
                if (!Directory.Exists($@"{diretoriodoaplicativo}LOGS"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}LOGS");

                /// Pega a informação da localização do arquivo de log de erro
                string caminhoarquivolog = $@"{diretoriodoaplicativo}LOGS\ERRO{identificadorproc}LOG.txt";

                /// Tratamento de erro da web
                if (errogeral == null)
                {
                    /// String para preenchimento dos dados do arquivo texto de erros
                    string dadosdotxterro = null;

                    /// Caso o arquivo texto LOGRDSERRO.txt ou similar exista então ele faz a condição abaixo
                    if (File.Exists(caminhoarquivolog))
                    {
                        /// Pega o arquivo texto para ler
                        using (StreamReader sr = new StreamReader(caminhoarquivolog, Encoding.Default))
                        {
                            /// Carrega arquivo e mantém carregado todas as linhas
                            dadosdotxterro = sr.ReadToEnd();
                        }
                    }

                    /// Pega os caracteres a verificar
                    string caracteresaanalisar = @"(?i)[^0-9]";

                    /// Faz um replace nos desnecessários
                    Regex rgx = new Regex(caracteresaanalisar);

                    /// Pega o resultado do replace
                    string coderro = rgx.Replace(weberrogeral, "");

                    /// Caso o erro seja 400
                    if (coderro == "400")
                        weberroexplic = "Este erro indica que o encoder que transmite a rádio pode não estar no ar \nOu o ponto de montagem informado não está correto!";

                    /// Caso o erro seja 401
                    if (coderro == "401")
                        weberroexplic = "Este erro indica que você errou a senha ou o ID \nOu o ponto de montagem do servidor não aceita o login e senha informados!";

                    /// Caso o erro seja 403
                    if (coderro == "403")
                    {
                        weberroexplic = "Este erro indica que o servidor proibiu o acesso aos dados \nOu o ponto de montagem do servidor não aceita o acesso!";
                        if (chkUsoproxy.Checked == true)
                        {
                            weberroexplic = $"Este erro indica que o servidor proxy {txtDoproxy.Text}:{txtPortaproxy.Text} proibiu o acesso! Será necessário solicitar desbloqueio para o endereço http://{txtDominioip.Text}:{txtPorta.Text}/ para que os dados sejam enviados!";
                        }
                    }

                    /// Caso o erro seja de falha de conexão
                    if (weberrogeralcode == "ConnectFailure")
                    {
                        /// Caso o proxy esteja marcado
                        if (chkUsoproxy.Checked == true)
                        {
                            weberroexplic = $"Este erro indica que o servidor ou o servidor proxy não está no ar. \nVerifique se o servidor http://{txtDominioip.Text}:{txtPorta.Text}/ está funcionando e se o proxy {txtDoproxy.Text}:{txtPortaproxy.Text} está funcionando!";
                        }
                        else
                            weberroexplic = $"Este erro indica que o servidor não está no ar. \nVerifique se o servidor http://{txtDominioip.Text}:{txtPorta.Text}/ está funcionando!";
                    }

                    /// Caso o erro seja de resolução de nome
                    if (weberrogeralcode == "NameResolutionFailure")
                        weberroexplic = $"Verifique se não há erros de digitação do domínio informado!";

                    /// Caso o erro seja de resolução de nome do servidor proxy
                    if (weberrogeralcode == "ProxyNameResolutionFailure")
                        weberroexplic = $"Verifique se não há erros de digitação na caixa de texto de domínio do servidor proxy informado!";

                    /// Caso o erro seja de autenticação de proxy
                    if (coderro == "407")
                    {
                        if (chkAutenticaproxy.Checked == true)
                        {
                            weberroexplic = $"Verifique se o servidor proxy: {txtDoproxy.Text}:{txtPortaproxy.Text}, o Login: {txtLoginproxy.Text} e a senha: {txtSenhaproxy.Text} do servidor estão corretos e se o servidor está funcionando e se há acesso nesse servidor!";
                        }
                        else
                            weberroexplic = $"Verifique se o servidor proxy: {txtDoproxy.Text}:{txtPortaproxy.Text} não requer autenticação adicional para acessar o servidor, se for o caso marque a opção 'Meu servidor requer autenticação de proxy' acima!";
                    }

                    /// Mensagem de erro que vai aparecer associada com o problema encontrado e data e hora completos
                    string mensagemerro = $"Título não atualizado devido a um erro ao conectar no servidor: \n{weberrogeral} \n{weberroexplic}";

                    /// Caso o botão ainda esteja visível não carregar na label de informações, Exibe informação na label para o usuário sobre o problema, a exibição na label se dá devido ao fato que não se pode parar a execução desse trecho de código
                    if (btnVerificardadosderds.Enabled == false)
                        lblInformacao.Text = $"{mensagemerro} \nData e hora do erro: {DateTime.Now.ToString()} - Por favor, Verifique a conexão com o servidor! ";
                    else
                    {
                        /// Mensagem de erro que vai aparecer associada com o problema encontrado e data e hora completos
                        mensagemerro = $"Houve um erro ao conectar no servidor: \n{weberrogeral} \n{weberroexplic}";

                        /// Avisa o usuário através de mensagem popup que tem um problema na conexão com o servidor
                        MessageBox.Show(mensagemerro + " \nPor favor, corrija a conexão com o servidor e tente novamente! ", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    /// Verifica se o usuário selecionou a caixa para não ser notificado, se não marcou, Transfere dados para um balão chato haha de texto
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro de conexão", mensagemerro, ToolTipIcon.Warning);

                    /// Gera arquivo texto ou abre um arquivo existente
                    FileStream fs = new FileStream(caminhoarquivolog, FileMode.OpenOrCreate);

                    /// Grava arquivo em UTF 8 com a solicitação
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                    /// Grava o texto com as informações nova do erro mais os erros anteriores
                    sw.WriteLine(dadosdotxterro);
                    sw.WriteLine(" -------------- Sessão novo erro CONEXAO --------------------------------- ");
                    sw.WriteLine("Data e hora da execução do erro: " + DateTime.Now);
                    sw.WriteLine("Mensagem completa exibida: " + mensagemerro + ". Por favor, Verifique a conexão com o servidor! ");
                    sw.WriteLine("Mensagem técnica exibida: " + weberrogeral);
                    sw.WriteLine("Código de erro: " + weberrogeralcode);
                    sw.WriteLine("Shoutcast Server versão 1: " + rbtShoutcastv1.Checked);
                    sw.WriteLine("Shoutcast Server versão 2: " + rbtShoutcastv2.Checked);
                    sw.WriteLine("Icecast Server: " + rbtIcecast.Checked);
                    sw.WriteLine("Não minimizar no system tray: " + chkNaominimsystray.Checked);
                    sw.WriteLine("Não notificar via systray: " + chkNaonotificarsomtray.Checked);
                    sw.WriteLine("Remover a Acentuação das palavras: " + chkAcentospalavras.Checked);
                    sw.WriteLine("Remover Caracteres especiais: " + chkCaracteresespeciais.Checked);
                    sw.WriteLine("Exibir no aplicativo dados sensiveis como senhas: " + chkDadossensiveis.Checked);
                    sw.WriteLine("Transmitir dados do próximo som: " + chkTransmproxsom.Checked);
                    sw.WriteLine("Uso de servidor proxy: " + chkUsoproxy.Checked);
                    sw.WriteLine("Uso de autenticação de servidor proxy: " + chkAutenticaproxy.Checked);
                    sw.WriteLine("Endereço de IP ou nome de domínio do servidor proxy: " + txtDoproxy.Text);
                    sw.WriteLine("Porta do servidor proxy: " + txtPortaproxy.Text);
                    sw.WriteLine("Login do servidor proxy: " + txtLoginproxy.Text);
                    sw.WriteLine("Senha do servidor proxy: " + txtSenhaproxy.Text);
                    sw.WriteLine("Tempo de execução: " + txtTempoexec.Text);
                    sw.WriteLine("Caminho completo do arquivo texto com nome do som: " + txtArquivotextosom.Text);
                    sw.WriteLine("Caminho completo do arquivo texto com nome do próximo som: " + txtArquivotextosomnext.Text);
                    sw.WriteLine("Atualizar via URL o título do som: " + chkUrlsom.Checked);
                    sw.WriteLine("URL para atualização de título: " + txtUrlsom.Text);
                    sw.WriteLine("Atualizar via URL o título do próximo som: " + chkUrlsomnext.Checked);
                    sw.WriteLine("URL para atualização de título de próximo som: " + txtUrlsomnext.Text);
                    sw.WriteLine("IP ou domínio digitado: " + txtDominioip.Text);
                    sw.WriteLine("Porta: " + txtPorta.Text);
                    sw.WriteLine("Ponto de montagem ou ID: " + txtIdoumont.Text);
                    sw.WriteLine("Login: " + txtLoginserver.Text);
                    sw.WriteLine("Senha: " + txtSenhaserver.Text);
                    sw.WriteLine(" -------------- Final da sessão novo erro CONEXAO --------------------------------- ");

                    /// Limpa a sessão
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    fs.Close();
                    fs.Dispose();
                }

                /// Tratamento de erro de aplicativo
                if (weberrogeral == null)
                {
                    /// String para preenchimento dos dados do arquivo texto de erros
                    string dadosdotxterro = null;

                    /// Caso o botão de verificação de dados esteja visível não carrega o erro na label de informações, Caso aconteça exceções sem tratamento, é carregado direto na label e a exibição na label se dá devido ao fato que não se pode parar a execução desse trecho de código
                    if (btnVerificardadosderds.Enabled == false)
                        lblInformacao.Text = errogeral;

                    /// Caso tenha contagem de erro e ela for menor que um
                    if (erroconta < 1 && errocontanext < 1)
                    {
                        /// Caso o arquivo LOGERRORDS.txt exista então ele faz a condição abaixo
                        if (File.Exists(caminhoarquivolog))
                        {
                            /// Pega o arquivo texto para ler
                            using (StreamReader sr = new StreamReader(caminhoarquivolog, Encoding.Default))
                            {
                                /// Carrega arquivo e mantém carregado todas as linhas
                                dadosdotxterro = sr.ReadToEnd();
                            }
                        }

                        /// Gera arquivo texto ou abre um arquivo existente
                        FileStream fs = new FileStream(caminhoarquivolog, FileMode.OpenOrCreate);

                        /// Grava arquivo em UTF 8 com a solicitação
                        StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                        /// Grava o texto com as informações nova do erro mais os erros anteriores
                        sw.WriteLine(dadosdotxterro);
                        sw.WriteLine(" -------------- Sessão novo erro --------------------------------- ");
                        sw.WriteLine("Data e hora da execução do erro: " + DateTime.Now);
                        sw.WriteLine("Stack Trace Completo: " + errogeralgravado);
                        sw.WriteLine("Mensagem completa exibida: " + errogeral);
                        sw.WriteLine("Shoutcast Server versão 1: " + rbtShoutcastv1.Checked);
                        sw.WriteLine("Shoutcast Server versão 2: " + rbtShoutcastv2.Checked);
                        sw.WriteLine("Icecast Server: " + rbtIcecast.Checked);
                        sw.WriteLine("Não minimizar no system tray: " + chkNaominimsystray.Checked);
                        sw.WriteLine("Não notificar via systray: " + chkNaonotificarsomtray.Checked);
                        sw.WriteLine("Remover a Acentuação das palavras: " + chkAcentospalavras.Checked);
                        sw.WriteLine("Remover Caracteres especiais: " + chkCaracteresespeciais.Checked);
                        sw.WriteLine("Exibir no aplicativo dados sensiveis como senhas: " + chkDadossensiveis.Checked);
                        sw.WriteLine("Transmitir dados do próximo som: " + chkTransmproxsom.Checked);
                        sw.WriteLine("Uso de servidor proxy: " + chkUsoproxy.Checked);
                        sw.WriteLine("Uso de autenticação de servidor proxy: " + chkAutenticaproxy.Checked);
                        sw.WriteLine("Endereço de IP ou nome de domínio do servidor proxy: " + txtDoproxy.Text);
                        sw.WriteLine("Porta do servidor proxy: " + txtPortaproxy.Text);
                        sw.WriteLine("Login do servidor proxy: " + txtLoginproxy.Text);
                        sw.WriteLine("Senha do servidor proxy: " + txtSenhaproxy.Text);
                        sw.WriteLine("Tempo de execução: " + txtTempoexec.Text);
                        sw.WriteLine("Caminho completo do arquivo texto com nome do som: " + txtArquivotextosom.Text);
                        sw.WriteLine("Caminho completo do arquivo texto com nome do próximo som: " + txtArquivotextosomnext.Text);
                        sw.WriteLine("Atualizar via URL o título do som: " + chkUrlsom.Checked);
                        sw.WriteLine("URL para atualização de título: " + txtUrlsom.Text);
                        sw.WriteLine("Atualizar via URL o título do próximo som: " + chkUrlsomnext.Checked);
                        sw.WriteLine("URL para atualização de título de próximo som: " + txtUrlsomnext.Text);
                        sw.WriteLine("IP ou domínio digitado: " + txtDominioip.Text);
                        sw.WriteLine("Porta: " + txtPorta.Text);
                        sw.WriteLine("Ponto de montagem ou ID: " + txtIdoumont.Text);
                        sw.WriteLine("Login: " + txtLoginserver.Text);
                        sw.WriteLine("Senha: " + txtSenhaserver.Text);
                        sw.WriteLine(" -------------- Final da sessão novo erro --------------------------------- ");

                        /// Limpa a sessão
                        sw.Flush();
                        sw.Close();
                        sw.Dispose();
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                /// Caso gere um erro aqui, o erro acaba aqui
                qualquerlixoaqui = ex.Message;
            }
        }

        public void UpdateAppRDS()
        {
            /// Informa versão do aplicativo para o usuário alterando a cor
            lblVersaoapp.Text = "Versão 0.0.1 Alfa\n(Sem verificar nova versão)";
            lblVersaoapp.ForeColor = Color.Yellow;

            /// Declara URL completa de onde vai verificar a atualização do software
            /// string urlcompletaversao = "http://localhost/versao.txt";
            string urlcompletaversao = "http://www.vanderson.net.br/updaterds/versao.txt";

            /// Declara URL completa de onde baixar o arquivo
            string urlcompletadownload = "http://www.vanderson.net.br/updaterds/UpdateRDSInstaller.exe";

            /// Declara nova versão
            string versaonovadoapp;

            /// Abre sessão de webclient
            WebClient wcurlcompletaversao = new WebClient();

            /// Define o UserAgent do webclient
            wcurlcompletaversao.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

            /// Define o servidor proxy caso tenha
            if (chkUsoproxy.Checked == true)
            {
                /// Chama a função
                DadosProxy();

                /// Define o servidor proxy global
                wcurlcompletaversao.Proxy = servidorproxydoaplicativo;
            }

            /// Pega os dados da URL
            Stream strurlcompleta = wcurlcompletaversao.OpenRead(urlcompletaversao);

            /// Pega os dados armazenados do que foi capturado na URL
            StreamReader rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.Default);

            /// Transfere os dados capturados da URL, a informação do texto para processamento
            versaonovadoapp = rdrurlcompleta.ReadLine();

            /// Encerra a sessão do Webclient
            wcurlcompletaversao.Dispose();
            strurlcompleta.Close();
            strurlcompleta.Dispose();
            rdrurlcompleta.Close();
            rdrurlcompleta.Dispose();

            if (versaonovadoapp != versaoappcurrent)
            {
                /// Define que existe uma versão nova para o aplicativo
                versaonova = true;

                /// Altera label de aviso
                lblVersaoapp.Text = "Versão 0.0.1 Alfa\n(DESATUALIZADO)";
                lblVersaoapp.ForeColor = Color.Red;

                /// Envia mensagem perguntando se o usuário gostaria de baixar a nova versão do aplicativo, Caso o usuário queira baixar o aplicativo
                if (MessageBox.Show($"Há uma nova versão do aplicativo disponível para download, gostaria de baixar a nova versão do aplicativo? a sua versão de aplicativo instalada atualmente é {versaoappcurrent} e a nova versão do aplicativo para baixar é {versaonovadoapp} sendo a nova versão com correções de problemas e outras correções de interface.", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!File.Exists($"{diretoriodoaplicativo}UpdateRDSInstaller.exe"))
                    {
                        /// Abre sessão de webclient
                        WebClient wcurlcompletadownload = new WebClient();

                        /// Define o UserAgent do webclient
                        wcurlcompletadownload.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                        /// Define o servidor proxy caso tenha
                        if (chkUsoproxy.Checked == true)
                        {
                            /// Chama a função
                            DadosProxy();

                            /// Define o servidor proxy global
                            wcurlcompletadownload.Proxy = servidorproxydoaplicativo;
                        }

                        /// Pega os dados da URL
                        wcurlcompletadownload.DownloadFile(urlcompletadownload, $"{diretoriodoaplicativo}UpdateRDSInstaller.exe");

                        /// Encerra a sessão do Webclient
                        wcurlcompletadownload.Dispose();
                    }

                    /// Envia mensagem de que o aplicativo está baixado
                    if (MessageBox.Show($"O aplicativo foi baixado com sucesso! Gostaria de instalar agora?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        /// Chama o aplicativo para instalação
                        Process.Start($"{diretoriodoaplicativo}UpdateRDSInstaller.exe");

                        foreach (Process processodoaplicativo in Process.GetProcessesByName("Update RDS"))
                        {
                            //////////////////////////////////////////////////// processodoaplicativo.Kill();
                        }
                    }
                    else
                    {
                        /// Avisa o usuário que o programa está baixado e pronto para instalar
                        MessageBox.Show($"O Aplicativo não foi instalado automáticamente!\nPara instalar manualmente a nova versão do aplicativo, entre no diretório {diretoriodoaplicativo} e execute o aplicativo 'UpdateRDSInstaller.exe' para instalar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    /// Avisa o usuário que o programa está desatualizado
                    MessageBox.Show("O Aplicativo permanecerá desatualizado!\nPara evitar problemas de execução, ter mais novidades de atualização etc desse aplicativo, clique em 'Verificar por atualizações' mais tarde se preferir!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                lblVersaoapp.Text = "Versão 0.0.1 Alfa\n(ATUALIZADO)";
                lblVersaoapp.ForeColor = Color.Green;
                versaonova = false;
            }
        }

        public void DadosProxy()
        {
            /// Pega dados de IP ou domínio e porta do servidor proxy
            string enderecoservidorproxy = $"http://{txtDoproxy.Text}:{txtPortaproxy.Text}";

            /// Declara URI do servidor proxy para uso
            Uri uridoproxyserver = new Uri(enderecoservidorproxy);

            /// Adiciona informações do URI no endereço
            servidorproxydoaplicativo.Address = uridoproxyserver;

            /// Não bypassa dados para proxy local
            /// servidorproxydoaplicativo.BypassProxyOnLocal = true;

            /// Caso o servidor proxy tenha credenciais, Define as credenciais do usuário
            if (chkAutenticaproxy.Checked == true)
                servidorproxydoaplicativo.Credentials = new NetworkCredential(txtLoginproxy.Text, txtSenhaproxy.Text);
        }

        public void ValidarInformacoes()
        {
            /// Pega as informações dos textos das caixas de preenchimento
            string ipserver = txtDominioip.Text;
            string portaserver = txtPorta.Text;
            string idoupontomont = txtIdoumont.Text;

            /// Pega as informações dos textos das caixas de preenchimento e adiciona informações na string
            string caminhoarquivo = $@"{txtArquivotextosom.Text}";
            string caminhoarquivonext = $@"{txtArquivotextosomnext.Text}";

            /// Verifica se é dados de Icecast, se for, realiza as condições de alteração abaixo para compatibilizar o icecast v2, Caso o usuário não preencha nenhuma informação de ponto de montagem, Então solicita para o usuário preencher
            if (rbtIcecast.Checked == true & string.IsNullOrEmpty(idoupontomont))
                throw new Exception("Preencha a caixa de texto ID com números para Shoutcast Server ou ponto de montagem para Icecast Server!");

            /// Carrega a URL padrão para Shoutcast Server v2 apenas caso marcado pelo usuário, Se as caixas de texto estiverem preenchidas diferente de números ou sem nenhum dado, Então solicita para o usuário preencher
            if (rbtShoutcastv2.Checked == true & !Regex.IsMatch(idoupontomont, @"^[0-9]+$"))
                throw new Exception("Preencha a caixa de texto ID ou ponto de montagem apenas com números para Shoutcast V2!");

            /// Verifica se a caixa de texto caminho completo do arquivo não está vazia, Se estiver vazia então apresenta mensagem de erro para o usuário preencher a caixa
            if (string.IsNullOrEmpty(caminhoarquivo) && chkUrlsom.Checked == false)
                throw new Exception("Preencha a caixa de texto Caminho do arquivo texto gerado pelo automatizador com o nome do áudio!");

            /// Se o usuário marcou para digitar um URL e está nulo ou vazio faz a condição de exceção, Se estiver vazia então apresenta mensagem de erro para o usuário preencher a caixa
            if (chkUrlsom.Checked == true && string.IsNullOrEmpty(txtUrlsom.Text))
                throw new Exception("Preencha a caixa de texto URL com o link que leva ao arquivo texto ou a URL do currentsong do servidor shoutcast!");

            /// Caso não seja uma shoutcast v2 entra na condição abaixo
            if (chkTransmproxsom.Checked == true && rbtShoutcastv2.Checked == true)
            {
                /// Se o usuário marcou para digitar um URL e está nulo ou vazio faz a condição de exceção, Se estiver vazia então apresenta mensagem de erro para o usuário preencher a caixa
                if (chkUrlsomnext.Checked == true && string.IsNullOrEmpty(txtUrlsomnext.Text))
                    throw new Exception("Preencha a caixa de texto URL com o link que leva ao arquivo texto de próximo som ou a URL do nextsong do servidor shoutcast!");

                /// Verifica se a caixa de texto caminho completo do arquivo NEXT SONG não está vazia, Se estiver vazia então apresenta mensagem de erro para o usuário preencher a caixa
                if (string.IsNullOrEmpty(caminhoarquivonext) && chkUrlsomnext.Checked == false)
                    throw new Exception("Preencha a caixa de texto de Caminho do arquivo texto de próximo audio gerado pelo automatizador com o nome do áudio!");

                /// Caso o arquivo texto nextsong.txt ou similar não exista então ele faz a condição de exceção, Avisa o usuário que o arquivo informado não existe no diretório
                if (!File.Exists(caminhoarquivonext) && chkUrlsomnext.Checked == false)
                    throw new Exception("O Caminho informado para o arquivo de texto com o nome do próximo áudio está incorreto! verifique se o arquivo realmente existe!");
            }

            /// Caso o usuário não preencha a caixa de tempo para verificar uma atualização de arquivo, Então envia mensagem de erro para que seja preenchida a caixa
            if (txtTempoexec.Text == "0" || !Regex.IsMatch(txtTempoexec.Text, @"^[0-9]+$"))
                throw new Exception("Preencha a caixa de tempo de verificação de arquivo APENAS COM NÚMEROS para verificar uma atualização de arquivo! NÃO PODE SER VAZIO OU ZERO!");

            /// Verifica se a caixa de texto endereço de IP ou nome de domínio não está vazia, Se a caixa de texto endereço de IP ou nome de domínio estiver vazia, então encaminha mensagem para o usuário resolver o problema
            if (string.IsNullOrEmpty(ipserver))
                throw new Exception("Preencha a caixa de texto endereço de IP ou nome de domínio!");

            /// Verifica se a caixa de texto porta não está vazia, Se a caixa de texto porta estiver vazia, então encaminha mensagem para o usuário resolver o problema preenchendo os dados
            if (string.IsNullOrEmpty(portaserver))
                throw new Exception("Preencha a caixa de texto porta!");

            /// Verifica se a caixa de texto porta tem apenas números, Se a caixa de texto porta estiver com outros caracteres diferentes de números gera exceção e avisa o usuário
            if (!Regex.IsMatch(portaserver, @"^[0-9]+$"))
                throw new Exception("Preencha a caixa de texto porta apenas com números!");

            /// Verifica se a caixa de texto login não está vazia, Se a caixa de texto login estiver vazia, então encaminha mensagem para o usuário resolver o problema preenchendo os dados
            if (string.IsNullOrEmpty(txtLoginserver.Text))
                throw new Exception("Preencha a caixa de texto login!");

            /// Verifica se a caixa de texto senha não está vazia, Se a caixa de texto senha estiver vazia, então encaminha mensagem para o usuário resolver o problema preenchendo os dados
            if (string.IsNullOrEmpty(txtSenhaserver.Text))
                throw new Exception("Preencha a caixa de texto senha!");

            /// Caso o arquivo texto currentsong.txt ou similar não exista então ele faz a condição de exceção, Avisa o usuário que o arquivo informado não existe no diretório
            if (!File.Exists(caminhoarquivo) && chkUrlsom.Checked == false)
                throw new Exception("O Caminho informado para o arquivo de texto com o nome do áudio está incorreto! verifique se o arquivo realmente existe!");

            /// Caso o arquivo texto seja o mesmo do anterior, Avisa o usuário que o arquivo informado não pode ser o mesmo
            if (chkUrlsom.Checked == false && chkUrlsomnext.Checked == false && caminhoarquivo == caminhoarquivonext)
                throw new Exception("O Caminho informado para o arquivo de texto com o nome do áudio é o mesmo do arquivo texto de proximo som! Você não pode colocar o mesmo arquivo, precisa ser necessariamente dois arquivos diferentes!");

            /// Caso as URLs sejam as mesmas, Avisa o usuário para alterar as URLs
            if (chkUrlsom.Checked == true && chkUrlsomnext.Checked == true && txtUrlsom.Text == txtUrlsomnext.Text)
                throw new Exception("A URL do próximo som é a mesma URL do som atual, as duas URLs não podem ser as mesmas! use URLs com textos diferentes para cadastrar no sistema!");

            /// Caso o usuário tenha marcado para usar um proxy server
            if (chkUsoproxy.Checked == true)
            {
                /// Valida as caixas de texto
                if (string.IsNullOrEmpty(txtDoproxy.Text))
                {
                    throw new Exception("O endereço de IP ou domínio do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                }
                if (string.IsNullOrEmpty(txtPortaproxy.Text))
                {
                    throw new Exception("A porta do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                }
                if (!Regex.IsMatch(txtPortaproxy.Text, @"^[0-9]+$"))
                    throw new Exception("Preencha a caixa de texto porta do servidor proxy apenas com números!");

                if (chkAutenticaproxy.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtLoginproxy.Text))
                    {
                        throw new Exception("O login do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                    }
                    if (string.IsNullOrEmpty(txtSenhaproxy.Text))
                    {
                        throw new Exception("A senha do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                    }
                }
            }
        }

        private void TratamentoURLNowNext()
        {
            /// Faz uma duplicação do número
            int numconta = erroconta;
            int numcontanext = errocontanext;

            /// Verifica o processo do aplicativo
            Process proc = Process.GetCurrentProcess();

            /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
            string identificadorproc = proc.Id.ToString();

            /// Pega as informações dos textos das caixas de preenchimento e adiciona informações na string
            string arquivotextoantigo = $@"{diretoriodoaplicativo}{identificadorproc}OLD.txt";
            string urlcompleta = txtUrlsom.Text;
            string dadoscapturadosdaurl;

            /// Caso o arquivo a ser processado é um next song, altera as propriedades dele
            if (eumnext == true)
            {
                urlcompleta = txtUrlsomnext.Text;
                arquivotextoantigo = $@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt";
            }

            /// Caso tenha valor de erros do NEXT SONG
            if (errocontanext > 0)
            {
                /// Caso tenha dado exceção decrescenta o valor até zero
                errocontanext = numcontanext - 1;
                if (chkUsoproxy.Checked == true)
                {
                    /// Avisa o usuário sobre o problema com o arquivo texto de próximo som
                    throw new Exception("O servidor proxy ou a URL do próximo som informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + errocontanext);
                }
                else
                    /// Avisa o usuário sobre o problema com o arquivo texto de próximo som
                    throw new Exception("A URL do próximo som informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + errocontanext);
            }

            /// Caso tenha valor de erros do som
            if (erroconta > 0)
            {
                /// Caso tenha dado exceção decrescenta o valor até zero
                erroconta = numconta - 1;
                if (chkUsoproxy.Checked == true)
                {
                    /// Avisa o usuário sobre o problema com o arquivo
                    throw new Exception("O servidor proxy ou a URL informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + erroconta);
                }
                else
                    /// Avisa o usuário sobre o problema com o arquivo
                    throw new Exception("A URL informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + erroconta);
            }
            try
            {
                /// Abre sessão de webclient
                WebClient wcurlcompleta = new WebClient();

                /// Define o UserAgent do webclient
                wcurlcompleta.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                /// Define o servidor proxy caso tenha
                if (chkUsoproxy.Checked == true)
                {
                    /// Chama a função
                    DadosProxy();

                    /// Define o servidor proxy global
                    wcurlcompleta.Proxy = servidorproxydoaplicativo;
                }

                /// Pega os dados da URL
                Stream strurlcompleta = wcurlcompleta.OpenRead(urlcompleta);

                /// Pega os dados armazenados do que foi capturado na URL
                StreamReader rdrurlcompleta = new StreamReader(strurlcompleta);

                /// Transfere os dados capturados da URL, a informação do texto para processamento
                dadoscapturadosdaurl = rdrurlcompleta.ReadLine();

                /// Encerra a sessão do Webclient
                wcurlcompleta.Dispose();
                strurlcompleta.Close();
                strurlcompleta.Dispose();
                rdrurlcompleta.Close();
                rdrurlcompleta.Dispose();

                /// Caso tenha erros e é um retorno da conexão então envia uma mensagem de reconexão para o usuário
                if (erroconta == 0)
                {
                    lblInformacao.Text = "Nome do som conectado no servidor! Aguarde atualização de título...";
                    erroconta = -1;
                }
                if (errocontanext == 0)
                {
                    lblInformacao.Text = "Nome do próximo som conectado no servidor! Aguarde atualização de título...";
                    errocontanext = -1;
                }
            }
            catch (WebException webexc)
            {
                /// Processa mensagem gerada diretamente da exceção
                errodaweblink = webexc.Message;

                /// Caso o botão esteja habilitado
                if (btnVerificardadosderds.Enabled == true)
                {
                    /// Caso seja um arquivo de next song
                    if (eumnext == true)
                    {
                        /// Carrega mensagem de erro
                        string erroconexaowebexc1 = "A URL do próximo som informada anteriormente está com problemas! \n" + errodaweblink + " \nPor favor, verifique se a URL está correta e se o servidor está funcionando!";

                        if (chkUsoproxy.Checked == true)
                        {
                            /// Carrega mensagem de erro
                            erroconexaowebexc1 = "A URL do próximo som informada anteriormente está com problemas! \n" + errodaweblink + " \nPor favor, verifique se a URL está correta, se o servidor proxy está funcionando e se o servidor está funcionando!";
                        }

                        /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                        if (chkNaonotificarsomtray.Checked == false)
                            ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro de conexão", erroconexaowebexc1, ToolTipIcon.Warning);

                        /// Avisa o usuário sobre o problema com o arquivo texto de próximo som
                        throw new Exception(erroconexaowebexc1);
                    }
                    else
                    {
                        /// Carrega mensagem de erro
                        string erroconexaowebexc2 = "A URL informada anteriormente está com problemas! \n" + errodaweblink + " \nPor favor, verifique se a URL está correta e se o servidor está funcionando!";

                        if (chkUsoproxy.Checked == true)
                        {
                            /// Carrega mensagem de erro
                            erroconexaowebexc2 = "A URL informada anteriormente está com problemas! \n" + errodaweblink + " \nPor favor, verifique se a URL está correta, se o servidor proxy está funcionando e se o servidor está funcionando!";
                        }

                        /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                        if (chkNaonotificarsomtray.Checked == false)
                            ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro de conexão", erroconexaowebexc2, ToolTipIcon.Warning);

                        /// Avisa o usuário sobre o problema com o arquivo
                        throw new Exception(erroconexaowebexc2);
                    }
                }

                /// Caso seja um arquivo de next song
                if (eumnext == true)
                {
                    /// Acrescenta valores para descrescentar depois
                    errocontanext = numcontanext + 250;

                    /// Carrega informações para apresentar para o usuário
                    string erroconexaowebexc3 = "A URL do próximo som informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + errocontanext;

                    if (chkUsoproxy.Checked == true)
                    {
                        /// Carrega informações para apresentar para o usuário
                        erroconexaowebexc3 = "O servidor proxy ou a URL do próximo som informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + errocontanext;
                    }

                    /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro de conexão", erroconexaowebexc3, ToolTipIcon.Warning);

                    /// Avisa o usuário sobre o problema com o arquivo texto de próximo som
                    throw new Exception(erroconexaowebexc3);
                }
                else
                {
                    /// Acrescenta valores para descrescentar depois
                    erroconta = numconta + 250;

                    /// Carrega informações para apresentar para o usuário
                    string erroconexaowebexc4 = "A URL informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + erroconta;

                    if (chkUsoproxy.Checked == true)
                    {
                        erroconexaowebexc4 = "O servidor proxy ou a URL informada anteriormente está com problemas! \n" + errodaweblink + " \nSerá feita uma nova tentativa de conexão. \nNovas tentativas de conexão a tentar novamente: " + erroconta;
                    }

                    /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Erro de conexão", erroconexaowebexc4, ToolTipIcon.Warning);

                    /// Avisa o usuário sobre o problema com o arquivo
                    throw new Exception(erroconexaowebexc4);
                }
            }

            /// Verifica se o arquivo texto com o nome da música é nulo ou vazio, se for
            if (string.IsNullOrEmpty(dadoscapturadosdaurl))
            {
                /// Caso seja um arquivo texto next, Avisa o usuário sobre o problema com o arquivo texto de próximo som
                if (eumnext == true)
                    throw new Exception("A URL do próximo som informada anteriormente está com problemas! verificar se o texto da URL do próximo som não está vazio!");

                /// Avisa o usuário sobre o problema com o arquivo
                throw new Exception("A URL informada anteriormente está com problemas! verificar se o texto da URL não está vazio!");
            }

            /// Sobrescreve o conteúdo do texto com os dados capturados da URL
            conteudotexto = dadoscapturadosdaurl;

            /// Caso o arquivo texto PID.txt ou similar não exista então ele faz a condição de criação do arquivo
            if (!File.Exists(arquivotextoantigo))
            {
                /// Caso o diretório de logs não exista, Criamos um com o nome LOGS
                if (!Directory.Exists($@"{diretoriodoaplicativo}"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}");

                /// Cria ou abre o arquivo existente para leitura
                FileStream fs = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate);

                /// Grava ou salva arquivo em UTF 8 com a solicitação
                StreamWriter sw = new StreamWriter(fs);

                /// Captura texto do arquivo texto e também dos dados adicionais para gravar no arquivo
                sw.WriteLine(dadoscapturadosdaurl);

                /// Limpa a sessão
                sw.Flush();
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }

            /// Pega o arquivo antigo para ler com o nome do áudio só pra fazer a comparação abaixo
            using (StreamReader srOld = new StreamReader(arquivotextoantigo))
            {
                /// Carrega arquivo de texto antigo e faz a leitura da primeira linha do arquivo com o nome do áudio
                conteudotextoantigo = srOld.ReadLine().ToString();

                /// Força o fechamento do arquivo para que não dê erro no delete do arquivo
                srOld.Close();

                /// Força o despejo da memória do arquivo carregado
                srOld.Dispose();
            }

            /// Verifica se o arquivo anterior é o mesmo do atual, se não for então apaga o arquivo anterior e copia o arquivo novo
            if (conteudotexto != conteudotextoantigo || btnVerificardadosderds.Enabled == true)
            {
                try
                {
                    /// Apaga os arquivos anteriores
                    File.Delete(arquivotextoantigo);

                    /// Cria ou abre o arquivo existente para leitura
                    FileStream fs = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate);

                    /// Grava ou salva arquivo em UTF 8 com a solicitação
                    StreamWriter sw = new StreamWriter(fs);

                    /// Captura texto do arquivo texto e também dos dados adicionais para gravar no arquivo
                    sw.WriteLine(dadoscapturadosdaurl);

                    /// Limpa a sessão
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    fs.Close();
                    fs.Dispose();
                }
                catch (IOException errfile)
                {
                    /// Grava o erro aqui
                    qualquerlixoaqui = errfile.Message;

                    /// Espera um tempo para apagar os arquivos
                    System.Threading.Thread.Sleep(1500);

                    /// Apaga os arquivos anteriores
                    File.Delete(arquivotextoantigo);

                    /// Espera um tempo para criar um novo arquivo
                    System.Threading.Thread.Sleep(1500);

                    /// Cria ou abre o arquivo existente para leitura
                    FileStream fs = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate);

                    /// Grava ou salva arquivo em UTF 8 com a solicitação
                    StreamWriter sw = new StreamWriter(fs);

                    /// Captura texto do arquivo texto e também dos dados adicionais para gravar no arquivo
                    sw.WriteLine(dadoscapturadosdaurl);

                    /// Limpa a sessão
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private void TratamentoTextoNowNext()
        {
            /// Verifica o processo do aplicativo
            Process proc = Process.GetCurrentProcess();

            /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
            string identificadorproc = proc.Id.ToString();

            /// Pega as informações dos textos das caixas de preenchimento e adiciona informações na string
            string arquivotexto = $@"{txtArquivotextosom.Text}";
            string arquivotextoantigo = $@"{txtArquivotextosom.Text}{identificadorproc}.txt";

            /// Caso o arquivo a ser processado é um next song, altera as propriedades dele
            if (eumnext == true)
            {
                arquivotexto = $@"{txtArquivotextosomnext.Text}";
                arquivotextoantigo = $@"{txtArquivotextosomnext.Text}{identificadorproc}.txt";
            }

            /// Se o arquivo texto exista
            if (File.Exists(arquivotexto))
            {
                /// Caso seja um arquivo texto next, Altera valor de erro na contagem
                if (eumnext == true && errfilecnext == 1)
                    errfilecnext = 0;

                /// Altera valor de erro na contagem
                if (eumnext == false && errfilec == 1)
                    errfilec = 0;
            }

            /// Caso tenha erros e o usuário corrigiu o arquivo de configuração, então faz a troca de informação da label
            if (errfilec == 0)
            {
                lblInformacao.Text = "Arquivo de nome do som corrigido com sucesso! Aguarde atualização de título...";
                errfilec = -1;
            }
            if (errfilecnext == 0)
            {
                lblInformacao.Text = "Arquivo de nome do próximo som corrigido com sucesso! Aguarde atualização de título...";
                errfilecnext = -1;
            }

            /// Caso o arquivo texto currentsong.txt ou similar não exista mais então ele faz a condição de exceção
            if (!File.Exists(arquivotexto))
            {
                /// Caso seja um arquivo texto next
                if (eumnext == true)
                {
                    /// Altera valor de erro na contagem
                    errfilecnext = 1;

                    /// Avisa o usuário sobre o problema com o arquivo
                    throw new Exception("O Caminho informado anteriormente para o arquivo de texto de próximo som está com problemas! \nVerificar se o arquivo ainda existe!");
                }
                /// Altera valor de erro na contagem
                errfilec = 1;

                /// Avisa o usuário sobre o problema com o arquivo
                throw new Exception("O Caminho informado anteriormente para o arquivo de texto está com problemas! \nVerificar se o arquivo ainda existe!");
            }

            try
            {
                try
                {
                    /// Pega o arquivo para ler com o nome do áudio
                    using (StreamReader sr = new StreamReader(arquivotexto, Encoding.Default))
                    {
                        /// Carrega arquivo e faz a leitura da primeira linha do arquivo com o nome do áudio
                        conteudotexto = sr.ReadLine().ToString();

                        /// Força o fechamento do arquivo para evitar erros
                        sr.Close();

                        /// Força o despejo da memória do arquivo carregado
                        sr.Dispose();
                    }
                }
                catch (Exception errofile)
                {
                    /// Grava o erro aqui
                    qualquerlixoaqui = errofile.Message;

                    /// Espera um tempo para tentar ler de novo o arquivo
                    System.Threading.Thread.Sleep(1500);

                    /// Pega o arquivo para ler com o nome do áudio
                    using (StreamReader sr = new StreamReader(arquivotexto, Encoding.Default))
                    {
                        /// Carrega arquivo e faz a leitura da primeira linha do arquivo com o nome do áudio
                        conteudotexto = sr.ReadLine().ToString();

                        /// Força o fechamento do arquivo para evitar erros
                        sr.Close();

                        /// Força o despejo da memória do arquivo carregado
                        sr.Dispose();
                    }
                }
            }
            catch (Exception errofilegeral)
            {
                /// Está sendo usado por outro processo
                if (errofilegeral.Source == "mscorlib")
                {
                    /// Avisa o usuário sobre o problema com o arquivo texto de próximo som
                    if (eumnext == true)
                        throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está em uso por outro aplicativo ou processo do sistema!");

                    /// Avisa o usuário sobre o problema com o arquivo
                    throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está em uso por outro aplicativo ou processo do sistema!");
                }

                /// Pega os dados do arquivo texto com o nome da música
                FileInfo arquivotextomusica = new FileInfo(arquivotexto);

                /// Verifica se o arquivo texto com o nome da música tem 0 bytes, se tiver
                if (arquivotextomusica.Length == 0)
                {
                    /// Caso seja um arquivo texto next, Avisa o usuário sobre o problema com o arquivo texto de próximo som
                    if (eumnext == true)
                        throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio!");

                    /// Avisa o usuário sobre o problema com o arquivo
                    throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio!");
                }
            }

            /// Caso o arquivo texto currentsong.txtPID.txt ou similar não exista então ele faz a condição de cópia, Copia o arquivo texto
            if (!File.Exists(arquivotextoantigo))
                File.Copy(arquivotexto, arquivotextoantigo);

            /// Pega o arquivo antigo para ler com o nome do áudio só pra fazer a comparação abaixo
            using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.Default))
            {
                /// Carrega arquivo de texto antigo e faz a leitura da primeira linha do arquivo com o nome do áudio
                conteudotextoantigo = srOld.ReadLine().ToString();

                /// Força o fechamento do arquivo para que não dê erro no delete do arquivo
                srOld.Close();

                /// Força o despejo da memória do arquivo carregado
                srOld.Dispose();
            }

            /// Caso o conteúdo do arquivo texto tenha mais de 2000 caracteres
            if (conteudotexto.Length > 2000)
            {
                /// Caso o texto é um next song, Exibe mensagem de erro
                if (eumnext == true)
                    throw new Exception("O arquivo texto de próximo som contém mais de 2000 caracteres \nO servidor não é capaz de receber essa quantidade de caracteres! \nTente apagar algumas palavras do arquivo!");

                /// Exibe mensagem de erro
                else
                    throw new Exception("O arquivo texto de som contém mais de 2000 caracteres \nO servidor não é capaz de receber essa quantidade de caracteres! \nTente apagar algumas palavras do arquivo!");
            }

            /// Caso o conteúdo do arquivo seja menor que um
            if (conteudotexto.Length < 1)
            {
                /// Caso seja um arquivo texto next, Avisa o usuário sobre o problema com o arquivo texto de próximo som
                if (eumnext == true)
                    throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio ou falta a primeira linha!");

                /// Avisa o usuário sobre o problema com o arquivo
                else
                    throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio ou falta a primeira linha!");
            }

            /// Verifica se o arquivo anterior é o mesmo do atual, se não for então apaga o arquivo anterior e copia o arquivo novo
            if (conteudotexto != conteudotextoantigo || btnVerificardadosderds.Enabled == true)
            {
                try
                {
                    /// Apaga o conteúdo do arquivo texto para escrever em texto limpo abaixo
                    File.WriteAllText(arquivotextoantigo, string.Empty);

                    /// Cria ou abre o arquivo existente para leitura
                    FileStream fsOld = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate);

                    /// Pega o arquivo antigo para escrever com dados novos
                    using (StreamWriter swOld = new StreamWriter(fsOld, Encoding.Default))
                    {
                        /// Carrega arquivo de texto antigo e sobrescreve todos os dados que tem no arquivo
                        swOld.WriteLine(conteudotexto);

                        /// Força o fechamento do arquivo
                        swOld.Flush();
                        swOld.Close();

                        /// Força o despejo da memória do arquivo carregado
                        swOld.Dispose();
                        fsOld.Close();
                        fsOld.Dispose();
                    }
                }
                catch (IOException errfile)
                {
                    /// Grava o erro aqui
                    qualquerlixoaqui = errfile.Message;

                    /// Espera um tempo para processar novamente os arquivos
                    System.Threading.Thread.Sleep(1000);

                    /// Cria ou abre o arquivo existente para leitura
                    FileStream fsOld = new FileStream(arquivotextoantigo, FileMode.OpenOrCreate);

                    /// Pega o arquivo antigo para escrever com dados novos
                    using (StreamWriter swOld = new StreamWriter(fsOld, Encoding.Default))
                    {
                        /// Carrega arquivo de texto antigo e sobrescreve todos os dados que tem no arquivo
                        swOld.WriteLine(conteudotexto);

                        /// Força o fechamento do arquivo
                        swOld.Flush();
                        swOld.Close();

                        /// Força o despejo da memória do arquivo carregado
                        swOld.Dispose();
                        fsOld.Close();
                        fsOld.Dispose();
                    }
                }
            }
        }

        private void RecInfoDosDadosCad()
        {
            /// Altera para é um next = false para toda vez ter que verificar
            eumnext = false;

            /// Verifica o processo do aplicativo
            Process proc = Process.GetCurrentProcess();

            /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
            string identificadorproc = proc.Id.ToString();

            /// Declaração de string nula para uso abaixo, essa será carregada com as informações abaixo OBS: a primeira NULL é para sw.WriteLine não dar BUG!
            string labeldeinformacao;
            string urlparacarregar;
            string dadosadicionais;
            string conteudoarquivotextonextsong = "Update RDS By GabardoHost - Vanderson Gabardo";
            string dadosarquivotexto = null;

            /// Pega as informações dos textos das caixas de preenchimento
            string ipserver = txtDominioip.Text;
            string portaserver = txtPorta.Text;
            string senhaserver = $"{txtLoginserver.Text}:{txtSenhaserver.Text}";
            string idoupontomont = txtIdoumont.Text;

            /// Pega as informações dos textos das caixas de preenchimento e adiciona informações na string
            string arquivodelog = $@"{diretoriodoaplicativo}LOGS\SOM{identificadorproc}LOG.csv";
            string urlshoutcastv1 = $"http://{ipserver}:{portaserver}/admin.cgi?mode=updinfo&song=";
            string urlshoutcastv2 = $"http://{ipserver}:{portaserver}/admin.cgi?sid={idoupontomont}&mode=updinfo&song=";
            string urlicecast = $"http://{ipserver}:{portaserver}/admin/metadata?mount=/{idoupontomont}&mode=updinfo&song=";

            /// Informa o usuário quando foi a última atualização do arquivo via label
            lblInformacaoid.Text = $"Última checagem de atualização: {DateTime.Now.ToString()} - ID do Processo do aplicativo em execução: {identificadorproc}";

            /// Caso o usuário queira transmitir play next para servidor shoutcast v2 então entra na condição abaixo
            if (chkTransmproxsom.Checked == true)
            {
                /// Muda o estado do é um next para true
                eumnext = true;

                /// Caso esteja marcado para verificar a URL, Chama o método para verificar a URL
                if (chkUrlsomnext.Checked == true)
                    TratamentoURLNowNext();

                /// Chama método para verificar o arquivo next nextsong ou similar
                else
                    TratamentoTextoNowNext();

                /// Altera dados do conteudo arquivo texto next song
                conteudoarquivotextonextsong = conteudotexto;
            }

            /// Caso esteja marcado para verificar a URL
            if (chkUrlsom.Checked == true)
            {
                /// Muda o estado do é um next para false
                eumnext = false;

                /// Chama o método para verificar a URL
                TratamentoURLNowNext();
            }
            else
            {
                /// Muda o estado do é um next para false
                eumnext = false;

                /// Chama método para verificar o arquivo now currentsong ou similar
                TratamentoTextoNowNext();
            }

            /// Pega o valor para uso abaixo
            string conteudoarquivotexto = conteudotexto;
            string conteudoarquivotextoantigo = conteudotextoantigo;

            /// Caso o conteúdo do som seja igual ao outro do next song
            if (conteudoarquivotexto == conteudoarquivotextonextsong)
            {
                /// Espera um tempo para carregar novamente a verificação
                System.Threading.Thread.Sleep(1500);

                /// Caso o usuário queira transmitir play next para servidor shoutcast v2 então entra na condição abaixo
                if (chkTransmproxsom.Checked == true)
                {
                    /// Muda o estado do é um next para true
                    eumnext = true;

                    /// Caso esteja marcado para verificar a URL, Chama o método para verificar a URL
                    if (chkUrlsomnext.Checked == true)
                        TratamentoURLNowNext();

                    /// Chama método para verificar o arquivo next nextsong ou similar
                    else
                        TratamentoTextoNowNext();

                    /// Altera dados do conteudo arquivo texto next song
                    conteudoarquivotextonextsong = conteudotexto;
                }

                /// Caso esteja marcado para verificar a URL
                if (chkUrlsom.Checked == true)
                {
                    /// Muda o estado do é um next para false
                    eumnext = false;

                    /// Chama o método para verificar a URL
                    TratamentoURLNowNext();
                }
                else
                {
                    /// Muda o estado do é um next para false
                    eumnext = false;

                    /// Chama método para verificar o arquivo now currentsong ou similar
                    TratamentoTextoNowNext();
                }
            }

            /// Verifica se o arquivo anterior é o mesmo do atual, se não for então atualiza o nome do arquivo no servidor
            if (conteudoarquivotexto != conteudoarquivotextoantigo || btnVerificardadosderds.Enabled == true)
            {
                /// Remove o & pelo e para não dar problemas na URL abaixo
                conteudoarquivotexto = conteudoarquivotexto.Replace("&", "e").Replace("_", " ");

                /// Remove do conteudo arquivo texto next song o & pelo e para não dar problemas na URL abaixo
                conteudoarquivotextonextsong = conteudoarquivotextonextsong.Replace("&", "e").Replace("_", " ");

                /// Caso o usuário queira remover os caracteres do nome da música então entra na condição
                if (chkCaracteresespeciais.Checked == true)
                {
                    /// Pega os caracteres a verificar
                    string caracteresaanalisar = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\-\s]";

                    /// Faz um replace nos desnecessários
                    Regex rgx = new Regex(caracteresaanalisar);

                    /// Pega o resultado do replace
                    string resultado = rgx.Replace(conteudoarquivotexto, " ");
                    conteudoarquivotexto = resultado;

                    /// Pega o resultado do replace do conteudo arquivo texto next song
                    string resultadonext = rgx.Replace(conteudoarquivotextonextsong, " ");
                    conteudoarquivotextonextsong = resultadonext;
                }

                /// Caso o usuário queira remover os acentos do texto no arquivo, então faz a condição abaixo
                if (chkAcentospalavras.Checked == true)
                {
                    /// Pega o texto com acentos e remove
                    string RemoveAcentos(string textopuroacentuado)
                    {
                        /// Remove acentos usando a codificação abaixo
                        return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(textopuroacentuado));
                    }

                    /// Altera o conteúdo da string com os acentos removidos
                    conteudoarquivotexto = RemoveAcentos(conteudoarquivotexto);

                    /// Pega o texto do arquivo next song com acentos e remove
                    string RemoveAcentosNext(string textopuroacentuadonext)
                    {
                        /// Remove acentos usando a codificação abaixo
                        return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(textopuroacentuadonext));
                    }

                    /// Altera o conteudo arquivo texto next song da string com os acentos removidos
                    conteudoarquivotextonextsong = RemoveAcentosNext(conteudoarquivotextonextsong);
                }

                /// Carrega por padrão a URL da versão 1 do shoutcast server, altera o link e faz replace no e comercial para não dar bug na URL de shoutcast
                urlparacarregar = urlshoutcastv1 + conteudoarquivotexto;

                /// Carrega icone para o aplicativo
                Icon = Resources.shoutcast;
                ntfIcone.Icon = Resources.shoutcast;
                pbFront.Image = Resources.shoutcast1;

                /// Verifica se é dados de Icecast, se for, realiza as condições de alteração abaixo para compatibilizar o icecast v2
                if (rbtIcecast.Checked == true)
                {
                    /// Carrega a URL padrão do Icecast para uso abaixo
                    urlparacarregar = urlicecast + conteudoarquivotexto;

                    /// Carrega icone para o aplicativo
                    Icon = Resources.icecast;
                    ntfIcone.Icon = Resources.icecast;
                    pbFront.Image = Resources.icecast1;
                }

                /// Carrega a URL padrão para Shoutcast Server v2 apenas caso marcado pelo usuário
                if (rbtShoutcastv2.Checked == true)
                {
                    /// Carrega a URL padrão para Shoutcast Server v2, altera o link e faz replace no e comercial para não dar bug na URL de shoutcast
                    urlparacarregar = urlshoutcastv2 + conteudoarquivotexto;

                    /// Caso o usuário marcou a opção de transmitir também o next song então entra na condição abaixo, Carrega a URL padrão para Shoutcast Server v2, altera o link e adiciona um next song no final da URL
                    if (chkTransmproxsom.Checked == true)
                        urlparacarregar = urlshoutcastv2 + conteudoarquivotexto + "&next=" + conteudoarquivotextonextsong;
                }

                /// Cria uma solicitação da web HTTP para o servidor
                HttpWebRequest webreqshouticecast = (HttpWebRequest)WebRequest.Create(urlparacarregar);

                /// Altera User-Agent da conexão para não dar bug na shoutcast
                webreqshouticecast.UserAgent = useragentdef;

                /// Autenticação do servidor informado com login e senha
                senhaserver = Convert.ToBase64String(Encoding.Default.GetBytes(senhaserver));
                webreqshouticecast.Headers.Add("Authorization", "Basic " + senhaserver);
                webreqshouticecast.Credentials = new NetworkCredential("username", "password");

                /// Método de conexão para a URL no caso é um GET simples
                webreqshouticecast.Method = WebRequestMethods.Http.Get;

                /// Permite redirecionamentos e não tem proxy server
                webreqshouticecast.AllowAutoRedirect = true;

                /// Caso tenha uso de servidor proxy
                if (chkUsoproxy.Checked == true)
                {
                    /// Chama função para ler dados do servidor proxy
                    DadosProxy();

                    /// Usa os dados processados do servidor proxy
                    webreqshouticecast.Proxy = servidorproxydoaplicativo;
                }
                else
                    webreqshouticecast.Proxy = null;

                /// Caso é uma shoutcast v1 faz a condição abaixo
                if (rbtShoutcastv1.Checked == true)
                {
                    try
                    {
                        /// Obtenha a resposta do servidor associada para o pedido acima
                        HttpWebResponse webrespshouticecast = (HttpWebResponse)webreqshouticecast.GetResponse();

                        /// Encerra a requisição HTTP para o servidor
                        webrespshouticecast.Close();
                    }
                    catch (WebException erroverificar)
                    {
                        /// Caso a mensagem de erro for diferente disso, carrega mensagem padrão do erro
                        if (erroverificar.Message != "A conexão subjacente estava fechada: A conexão foi fechada de modo inesperado.")
                            throw new WebException(erroverificar.Message);
                    }
                }
                else
                {
                    /// Obtenha a resposta do servidor associada para o pedido acima
                    HttpWebResponse webrespshouticecast = (HttpWebResponse)webreqshouticecast.GetResponse();

                    /// Encerra a requisição HTTP para o servidor
                    webrespshouticecast.Close();
                }

                /// Novas informações para adicionar no arquivo texto
                string novosdadosarquivotexto = DateTime.Now.ToString() + ";" + conteudoarquivotexto;

                /// Caso tenha um arquvio de som next, Adiciona a informação do next song no arquivo
                if (chkTransmproxsom.Checked == true)
                    novosdadosarquivotexto = novosdadosarquivotexto + ";" + conteudoarquivotextonextsong;

                /// Caso o diretório de logs não exista, Criamos um com o nome LOGS
                if (!Directory.Exists($@"{diretoriodoaplicativo}LOGS"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}LOGS");

                /// Caso o arquivo texto LOGRDS.txt ou similar exista então ele faz a condição de leitura
                if (File.Exists(arquivodelog))
                {
                    /// Pega o arquivo texto para ler
                    using (StreamReader sr = new StreamReader(arquivodelog, Encoding.Default))
                    {
                        /// Carrega arquivo e mantém carregado todas as linhas
                        dadosarquivotexto = sr.ReadToEnd();
                        dadosarquivotexto = dadosarquivotexto.Substring(0, dadosarquivotexto.Length - 2);
                    }
                }

                /// Cria ou abre o arquivo existente para leitura
                FileStream fs = new FileStream(arquivodelog, FileMode.OpenOrCreate);

                /// Grava ou salva arquivo em UTF 8 com a solicitação
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                /// Captura texto do arquivo texto e também dos dados adicionais para gravar no arquivo, Grava o arquivo com os dados para montagem da CSV
                if (string.IsNullOrEmpty(dadosarquivotexto))
                    dadosarquivotexto = "Data e Hora:;Nome do som:;Nome do próximo som:";

                /// Grava os dados no arquivo
                sw.WriteLine(dadosarquivotexto);
                sw.WriteLine(novosdadosarquivotexto);

                /// Limpa a sessão
                sw.Flush();
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();

                /// Pega os dados do arquivo
                FileInfo arquivotextolog = new FileInfo(arquivodelog);

                /// Move o arquivo se o arquivo tiver mais de 10 MB, Adiciona informações no nome de arquivo
                if (arquivotextolog.Length > 10485760)
                    File.Move(arquivodelog, arquivodelog + DateTime.Now.ToString().Replace(":", "").Replace("/", "") + ".txt");

                /// Caso o valor dos caracteres seja muito grande, tira uma parte para não exibir, Não exibe mais de 100 caracteres
                if (conteudotexto.Length > 100)
                    conteudoarquivotexto = conteudoarquivotexto.Substring(0, 100) + "...";

                /// Caso o valor dos caracteres de next song seja muito grande, tira uma parte para não exibir, Não exibe mais de 50 caracteres
                if (conteudoarquivotextonextsong.Length > 50)
                    conteudoarquivotextonextsong = conteudoarquivotextonextsong.Substring(0, 50) + "...";

                /// Se o botão estiver visível mostra a label abaixo com o texto
                if (btnVerificardadosderds.Enabled == true)
                {
                    /// Renomeia a label para o texto
                    labeldeinformacao = $"O RDS Transmitiu agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()} \nSe estiver tudo certo com o cadastro, clique no botão abaixo para começar a transmitir os dados:";

                    /// Caso tenha um next, Renomeia a label para o texto
                    if (chkTransmproxsom.Checked == true)
                        labeldeinformacao = $"O RDS Transmitiu agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nA Informação de próximo som é: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()} \nSe estiver tudo certo com o cadastro, clique no botão abaixo para começar a transmitir os dados:";

                    /// Apaga a informação da label para não dar bug na interface
                    lblInformacaoid.Text = "";
                }
                else
                {
                    /// Mostra na tela para o usuário que nome está transmitindo
                    labeldeinformacao = $"O RDS Está transmitindo agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()}";

                    /// Caso tenha um next, Mostra na tela para o usuário que nome está transmitindo
                    if (chkTransmproxsom.Checked == true)
                        labeldeinformacao = $"O RDS Está transmitindo agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nPróximo som: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()}";
                }

                /// Carrega e exibe os dados na label
                lblInformacao.Text = labeldeinformacao;

                /// String para adicionar mais informações no arquivo texto
                dadosadicionais = $"No ar o som: {conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()}";

                /// Caso tenha um next, String para adicionar mais informações no arquivo texto
                if (chkTransmproxsom.Checked == true)
                    dadosadicionais = $"No ar o som: {conteudoarquivotexto} \nPróximo som: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()}";

                /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                if (chkNaonotificarsomtray.Checked == false)
                    ntfIcone.ShowBalloonTip(60000, "Update RDS - Atualização de título de som", dadosadicionais, ToolTipIcon.Info);
            }
        }

        private void Temporizacao_Tick(object Sender, EventArgs e)
        {
            try
            {
                try
                {
                    /// Limpa os erros anteriores caso tenha
                    errogeralgravado = null;
                    errogeral = null;
                    weberrogeralcode = null;
                    weberrogeral = null;

                    /// Pega as informações dos dados para fazer o processamento
                    RecInfoDosDadosCad();
                }
                /// Exceção do web request client
                catch (WebException wex)
                {
                    /// Carrega na string geral os erros
                    weberrogeral = wex.Message;
                    weberrogeralcode = wex.Status.ToString();

                    /// Chama método para gravação de arquivos texto de erro
                    InfoErroGeral();
                }
            }
            /// Exceção geral do código
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();
            }
        }

        private void BtnVerificardadosderds_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Chama método para carregar as validações
                ValidarInformacoes();

                /// Tratamento de exceção para web request client
                try
                {
                    /// Chama método para carregar o processamento dos dados e informações
                    RecInfoDosDadosCad();

                    /// Torna invisível o botão para verificar os dados
                    btnVerificardadosderds.Enabled = false;

                    /// Torna visível o botão para revisar dados informados
                    btnRevisarinfo.Enabled = true;

                    /// Torna visível o botão para enviar o RDS para o servidor
                    btnEnviardadosrds.Enabled = true;

                    /// Desabilita as caixas e checkbox para que os dados não sejam mais alterados depois do teste da URL, links e textos
                    rbtShoutcastv1.Enabled = false;
                    rbtShoutcastv2.Enabled = false;
                    rbtIcecast.Enabled = false;
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

                    /// Limpa label de informações de atualização
                    lblInformacaoid.Text = "";
                }

                /// Exceção do web request client
                catch (WebException wex)
                {
                    /// Carrega na string geral os erros
                    weberrogeral = wex.Message;
                    weberrogeralcode = wex.Status.ToString();

                    /// Chama método para gravação de arquivos texto de erro
                    InfoErroGeral();

                    /// Apaga a informação da label para não dar bug na interface
                    lblInformacaoid.Text = "Aplicativo em execução - Registro de erro na data e hora: " + DateTime.Now;
                }
            }
            /// Exceção geral da execução do código e clique do botão
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEnviardadosrds_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Pega informação da caixa de texto para fazer o tempo determinado pelo usuário
                int tempoescolhido = Convert.ToInt32(txtTempoexec.Text + "000");

                /// Informa o usuário que já tem transmissão de dados para o servidor
                lblInformacao.Text = "O RDS está verificando e transmitindo dados! Aguarde próxima atualização de título... \nOu atualize o arquivo texto manualmente para que a informação seja atualizada!";

                /// Desabilita botão para que não possa ser clicado novamente depois de enviar os dados
                btnEnviardadosrds.Enabled = false;
                btnRevisarinfo.Enabled = false;

                /// Habilita botão para parar o envio de dados para o servidor
                btnPararenviords.Enabled = true;

                /// Habilita execução
                temporizadorgeral.Enabled = true;

                /// Define um intervalo para o bloco de códigos executar novamente definido para 5000 milisegundos
                temporizadorgeral.Interval = tempoescolhido;

                /// Evento que ocorre a cada 5000 milisegundos para atualizar o texto do servidor
                temporizadorgeral.Tick += new EventHandler(Temporizacao_Tick);

                /// Inicia o relógio
                temporizadorgeral.Start();
            }

            /// Exceção geral do botão
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Caso tenha um problema geral, exibe mensagem bruta sem tratamentos
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPararenviords_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;

                /// Verifica o processo do aplicativo
                Process proc = Process.GetCurrentProcess();

                /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
                string identificadorproc = proc.Id.ToString();

                /// Pega as informações dos textos das caixas de preenchimento e adiciona informações na string
                string caminhoarquivoantigo = $@"{txtArquivotextosom.Text}{identificadorproc}.txt";
                string caminhoarquivoantigonext = $@"{txtArquivotextosomnext.Text}{identificadorproc}.txt";

                /// Envia mensagem confirmando se o usuário gostaria mesmo de parar o envio de dados
                if (MessageBox.Show("Você gostaria de parar o envio de dados? ao parar, os dados de RDS não serão enviados para o servidor!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    /// Avisa o usuário que o programa vai parar de enviar os dados do RDS
                    MessageBox.Show("Os dados de RDS pararam de ser enviados para o servidor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /// Faz o relógio parar de rodar
                    temporizadorgeral.Stop();

                    /// Desabilita o botão de parar
                    btnPararenviords.Enabled = false;

                    /// Reabilitação dos botões e formulário para edição
                    rbtShoutcastv1.Enabled = true;
                    rbtShoutcastv2.Enabled = true;
                    rbtIcecast.Enabled = true;
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

                    /// Se o chkurl estiver marcado então habilita o botão btnLocalizacs.Enabled = true;
                    if (chkUrlsom.Checked == false)
                        btnLocalizatxtsom.Enabled = true;

                    if (chkUrlsom.Checked == true)
                        txtUrlsom.Enabled = true;

                    /// Se o shoutcast de versão 1 estiver desabilitado, Então habilita a caixa de edição mountid
                    if (rbtShoutcastv1.Checked == false)
                        txtIdoumont.Enabled = true;

                    /// Caso o botão shoutcast v2 esteja marcado
                    if (rbtShoutcastv2.Checked == true)
                    {
                        /// Habilita checkbox para transmitir next song
                        chkTransmproxsom.Enabled = true;

                        /// Caso esteja marcado o transmitir next song
                        if (chkTransmproxsom.Checked == true)
                        {
                            /// Caso a URL esteja desmarcada, Habilita o botão
                            if (chkUrlsomnext.Checked == false)
                                btnLocalizatxtsomnext.Enabled = true;

                            /// Caso a URL esteja marcada, Habilita caixa de texto
                            if (chkUrlsomnext.Checked == true)
                                txtUrlsomnext.Enabled = true;

                            /// Habilita caixa de seleção URL NEXT
                            chkUrlsomnext.Enabled = true;
                        }
                    }

                    /// Habilita novamente o botão para verificação de dados
                    btnVerificardadosderds.Enabled = true;

                    /// Limpa a label de informações e altera informações da label
                    lblInformacao.Text = "O RDS Não está sendo transmitido para o servidor! Para continuar enviando dados, clique no botão abaixo:";
                    lblInformacaoid.Text = "Última checagem de modificação do arquivo: " + DateTime.Now.ToString();

                    /// Apaga os arquivos anteriores
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
                    File.Delete(caminhoarquivoantigo);
                    File.Delete(caminhoarquivoantigonext);

                    /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Informação", "O RDS Não está sendo transmitido para o servidor!", ToolTipIcon.Info);
                }

                /// Caso o usuário não queira parar, faz o procedimento abaixo, Avisa o usuário que o programa vai enviar dados de RDS
                else
                    MessageBox.Show("Os dados de RDS continuarão sendo enviados para o servidor", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /// Exceção geral da execução do código e clique do botão
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRevisarinfo_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Reabilita as caixas e checkbox para que os dados sejam alterados depois do teste da URL, links e textos
                rbtShoutcastv1.Enabled = true;
                rbtShoutcastv2.Enabled = true;
                rbtIcecast.Enabled = true;
                chkUsoproxy.Enabled = true;
                txtTempoexec.Enabled = true;
                btnSalvadados.Enabled = true;
                btnCarregadados.Enabled = true;
                chkUrlsom.Enabled = true;
                btnResolvernomeip.Enabled = true;
                txtDominioip.Enabled = true;
                txtPorta.Enabled = true;
                txtLoginserver.Enabled = true;
                txtSenhaserver.Enabled = true;

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

                /// Se o shoutcast de versão 1 estiver desabilitado, Então habilita a caixa de edição mountid
                if (rbtShoutcastv1.Checked == false)
                    txtIdoumont.Enabled = true;

                /// Caso o botão shoutcast v2 esteja marcado
                if (rbtShoutcastv2.Checked == true)
                {
                    /// Habilita checkbox para transmitir next song
                    chkTransmproxsom.Enabled = true;

                    /// Caso esteja marcado o transmitir next song
                    if (chkTransmproxsom.Checked == true)
                    {
                        /// Caso a URL esteja desmarcada, Habilita o botão
                        if (chkUrlsomnext.Checked == false)
                            btnLocalizatxtsomnext.Enabled = true;

                        /// Caso a URL esteja marcada, Habilita caixa de texto
                        if (chkUrlsomnext.Checked == true)
                            txtUrlsomnext.Enabled = true;

                        /// Habilita caixa de seleção URL NEXT
                        chkUrlsomnext.Enabled = true;
                    }
                }

                /// Se o chkurl estiver marcado então habilita o botão btnLocalizacs.Enabled = true;
                if (chkUrlsom.Checked == false)
                    btnLocalizatxtsom.Enabled = true;

                if (chkUrlsom.Checked == true)
                    txtUrlsom.Enabled = true;

                /// Oculta botões para que os botões não sejam clicados indevidamente
                btnRevisarinfo.Enabled = false;
                btnEnviardadosrds.Enabled = false;

                /// Habilita novamente o botão de verificação de dados para que possa verificar novamente os dados
                btnVerificardadosderds.Enabled = true;

                /// Limpa a label caso venha preenchida com os dados
                lblInformacaoid.Text = "";
                lblInformacao.Text = "O Processo de envio foi interrompido com sucesso! \nPara retomar o envio dos dados, preencha ou faça as correções e clique no botão abaixo:";
                /// Verifica o processo do aplicativo
                Process proc = Process.GetCurrentProcess();

                /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
                string identificadorproc = proc.Id.ToString();

                /// Pega o caminho dos arquivos antigos para apagar ao fechar o programa
                string caminhoarquivoantigo = $@"{txtArquivotextosom.Text}{identificadorproc}.txt";
                string caminhoarquivoantigonext = $@"{txtArquivotextosomnext.Text}{identificadorproc}.txt";

                /// Apaga os arquivos antigos
                File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");
                File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
                File.Delete(caminhoarquivoantigo);
                File.Delete(caminhoarquivoantigonext);
            }

            /// Exceção geral do botão para tratamento
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Caso tenha um problema geral do botão, aparece mensagem bruta em forma de popup
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalvadados_Click(object sender, EventArgs e)
        {
            try
            {
                /// Declara stream para uso abaixo
                Stream nomedoarquivoxml;

                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Carrega método para validar as informações antes de salvar
                ValidarInformacoes();

                /// Cria um novo save dialog
                SaveFileDialog salvardadosdexml = new SaveFileDialog
                {
                    Filter = "Arquivos XML (*.XML)|*.XML|Arquivos XML (*.xml)|*.xml",
                    FilterIndex = 2,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                /// Mostra a caixa de diálogo para o usuário e se for OK
                if (salvardadosdexml.ShowDialog() == DialogResult.OK)
                {
                    /// Salva o arquivo XML
                    if ((nomedoarquivoxml = salvardadosdexml.OpenFile()) != null)
                    {
                        ///  Esta linha indica que o arquivo xml sera salvo
                        XmlTextWriter xtw = new XmlTextWriter(nomedoarquivoxml, Encoding.Default)
                        {
                            /// A linha abaixo vai identar o código, se não usar isso tudo ficará em uma linha.
                            Formatting = Formatting.Indented
                        };

                        /// Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>
                        xtw.WriteStartDocument();

                        /// Grava os dados das caixas, checkbox e outros dados
                        xtw.WriteStartElement("Configuracao");
                        xtw.WriteElementString("SOFTWARE-UPDATE-RDS-XML-VERSION", "Ver-XML-1.0");
                        xtw.WriteElementString("SHOUTCASTV1", rbtShoutcastv1.Checked.ToString());
                        xtw.WriteElementString("SHOUTCASTV2", rbtShoutcastv2.Checked.ToString());
                        xtw.WriteElementString("ICECASTV2", rbtIcecast.Checked.ToString());
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
                        xtw.WriteElementString("TXTARQUIVODETEXTOSOM", txtArquivotextosom.Text);
                        xtw.WriteElementString("ATUALIZARSOMPORURL", chkUrlsom.Checked.ToString());
                        xtw.WriteElementString("TXTURLSOM", txtUrlsom.Text);
                        xtw.WriteElementString("TXTARQUIVOPROXIMOSOM", txtArquivotextosomnext.Text);
                        xtw.WriteElementString("URLPROXIMOSOM", chkUrlsomnext.Checked.ToString());
                        xtw.WriteElementString("TXTURLPROXIMOSOM", txtUrlsomnext.Text);
                        xtw.WriteElementString("IPOUDOMINIO", txtDominioip.Text);
                        xtw.WriteElementString("PORTA", txtPorta.Text);
                        xtw.WriteElementString("IDOUPONTODEMONTAGEM", txtIdoumont.Text);
                        xtw.WriteElementString("LOGINDOSERVER", txtLoginserver.Text);
                        xtw.WriteElementString("SENHADOSERVER", txtSenhaserver.Text);
                        xtw.WriteElementString("DATAEHORASALVOXML", DateTime.Now.ToString());
                        xtw.WriteEndElement();
                        xtw.WriteEndDocument();

                        /// Libera o XmlTextWriter
                        xtw.Flush();

                        /// Fecha o XmlTextWriter
                        xtw.Close();

                        /// Codigo que escreve o stream está aqui
                        nomedoarquivoxml.Close();

                        /// Envia mensagem que os dados foram salvos com sucesso
                        MessageBox.Show("As informações preenchidas aqui foram salvas com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    /// Envia mensagem que os dados não foram salvos
                    MessageBox.Show("As informações preenchidas aqui não foram salvas!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCarregadados_Click(object sender, EventArgs e)
        {
            try
            {
                /// Declara nome do arquivo com os adicionais
                Stream nomearquivoxml;

                /// Declara uma versão para comparação para carregar os dados
                string versaoparacomparacao = "Ver-XML-1.0";
                string versaoxmlcarregado;

                /// Cria um novo save dialog
                OpenFileDialog carregardadosdexml = new OpenFileDialog
                {
                    Filter = "Arquivos XML (*.XML)|*.XML|Arquivos XML (*.xml)|*.xml",
                    FilterIndex = 2,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                /// Mostra a caixa de diálogo para o usuário e se for OK
                if (carregardadosdexml.ShowDialog() == DialogResult.OK)
                {
                    /// Carrega o arquivo XML
                    if ((nomearquivoxml = carregardadosdexml.OpenFile()) != null)
                    {
                        /// Cria uma instância de um documento XML
                        XmlDocument oXML = new XmlDocument();

                        /// Carrega o arquivo XML
                        oXML.Load(nomearquivoxml);

                        XmlElement root = oXML.DocumentElement;
                        XmlNodeList lst = root.GetElementsByTagName("SOFTWARE-UPDATE-RDS-XML-VERSION");

                        if (lst.Count == 0)
                            throw new Exception("Aviso! O arquivo XML carregado é inválido! Preencha novamente os dados da tela e salve um XML novo, ou procure o arquivo XML salvo pelo aplicativo!");

                        /// Lê o filho de um Nó Pai específico e adiciona as informações nas caixas de checagem
                        versaoxmlcarregado = oXML.SelectSingleNode("Configuracao").ChildNodes[0].InnerText;

                        if (versaoparacomparacao != versaoxmlcarregado)
                        {
                            throw new Exception("Aviso! A versão do XML carregada é incompatível com a versão desse software! Preencha novamente os dados da tela para esta versão e salve um XML novo!");
                        }

                        rbtShoutcastv1.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[1].InnerText);
                        rbtShoutcastv2.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[2].InnerText);
                        rbtIcecast.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[3].InnerText);
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
                        txtArquivotextosom.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[17].InnerText;
                        chkUrlsom.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[18].InnerText);
                        txtUrlsom.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[19].InnerText;
                        txtArquivotextosomnext.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[20].InnerText;
                        chkUrlsomnext.Checked = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[21].InnerText);
                        txtUrlsomnext.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[22].InnerText;
                        txtDominioip.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[23].InnerText;
                        txtPorta.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[24].InnerText;
                        txtIdoumont.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[25].InnerText;
                        txtLoginserver.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[26].InnerText;
                        txtSenhaserver.Text = oXML.SelectSingleNode("Configuracao").ChildNodes[27].InnerText;
                        qualquerlixoaqui = oXML.SelectSingleNode("Configuracao").ChildNodes[28].InnerText;

                        /// Envia mensagem que os dados foram carregados com sucesso
                        MessageBox.Show("As informações foram carregadas com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    throw new Exception("Não foi possível carregar a configuração pois houve o cancelamento da abertura do arquivo XML!");
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnLocalizatxtsom_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
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
                    txtArquivotextosom.Text = ofdCca.FileName;
            }
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Caso tenha um problema geral do botão, aparece mensagem bruta em forma de popup
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLocalizatxtsomnext_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
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
                    txtArquivotextosomnext.Text = ofdCca.FileName;
            }
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Caso tenha um problema geral do botão, aparece mensagem bruta em forma de popup
                MessageBox.Show(ex.Message, "Erro do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnResolvernomeip_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Pega o nome ou o IP do domínio
                string nomeouip = txtDominioip.Text;

                bool endipvalido = IPAddress.TryParse(nomeouip, out IPAddress IP);

                if (endipvalido == true)
                    throw new Exception("Você digitou um endereço de IP! \nAqui você não consegue resolver endereço IP em nome de domínio, somente nome para endereço de IP! \nDigite um nome de domínio válido, como no exemplo: seuservidor.suaurl.com.br");
                try
                {
                    /// Pega o endereço de IP do host
                    IPHostEntry host = Dns.GetHostEntry(nomeouip);

                    /// Faz um tratamento para achar o IP do domínio
                    foreach (IPAddress ip in host.AddressList)
                    {
                        /// Caso seja um IPv4 faz a condição abaixo, Retorna o IP e sobrepoe no campo de IP ou nome
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                            txtDominioip.Text = ip.ToString();
                    }
                }
                catch (SocketException sexept)
                {
                    if (sexept.SocketErrorCode.ToString() == "HostNotFound")
                        throw new Exception($"Ocorreu um erro ao tentar resolver o nome para o IP: \nO domínio {txtDominioip.Text} não pode ser resolvido, verifique se não errou a digitação do endereço!");

                    throw new Exception("Ocorreu um erro ao tentar resolver o nome para o IP: " + sexept.Message);
                }
            }
            catch (Exception ex)
            {
                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Caso tenha um problema geral do botão, aparece mensagem bruta em forma de popup
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NtfIcone_Click(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Caso a tela não esteja maximizada executa as funções abaixo
                if (WindowState != FormWindowState.Normal)
                {
                    /// Caso o usuário clique no ícone na bandeja do sistema, então ele restaura para a tela normal
                    Show();
                    WindowState = FormWindowState.Normal;

                    /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Você clicou em mim", "E eu abri :D", ToolTipIcon.Info);
                }
                else
                {
                    /// Verifica se o usuário selecionou a caixa para não ser notificado, Transfere dados para um balão chato haha de texto
                    if (chkNaonotificarsomtray.Checked == false)
                        ntfIcone.ShowBalloonTip(60000, "Update RDS - Você clicou em mim", "E eu já estou aberto, não está me vendo? :D", ToolTipIcon.Info);

                    /// Envia mensagem perguntando se o usuário gostaria de minimizar o aplicativo na system tray, Caso o usuário queira minimizar o aplicativo
                    if (chkNaominimsystray.Checked == false && MessageBox.Show("Você gostaria de minimizar na bandeja do sistema?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        WindowState = FormWindowState.Minimized;
                }
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RbtShoutcastv1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Caso o usuário mantenha a versão 1 marcada
                if (rbtShoutcastv1.Checked == true)
                {
                    /// Desabilita a caixa de texto e limpa a caixa de texto
                    txtIdoumont.Enabled = false;
                    txtIdoumont.Text = "";
                }

                /// Caso não tenha mantido marcada a versão 1, habilita a caixa de texto para edição
                else
                    txtIdoumont.Enabled = true;
            }
            catch (Exception ex)
            {
                /// Caso ocorra algum erro de exceção, faz o descarte aqui
                qualquerlixoaqui = ex.Message;
            }
        }

        private void RbtShoutcastv2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Caso o usuário mantenha a versão 2 marcada, Habilita a caixa de texto
                if (rbtShoutcastv2.Checked == true)
                    chkTransmproxsom.Enabled = true;

                else
                {
                    /// Caso não tenha mantido marcada a versão 2, desabilita a caixa de texto para edição e limpa a caixa de texto
                    chkTransmproxsom.Checked = false;
                    chkTransmproxsom.Enabled = false;
                    btnLocalizatxtsomnext.Enabled = false;
                    txtArquivotextosomnext.Text = "";
                }
            }
            catch (Exception ex)
            {
                /// Caso ocorra algum erro de exceção, faz o descarte aqui
                qualquerlixoaqui = ex.Message;
            }
        }

        private void ChkTransmproxsom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Caso o usuário mantenha transmitir marcado
                if (chkTransmproxsom.Checked == true)
                {
                    /// Habilita botão
                    btnLocalizatxtsomnext.Enabled = true;

                    /// Habilita checkbox para URL
                    chkUrlsomnext.Enabled = true;
                }
                else
                {
                    /// Caso não tenha mantido marcado, desabilita o botão e checkbox da URL e limpa a caixa de texto
                    btnLocalizatxtsomnext.Enabled = false;
                    txtArquivotextosomnext.Text = "";
                    txtUrlsomnext.Text = "";
                    chkUrlsomnext.Enabled = false;
                    chkUrlsomnext.Checked = false;
                }
            }
            catch (Exception ex)
            {
                /// Caso ocorra algum erro de exceção, faz o descarte aqui
                qualquerlixoaqui = ex.Message;
            }
        }

        private void ChkUrlsom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Caso o usuário marque a caixa de seleção
                if (chkUrlsom.Checked == true)
                {
                    /// Habilita caixa de texto
                    txtUrlsom.Enabled = true;

                    /// Desabilita botão
                    btnLocalizatxtsom.Enabled = false;

                    /// Limpa caixa de texto
                    txtArquivotextosom.Text = "";
                }
                else
                {
                    /// Caso não tenha mantido marcado, Habilita o botão e limpa a caixa de texto
                    btnLocalizatxtsom.Enabled = true;
                    txtUrlsom.Enabled = false;
                    txtUrlsom.Text = "";
                }
            }
            catch (Exception ex)
            {
                /// Caso ocorra algum erro de exceção, faz o descarte aqui
                qualquerlixoaqui = ex.Message;
            }
        }

        private void ChkUrlsomnext_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Caso o usuário marque a caixa de seleção
                if (chkUrlsomnext.Checked == true)
                {
                    /// Habilita caixa de texto
                    txtUrlsomnext.Enabled = true;

                    /// Desabilita botão
                    btnLocalizatxtsomnext.Enabled = false;

                    /// Limpa caixa de texto
                    txtArquivotextosomnext.Text = "";
                }
                else
                {
                    /// Caso não tenha mantido marcado, Habilita o botão e limpa a caixa de texto
                    if (chkTransmproxsom.Checked == true)
                        btnLocalizatxtsomnext.Enabled = true;

                    txtUrlsomnext.Enabled = false;
                    txtUrlsomnext.Text = "";
                }
            }
            catch (Exception ex)
            {
                /// Caso ocorra algum erro de exceção, faz o descarte aqui
                qualquerlixoaqui = ex.Message;
            }
        }

        private void ChkDadossensiveis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;

                /// Caso o exibir no aplicativo dados sensiveis for habilitado, Então ele remove a passwordchar da caixa de texto
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
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRDS_Resize(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;

                /// Caso esteja minimizado, e o não minimizar não esteja marcado, Então ele minimiza o aplicativo na bandeja do sistema
                if (WindowState == FormWindowState.Minimized & chkNaominimsystray.Checked == false)
                    Hide();
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRDS_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;
                erroconta = -1;
                errocontanext = -1;

                /// Verifica se o usuário realmente cancelou o fechamento do programa
                bool canceloufechamento = false;

                /// Caso o botão de verificação de dados não estiver mais disponível
                if (btnVerificardadosderds.Enabled == false)
                {
                    /// Então envia mensagem confirmando se o usuário gostaria mesmo de fechar o aplicativo
                    if (MessageBox.Show("Você gostaria MESMO de fechar esse programa? ao fechar o aplicativo, os dados de RDS não serão enviados para o servidor!", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        /// Caso o usuário não queira fechar o aplicativo, então cancela a ação de fechamento do aplicativo
                        e.Cancel = true;

                        /// Avisa o usuário que o programa vai continuar enviando os dados do RDS
                        MessageBox.Show("Os dados de RDS continuam sendo enviados para o servidor!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        /// Altera a verificação de cancelamento de fechamento do programa
                        canceloufechamento = true;
                    }

                    /// Caso o usuário queira realmente fechar o aplicativo, faz o procedimento abaixo
                    else
                    {
                        /// Espera um segundo para fechar o programa
                        System.Threading.Thread.Sleep(1000);

                        /// Avisa o usuário que o programa está encerrado
                        MessageBox.Show("O Aplicativo encerrou a execução, verifique se existe mais algum arquivo temporário na pasta do texto, caso tenha é só apagar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                /// Verifica o processo do aplicativo
                Process proc = Process.GetCurrentProcess();

                /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
                string identificadorproc = proc.Id.ToString();

                /// Pega o caminho dos arquivos antigos para apagar ao fechar o programa
                string caminhoarquivoantigo = $@"{txtArquivotextosom.Text}{identificadorproc}.txt";

                /// Caso o arquivo texto currentsong.txtPID.txt ou similar exista então ele faz a condição abaixo, Apaga os arquivos antigos
                if (File.Exists(caminhoarquivoantigo) && canceloufechamento == false)
                    File.Delete(caminhoarquivoantigo);

                /// Pega o caminho dos arquivos NEXT SONG antigos para apagar ao fechar o programa
                string caminhoarquivoantigonext = $@"{txtArquivotextosomnext.Text}{identificadorproc}.txt";

                /// Caso o arquivo texto nextsong.txtPID.txt ou similar exista então ele faz a condição abaixo, Apaga os arquivos antigos
                if (File.Exists(caminhoarquivoantigonext) && canceloufechamento == false)
                    File.Delete(caminhoarquivoantigonext);

                /// Caso o arquivo texto PIDNEXT.txt ou similar exista então ele faz a condição abaixo, Apaga os arquivos antigos
                if (File.Exists($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt") && canceloufechamento == false)
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");

                /// Caso o arquivo texto PIDOLD.txt ou similar exista então ele faz a condição abaixo, Apaga os arquivos antigos
                if (File.Exists($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt") && canceloufechamento == false)
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChkUsoproxy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;

                /// Caso o uso de proxy server for habilitado, Então ele altera a caixa de texto para visível
                if (chkUsoproxy.Checked == true)
                {
                    txtDoproxy.Enabled = true;
                    txtPortaproxy.Enabled = true;
                    chkAutenticaproxy.Enabled = true;
                }
                else
                {
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
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChkAutenticaproxy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                /// Limpa os erros anteriores caso tenha
                errogeralgravado = null;
                errogeral = null;
                weberrogeralcode = null;
                weberrogeral = null;

                /// Caso o uso de proxy server for habilitado, Então ele altera a caixa de texto para visível
                if (chkAutenticaproxy.Checked == true)
                {
                    txtLoginproxy.Enabled = true;
                    txtSenhaproxy.Enabled = true;
                }
                else
                {
                    txtLoginproxy.Enabled = false;
                    txtSenhaproxy.Enabled = false;
                    txtLoginproxy.Text = null;
                    txtSenhaproxy.Text = null;
                }
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnVerupdate_Click(object sender, EventArgs e)
        {
            try
            {
                /// Chama método para baixar a nova versão do aplicativo
                UpdateAppRDS();

                /// Caso a versão do aplicativo esteja na versão atual
                if (versaonova == false)
                {
                    /// Avisa o usuário que o programa está atualizado
                    MessageBox.Show($"O Aplicativo instalado nesse sistema está atualizado!\nVerifique se existe uma nova versão do aplicativo novamente mais tarde!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                /// Apaga a informação da label para não dar bug na interface
                lblInformacaoid.Text = "";

                /// Carrega na string geral os erros
                errogeral = ex.Message;
                errogeralgravado = ex.StackTrace;

                /// Chama método para gravação de arquivos texto de erro
                InfoErroGeral();

                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show($"Infelizmente não foi possível verificar a atualização do aplicativo!\nNão foi possível verificar devido ao seguinte problema:\n{ex.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}