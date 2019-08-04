using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UpdateRDS
{
    public partial class UpdateRDSInfo : Form
    {
        string caminhoarquivo;
        bool updateviaurl = false;
        static readonly string diretoriodoaplicativo = $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Update RDS\";

        public UpdateRDSInfo()
        {
            InitializeComponent();

            /// Verifica o processo do aplicativo
            Process proc = Process.GetCurrentProcess();

            /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
            string identificadorproc = proc.Id.ToString();

            /// Informa o ID do processo em execução para o usuário
            lblInfoid.Text = "ID do processo em execução: " + identificadorproc;
        }

        public void ArquivoTextoSom(string diretorio, bool eumaurl)
        {
            caminhoarquivo = diretorio;
            updateviaurl = eumaurl;
        }

        public void CarregaInfo(string informacao)
        {
            lblInfo.Text = informacao;
        }

        public void InfoEmiNome(string nomedaemissora)
        {
            lblTituloemissora.Text = $"Nome da rádio: {nomedaemissora}";
        }

        private void BtnEnviartitulosom_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateviaurl == true)
                {
                    throw new Exception("Não é possível enviar título de som manualmente!\nOs títulos de som estão sendo enviados via URL, não é possível atualizar título de som se está sendo atualizado via URL!");
                }
                if (File.Exists(caminhoarquivo))
                {
                    /// Declara os dados do arquivo texto para comparação
                    string dadosdoarquivotexto;

                    using (StreamReader srManual = new StreamReader(caminhoarquivo, Encoding.Default))
                    {
                        /// Carrega arquivo de texto antigo e sobrescreve todos os dados que tem no arquivo
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

                    /// Apaga o conteúdo do arquivo texto para escrever em texto limpo abaixo
                    File.WriteAllText(caminhoarquivo, string.Empty);

                    /// Pega o arquivo antigo para escrever com dados novos
                    File.WriteAllText(caminhoarquivo, txtTitulodesom.Text.Replace("&", "e"));
                }
                else
                    throw new Exception("Ainda não é possível enviar título de som manualmente!\nExperimente iniciar a transmissão de dados de RDS primeiro!");
            }
            catch (Exception ex)
            {
                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRDSInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                Hide();
            }
            catch (Exception ex)
            {
                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnApagarlogerro_Click(object sender, EventArgs e)
        {
            try
            {
                bool arquivosdeletados = false;
                if (Directory.Exists($"{diretoriodoaplicativo}LOGS"))
                {
                    DirectoryInfo dir = new DirectoryInfo(diretoriodoaplicativo + @"LOGS\");
                    FileInfo[] arquivostexto = dir.GetFiles();
                    foreach (FileInfo file in arquivostexto)
                    {
                        string indexnome = $"ERRO {DateTime.Now.Date.ToString().Replace("00:00:00", "").Replace("/", "")}";
                        if (file.Name.IndexOf(indexnome) > -1)
                        {
                            file.Delete();
                            arquivosdeletados = true;
                        }
                    }
                    if (arquivosdeletados == true)
                    {
                        /// Exibe mensagem que os arquivos foram apagados
                        MessageBox.Show("Os arquivos de erro foram apagados com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        /// Exibe mensagem que os arquivos foram apagados
                        MessageBox.Show("Não existem arquivos de erro para apagar!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
