using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UpdateRDS
{
    static class Program
    {
        static readonly UpdateRDSManutencao manutencaodoaplicativo = new UpdateRDSManutencao();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "Update RDS - Modo Texto";
            try
            {
                bool modotexto = false;

                string[] comandosdados = Environment.GetCommandLineArgs();

                foreach (string comando in comandosdados)
                {
                    if (!comando.Contains("Update RDS.exe"))
                    {
                        if (comando.Contains("-T"))
                        {
                            modotexto = true;
                        }
                    }
                }

                if (!File.Exists("Update RDS.exe"))
                {
                    throw new Exception("O aplicativo principal está com um nome diferente do padrão, renomeie o aplicativo para Update RDS.exe e execute novamente!");
                }

                if (!File.Exists("ATUpdate.exe"))
                {
                    throw new Exception("Um componente do aplicativo está faltando, o ATUpdate.exe e sem ele, o aplicativo encerrará a execução. Reinstale o aplicativo e tente executar novamente!");
                }

                if (!File.Exists("wget.exe"))
                {
                    throw new Exception("Um componente do aplicativo está faltando, o wget.exe e sem ele, o aplicativo encerrará a execução. Reinstale o aplicativo e tente executar novamente!");
                }

                if (modotexto == false)
                {
                    IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                    ShowWindow(h, 0);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new UpdateRDS());
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Bem vindo! Estamos carregando... aguarde!");
                    Console.WriteLine();
                    UpdateRDSTextMode execucaotexto = new UpdateRDSTextMode();
                    execucaotexto.InicTxtMode(true);
                    Console.WriteLine("O Aplicativo está em execução! Para encerrar a execução do aplicativo pressione ENTER");
                    Console.ReadLine();
                    execucaotexto.EncerrarExecucao();
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
                MessageBox.Show($"Ocorreu um erro irrecuperável do aplicativo, o aplicativo encontrou o seguinte problema:\n{ex.Message}", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class UpdateRDSManutencao
    {
        public void ErroGenerico(string mensagemdeerro, string stacktracecompleto, string origemdoerro)
        {
            try
            {
                Process processodoaplicativo = Process.GetCurrentProcess();

                string origemdoerrodeaplicativo = "Update RDS By GabardoHost";

                if (!EventLog.SourceExists(origemdoerrodeaplicativo))
                {
                    EventLog.CreateEventSource(origemdoerrodeaplicativo, "Application");
                }

                using (EventLog log = new EventLog())
                {
                    log.Source = origemdoerrodeaplicativo;
                    log.WriteEntry($"Ocorreu um erro interno do aplicativo {origemdoerrodeaplicativo} com a seguinte mensagem de erro: {mensagemdeerro}\nStackTrace de erro completo: {stacktracecompleto}\nOrigem do erro no aplicativo: {origemdoerro}\nID do processo em execução: {processodoaplicativo}\nEsse erro é interno do aplicativo, não causando problemas na execução do aplicativo, caso o problema venha a comprometer a execução do aplicativo, entrar em contato com o desenvolvimento deste.", EventLogEntryType.Error, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro interno do aplicativo, o aplicativo encontrou o seguinte problema:\n{ex.Message}\n{ex.StackTrace}\n{ex.Source}\nEntre em contato com o desenvolvedor do aplicativo com o seguinte código de erro: 01 - EX GEN FONTE", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}