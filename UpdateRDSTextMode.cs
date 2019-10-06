using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace UpdateRDS
{
    public partial class UpdateRDSTextMode
    {
        public void InicTxtMode(bool execucaoemtextmode)
        {
            try
            {
                if (execucaoemtextmode == true)
                {
                    UpdateAppRDS(null);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Carregando configurações...");
                    Console.WriteLine();
                    VerifArqConfig(null);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Configurações carregadas com sucesso! Validando...");
                    Console.WriteLine();
                    ValidarInformacoes();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Informações validadas com sucesso! Enviando dados...");
                    Console.WriteLine();
                    EnviarDadosRds(null, null);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Dados de RDS enviados com sucesso! ID de execução do aplicativo: " + processodoaplicativo.Id.ToString());
                    Console.WriteLine();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                ErrGerApl(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void ErrGerApl(string errogeral, string errogeraltrace, string origemerro)
        {
            try
            {
                string datadeagora = DateTime.Now.ToString().Replace(":", "").Replace("/", "");
                string identificadorproc = processodoaplicativo.Id.ToString();
                string caminhoarquivologapp = $@"{diretoriodoaplicativo}LOGS\ERRO {datadeagora} LOG.html";

                if (!Directory.Exists($@"{diretoriodoaplicativo}LOGS"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}LOGS");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{errogeral}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (arquivoerroconta < 1 && arquivoerrocontanext < 1 && erroconta < 1 && errocontanext < 1)
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

                    if (arquivoerroconta < -1)
                    {
                        arquivoerroconta = 250;
                        errfilec = errogeral;
                    }

                    if (arquivoerrocontanext < -1)
                    {
                        arquivoerrocontanext = 250;
                        errfilecnext = errogeral;
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
                        xtwerr.WriteElementString("p", $"Origem do erro: {origemerro}");
                        xtwerr.WriteElementString("p", $"Nome da emissora: {txtNomeemi}");
                        xtwerr.WriteElementString("p", $"Codificação de caracteres: {cbCaracteres}. Item selecionado: {cbCaracteres}");
                        xtwerr.WriteElementString("p", $"Tipo de servidor: {cbTiposervidor}. Item selecionado: {cbTiposervidor}");
                        VerTrueFalse(chkNaominimsystray);
                        xtwerr.WriteElementString("p", "Não notificar no system tray: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkAcentospalavras);
                        xtwerr.WriteElementString("p", "Remover acentos das palavras: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkCaracteresespeciais);
                        xtwerr.WriteElementString("p", "Remover caracteres especiais: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkDadossensiveis);
                        xtwerr.WriteElementString("p", "Exibir dados sensíveis como senhas: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkTransmproxsom);
                        xtwerr.WriteElementString("p", "Transmitir próximo som: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkUsoproxy);
                        xtwerr.WriteElementString("p", "Uso um servidor proxy para acesso a internet: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        VerTrueFalse(chkAutenticaproxy);
                        xtwerr.WriteElementString("p", "Uso autenticação para o servidor proxy: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        xtwerr.WriteElementString("p", $"Domínio ou endereço de IP do servidor proxy: {txtDoproxy}");
                        xtwerr.WriteElementString("p", $"Porta do servidor proxy: {txtPortaproxy}");
                        xtwerr.WriteElementString("p", $"Login do servidor proxy: {txtLoginproxy}");
                        xtwerr.WriteElementString("p", $"Senha do servidor proxy: {txtSenhaproxy}");
                        xtwerr.WriteElementString("p", $"Tempo de execução para verificação de arquivo ou URL: {txtTempoexec}");
                        xtwerr.WriteElementString("p", $"Caminho do arquivo de texto do som: {lblArquivotextosom}");
                        xtwerr.WriteElementString("p", $"Caminho do arquivo de texto do próximo som: {lblArquivotextosomnext}");
                        VerTrueFalse(chkUrlsom);
                        xtwerr.WriteElementString("p", "Atualizar título de som através de uma URL: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        xtwerr.WriteElementString("p", $"URL Informada para captura da informação do nome de som: {txtUrlsom}");
                        VerTrueFalse(chkUrlsomnext);
                        xtwerr.WriteElementString("p", "Atualizar título do próximo som através de uma URL: ");
                        xtwerr.WriteElementString($"input type='checkbox' {htmltestado}", "");
                        xtwerr.WriteElementString("p", $"URL Informada para captura da informação do nome do próximo som: {txtUrlsomnext}");
                        xtwerr.WriteElementString("p", $"Domínio ou endereço de IP informado para o servidor: {txtDominioip}");
                        xtwerr.WriteElementString("p", $"Porta informada para o servidor: {txtPorta}");
                        xtwerr.WriteElementString("p", $"Ponto de montagem ou ID informado para o servidor: {txtIdoumont}");
                        xtwerr.WriteElementString("p", $"Login do servidor: {txtLoginserver}");
                        xtwerr.WriteElementString("p", $"Senha do servidor: {txtSenhaserver}");
                        xtwerr.WriteElementString("p", $"ID do processo em execução: {identificadorproc}");
                        xtwerr.WriteEndElement();
                        xtwerr.WriteEndElement();
                    }
                }
            }
            catch (Exception ex)
            {
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
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
                manutencaodoaplicativo.ErroGenerico(ex.Message, ex.StackTrace, ex.Source);
            }
        }

        private void VerifArqConfig(string diretoriodoxml)
        {
            string[] comandosdados = Environment.GetCommandLineArgs();

            foreach (string comando in comandosdados)
            {
                if (!comando.Contains("Update RDS.exe"))
                {
                    if (comando.Contains("-o"))
                    {
                        throw new Exception("ATENÇÃO! Remova do parâmetro de inicialização do aplicativo a opção -o para carregar!");
                    }
                    if (comando.Contains("-O"))
                    {
                        throw new Exception("ATENÇÃO! Remova do parâmetro de inicialização do aplicativo a opção -O para carregar!");
                    }
                    if (!comando.Contains("-T"))
                    {
                        diretoriodoxml = comando.ToString();
                    }
                }
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
            else
                throw new Exception("Aviso! O arquivo XML configurado na inicialização desse aplicativo não foi carregado! Verifique se o arquivo foi configurado!");
        }

        private void CarregaXml(string diretoriodoarquivoxml)
        {
            string versaoparacomparacao = "Ver-XML-1.0";
            string versaoxmlcarregado;

            int cbNotificacoes;

            using (FileStream arquivoxmlpracarregar = new FileStream(diretoriodoarquivoxml, FileMode.Open))
            {
                XmlDocument oXML = new XmlDocument();
                oXML.Load(arquivoxmlpracarregar);
                XmlElement root = oXML.DocumentElement;
                XmlNodeList lst = root.GetElementsByTagName("SOFTWARE-UPDATE-RDS-XML-VERSION");

                if (lst.Count == 0)
                    throw new Exception("Aviso! O arquivo XML carregado é inválido! Preencha novamente os dados da tela e salve um XML novo, ou procure o arquivo XML salvo pelo aplicativo!");

                versaoxmlcarregado = oXML.SelectSingleNode("Configuracao").ChildNodes[0].InnerText;

                if (versaoparacomparacao != versaoxmlcarregado)
                {
                    throw new Exception("Aviso! A versão do XML carregada é incompatível com a versão desse software! Preencha novamente os dados da tela para esta versão e salve um XML novo!");
                }

                cbCaracteres = int.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[1].InnerText);
                cbTiposervidor = int.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[2].InnerText);
                chkEnviatitulosom = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[3].InnerText);
                chkNaominimsystray = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[4].InnerText);
                cbNotificacoes = int.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[5].InnerText);
                chkAcentospalavras = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[6].InnerText);
                chkCaracteresespeciais = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[7].InnerText);
                chkDadossensiveis = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[8].InnerText);
                chkTransmproxsom = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[9].InnerText);
                chkUsoproxy = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[10].InnerText);
                chkAutenticaproxy = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[11].InnerText);
                txtDoproxy = oXML.SelectSingleNode("Configuracao").ChildNodes[12].InnerText;
                txtPortaproxy = oXML.SelectSingleNode("Configuracao").ChildNodes[13].InnerText;
                txtLoginproxy = oXML.SelectSingleNode("Configuracao").ChildNodes[14].InnerText;
                txtSenhaproxy = oXML.SelectSingleNode("Configuracao").ChildNodes[15].InnerText;
                txtTempoexec = oXML.SelectSingleNode("Configuracao").ChildNodes[16].InnerText;
                lblArquivotextosom = oXML.SelectSingleNode("Configuracao").ChildNodes[17].InnerText;
                chkUrlsom = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[18].InnerText);
                txtUrlsom = oXML.SelectSingleNode("Configuracao").ChildNodes[19].InnerText;
                lblArquivotextosomnext = oXML.SelectSingleNode("Configuracao").ChildNodes[20].InnerText;
                chkUrlsomnext = Boolean.Parse(oXML.SelectSingleNode("Configuracao").ChildNodes[21].InnerText);
                txtUrlsomnext = oXML.SelectSingleNode("Configuracao").ChildNodes[22].InnerText;
                txtDominioip = oXML.SelectSingleNode("Configuracao").ChildNodes[23].InnerText;
                txtPorta = oXML.SelectSingleNode("Configuracao").ChildNodes[24].InnerText;
                txtIdoumont = oXML.SelectSingleNode("Configuracao").ChildNodes[25].InnerText;
                txtLoginserver = oXML.SelectSingleNode("Configuracao").ChildNodes[26].InnerText;
                txtSenhaserver = oXML.SelectSingleNode("Configuracao").ChildNodes[27].InnerText;
                txtNomeemi = oXML.SelectSingleNode("Configuracao").ChildNodes[28].InnerText;

                if (!string.IsNullOrEmpty(txtNomeemi))
                {
                    Console.Title = "Update RDS - Modo Texto - " + txtNomeemi;
                }
            }
        }

        private void ValidarInformacoes()
        {
            string ipserver = txtDominioip;
            string portaserver = txtPorta;
            string idoupontomont = txtIdoumont;

            string caminhoarquivo = $@"{lblArquivotextosom}";
            string caminhoarquivonext = $@"{lblArquivotextosomnext}";

            if (string.IsNullOrEmpty(caminhoarquivo) && chkUrlsom == false)
            {
                throw new Exception("Selecione o arquivo no Caminho do arquivo texto gerado pelo automatizador com o nome do áudio!");
            }

            if (txtTempoexec == "0" || !Regex.IsMatch(txtTempoexec, @"^[0-9]+$"))
            {
                throw new Exception("Preencha a caixa de tempo de verificação de arquivo APENAS COM NÚMEROS para verificar uma atualização de arquivo! NÃO PODE SER VAZIO OU ZERO!");
            }

            if (string.IsNullOrEmpty(ipserver))
            {
                throw new Exception("Preencha a caixa de texto endereço de IP ou nome de domínio!");
            }

            if (string.IsNullOrEmpty(portaserver))
            {
                throw new Exception("Preencha a caixa de texto porta!");
            }

            if (!Regex.IsMatch(portaserver, @"^[0-9]+$"))
                throw new Exception("Preencha a caixa de texto porta apenas com números!");

            if (Convert.ToInt32(portaserver) > 65535)
            {
                throw new Exception("Preencha a caixa de texto porta apenas com números inferiores a 65535!");
            }

            if (Convert.ToInt32(portaserver) < 1)
            {
                throw new Exception("Preencha a caixa de texto porta apenas com números superiores a 1!");
            }

            if (string.IsNullOrEmpty(txtLoginserver))
            {
                throw new Exception("Preencha a caixa de texto login!");
            }

            if (string.IsNullOrEmpty(txtSenhaserver))
            {
                throw new Exception("Preencha a caixa de texto senha!");
            }

            if (cbTiposervidor == 2 & string.IsNullOrEmpty(idoupontomont))
            {
                throw new Exception("Preencha a caixa de texto ID com números para Shoutcast Server ou ponto de montagem para Icecast Server!");
            }

            if (cbTiposervidor == 1 & !Regex.IsMatch(idoupontomont, @"^[0-9]+$"))
            {
                throw new Exception("Preencha a caixa de texto ID ou ponto de montagem apenas com números para Shoutcast V2!");
            }

            if (chkUrlsom == true && string.IsNullOrEmpty(txtUrlsom))
            {
                throw new Exception("Preencha a caixa de texto URL com o link que leva ao arquivo texto ou a URL do currentsong do servidor shoutcast!");
            }

            if (chkUrlsom == true && !Uri.IsWellFormedUriString(txtUrlsom, UriKind.Absolute))
            {
                throw new Exception("Preencha a caixa de texto URL com o link VÁLIDO http://link/arquivotexto.txt que leva ao arquivo texto ou a URL do currentsong do servidor shoutcast!");
            }

            if (chkTransmproxsom == true && cbTiposervidor == 1)
            {
                if (chkUrlsomnext == true && string.IsNullOrEmpty(txtUrlsomnext))
                {
                    throw new Exception("Preencha a caixa de texto URL com o link que leva ao arquivo texto de próximo som ou a URL do próximo som do servidor shoutcast!");
                }

                if (chkUrlsomnext == true && !Uri.IsWellFormedUriString(txtUrlsomnext, UriKind.Absolute))
                {
                    throw new Exception("Preencha a caixa de texto URL com um link VÁLIDO http://link/arquivotexto.txt que leva ao arquivo texto de próximo som ou a URL do próximo som do servidor shoutcast!");
                }

                if (string.IsNullOrEmpty(caminhoarquivonext) && chkUrlsomnext == false)
                {
                    throw new Exception("Selecione o arquivo de Caminho do arquivo texto de próximo audio gerado pelo automatizador com o nome do áudio!");
                }

                if (!File.Exists(caminhoarquivonext) && chkUrlsomnext == false)
                    throw new Exception("O Caminho selecionado para o arquivo de texto com o nome do próximo áudio está incorreto! verifique se o arquivo realmente existe!");
            }

            if (!File.Exists(caminhoarquivo) && chkUrlsom == false)
                throw new Exception("O Caminho selecionado para o arquivo de texto com o nome do áudio está incorreto! verifique se o arquivo realmente existe!");

            if (chkUrlsom == false && chkUrlsomnext == false && caminhoarquivo == caminhoarquivonext)
            {
                throw new Exception("O Caminho selecionado para o arquivo de texto com o nome do áudio é o mesmo do arquivo texto de proximo som! Você não pode colocar o mesmo arquivo, precisa ser necessariamente dois arquivos diferentes!");
            }

            if (chkUrlsom == true && chkUrlsomnext == true && txtUrlsom == txtUrlsomnext)
                throw new Exception("A URL do próximo som é a mesma URL do som atual, as duas URLs não podem ser as mesmas! use URLs com textos diferentes para cadastrar no sistema!");

            if (chkUsoproxy == true)
            {
                if (string.IsNullOrEmpty(txtDoproxy))
                {
                    throw new Exception("O endereço de IP ou domínio do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                }

                if (string.IsNullOrEmpty(txtPortaproxy))
                {
                    throw new Exception("A porta do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                }

                if (!Regex.IsMatch(txtPortaproxy, @"^[0-9]+$"))
                    throw new Exception("Preencha a caixa de texto porta do servidor proxy apenas com números!");

                if (Convert.ToInt32(txtPortaproxy) > 65535)
                {
                    throw new Exception("Preencha a caixa de texto porta do servidor proxy apenas com números inferiores a 65535!");
                }

                if (Convert.ToInt32(txtPortaproxy) < 1)
                {
                    throw new Exception("Preencha a caixa de texto porta do servidor proxy apenas com números superiores a 1!");
                }

                if (chkAutenticaproxy == true)
                {
                    if (string.IsNullOrEmpty(txtLoginproxy))
                    {
                        throw new Exception("O login do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                    }

                    if (string.IsNullOrEmpty(txtSenhaproxy))
                    {
                        throw new Exception("A senha do servidor proxy não pode ser em branco, preencha os dados corretamente para continuar!");
                    }
                }
            }
        }

        private void TratamentoURLNowNext(bool eumurlnext)
        {
            string identificadorproc = processodoaplicativo.Id.ToString();
            string arquivotextoantigo = $@"{diretoriodoaplicativo}{identificadorproc}OLD.txt";
            string urlcompleta = txtUrlsom;
            string dadoscapturadosdaurl = null;
            int contanext = errocontanext;
            int conta = erroconta;

            if (eumurlnext == true)
            {
                urlcompleta = txtUrlsomnext;
                arquivotextoantigo = $@"{diretoriodoaplicativo}{identificadorproc}NEXTOLD.txt";
            }

            if (errocontanext > 0)
            {
                errocontanext = contanext - 1;
                throw new Exception(errfilecnext + " Nova tentativa de verificar a URL: " + errocontanext);
            }

            if (erroconta > 0)
            {
                erroconta = conta - 1;
                throw new Exception(errfilec + " Nova tentativa de verificar a URL: " + erroconta);
            }

            Stream strurlcompleta = null;
            StreamReader rdrurlcompleta;

            try
            {
                using (WebClient wcurlcompleta = new WebClient())
                {
                    wcurlcompleta.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);

                    if (chkUsoproxy == true)
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

                    if (chkUsoproxy == true)
                    {
                        erroconexaowebexc1 = $"A URL do próximo som informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta, se o servidor proxy está funcionando e se o servidor está funcionando!";
                    }
                    errocontanext = -2;
                    throw new Exception(erroconexaowebexc1);
                }
                else
                {
                    string erroconexaowebexc2 = $"A URL informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta e se o servidor está funcionando!";

                    if (chkUsoproxy == true)
                    {
                        erroconexaowebexc2 = $"A URL informada anteriormente está com problemas! \n{excecaointerna.Message}\nPor favor, verifique se a URL está correta, se o servidor proxy está funcionando e se o servidor está funcionando!";
                    }
                    erroconta = -2;
                    throw new Exception(erroconexaowebexc2);
                }
            }

            if (cbCaracteres == 0)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres == 1)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.Default))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres == 2)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.UTF8))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres == 3)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.UTF7))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (cbCaracteres == 4)
            {
                using (rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.ASCII))
                {
                    dadoscapturadosdaurl = rdrurlcompleta.ReadLine();
                }
            }

            if (erroconta == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nNome do som conectado no servidor! Aguarde atualização de título...");
                erroconta = -1;
            }

            if (errocontanext == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nNome do próximo som conectado no servidor! Aguarde atualização de título...");
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

            if (conteudotexto != conteudotextoantigo)
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
                    manutencaodoaplicativo.ErroGenerico(errfile.Message, errfile.StackTrace, errfile.Source);

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
            string identificadorproc = processodoaplicativo.Id.ToString();
            string arquivotexto = $@"{lblArquivotextosom}";
            string arquivotextoantigo = $@"{lblArquivotextosom}{identificadorproc}.txt";
            int conta = arquivoerroconta;
            int contanext = arquivoerrocontanext;

            if (eumarquivonext == true)
            {
                arquivotexto = $@"{lblArquivotextosomnext}";
                arquivotextoantigo = $@"{lblArquivotextosomnext}{identificadorproc}.txt";
            }

            if (arquivoerrocontanext > 0)
            {
                arquivoerrocontanext = contanext - 1;
                throw new Exception(errfilecnext + " Nova tentativa de verificar o arquivo: " + arquivoerrocontanext);
            }

            if (arquivoerroconta > 0)
            {
                arquivoerroconta = conta - 1;
                throw new Exception(errfilec + " Nova tentativa de verificar o arquivo: " + arquivoerroconta);
            }

            if (arquivoerroconta == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nArquivo de nome do som corrigido com sucesso! Aguarde atualização de título...");
                arquivoerroconta = -1;
            }

            if (arquivoerrocontanext == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nArquivo de nome do próximo som corrigido com sucesso! Aguarde atualização de título...");
                arquivoerrocontanext = -1;
            }

            if (!File.Exists(arquivotexto))
            {
                if (eumarquivonext == true)
                {
                    arquivoerrocontanext = -4;

                    throw new Exception("O Caminho informado anteriormente para o arquivo de texto de próximo som está com problemas! \nVerificar se o arquivo ainda existe!");
                }

                arquivoerroconta = -4;

                throw new Exception("O Caminho informado anteriormente para o arquivo de texto está com problemas! \nVerificar se o arquivo ainda existe!");
            }

            try
            {
                try
                {
                    if (cbCaracteres == 0)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 1)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.Default))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 2)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF8))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 3)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF7))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 4)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.ASCII))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }
                }
                catch (Exception errofile)
                {
                    manutencaodoaplicativo.ErroGenerico(errofile.Message, errofile.StackTrace, errofile.Source);

                    System.Threading.Thread.Sleep(1500);

                    if (cbCaracteres == 0)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 1)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.Default))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 2)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF8))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 3)
                    {
                        using (StreamReader sr = new StreamReader(arquivotexto, Encoding.UTF7))
                        {
                            conteudotexto = sr.ReadLine().ToString();
                        }
                    }

                    if (cbCaracteres == 4)
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
                        arquivoerrocontanext = -4;

                        throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está em uso por outro aplicativo ou processo do sistema!");
                    }
                    else
                    {
                        arquivoerroconta = -4;

                        throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está em uso por outro aplicativo ou processo do sistema!");
                    }
                }

                FileInfo arquivotextomusica = new FileInfo(arquivotexto);

                if (arquivotextomusica.Length == 0)
                {
                    if (eumarquivonext == true)
                    {
                        arquivoerrocontanext = -4;

                        throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio!");
                    }
                    else
                    {
                        arquivoerroconta = -4;

                        throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio!");
                    }
                }
            }

            if (!File.Exists(arquivotextoantigo))
                File.Copy(arquivotexto, arquivotextoantigo);

            if (cbCaracteres == 0)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres == 1)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.Default))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres == 2)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.UTF8))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres == 3)
            {
                using (StreamReader srOld = new StreamReader(arquivotextoantigo, Encoding.UTF7))
                {
                    conteudotextoantigo = srOld.ReadLine().ToString();
                }
            }

            if (cbCaracteres == 4)
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
                    arquivoerrocontanext = -4;

                    throw new Exception("O arquivo texto de próximo som contém mais de 2000 caracteres \nO servidor não é capaz de receber essa quantidade de caracteres! \nTente apagar algumas palavras do arquivo!");
                }
                else
                {
                    arquivoerroconta = -4;

                    throw new Exception("O arquivo texto de som contém mais de 2000 caracteres \nO servidor não é capaz de receber essa quantidade de caracteres! \nTente apagar algumas palavras do arquivo!");
                }
            }

            if (conteudotexto.Length < 1)
            {
                if (eumarquivonext == true)
                {
                    arquivoerrocontanext = -4;

                    throw new Exception("O arquivo texto de próximo som informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio ou falta a primeira linha!");
                }
                else
                {
                    arquivoerroconta = -4;

                    throw new Exception("O arquivo texto informado anteriormente está com problemas! \nVerificar se o arquivo texto não está vazio ou falta a primeira linha!");
                }
            }

            if (conteudotexto != conteudotextoantigo)
            {
                try
                {
                    File.WriteAllText(arquivotextoantigo, string.Empty);

                    if (cbCaracteres == 0)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto);
                    }

                    if (cbCaracteres == 1)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.Default);
                    }

                    if (cbCaracteres == 2)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF8);
                    }

                    if (cbCaracteres == 3)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF7);
                    }

                    if (cbCaracteres == 4)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.ASCII);
                    }
                }
                catch (IOException errfile)
                {
                    manutencaodoaplicativo.ErroGenerico(errfile.Message, errfile.StackTrace, errfile.Source);

                    System.Threading.Thread.Sleep(1000);

                    File.WriteAllText(arquivotextoantigo, string.Empty);

                    if (cbCaracteres == 0)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto);
                    }

                    if (cbCaracteres == 1)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.Default);
                    }

                    if (cbCaracteres == 2)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF8);
                    }

                    if (cbCaracteres == 3)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.UTF7);
                    }

                    if (cbCaracteres == 4)
                    {
                        File.WriteAllText(arquivotextoantigo, conteudotexto, Encoding.ASCII);
                    }
                }
            }
        }

        private void DadosProxy()
        {
            string enderecoservidorproxy = $"http://{txtDoproxy}:{txtPortaproxy}";

            Uri uridoproxyserver = new Uri(enderecoservidorproxy);

            servidorproxydoaplicativo.Address = uridoproxyserver;

            // Não bypassa dados para proxy local
            // servidorproxydoaplicativo.BypassProxyOnLocal = true;

            if (chkAutenticaproxy == true)
                servidorproxydoaplicativo.Credentials = new NetworkCredential(txtLoginproxy, txtSenhaproxy);
        }

        private void ErroWebConServ(string weberrogeral, string weberrogeralcode)
        {
            string weberroexplic = null;

            string caracteresaanalisar = @"(?i)[^0-9]";

            Regex rgx = new Regex(caracteresaanalisar);

            string coderro = rgx.Replace(weberrogeral, "");

            if (cbTiposervidor == 0 && weberrogeral == "Impossível conectar-se ao servidor remoto")
            {
                weberroexplic = $"Este erro indica que o servidor não está no ar. \nVerifique se o servidor http://{txtDominioip}:{txtPorta}/ está funcionando!";
            }

            if (coderro == "400")
                weberroexplic = "Este erro indica que o encoder que transmite a rádio pode não estar no ar \nOu o ponto de montagem informado não está correto!";

            if (coderro == "401")
                weberroexplic = "Este erro indica que você errou a senha ou o ID \nOu o ponto de montagem do servidor não aceita o login e senha informados!";

            if (coderro == "403")
            {
                weberroexplic = "Este erro indica que o servidor proibiu o acesso aos dados \nOu o ponto de montagem do servidor não aceita o acesso!";
                if (chkUsoproxy == true)
                {
                    weberroexplic = $"Este erro indica que o servidor proxy {txtDoproxy}:{txtPortaproxy} proibiu o acesso! Será necessário solicitar desbloqueio para o endereço http://{txtDominioip}:{txtPorta}/ para que os dados sejam enviados!";
                }
            }

            if (coderro == "503")
            {
                weberroexplic = "Este erro indica que o servidor que você está se conectando está com problemas! \nOu o erro pode ser um retorno de erro do servidor proxy, checar se o servidor proxy está funcionando corretamente!";
            }

            if (weberrogeralcode == "ConnectFailure")
            {
                if (chkUsoproxy == true)
                {
                    weberroexplic = $"Este erro indica que o servidor ou o servidor proxy não está no ar. \nVerifique se o servidor http://{txtDominioip}:{txtPorta}/ está funcionando e se o proxy {txtDoproxy}:{txtPortaproxy} está funcionando!";
                }
                else
                    weberroexplic = $"Este erro indica que o servidor não está no ar. \nVerifique se o servidor http://{txtDominioip}:{txtPorta}/ está funcionando!";
            }

            if (weberrogeralcode == "NameResolutionFailure")
                weberroexplic = $"Verifique se não há erros de digitação do domínio informado!";


            if (weberrogeralcode == "ProxyNameResolutionFailure")
                weberroexplic = $"Verifique se não há erros de digitação na caixa de texto de domínio do servidor proxy informado!";

            if (coderro == "407")
            {
                if (chkAutenticaproxy == true)
                {
                    weberroexplic = $"Verifique se o servidor proxy: {txtDoproxy}:{txtPortaproxy}, o Login: {txtLoginproxy} e a senha: {txtSenhaproxy} do servidor estão corretos e se o servidor está funcionando e se há acesso nesse servidor!";
                }
                else
                    weberroexplic = $"Verifique se o servidor proxy: {txtDoproxy}:{txtPortaproxy} não requer autenticação adicional para acessar o servidor, se for o caso marque a opção 'Meu servidor requer autenticação de proxy' acima!";
            }

            string mensagemerro;

            if (string.IsNullOrEmpty(weberroexplic))
            {
                mensagemerro = $"Título não atualizado devido a um erro ao conectar no servidor: \n{weberrogeral}";
            }
            else
            {
                mensagemerro = $"Título não atualizado devido a um erro ao conectar no servidor: \n{weberrogeral} \n{weberroexplic}";
            }
            erroconta = -3;
            throw new Exception(mensagemerro);
        }

        private void ChecarServerSC(string urlcompletasc)
        {
            string status = "";
            try
            {
                using (WebClient wcurlcompletasc = new WebClient())
                {
                    wcurlcompletasc.Headers.Add(HttpRequestHeader.UserAgent, useragentdef);
                    if (chkUsoproxy == true)
                    {
                        DadosProxy();
                        wcurlcompletasc.Proxy = servidorproxydoaplicativo;
                    }
                    Stream strurlcompleta = wcurlcompletasc.OpenRead(urlcompletasc);

                    using (StreamReader rdrurlcompleta = new StreamReader(strurlcompleta, Encoding.Default))
                    {
                        XmlDocument oXML = new XmlDocument();
                        oXML.Load(rdrurlcompleta);
                        XmlElement root = oXML.DocumentElement;
                        XmlNodeList xnList = root.GetElementsByTagName("STREAMSTATUS");

                        for (int i = 0; i < xnList.Count; i++)
                        {
                            status = xnList[i].InnerText;
                        }

                        if (status == "0")
                        {
                            erroconta = -3;
                            throw new Exception("Não há transmissão de áudio para o servidor, o encoder não está conectado no servidor shoutcast!");
                        }
                    }
                }
            }
            catch (WebException wexc)
            {
                status = wexc.Message;
            }
        }

        private void RecInfoDosDadosCad(string ipserver, string portaserver, string senhaserver, string idoupontomont)
        {
            string identificadorproc = processodoaplicativo.Id.ToString();
            string urlparacarregar;
            string conteudoarquivotextonextsong = "Update RDS By GabardoHost - Vanderson Gabardo";
            string dadosarquivotexto = null;
            string arquivodelog = $@"{diretoriodoaplicativo}LOGS\SOM{identificadorproc}LOG.csv";
            string urlshoutcastv1 = "REMOVIDO";
            string urlshoutcastv2 = $"http://{ipserver}:{portaserver}/admin.cgi?sid={idoupontomont}&mode=updinfo&song=";
            string urlicecast = $"http://{ipserver}:{portaserver}/admin/metadata?mount=/{idoupontomont}&mode=updinfo&song=";

            if (chkTransmproxsom == true)
            {
                if (chkUrlsomnext == true)
                    TratamentoURLNowNext(true);

                else
                    TratamentoTextoNowNext(true);

                conteudoarquivotextonextsong = conteudotexto;
            }

            if (chkUrlsom == true)
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

                if (chkTransmproxsom == true)
                {
                    if (chkUrlsomnext == true)
                        TratamentoURLNowNext(true);

                    else
                        TratamentoTextoNowNext(true);

                    conteudoarquivotextonextsong = conteudotexto;
                }

                if (chkUrlsom == true)
                {
                    TratamentoURLNowNext(false);
                }
                else
                {
                    TratamentoTextoNowNext(false);
                }

                conteudoarquivotexto = conteudotexto;
            }

            if (conteudoarquivotexto != conteudoarquivotextoantigo)
            {
                conteudoarquivotexto = conteudoarquivotexto.Replace("&", "e").Replace("_", " ");
                conteudoarquivotextonextsong = conteudoarquivotextonextsong.Replace("&", "e").Replace("_", " ");

                if (chkCaracteresespeciais == true)
                {
                    string caracteresaanalisar = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\-\s]";

                    Regex rgx = new Regex(caracteresaanalisar);

                    string resultado = rgx.Replace(conteudoarquivotexto, " ");
                    conteudoarquivotexto = resultado;

                    string resultadonext = rgx.Replace(conteudoarquivotextonextsong, " ");
                    conteudoarquivotextonextsong = resultadonext;
                }

                if (chkAcentospalavras == true)
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

                if (cbTiposervidor == 2)
                {
                    urlparacarregar = urlicecast + conteudoarquivotexto;
                }

                if (cbTiposervidor == 1)
                {
                    urlparacarregar = urlshoutcastv2 + conteudoarquivotexto;
                    ChecarServerSC($"http://{ipserver}:{portaserver}/stats?sid={idoupontomont}");
                    if (chkTransmproxsom == true)
                        urlparacarregar = urlshoutcastv2 + conteudoarquivotexto + "&next=" + conteudoarquivotextonextsong;
                }

                try
                {
                    if (cbTiposervidor == 0)
                    {
                        string saidadowget = "";
                        Process processowget = new Process();
                        processowget.StartInfo.CreateNoWindow = true;
                        processowget.StartInfo.FileName = "wget.exe";
                        processowget.StartInfo.Arguments = $"--server-response -t 1 --user-agent=\"{useragentdef}\" \"http://{ipserver}:{portaserver}/admin.cgi?pass={txtSenhaserver}&mode=updinfo&song={conteudoarquivotexto}\" -O \"{diretoriodoaplicativo}{identificadorproc}txt.txt\"";
                        processowget.StartInfo.UseShellExecute = false;
                        processowget.StartInfo.RedirectStandardOutput = true;
                        processowget.StartInfo.RedirectStandardError = true;
                        processowget.Start();
                        saidadowget = processowget.StandardOutput.ReadToEnd() + processowget.StandardError.ReadToEnd();
                        if (!saidadowget.Contains("No data received") && !saidadowget.Contains("Saving to"))
                        {
                            throw new WebException("Impossível conectar-se ao servidor");
                        }
                        if (File.Exists($"{diretoriodoaplicativo}{identificadorproc}txt.txt"))
                        {
                            File.Delete($"{diretoriodoaplicativo}{identificadorproc}txt.txt");
                        }
                    }
                    else
                    {
                        HttpWebRequest webreqshouticecast = (HttpWebRequest)WebRequest.Create(urlparacarregar);
                        webreqshouticecast.UserAgent = useragentdef;
                        senhaserver = Convert.ToBase64String(Encoding.Default.GetBytes(senhaserver));
                        webreqshouticecast.Headers.Add("Authorization", "Basic " + senhaserver);
                        webreqshouticecast.Credentials = new NetworkCredential("username", "password");
                        webreqshouticecast.Method = WebRequestMethods.Http.Get;
                        webreqshouticecast.AllowAutoRedirect = true;

                        if (chkUsoproxy == true)
                        {
                            DadosProxy();
                            webreqshouticecast.Proxy = servidorproxydoaplicativo;
                        }
                        else
                            webreqshouticecast.Proxy = null;

                        HttpWebResponse webrespshouticecast = (HttpWebResponse)webreqshouticecast.GetResponse();
                        webrespshouticecast.Close();
                    }
                }
                catch (WebException excecaodoservidor)
                {
                    ErroWebConServ(excecaodoservidor.Message, excecaodoservidor.Status.ToString());
                }

                string novosdadosarquivotexto = $"{DateTime.Now.ToString()};{conteudoarquivotexto}";

                if (chkTransmproxsom == true)
                    novosdadosarquivotexto = $"{novosdadosarquivotexto};{conteudoarquivotextonextsong}";

                if (!Directory.Exists($@"{diretoriodoaplicativo}LOGS"))
                    Directory.CreateDirectory($@"{diretoriodoaplicativo}LOGS");

                if (File.Exists(arquivodelog))
                {
                    using (StreamReader sr = new StreamReader(arquivodelog, Encoding.Default))
                    {
                        dadosarquivotexto = sr.ReadToEnd();
                    }
                }

                if (string.IsNullOrEmpty(dadosarquivotexto))
                    dadosarquivotexto = "Data e Hora:;Nome do som:;Nome do próximo som:";

                File.WriteAllText(arquivodelog, $"{dadosarquivotexto}\n{novosdadosarquivotexto}", Encoding.Default);

                FileInfo arquivotextolog = new FileInfo(arquivodelog);

                if (arquivotextolog.Length > 10485760)
                    File.Move(arquivodelog, $"{arquivodelog}{DateTime.Now.ToString().Replace(":", "").Replace("/", "")}.csv");

                if (conteudoarquivotexto.Length > 250)
                    conteudoarquivotexto = conteudoarquivotexto.Substring(0, 250) + "...";

                if (conteudoarquivotextonextsong.Length > 250)
                    conteudoarquivotextonextsong = conteudoarquivotextonextsong.Substring(0, 250) + "...";

                if (chkTransmproxsom == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nO RDS Está transmitindo agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \n\nPróximo som: {conteudoarquivotextonextsong} \nNa data e hora: {DateTime.Now.ToString()}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nO RDS Está transmitindo agora o seguinte nome para o servidor: \n{conteudoarquivotexto} \nNa data e hora: {DateTime.Now.ToString()}");
                }
            }
        }
    }
}