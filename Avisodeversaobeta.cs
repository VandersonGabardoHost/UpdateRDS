using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// Update RDS By GabardoHost - Versão 0.5 Beta build
/// @file Avisodeversaobeta.cs
/// <summary>
/// Este arquivo é o código de inicialização principal do aplicativo
/// </summary>
/// Fiz esse programa de computador para o objetivo de usar apenas na Rádio CBS - Comunicações Brasileira de Sistemas - A Rádio dos profissionais de Tecnologia da informação!
/// Mas resolvi criar um para disponibilizar para todos, pois esse programa ou vem associado a um encoder ou não tem para download, então fiz um!
/// Minha ideia é essa, se uma coisa não existe e você precisa muito, então crie você mesmo! pode ser carro, casa, transmissor de FM, programa de PC, celular etc... CRIE VOCÊ MESMO!!!
/// @author Vanderson Gabardo <vanderson@vanderson.net.br>
/// @date 03/09/2019
/// $Id: UpdateRDS.cs, v0.5 2019/09/03 23:30:00 Vanderson Gabardo $

namespace UpdateRDS
{
    public partial class Avisodeversaobeta : Form
    {
        static readonly UpdateRDSManutencao manutencaodoaplicativo = new UpdateRDSManutencao();

        public Avisodeversaobeta()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }
    }
}
