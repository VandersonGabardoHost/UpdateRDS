using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;

namespace UpdateRDS
{
    public partial class UpdateRDSTextMode
    {
        static readonly WebProxy servidorproxydoaplicativo = new WebProxy();
        static readonly string useragentdef = "Update RDS By GabardoHost v0.8 RC Final - Mozilla/50MIL.0 (Windows NeanderThal) KHTML like Gecko Chrome Opera Safari Netscape Internet Exploit Firefox Godzilla Giroflex Alex Marques Print";
        static readonly string versaoappcurrent = "Versao " + System.Windows.Forms.Application.ProductVersion;
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
        static readonly Timer temporizadorgeral = new Timer();

        int cbCaracteres;
        int cbTiposervidor;
        bool chkEnviatitulosom;
        bool chkNaominimsystray;
        bool chkAcentospalavras;
        bool chkCaracteresespeciais;
        bool chkDadossensiveis;
        bool chkTransmproxsom;
        bool chkUsoproxy;
        bool chkAutenticaproxy;
        string txtDoproxy;
        string txtPortaproxy;
        string txtLoginproxy;
        string txtSenhaproxy;
        string txtTempoexec;
        string lblArquivotextosom;
        bool chkUrlsom;
        string txtUrlsom;
        string lblArquivotextosomnext;
        bool chkUrlsomnext;
        string txtUrlsomnext;
        string txtDominioip;
        string txtPorta;
        string txtIdoumont;
        string txtLoginserver;
        string txtSenhaserver;
        string txtNomeemi;

        private void Temporizador(object sender, EventArgs e)
        {
            try
            {
                string senhadoserver = $"{txtLoginserver}:{txtSenhaserver}";

                RecInfoDosDadosCad(txtDominioip, txtPorta, senhadoserver, txtIdoumont);
            }
            catch (Exception ex)
            {
                if (erroconta == -3 || errocontanext == -2 || erroconta == -2)
                {
                    ErrGerApl(ex.Message, ex.StackTrace, ex.Source);
                }
                else
                {
                    ErrGerApl(ex.Message, ex.StackTrace, ex.Source);
                }
            }
        }

        private void EnviarDadosRds(object sender, EventArgs e)
        {
            try
            {
                erroconta = -1;
                errocontanext = -1;

                if (chkEnviatitulosom == true)
                {
                    if (!File.Exists(diretoriodoaplicativo + "CT.txt"))
                    {
                        if (!string.IsNullOrEmpty(txtNomeemi))
                        {
                            File.WriteAllText(diretoriodoaplicativo + "CT.txt", txtNomeemi);
                        }
                        else
                            File.WriteAllText(diretoriodoaplicativo + "CT.txt", "Update RDS - Enviando dados para o servidor");
                    }

                    lblArquivotextosom = diretoriodoaplicativo + "CT.txt";
                }

                string senhadoserver = $"{txtLoginserver}:{txtSenhaserver}";

                RecInfoDosDadosCad(txtDominioip, txtPorta, senhadoserver, txtIdoumont);

                int tempoescolhido = Convert.ToInt32(txtTempoexec + "000");

                temporizadorgeral.Interval = tempoescolhido;
                temporizadorgeral.Elapsed += new ElapsedEventHandler(Temporizador);
                temporizadorgeral.Enabled = true;
                temporizadorgeral.Start();
            }
            catch (Exception ex)
            {
                if (erroconta == -3 || errocontanext == -2 || erroconta == -2)
                {
                    ErrGerApl(ex.Message, ex.StackTrace, ex.Source);
                }
                else
                {
                    ErrGerApl(ex.Message, ex.StackTrace, ex.Source);
                }
            }
        }

        public void EncerrarExecucao()
        {
            try
            {
                temporizadorgeral.Stop();
                temporizadorgeral.Enabled = false;

                string identificadorproc = processodoaplicativo.Id.ToString();
                string caminhoarquivoantigo = $@"{lblArquivotextosom}{identificadorproc}.txt";

                if (File.Exists(caminhoarquivoantigo))
                    File.Delete(caminhoarquivoantigo);

                string caminhoarquivoantigonext = $@"{lblArquivotextosomnext}{identificadorproc}.txt";

                if (File.Exists(caminhoarquivoantigonext))
                    File.Delete(caminhoarquivoantigonext);

                if (File.Exists($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt"))
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}OLD.txt");

                if (File.Exists($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt"))
                    File.Delete($@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt");
            }
            catch (Exception ex)
            {
                ErrGerApl(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void UpdateAppRDS(string versaonovadoapp)
        {
            if (!Directory.Exists(diretoriodoaplicativo))
                Directory.CreateDirectory(diretoriodoaplicativo);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Carregando Versão 0.8 RC Final (A verificar nova versão...)");
            Console.WriteLine();
            // string urlcompletaversao = "http://localhost/updaterds/versao.txt";
            string urlcompletaversao = "http://www.vanderson.net.br/updaterds/versao.txt";

            try
            {
                using (WebClient wcurlcompletaversao = new WebClient())
                {
                    wcurlcompletaversao.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                    if (chkUsoproxy == true)
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A Versão 0.8 RC Final está DESATUALIZADA!");

                if (System.Windows.Forms.MessageBox.Show($"Há uma nova versão do aplicativo disponível para download, gostaria de baixar a nova versão do aplicativo? a sua versão de aplicativo instalada atualmente é {versaoappcurrent} e a nova versão do aplicativo para baixar é {versaonovadoapp} sendo a nova versão com correções de problemas e outras correções de interface.", "Pergunta", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string diretorioexecucaoap = AppDomain.CurrentDomain.BaseDirectory.ToString();

                    if (File.Exists($"{diretorioexecucaoap}ATUpdate.exe"))
                    {
                        Process.Start($"{diretorioexecucaoap}ATUpdate.exe", "-u");
                    }
                    else
                        System.Windows.Forms.MessageBox.Show("O arquivo de atualização do aplicativo não foi encontrado! para que o aplicativo atualize, verifique se no diretório de instalação o ATUpdate existe!", "Informação", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("O Aplicativo permanecerá desatualizado!\nPara evitar problemas de execução, ter mais novidades de atualização etc desse aplicativo, clique em 'Verificar por atualizações' mais tarde se preferir!", "Informação", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A Versão 0.8 RC Final está ATUALIZADA!");
            }
        }
    }
}