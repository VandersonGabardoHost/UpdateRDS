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
        string nomedaemissorards;
        /// Verifica o processo do aplicativo
        readonly Process proc = Process.GetCurrentProcess();
        public UpdateRDSInfo()
        {
            InitializeComponent();

            /// Passa o ID de execução por parâmetro para adicionar no nome do arquivo texto abaixo
            string identificadorproc = proc.Id.ToString();

            lblInfoid.Text = "ID do processo em execução: " + identificadorproc;
        }

        public void AarquivoTextoSom(string diretorio)
        {
            caminhoarquivo = diretorio;
        }

        public void CarregaInfo(string informacao)
        {
            lblInfo.Text = informacao;
        }

        private void BtnEnviartitulosom_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(caminhoarquivo))
                {
                    if (string.IsNullOrEmpty(txtTitulodesom.Text))
                    {
                        throw new Exception("O título de som informado não pode ser vazio! Será necessário preencher a caixa de texto antes de enviar os dados!");
                    }
                    if (txtTitulodesom.Text.Length > 2000)
                    {
                        throw new Exception("O título de som informado ultrapassa os 2000 caracteres! Será necessário apagar alguns caracteres do texto antes de enviar os dados!");
                    }
                    /// Apaga o conteúdo do arquivo texto para escrever em texto limpo abaixo
                    File.WriteAllText(caminhoarquivo, string.Empty);

                    /// Cria ou abre o arquivo existente para leitura
                    FileStream fsManual = new FileStream(caminhoarquivo, FileMode.OpenOrCreate);

                    /// Pega o arquivo antigo para escrever com dados novos
                    using (StreamWriter swManual = new StreamWriter(fsManual, Encoding.Default))
                    {
                        /// Carrega arquivo de texto antigo e sobrescreve todos os dados que tem no arquivo
                        swManual.WriteLine(txtTitulodesom.Text.Replace("&", "e"));

                        /// Força o fechamento do arquivo
                        swManual.Flush();
                        swManual.Close();

                        /// Força o despejo da memória do arquivo carregado
                        swManual.Dispose();
                        fsManual.Close();
                        fsManual.Dispose();
                    }
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

        private void BtnPersonalizanomeradio_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNomeemissora.Text))
                {
                    throw new Exception("A caixa de texto de nome da emissora está vazio, informe um nome válido!");
                }
                nomedaemissorards = $"Nome da rádio: {txtNomeemissora.Text}";
                lblTituloemissora.Text = nomedaemissorards;
            }
            catch (Exception ex)
            {
                /// Exibe exceção bruta de sistema caso não tenha mensagem personalizada
                MessageBox.Show(ex.Message, "Aviso do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
