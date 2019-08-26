using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace UpdateRDS
{
    static class Program
    {
        static readonly UpdateRDSManutencao manutencaodoaplicativo = new UpdateRDSManutencao();
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Avisodeversaobeta());
                Application.Run(new UpdateRDS());
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }
    }

    public class UpdateRDSManutencao
    {
        public void ErroGenerico(string mensagemdeerro, string stacktracecompleto, string origemdoerro)
        {
            try
            {
                string origemdoerrodeaplicativo = "Update RDS By GabardoHost";

                if (!EventLog.SourceExists(origemdoerrodeaplicativo))
                {
                    EventLog.CreateEventSource(origemdoerrodeaplicativo, "Application");
                }

                EventLog log = new EventLog();
                log.Source = origemdoerrodeaplicativo;
                log.WriteEntry($"Ocorreu um erro interno do aplicativo {origemdoerrodeaplicativo} com a seguinte mensagem de erro: {mensagemdeerro}\nStackTrace de erro completo: {stacktracecompleto}\nOrigem do erro no aplicativo: {origemdoerro}\nEsse erro é interno do aplicativo, não causando problemas na execução do aplicativo, caso o problema venha a comprometer a execução do aplicativo, entrar em contato com o desenvolvimento deste.", EventLogEntryType.Error, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro interno do aplicativo, o aplicativo encontrou o seguinte problema:\n{ex.Message}\n{ex.StackTrace}\n{ex.Source}\nEntre em contato com o desenvolvedor do aplicativo com o seguinte código de erro: 01 - EX GEN FONTE", "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
