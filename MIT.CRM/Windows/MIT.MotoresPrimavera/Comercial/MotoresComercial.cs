using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interop.ErpBS900;
using Interop.GcpBE900;
using Interop.StdBE900;
using Interop.CrmBE900;


using MIT.Data;
using MIT.Data.Model;
using System.IO;
using Interop.StdPlatBS900;
using MIT.MotoresPrimavera.Parametros;

namespace MIT.MotoresPrimavera.Comercial
{
    public class MotoresComercial
    {
        private ErpBS _erpBs;
        
        //private StdPlatBS _plataforma;

        public GcpBEDocumentoVenda _documentoVenda;
        public GcpBEDocumentoCompra _documentoCompra;
        private GcpBELinhasDocumentoVenda linhasDocVenda;
        private GcpBEAvenca documentoAvenca;
        private GcpBEDocumentoLiq documentoLiquidacao;
        public GcpBEPendente DocAdiantamento;
        public GcpBEDocumentoStock _documentoStock;


        public string modulo = "V";

        public MotoresComercial(ErpBS _erpBs)
        {
            this._erpBs = _erpBs;
            
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// Método para resolução das assemblies.
        /// </summary>
        /// <param name="sender">Application</param>
        /// <param name="args">Resolving Assembly Name</param>
        /// <returns>Assembly</returns>
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;
            System.Reflection.AssemblyName assemblyName;
            string PRIMAVERA_COMMON_FILES_FOLDER = Instancia.daPastaConfig();//pasta dos ficheiros comuns especifica da versão do ERP PRIMAVERA utilizada.
            assemblyName = new System.Reflection.AssemblyName(args.Name);
            assemblyFullName = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86), PRIMAVERA_COMMON_FILES_FOLDER), assemblyName.Name + ".dll");
            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else
                return null;
        }

        #region numSeries TVSD
        public void add_NumSeries(string modulo, List<Dictionary<string, string> >listaNumSerie, int i)
        {
            switch (modulo)
            {
                case "V": add_NumSeriesVendas(listaNumSerie, i);break;
                case "C": add_NumSeriesCompras(listaNumSerie,i); break;
                case "S": add_NumSeriesStocks(listaNumSerie,i); break;
            }
        }

        private void add_NumSeriesVendas(List<Dictionary<string, string>> listaNumSerie, int i)
        {
            GcpBENumeroSerie _objNumSerie;

            foreach (Dictionary<string, string> item in listaNumSerie)
            {
                _objNumSerie = new GcpBENumeroSerie();
                _objNumSerie.set_Modulo("V");
                _objNumSerie.set_NumeroSerie(item.Keys.First());
                _objNumSerie.set_Manual(1);

                _documentoVenda.get_Linhas()[i].get_NumerosSerie().Insere(_objNumSerie);
            }
        }

        private void add_NumSeriesCompras(List<Dictionary<string, string>> listaNumSerie,int i)
        {
            GcpBENumeroSerie _objNumSerie;

            _objNumSerie = new GcpBENumeroSerie();
            _objNumSerie.set_Modulo("C");
            _objNumSerie.set_NumeroSerie("1111");
            _objNumSerie.set_Manual(1);

            _documentoCompra.get_Linhas()[1].get_NumerosSerie().Insere(_objNumSerie);
            
            //foreach (Dictionary<string, string> item in listaNumSerie)
            //{
            //    _objNumSerie = new GcpBENumeroSerie();
            //    _objNumSerie.set_Modulo("C");
            //    _objNumSerie.set_NumeroSerie(item.Keys.First());
            //    _objNumSerie.set_Manual(1);

            //    _documentoCompra.get_Linhas()[i].get_NumerosSerie().Insere(_objNumSerie);
            //}
        }

        private void add_NumSeriesStocks(List<Dictionary<string, string>> listaNumSerie, int i)
        {
            GcpBENumeroSerie _objNumSerie;

            foreach (Dictionary<string, string> item in listaNumSerie)
            {
                _objNumSerie = new GcpBENumeroSerie();
                _objNumSerie.set_Modulo("S");
                _objNumSerie.set_NumeroSerie(item.Keys.First());
                _objNumSerie.set_Manual(1);

                _documentoStock.get_Linhas()[i].get_NumerosSerie().Insere(_objNumSerie);
            }
        }
        #endregion

        #region Documentos de Vendas

        /// <summary>
        /// Metodo para o preenchemento do documento de venda, prenche-se os campos por defeito do documento de venda e os campos enviados no metodo
        /// </summary>
        /// <param name="tipodoc"></param>
        /// <param name="dataDoc"></param>
        /// <param name="dataGravacao"></param>
        /// <param name="tipoEntidade"></param>
        /// <param name="codigoEntidade"></param>
        /// <returns></returns>
        public PrimaveraResultStructure PreencheCabecalho_DocumentoVenda(string tipodoc, DateTime dataDoc, DateTime dataGravacao, string tipoEntidade, string codigoEntidade, string referencia = "")
        {
            string sourceName = "PreencheCabecalho_DocumentoVenda";

            PrimaveraResultStructure result = new PrimaveraResultStructure();

            try
            {
                _documentoVenda = new GcpBEDocumentoVenda();
                
                _documentoVenda.set_Tipodoc(tipodoc);
                _documentoVenda.set_Serie(_erpBs.Comercial.Series.DaSerieDefeito("V", tipodoc));
                _documentoVenda.set_DataDoc(dataDoc);
                _documentoVenda.set_DataGravacao(dataGravacao);

                _documentoVenda.set_TipoEntidade(tipoEntidade);
                _documentoVenda.set_Entidade(codigoEntidade);

                _documentoVenda.set_Requisicao(referencia);
                _documentoVenda.set_Referencia(referencia);

                _erpBs.Comercial.Vendas.PreencheDadosRelacionados(_documentoVenda);


                result = new PrimaveraResultStructure()
                {
                    codigo = 0
                };

            }
            catch (Exception e)
            {
                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = "Erro Logica Primavera - Preenche Cabeçalho do Documento " + e.Message,
                    procedimento = "Contacto os Responsaveis do Projecto"
                };

                PrimaveraWSLogger.escreveErro(result.ToString(), sourceName);
            }

            return result;
        }

        /// <summary>
        /// Metodo para adicionar-se linhas/produtos nos documentos de venda
        /// </summary>
        /// <param name="artigo"></param>
        /// <param name="quantidade"></param>
        /// <param name="armazem"></param>
        /// <param name="localizacao"></param>
        /// <param name="precoUnitario"></param>
        /// <returns></returns>
        public PrimaveraResultStructure Adiciona_Linhas_DocumentoVenda(string artigo, double quantidade,
            ref string armazem, ref string localizacao, double precoUnitario)
        {
            PrimaveraResultStructure result = new PrimaveraResultStructure();
            string source = "Adiciona_Linhas_DocumentoVenda";
            try
            {
                _erpBs.Comercial.Vendas.AdicionaLinha(_documentoVenda, artigo, quantidade, armazem, localizacao, precoUnitario);

                result = new PrimaveraResultStructure()
                {
                    codigo = 0
                };
            }
            catch (Exception e)
            {
                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = "Erro Logica Primavera - Adiciona Linhas DocumentoVenda " + e.Message,
                    procedimento = "Contacto os Responsaveis do Projecto"
                };
                PrimaveraWSLogger.escreveErro(string.Format("[{0}] Erro Logica Primavera devido ao: {1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), e.Message), source);
            }

            return result;

        }

        /// <summary>
        /// Metodo para adicionar-se linhas/produtos ao documento de venda
        /// </summary>
        /// <param name="artigo"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>
        public PrimaveraResultStructure Adiciona_Linhas_DocumentoVenda(string artigo, double quantidade, double desconto)
        {
            ErpBS objmotor = _erpBs;
            string source = "Adiciona_Linhas_DocumentoVenda";

            PrimaveraResultStructure result = new PrimaveraResultStructure();
            try
            {
                int tamanho = _documentoVenda.get_Linhas().NumItens;
                int contandor = 0;
                objmotor.Comercial.Vendas.AdicionaLinha(_documentoVenda, artigo, quantidade);

                foreach (GcpBELinhaDocumentoVenda linhas in _documentoVenda.get_Linhas())
                {
                    if (contandor == tamanho)
                    {
                        linhas.set_Quantidade(quantidade);
                        linhas.set_Desconto1(float.Parse(desconto.ToString()));
                    }
                    contandor++;
                }

                result = new PrimaveraResultStructure()
                {
                    codigo = 0
                };
            }
            catch (Exception e)
            {
                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = "Erro Logica Primavera - Adiciona Linhas DocumentoVenda " + e.Message,
                    procedimento = "Contacto os Responsaveis do Projecto"
                };
                PrimaveraWSLogger.escreveErro(string.Format("[{0}] Erro Logica Primavera devido ao: {1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), e.Message), source);
            }

            return result;
        }

        /// <summary>
        /// Metodo para a gravação de um determinado documento de venda
        /// </summary>
        /// <param name="nomeUtilizador"></param>
        /// <param name="cdu_semestre"></param>
        /// <returns></returns>
        public PrimaveraResultStructure Gravar_DocumentoVenda(string nomeUtilizador = "", string cdu_semestre = "")
        {
            ErpBS objmotor = _erpBs;
            string source = "Gravar_DocumentoVenda";

            PrimaveraResultStructure resultado = new PrimaveraResultStructure();
            string str_avisos = "";
            try
            {
                objmotor.Comercial.Vendas.Actualiza(_documentoVenda, str_avisos);
                objmotor.Comercial.Vendas.ActualizaValorAtributo(_documentoVenda.get_Filial(), _documentoVenda.get_Tipodoc(),
                    _documentoVenda.get_Serie(), _documentoVenda.get_NumDoc(), "CDU_NOME_UTILIZADOR", nomeUtilizador);

                objmotor.Comercial.Vendas.ActualizaValorAtributo(_documentoVenda.get_Filial(), _documentoVenda.get_Tipodoc(),
                    _documentoVenda.get_Serie(), _documentoVenda.get_NumDoc(), "CDU_Semestre", cdu_semestre);

                resultado.codigo = 0;
                resultado.tipoProblema = str_avisos;
                resultado.descricao = string.Format("Foi Gerado o documento: #{0} {1}/{2}#", _documentoVenda.get_Tipodoc(), _documentoVenda.get_NumDoc(), _documentoVenda.get_Serie());

            }
            catch (Exception e)
            {
                resultado.codigo = 3;
                resultado.descricao = e.Message;
                PrimaveraWSLogger.escreveErro(string.Format("[{0}] Erro Logica Primavera devido ao: {1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), e.Message), source);
            }

            return resultado;
        }


        /// <summary>
        /// Metodo para impressão do documento de venda, ainda não implementado
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        private Stream imprimirDoc(string idDoc)
        {
            return null;
        }

        /// <summary>
        /// Metodo para a criação das Avenças com base no documento de venda 
        /// </summary>
        /// <returns></returns>
        public PrimaveraResultStructure CriaAvenca()
        {
            PrimaveraResultStructure resultado = new PrimaveraResultStructure();
            string source = "CriaAvenca";
            try
            {

                DateTime dataInicio, dataFim, dataPrimeiraPropina;
                string entidade, semestre, semestreAntigo, str_avisos = "";



                semestre = _erpBs.Comercial.Vendas.DaValorAtributo(
                    _documentoVenda.get_Filial(),
                    _documentoVenda.get_Tipodoc(),
                    _documentoVenda.get_Serie(),
                    _documentoVenda.get_NumDoc(),
                    "CDU_Semestre"
                    ).ToString();

                var sql_query = String.Format("select * from tdu_semestre where CDU_IdSemestre ='{0}'", semestre);
                var objLista = _erpBs.Consulta(sql_query);

                if (!objLista.Vazia())
                {
                    dataInicio = (DateTime)objLista.Valor("CDU_DataInicio");
                    dataFim = (DateTime)objLista.Valor("CDU_DataUltimaPropina");
                    dataPrimeiraPropina = (DateTime)objLista.Valor("CDU_DataPrimeiraPropina");

                    entidade = _erpBs.Comercial.Vendas.DaValorAtributo(
                        _documentoVenda.get_Filial(),
                        _documentoVenda.get_Tipodoc(),
                        _documentoVenda.get_Serie(),
                        _documentoVenda.get_NumDoc(),
                        "Entidade"
                        ).ToString();

                    var _aluno = _erpBs.Comercial.Clientes.Edita(entidade);

                    if (_aluno.get_TipoTerceiro() != "")
                    {
                        if (_erpBs.Comercial.Avencas.Existe(_documentoVenda.get_Filial(), entidade))
                        {
                            documentoAvenca = _erpBs.Comercial.Avencas.Edita(_documentoVenda.get_Filial(), entidade);

                            semestreAntigo = _erpBs.Comercial.Vendas.DaValorAtributo(
                                documentoAvenca.get_FilialDocOriginal(),
                                documentoAvenca.get_TipoDocOriginal(),
                                documentoAvenca.get_SerieDocOriginal(),
                                documentoAvenca.get_NumDocOriginal(),
                                "CDU_Semestre"
                                ).ToString();

                            if (semestreAntigo != semestre)
                            {
                                documentoAvenca.set_SerieDocOriginal(_documentoVenda.get_Serie());
                                documentoAvenca.set_TipoDocOriginal(_documentoVenda.get_Tipodoc());
                                documentoAvenca.set_NumDocOriginal(_documentoVenda.get_NumDoc());

                                documentoAvenca.set_DataInicio(dataInicio);
                                documentoAvenca.set_DataFim(dataFim);
                                documentoAvenca.set_DataProximoProcessamento(dataPrimeiraPropina);

                                _erpBs.Comercial.Avencas.Actualiza(documentoAvenca, str_avisos);
                            }
                            else
                            {
                                documentoAvenca.set_SerieDocOriginal(_documentoVenda.get_Serie());
                                documentoAvenca.set_TipoDocOriginal(_documentoVenda.get_Tipodoc());
                                documentoAvenca.set_NumDocOriginal(_documentoVenda.get_NumDoc());

                                _erpBs.Comercial.Avencas.Actualiza(documentoAvenca, str_avisos);
                            }

                            resultado = new PrimaveraResultStructure()
                            {
                                codigo = 0,
                                descricao = string.Format("Foi actualizado com sucesso o contrato {0} {1}/{2} do aluno {3} e a respectiva avença para o semestre {4}",
                                        _documentoVenda.get_Tipodoc(),
                                        _documentoVenda.get_NumDoc(),
                                        _documentoVenda.get_Serie(),
                                        _documentoVenda.get_Nome(),
                                        semestre)
                            };


                        }
                        else
                        {
                            documentoAvenca = new GcpBEAvenca();
                            //Dados da avença
                            documentoAvenca.set_Descricao((semestre + " " + _documentoVenda.get_Nome()).Substring(0, 19));
                            documentoAvenca.set_Avenca(_documentoVenda.get_Entidade());
                            documentoAvenca.set_Entidade(_documentoVenda.get_Entidade());
                            documentoAvenca.set_TipoEntidade("C");
                            documentoAvenca.set_TipoEntidadeFac("C");
                            documentoAvenca.set_EntidadeFac(_documentoVenda.get_Entidade());

                            //Dados do documento base
                            documentoAvenca.set_SerieDocOriginal(_documentoVenda.get_Serie());
                            documentoAvenca.set_TipoDocOriginal(_documentoVenda.get_Tipodoc());
                            documentoAvenca.set_NumDocOriginal(_documentoVenda.get_NumDoc());
                            documentoAvenca.set_FilialDocOriginal(_documentoVenda.get_Filial());

                            //Dados do documento Destino
                            documentoAvenca.set_TipoDocDestino("FA");
                            documentoAvenca.set_SerieDocDestino(_documentoVenda.get_Serie());

                            //Dados do Processamento da Avença
                            documentoAvenca.set_DataInicio(dataInicio);
                            documentoAvenca.set_DataFim(dataFim);
                            documentoAvenca.set_DataProximoProcessamento(dataPrimeiraPropina);
                            documentoAvenca.set_Periodicidade("0");

                            _erpBs.Comercial.Avencas.Actualiza(documentoAvenca, str_avisos);

                            resultado = new PrimaveraResultStructure()
                            {
                                codigo = 0,
                                descricao = string.Format("Foi criado com sucesso o contrato {0} {1}/{2} do aluno {3} e a respectiva avença para o semestre {4}",
                                        _documentoVenda.get_Tipodoc(),
                                        _documentoVenda.get_NumDoc(),
                                        _documentoVenda.get_Serie(),
                                        _documentoVenda.get_Nome(),
                                        semestre)
                            };
                        }
                    }
                    else
                    {
                        resultado = new PrimaveraResultStructure()
                        {
                            codigo = 3,
                            tipoProblema = "Erro Logica Primavera",
                            procedimento = "Consulte aos Tecnicos do Projecto",
                            descricao = string.Format("Erro ao criar a avença porque o aluno {0} - {1}  no ERP Primavera",
                                   _documentoVenda.get_Entidade(),
                                   _documentoVenda.get_Nome())
                        };
                    }
                }
                else
                {
                    resultado = new PrimaveraResultStructure()
                    {
                        codigo = 3,
                        tipoProblema = "Erro Logica Primavera",
                        procedimento = "Consulte aos Tecnicos do Projecto",
                        descricao = string.Format("Erro ao criar a avença do aluno {0} - {1} não existe o semestre {2}",
                                   _documentoVenda.get_Entidade(),
                                   _documentoVenda.get_Nome(),
                                   semestre)
                    };
                }
            }
            catch (Exception ex)
            {
                resultado.codigo = 3;
                resultado.descricao = ex.Message;

                PrimaveraWSLogger.escreveErro(string.Format("[{0}] Erro Logica Primavera na gravação da avença, devido ao: {1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), ex.Message), source);

            }

            return resultado;
        }

        /// <summary>
        /// Metodo para a consulta de conta do cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public List<Pendente> ConsultaConta(string cliente)
        {
            string source = "ConsultaConta";

            List<Pendente> listaPendente = new List<Pendente>();
            try
            {
                string str_query = String.Format("select p.TipoEntidade, p.Entidade,p.DataDoc,p.DataVenc,p.TipoDoc,p.NumDocInt,p.Serie,p.Moeda,p.ValorPendente,p.ValorTotal, cd.Requisicao from Pendentes p " +
                    "left join CabecDoc cd on cd.TipoDoc = p.TipoDoc and cd.NumDoc = p.NumDocInt and cd.Serie = p.Serie and cd.Filial = p.Filial and p.Modulo = 'V' " +
                    "where p.entidade = '{0}' and p.tipoentidade='C'", cliente);
                var objLista = _erpBs.Consulta(str_query);

                while (!(objLista.NoInicio() || objLista.NoFim()))
                {
                    Pendente pendente = new Pendente();

                    string numdoc = objLista.Valor("NumDocInt").ToString();

                    pendente.tipoEntidade = (string)objLista.Valor("TipoEntidade");
                    pendente.entidade = (string)objLista.Valor("Entidade");
                    
                    pendente.moedaId = (string)objLista.Valor("Moeda");

                    pendente.valorPendente = (double)objLista.Valor("ValorPendente");
                    pendente.valorTotal = (double)objLista.Valor("ValorTotal");

                    pendente.dataCriacao = (DateTime)objLista.Valor("DataDoc");
                    pendente.dataVencimento = (DateTime)objLista.Valor("DataVenc");
                    pendente.tipoDoc = (string)objLista.Valor("TipoDoc");
                    pendente.numDoc = Convert.ToInt32(numdoc);
                    pendente.serie = (string)objLista.Valor("Serie");
                    pendente.referencia = (string)objLista.Valor("Requisicao");
                    

                    listaPendente.Add(pendente);
                    objLista.Seguinte();
                }
            }
            catch (Exception ex)
            {
                PrimaveraWSLogger.escreveErro(string.Format("Ocorreu um erro na consulta do saldo do cliente {0} devido ao seguinte erro: {1}", cliente, ex.Message), source);

            }



            return listaPendente;

        }

        #endregion

        #region Clientes

        public Entidade daCliente(string codigo)
        {
            List<Entidade> listaEntidade = new List<Entidade>();

            try
            {
                string str_query = String.Format("select c.Cliente,c.Nome,c.NomeFiscal,c.Fac_Mor,c.Fac_Local,c.Fac_Tel");
                str_query += ",c.NumContrib, ISNULL( sum(p.ValorPendente),0) as ValorPendente, c.ClienteAnulado,c.AvisosVenc ";
                str_query += "from clientes c left join pendentes p on p.entidade = c.Cliente and p.TipoEntidade = 'C'";
                str_query += "where c.cliente='"+ codigo +"' and c.Cliente <> 'VD' group by c.Cliente,c.Nome,c.NomeFiscal,c.Fac_Mor,c.Fac_Local,c.Fac_Tel,c.NumContrib,c.ClienteAnulado,c.AvisosVenc ";
                str_query += "having sum(p.ValorPendente) > 0 ";

                var objLista = _erpBs.Consulta(str_query);

                while (!(objLista.NoInicio() || objLista.NoFim()))
                {
                    Entidade cliente = new Entidade()
                    {
                        entidade = (string)objLista.Valor("Cliente"),
                        tipoEntidade = "C",
                        nome = (string)objLista.Valor("Nome"),
                        fac_Mor = (string)objLista.Valor("Fac_Mor"),
                        fac_Local = (string)objLista.Valor("Fac_Local"),
                        fac_Tel = (string)objLista.Valor("Fac_Tel"),
                        valorPendente = (double)objLista.Valor("ValorPendente"),
                        enviaCobranca = (bool)objLista.Valor("AvisosVenc"),

                    };
                    cliente.contactos = daContactosEntidade(cliente.tipoEntidade, cliente.entidade);

                    listaEntidade.Add(cliente);
                    objLista.Seguinte();
                }
            }
            catch (Exception ex)
            { }



            return listaEntidade.First();
        }

        public List<Entidade> daListaClientes()
        {
            List<Entidade> listaEntidade = new List<Entidade>();

            try
            {
                string str_query = String.Format("select c.Cliente,c.Nome,c.NomeFiscal,c.Fac_Mor,c.Fac_Local,c.Fac_Tel");
                str_query += ",c.NumContrib, ISNULL( sum(p.ValorPendente),0) as ValorPendente, c.ClienteAnulado,c.AvisosVenc ";
                str_query += "from clientes c inner join pendentes p on p.entidade = c.Cliente and p.TipoEntidade = 'C'";
                str_query += "where c.Cliente <> 'VD' group by c.Cliente,c.Nome,c.NomeFiscal,c.Fac_Mor,c.Fac_Local,c.Fac_Tel,c.NumContrib,c.ClienteAnulado,c.AvisosVenc ";
                str_query += "having sum(p.ValorPendente) > 0 ";

                var objLista = _erpBs.Consulta(str_query);

                while (!(objLista.NoInicio() || objLista.NoFim()))
                {
                    Entidade cliente = new Entidade()
                    {
                        entidade = (string)objLista.Valor("Cliente"),
                        tipoEntidade = "C",
                        nome = (string)objLista.Valor("Nome"),
                        fac_Mor = (string)objLista.Valor("Fac_Mor"),
                        fac_Local = (string)objLista.Valor("Fac_Local"),
                        fac_Tel = (string)objLista.Valor("Fac_Tel"),
                        valorPendente = (double)objLista.Valor("ValorPendente"),
                        enviaCobranca = (bool)objLista.Valor("AvisosVenc"),
                        
                    };
                    cliente.contactos = daContactosEntidade(cliente.tipoEntidade, cliente.entidade);

                    listaEntidade.Add(cliente);
                    objLista.Seguinte();
                }
            }
            catch (Exception ex)
            {               }

           

            return listaEntidade;
        }

        public List<Contactos> daContactosEntidade(string tipoEntidade,string entidade)
        {
            List<Contactos> lstContactos = new List<Contactos>();

            var lista = _erpBs.CRM.Contactos.ListaContactosDaEntidade(tipoEntidade, entidade);

            foreach (CrmBEContacto contacto in lista)
            {
                lstContactos.Add(new Contactos()
                    {
                        Nome = contacto.get_Nome(),
                        Email = contacto.get_Email(),
                        Titulo = contacto.get_Titulo(),
                        tipoContacto = _erpBs.CRM.Contactos.DaValorAtributoIDEntidade(contacto.get_ID(),tipoEntidade,
                            entidade,"TipoContacto"),
                        telefone = contacto.get_Telefone()
                    }
                );
            }
            return lstContactos;
        }

        /// <summary>
        /// Metodo para do codigo do cliente, este metodo altera no primavera todas as tabelas relacionados com um determinado
        /// cliente
        /// </summary>
        /// <param name="codigoAntigo"></param>
        /// <param name="codigoNovo"></param>
        /// <returns></returns>
        public PrimaveraResultStructure AlteraCodigoCliente(string codigoAntigo, string codigoNovo)
        {
            PrimaveraResultStructure result;

            try
            {
                _erpBs.Comercial.Clientes.AlteraCodigoCliente(codigoAntigo, codigoNovo);
                result = new PrimaveraResultStructure()
                {
                    codigo = 0,
                    descricao = string.Format("Foi actualizado o codigo do cliente {0} para {1}", codigoAntigo, codigoNovo)
                };
            }
            catch
            {
                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    subNivel = "303",
                    descricao = string.Format("Erro na alteração do codigo do cliente {0} para {1}", codigoAntigo, codigoNovo),
                    procedimento = "Consultar os técnicos do projecto"
                };
            }


            return result;
        }

        /// <summary>
        /// Valida se existe existe no Erp Primavera
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public bool ExisteCliente(string cliente)
        {
            try
            {
                return _erpBs.Comercial.Clientes.Existe(cliente);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para a gravação/actualização do cliente no Erp Primavera usando os Motores Primavera
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nome"></param>
        /// <param name="nuit"></param>
        /// <param name="vendedor"></param>
        /// <param name="observacao"></param>
        /// <param name="fac_Morada"></param>
        /// <param name="fac_Localidade"></param>
        /// <param name="fac_Telefone"></param>
        /// <param name="desconto"></param>
        /// <param name="cdu_bolsa"></param>
        /// <param name="cdu_geraMulta"></param>
        /// <param name="cdu_tipoIngresso"></param>
        /// <param name="cdu_codLic"></param>
        /// <param name="cdu_turma"></param>
        /// <returns></returns>
        public PrimaveraResultStructure GravarCliente(string codigo, string nome, string nuit, string vendedor, string observacao, string fac_Morada,
            string fac_Localidade, string fac_Telefone, float desconto = 0, bool cdu_bolsa = false, bool cdu_geraMulta = true, string cdu_tipoIngresso = "",
            string cdu_codLic = "", string cdu_turma = "")
        {
            string source = "GravarCliente";

            PrimaveraResultStructure result;
            try
            {
                var str_query = String.Format("select cdu_condpag from TDU_ParametrosISUTC where cdu_vendedor='{0}'", vendedor);
                var condpag = this._erpBs.Consulta(str_query).Valor("cdu_condpag").ToString();

                // Verifica se o cliente Existe
                if (_erpBs.Comercial.Clientes.Existe(codigo) == true)
                {
                    //Actualização dos dados do Cliente
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "nome", nome);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "NomeFiscal", nome);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Moeda", _erpBs.Contexto.MoedaBase);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "NumContribuinte", nuit);

                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Vendedor", vendedor);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CondPag", condpag);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Desconto", desconto);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Morada", fac_Morada);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Telefone", fac_Telefone);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Localidade", fac_Localidade);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "Observacoes", observacao);

                    //Campos de utilizador
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_Bolsa", cdu_bolsa);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_GeraMulta", cdu_geraMulta);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_TipoIngresso", cdu_tipoIngresso);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_CodLic", cdu_codLic);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_Turma", cdu_turma);


                    result = new PrimaveraResultStructure()
                    {
                        codigo = 0,
                        descricao = "Codigos de Sucesso",
                        tipoProblema = String.Format("Os dados Aluno {0} - {1} Gravados Com Sucesso Completo", codigo, nome)
                    };

                    PrimaveraWSLogger.escreveInformacao(String.Format("[{0}] Os dados Aluno {1} - {2} Gravados Com Sucesso Completo", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), codigo, nome), source);
                }
                else
                {
                    GcpBECliente _cliente = new GcpBECliente();

                    _cliente.set_Cliente(codigo);
                    _cliente.set_Nome(nome);
                    _cliente.set_NomeFiscal(nome);

                    _cliente.set_Moeda(_erpBs.Contexto.MoedaBase);
                    _cliente.set_NumContribuinte(nuit);
                    _cliente.set_Desconto(desconto);
                    _cliente.set_Vendedor(vendedor);
                    _cliente.set_CondPag(condpag);

                    _cliente.set_Morada(fac_Morada);
                    _cliente.set_Telefone(fac_Telefone);
                    _cliente.set_Localidade(fac_Localidade);
                    _cliente.set_Observacoes(observacao);

                    _erpBs.Comercial.Clientes.Actualiza(_cliente);

                    //Campos de utilizador
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_Bolsa", cdu_bolsa);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_GeraMulta", cdu_geraMulta);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_Turma", cdu_turma);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_CodLic", cdu_codLic);
                    _erpBs.Comercial.Clientes.ActualizaValorAtributo(codigo, "CDU_TipoIngresso", cdu_tipoIngresso);

                    result = new PrimaveraResultStructure()
                    {
                        codigo = 0,
                        descricao = "Codigos de Sucesso",
                        tipoProblema = String.Format("Os dados Aluno {0} - {1} Actualizados Com Sucesso Completo", codigo, nome)
                    };
                    PrimaveraWSLogger.escreveInformacao(String.Format("[{0}] Os dados Aluno {1} - {2} Actualizados Com Sucesso Completo", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), codigo, nome), source);

                }
            }
            catch (Exception e)
            {

                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = "Erro ao gravar o aluno - " + e.Message,
                    tipoProblema = "Erro Logica no Primavera",
                    procedimento = "Consultar os técnicos do projecto",
                    subNivel = "30 - O Erro ao gravar o aluno"
                };
                PrimaveraWSLogger.escreveErro(String.Format("[{0}] Erro ao gravar o aluno {1} - {2} devido ao seguinte erro: {3} ", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), codigo, nome, e.Message), source);

            }
            return result;

        }

        /// <summary>
        /// Metodo para a gravação/actualização do contacto de um determinado cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="codigo"></param>
        /// <param name="primeiroNome"></param>
        /// <param name="ultimoNome"></param>
        /// <param name="nomeCompleto"></param>
        /// <param name="morada"></param>
        /// <param name="localidade"></param>
        /// <param name="emailPrincipal"></param>
        /// <param name="emailAlternativo"></param>
        /// <param name="nrTelefone"></param>
        /// <param name="nrTelefoneAlternativo"></param>
        /// <returns></returns>
        public PrimaveraResultStructure GravarContactoCliente(string cliente, string codigo, string primeiroNome,
            string ultimoNome, string nomeCompleto, string morada, string localidade, string emailPrincipal, string emailAlternativo,
            string nrTelefone, string nrTelefoneAlternativo, string tipoContacto)
        {
            PrimaveraResultStructure result;
            string source = "GravarContactoCliente";
            try
            {
                CrmBEContacto objContacto = new CrmBEContacto();
                CrmBELinhaContactoEntidade objEntidateContacto = new CrmBELinhaContactoEntidade();

                //Valida se existe o contacto do aluno no sistema

                if (_erpBs.CRM.Contactos.Existe(codigo))
                {

                    //Actualiza os contactos do Cliente
                    objContacto = _erpBs.CRM.Contactos.Edita(codigo);

                    objContacto.set_EmModoEdicao(true);
                    objContacto.set_Contacto(codigo); //validar este campo, por ser a chave do contacto
                    objContacto.set_PrimeiroNome(primeiroNome);
                    objContacto.set_UltimoNome(ultimoNome);
                    objContacto.set_Nome(nomeCompleto);
                    objContacto.set_Morada(morada);
                    objContacto.set_Localidade(localidade);
                    objContacto.set_Telefone(nrTelefone);
                    objContacto.set_Telefone2(nrTelefoneAlternativo);
                    objContacto.set_Email(emailPrincipal);
                    objContacto.set_EmailResid(emailAlternativo);

                    objEntidateContacto = new CrmBELinhaContactoEntidade();

                    _erpBs.CRM.Contactos.Actualiza(objContacto);

                    result = new PrimaveraResultStructure()
                    {
                        codigo = 0,
                        descricao = "Codigos de Sucesso",
                        tipoProblema = "Os Contactos do Aluno Actualizado Com Sucesso Completo"
                    };

                }
                else
                {

                    objContacto = new CrmBEContacto();

                    objContacto.set_ID(Guid.NewGuid().ToString());
                    objContacto.set_Contacto(codigo);
                    objContacto.set_PrimeiroNome(primeiroNome);
                    objContacto.set_UltimoNome(ultimoNome);
                    objContacto.set_Nome(nomeCompleto);
                    objContacto.set_Morada(morada);
                    objContacto.set_Localidade(localidade);
                    objContacto.set_Telefone(nrTelefone);
                    objContacto.set_Telefone2(nrTelefoneAlternativo);
                    objContacto.set_Email(emailPrincipal);
                    objContacto.set_EmailResid(emailAlternativo);

                    objEntidateContacto = new CrmBELinhaContactoEntidade();

                    objEntidateContacto.set_IDContacto(objContacto.get_ID());
                    objEntidateContacto.set_Entidade(cliente);
                    objEntidateContacto.set_TipoEntidade("C");
                    objEntidateContacto.set_Email(emailPrincipal);
                    objEntidateContacto.set_Telefone(nrTelefone);
                    objEntidateContacto.set_Telemovel(nrTelefoneAlternativo);
                    objEntidateContacto.set_TipoContacto(tipoContacto); // Para já Fixo

                    objContacto.get_LinhasEntidade().Insere(objEntidateContacto);

                    _erpBs.CRM.Contactos.Actualiza(objContacto);

                    result = new PrimaveraResultStructure()
                    {
                        codigo = 0,
                        descricao = "Codigos de Sucesso",
                        tipoProblema = "Os Contactos do Aluno Gravado Com Sucesso Completo"
                    };
                    PrimaveraWSLogger.escreveInformacao(string.Format("[{0}] O contacto {1} - {2} foi gravado e adicionado ao cliente {3} com Sucesso", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), codigo, nomeCompleto, cliente), source);
                }




            }
            catch (Exception e)
            {
                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = "Erro ao gravar o aluno - " + e.Message,
                    tipoProblema = "Erro Logica no Primavera",
                    procedimento = "Consultar os técnicos do projecto",
                    subNivel = "30 - O Erro ao gravar o aluno"
                };
                PrimaveraWSLogger.escreveErro(string.Format("[{0}] Erro ao adicionar o contacto {1} - {2} ao cliente {3} devido ao seguinte erro: {4}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), codigo, nomeCompleto, cliente, e.Message), source);
            }

            return result;
        }

        public PrimaveraResultStructure RemoveContactoCliente(string cliente)
        {
            string source = "RemoveContactoCliente";
            PrimaveraResultStructure result = new PrimaveraResultStructure();
            try
            {

                List<string> lista = new List<string>();
                var l = _erpBs.CRM.Contactos.ListaContactosDaEntidade("C", cliente);
                for (int i = 1; i <= l.NumItens; i++)
                {
                    lista.Add(l.Edita[i].get_ID());
                }

                //_erpBs.CRM.Contactos.RemoveContactosDaEntidade("C", cliente);

                foreach (var c in lista)
                    _erpBs.CRM.Contactos.RemoveID(c);

                result = new PrimaveraResultStructure()
                {
                    codigo = 0,
                    descricao = "Codigos de Sucesso",
                    tipoProblema = string.Format("[{0}] Os contactos da entidade {1} foram removidos com Sucesso", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), cliente)
                };
                PrimaveraWSLogger.escreveInformacao(string.Format("[{0}] Os contactos da entidade {1} foram removidos com Sucesso", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), cliente), source);

            }
            catch (Exception e)
            {
                result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = string.Format("[{0}] Erro ao remover os contactos do cliente {1} devido ao seguinte erro: {2}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), cliente, e.Message),
                    tipoProblema = "Erro Logica no Primavera",
                    procedimento = "Consultar os técnicos do projecto",
                    subNivel = "30 - O Erro ao actualizar o aluno"
                };
                PrimaveraWSLogger.escreveErro(string.Format("[{0}] Erro ao remover os contactos do cliente {1} devido ao seguinte erro: {2}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), cliente, e.Message), source);
            }

            return result;
        }

        /// <summary>
        /// Actualiza o codigo do tipo terceiro com base no codigo da licienciatura
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="codLic"></param>
        /// <returns></returns>
        public PrimaveraResultStructure ActualizaTipoTerceiro(string cliente, string codLic)
        {
            PrimaveraResultStructure resultado = new PrimaveraResultStructure();

            try
            {
                string str_query = String.Format("Select TipoTerceiro from tipoTerceiros where descricao ='{0}'", codLic);
                var objLista = _erpBs.Consulta(str_query);

                if (!objLista.Vazia())
                {
                    var objCliente = _erpBs.Comercial.Clientes.Edita(cliente);
                    objCliente.set_TipoTerceiro(objLista.Valor("TipoTerceiro").ToString());
                    _erpBs.Comercial.Clientes.Actualiza(objCliente);

                    resultado.codigo = 0;
                }
            }
            catch (Exception ex)
            {
                resultado.codigo = 3;
                resultado.descricao = ex.Message;
            }

            return resultado;
        }

        /// <summary>
        /// Metodo que pesquisa no primavera o codigo do instituto com base no vendedor
        /// </summary>
        /// <param name="vendedor"></param>
        /// <returns>Devovle do codigo do Instituto</returns>
        public string da_Codigo_Instituto(string vendedor)
        {
            string str_query = String.Format("select cdu_instituto from tdu_parametrosIsutc where cdu_vendedor = '{0}'", vendedor);
            var _lista = _erpBs.Consulta(str_query);

            if (!_lista.Vazia())
            {
                return _lista.Valor("cdu_instituto").ToString();
            }
            else
            {
                return "";
            }

        }

        #endregion

        #region Documentos de Liquidacao

        public string DocExtactoContasToPDF(string cliente)
        {

            try
            {
                var objConfPlat = new StdBSConfApl();

                objConfPlat.AbvtApl = "GCP";
                objConfPlat.Instancia = "DEFAULT";
                objConfPlat.Utilizador = _erpBs.Contexto.UtilizadorActual;
                objConfPlat.PwdUtilizador = _erpBs.Contexto.PasswordUtilizadorActual;
                objConfPlat.LicVersaoMinima = "9.00";

                StdBETransaccao objTrans = new StdBETransaccao();
                StdPlatBS _plataforma = new StdPlatBS();

                _plataforma.AbrePlataformaEmpresa(_erpBs.Contexto.CodEmp, null, objConfPlat, _erpBs.Contexto.TipoPlataforma, "");


                string _CurrentDocId;
                string folder;

               
                _CurrentDocId = String.Format("{0}_{1}_{2}", "Pendentes",cliente, DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss"));

                folder = String.Format(@"{0}{1}\", @"C:\CRM\wwwroot\Ficheiros\Clientes\", _erpBs.Contexto.CodEmp);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string sFileName = String.Format("{0}{1}.pdf", folder, _CurrentDocId);

                

                if (!File.Exists(sFileName))
                {
                    _plataforma.Mapas.Inicializar("GCP");
                    _plataforma.Contexto.Erp.set_Inicializado(true);

                    // Inicialização dos parametros do report
                    //string inicializaParametros = "NumberVar TipoDesc;NumberVar RegimeIva;NumberVar DecQde;NumberVar DecPrecUnit;StringVar MotivoIsencao;StringVar PRI_TextoCertificacao;";

                    //inicializaParametros += String.Format("PRI_TextoCertificacao:='{0}'", _erpBs.Comercial.Vendas.DevolveTextoAssinaturaDoc("FA", "2015", 1, "000"));


                    // Dados da empresa para colocar no mapa
                    string dadosEmpresa = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;";
                    dadosEmpresa += String.Format("PRI_IDNome:='{0}';", _erpBs.Contexto.IDNome);
                    dadosEmpresa += String.Format("PRI_IDMorada:='{0}';", _erpBs.Contexto.IDMorada);
                    dadosEmpresa += String.Format("PRI_IDLocalidade:='{0}';", _erpBs.Contexto.IDLocalidade);
                    dadosEmpresa += String.Format("CodPostal:='{0}';", _erpBs.Contexto.IDCodPostal);
                    dadosEmpresa += String.Format("PRI_IDTelefone:='{0}';", _erpBs.Contexto.IDTelefone);
                    dadosEmpresa += String.Format("Fax:='{0}';", _erpBs.Contexto.IDFax);
                    dadosEmpresa += String.Format("PRI_IFNIF:='{0}';", _erpBs.Contexto.IFNIF);
                    dadosEmpresa += String.Format("PRI_IDEmail:='{0}';", "cmelo@accsys.co.mz");
                    dadosEmpresa += String.Format("Conservatoria:='{0}';", _erpBs.Contexto.ICConservatoria);
                    dadosEmpresa += String.Format("Matricula:='{0}';", _erpBs.Contexto.ICMatricula);
                    dadosEmpresa += String.Format("MoedaCapitalSocial:='{0}';", _erpBs.Contexto.ICMoedaCapSocial);


                    // filtro para o documento a imprimir
                    // string sFiltro = String.Format("{{CabecDoc.Serie}}='{0}' and {{CabecDoc.TipoDoc}}='{1}' and {{CabecDoc.NumDoc}}={2} ", 2015,"FA",1);

                    // Inicialização dos parametros do report
                    string inicializaParametros = "NumberVar TipoDesc;NumberVar RegimeIva;NumberVar DecQde;NumberVar DecPrecUnit;StringVar MotivoIsencao;StringVar PRI_TextoCertificacao;";
                    inicializaParametros += "TipoDesc:=" + 1 + ";RegimeIva:=3;DecQde:=1;DecPrecUnit:=" + 2 + ";MotivoIsencao:=' ';";
                    //inicializaParametros += String.Format("PRI_TextoCertificacao:='{0}'", _erpBs.Comercial.Vendas.DevolveTextoAssinaturaDoc(tipodoc, serie, numdoc, "000"));

                    // Dados da empresa para colocar no mapa
                    //string dadosEmpresa = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;";
                    //dadosEmpresa += String.Format("Nome:='{0}';", _erpBs.Contexto.IDNome);
                    //dadosEmpresa += String.Format("Localidade:='{0}';", _erpBs.Contexto.IDLocalidade);
                    //dadosEmpresa += String.Format("CodPostal:='{0}';", _erpBs.Contexto.IDCodPostal);
                    //dadosEmpresa += String.Format("Telefone:='{0}';", _erpBs.Contexto.IDTelefone);
                    //dadosEmpresa += String.Format("Fax:='{0}';", _erpBs.Contexto.IDFax);
                    //dadosEmpresa += String.Format("Contribuinte:='{0}';", _erpBs.Contexto.IFNIF);
                    //dadosEmpresa += String.Format("CapitalSocial:='{0}';", _erpBs.Contexto.ICCapitalSocial);
                    //dadosEmpresa += String.Format("Conservatoria:='{0}';", _erpBs.Contexto.ICConservatoria);
                    //dadosEmpresa += String.Format("Matricula:='{0}';", _erpBs.Contexto.ICMatricula);
                    //dadosEmpresa += String.Format("MoedaCapitalSocial:='{0}';", _erpBs.Contexto.ICMoedaCapSocial);


                    string sFiltro = "{Pendentes.Entidade}='" + cliente + "' and {Pendentes.TipoEntidade}='C'";
                    //string sFiltro = "";
                    //string sMapa = "GCPVLS02";
                    string sMapa =  "PENS02";

                    bool bMapaSistema;

                    try
                    {
                        bMapaSistema = (int)_plataforma.Administrador.Consulta(String.Format("select custom from mapas where mapa='{0}'", sMapa)).Valor(0) == 0;
                    }
                    catch
                    {
                        bMapaSistema = false;
                    }

                    // formulas para o mapa
                    _plataforma.Mapas.AddFormula("InicializaParamentros", inicializaParametros, true);
                    _plataforma.Mapas.AddFormula("DadosEmpresa", dadosEmpresa, true);
                    
                    // Propriedades para o output dos mapas
                    _plataforma.Mapas.set_Destino(CRPEExportDestino.edFicheiro);
                    _plataforma.Mapas.SetFileProp(CRPEExportFormat.efPdf, sFileName);

                    // Imprime a listagem (Gera o PDF)
                    short a = _plataforma.Mapas.ImprimeListagem(sMapa, "Extrato Pendentes", "P", 1, "S", sFiltro, CRPESentidoOrdenacao.soNenhuma, false, bMapaSistema,"", false, 0,false);

                    if (File.Exists(sFileName))
                    {
                        return (sFileName);
                    }
                    else
                    {
                        return "";
                    }
                }
                else return (sFileName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return ex.Message;
            }

        }

        public string DocFacturaToPDF(string cliente,string serie, string tipodoc,int numdoc)
        {
            try
            {
                var objConfPlat = new StdBSConfApl();

                objConfPlat.AbvtApl = "GCP";
                objConfPlat.Instancia = "DEFAULT";
                objConfPlat.Utilizador = _erpBs.Contexto.UtilizadorActual;
                objConfPlat.PwdUtilizador = _erpBs.Contexto.PasswordUtilizadorActual;
                objConfPlat.LicVersaoMinima = "9.00";

                StdBETransaccao objTrans = new StdBETransaccao();
                StdPlatBS _plataforma = new StdPlatBS();

                _plataforma.AbrePlataformaEmpresa(_erpBs.Contexto.CodEmp, null, objConfPlat, _erpBs.Contexto.TipoPlataforma, "");


                string _CurrentDocId;
                string folder;
                

                _CurrentDocId = String.Format("{0}_{1}_{2}", "Pendentes", cliente, DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss"));

                folder = String.Format(@"{0}{1}\", @"c:\Emails\", _erpBs.Contexto.CodEmp);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string sFileName = String.Format("{0}{1}.pdf", folder, _CurrentDocId);



                if (!File.Exists(sFileName))
                {

                    _plataforma.Mapas.Inicializar("GCP");
                    _plataforma.Contexto.Erp.set_Inicializado(true);

                    // Inicialização dos parametros do report
                    string inicializaParametros = "NumberVar TipoDesc;NumberVar RegimeIva;NumberVar DecQde;NumberVar DecPrecUnit;StringVar MotivoIsencao;StringVar PRI_TextoCertificacao;";
                    inicializaParametros += "TipoDesc:=" + 1 + ";RegimeIva:=3;DecQde:=1;DecPrecUnit:=" + 2 + ";MotivoIsencao:=' ';";
                    inicializaParametros += String.Format("PRI_TextoCertificacao:='{0}'", _erpBs.Comercial.Vendas.DevolveTextoAssinaturaDoc(tipodoc, serie, numdoc,"000"));

                    // Dados da empresa para colocar no mapa
                    string dadosEmpresa = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;";
                    dadosEmpresa += String.Format("Nome:='{0}';", _erpBs.Contexto.IDNome);
                    dadosEmpresa += String.Format("Localidade:='{0}';", _erpBs.Contexto.IDLocalidade);
                    dadosEmpresa += String.Format("CodPostal:='{0}';", _erpBs.Contexto.IDCodPostal);
                    dadosEmpresa += String.Format("Telefone:='{0}';", _erpBs.Contexto.IDTelefone);
                    dadosEmpresa += String.Format("Fax:='{0}';", _erpBs.Contexto.IDFax);
                    dadosEmpresa += String.Format("Contribuinte:='{0}';", _erpBs.Contexto.IFNIF);
                    dadosEmpresa += String.Format("CapitalSocial:='{0}';", _erpBs.Contexto.ICCapitalSocial);
                    dadosEmpresa += String.Format("Conservatoria:='{0}';", _erpBs.Contexto.ICConservatoria);
                    dadosEmpresa += String.Format("Matricula:='{0}';", _erpBs.Contexto.ICMatricula);
                    dadosEmpresa += String.Format("MoedaCapitalSocial:='{0}';", _erpBs.Contexto.ICMoedaCapSocial);

                    // filtro para o documento a imprimir
                    string sFiltro = String.Format("{{Cabecdoc.Serie}}='{0}' and {{cabecdoc.tipodoc}}='{1}' and {{cabecdoc.numdoc}}={2}", serie, tipodoc, numdoc);

                    // Modelo definido para a serie
                    string sMapa = _erpBs.Comercial.Series.DaConfig("V", tipodoc, serie);
                    bool bMapaSistema;
                    try
                    {
                        bMapaSistema = (int)_plataforma.Administrador.Consulta(String.Format("select custom from mapas where mapa='{0}'", sMapa)).Valor(0) == 0;
                    }
                    catch
                    {
                        bMapaSistema = false;
                    }
                    
                    // formulas para o mapa
                    _plataforma.Mapas.AddFormula("InicializaParamentros", inicializaParametros, true);
                    _plataforma.Mapas.AddFormula("DadosEmpresa", dadosEmpresa, true);

                    // Propriedades para o output dos mapas
                    _plataforma.Mapas.set_Destino(CRPEExportDestino.edFicheiro);
                    _plataforma.Mapas.SetFileProp(CRPEExportFormat.efPdf, sFileName);

                    // Imprime a listagem (Gera o PDF)
                    //short a = _plataforma.Mapas.ImprimeListagem(sMapa, "Extrato Pendentes", "P", 1, "N", sFiltro, CRPESentidoOrdenacao.soNenhuma, false, bMapaSistema, "", false, 0, false);
                    short a = _plataforma.Mapas.ImprimeListagem(sMapa, "FACTURA", "P", 1, "S", sFiltro, CRPESentidoOrdenacao.soNenhuma, false, bMapaSistema, "", false, 0, false);
                    if (File.Exists(sFileName))
                    {
                        return (sFileName);
                    }
                    else
                    {
                        return "";
                    }
                }
                else return (sFileName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return ex.Message;
            }

        }

        public GcpBEDocumentoLiq CriaCabecalhoLiquidacao(String TipoEntidade, String Entidade, String TipoDocLiq, DateTime Data, String ReferenciaMB, String IdTransBMEPS, String FicheiroBMEPS,
            String Instituto, string ContaBancaria = "", string MovimentoBancario = "")
        {
            GcpBEDocumentoLiq objDocLiquidacao;

            try
            {
                objDocLiquidacao = new GcpBEDocumentoLiq();
                objDocLiquidacao.set_TipoEntidade(TipoEntidade);
                objDocLiquidacao.set_Entidade(Entidade);

                objDocLiquidacao.set_Tipodoc(TipoDocLiq);
                objDocLiquidacao.set_Serie(_erpBs.Comercial.Series.DaSerieDefeito("M", TipoDocLiq, Data));

                objDocLiquidacao.set_Moeda(_erpBs.Contexto.MoedaBase);
                objDocLiquidacao.set_Utilizador(_erpBs.Contexto.UtilizadorActual);

                objDocLiquidacao.set_DataIntroducao(DateTime.Now);


                objDocLiquidacao.get_CamposUtil().get_Item("CDU_Referencia").Valor = ReferenciaMB;
                objDocLiquidacao.get_CamposUtil().get_Item("CDU_IDTransBMEPS").Valor = IdTransBMEPS;
                objDocLiquidacao.get_CamposUtil().get_Item("CDU_FicheiroBMEPS").Valor = FicheiroBMEPS;

                _erpBs.Comercial.Liquidacoes.PreencheDadosRelacionados(objDocLiquidacao);

                objDocLiquidacao.set_DataDoc(Data);

                objDocLiquidacao.set_ModoPag(MovimentoBancario);
                objDocLiquidacao.set_ContaBancaria(ContaBancaria);

            }
            catch
            {
                objDocLiquidacao = null;
            }


            return objDocLiquidacao;

        }

        public bool AdiccionaPendente(GcpBEDocumentoLiq docLiquidacao, GcpBEPendente pendente, double valorLiq)
        {
            
            string strEstadoLiq;
            if (docLiquidacao != null && valorLiq > 0)
            {
                strEstadoLiq = _erpBs.Comercial.TabVendas.DaValorAtributo(pendente.get_Tipodoc(), "Estado").ToString();
                //Adiciona o documento de compra ao documento de liquidação
                _erpBs.Comercial.Liquidacoes.AdicionaLinha(docLiquidacao, pendente.get_Filial()
                    , pendente.get_Modulo(), pendente.get_Tipodoc(), pendente.get_Serie(),
                    pendente.get_NumDocInt(), pendente.get_NumPrestacao(), strEstadoLiq, pendente.get_NumTransferencia(),
                    valorLiq);
                return true;
            }
            else
                return false;
        }

        public PrimaveraResultStructure GravaDocumentoLiquidacao(GcpBEDocumentoLiq docLiquidacao)
        {
            string strErro = "", strAvisos = "";
            PrimaveraResultStructure result;

            //Actualiza a Liquidação
            if (_erpBs.Comercial.Liquidacoes.ValidaActualizacao(docLiquidacao, strErro))
            {
                _erpBs.Comercial.Liquidacoes.Actualiza(docLiquidacao, strAvisos);
                return result = new PrimaveraResultStructure()
                {
                    codigo = 0,
                    descricao = string.Format("- Doc. Liquidação criado com Sucesso (Entidade: {0} ) - {1} {2}/{3} {4}",
                    docLiquidacao.get_Entidade(), docLiquidacao.get_Tipodoc(), docLiquidacao.get_NumDoc(), docLiquidacao.get_Serie(),
                    strAvisos == "" ? strAvisos : "-Aviso: " + strAvisos)
                };
            }
            else
                return result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = string.Format("Erro ao gravar documento Liquidação (Entidade: {0}) devido ao erro {1} ", docLiquidacao.get_Entidade(), strErro)
                };
        }

        public PrimaveraResultStructure GravaLigacaoBancos(string TipoEntidade, string Entidade, string Moeda, double Valor, DateTime DataDoc, string IdDoc, string Instituto, string RefATM,
            string strTipoDocTes, string strContaBancaria, string strMovBancario, bool DocumentoAdiantamento = false)
        {
            PrimaveraResultStructure result;

            try
            {
                string strSerieDocTes, strErro = "", strAvisos = "";


                GcpBEDocumentoTesouraria documentoTesouraria = new GcpBEDocumentoTesouraria();
                strSerieDocTes = _erpBs.Comercial.Series.DaSerieDefeito("B", strTipoDocTes);

                //'SE O DOCUMENTO DE LOGISTICA FOR UMA ADIANTAMENTO
                if (DocumentoAdiantamento)
                    Valor = Math.Abs(Valor);


                documentoTesouraria.set_EmModoEdicao(false);
                documentoTesouraria.set_ModuloOrigem("M");
                documentoTesouraria.set_Filial("000");
                documentoTesouraria.set_TipoLancamento("000");
                documentoTesouraria.set_Tipodoc(strTipoDocTes);
                documentoTesouraria.set_Serie(strSerieDocTes);
                documentoTesouraria.set_Data(DataDoc);
                documentoTesouraria.set_TipoEntidade(TipoEntidade);
                documentoTesouraria.set_Entidade(Entidade);
                documentoTesouraria.set_ContaOrigem(strContaBancaria);
                documentoTesouraria.set_Moeda(Moeda);
                documentoTesouraria.set_Cambio(_erpBs.Contexto.MBaseCambioCompra);
                documentoTesouraria.set_CambioMBase(_erpBs.Contexto.MBaseCambioCompra);
                documentoTesouraria.set_CambioMAlt(_erpBs.Contexto.MAltCambioCompra);
                documentoTesouraria.set_IdDocOrigem(IdDoc);
                documentoTesouraria.set_AgrupaMovimentos(false);

                _erpBs.Comercial.Tesouraria.AdicionaLinha(documentoTesouraria, strMovBancario, strContaBancaria, _erpBs.Contexto.MoedaBase, Valor, TipoEntidade, Entidade);

                var linhas = documentoTesouraria.get_Linhas();
                int count = 0;
                foreach (GcpBELinhaDocTesouraria linha in linhas)
                {
                    if (count == 0)
                    {
                        linha.set_DataMovimento(DataDoc.ToString());
                        linha.set_DataValor(DataDoc.ToString());
                        linha.set_Cambio(_erpBs.Contexto.MBaseCambioCompra);
                        linha.set_CambioMBase(_erpBs.Contexto.MBaseCambioCompra);
                        linha.set_CambioMAlt(_erpBs.Contexto.MAltCambioCompra);
                        linha.set_Descricao("Ref. ATM " + RefATM);

                    }
                    count++;
                }


                _erpBs.Comercial.Tesouraria.Actualiza(documentoTesouraria, strAvisos);

                return result = new PrimaveraResultStructure()
                {
                    codigo = 0,
                    descricao = string.Format("- Doc. Tesouraria criado com Sucesso (Entidade: {0} ) - {1} {2}/{3} {4}",
                    documentoTesouraria.get_Entidade(), documentoTesouraria.get_Tipodoc(), documentoTesouraria.get_NumDoc(), documentoTesouraria.get_Serie(),
                    strAvisos == "" ? strAvisos : "-Aviso: " + strAvisos)
                };


            }

            catch (Exception ex)
            {
                return result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = string.Format("- Erro ao gravar documento tesouraria: {0}", ex.Message)
                };
            }



        }

        public PrimaveraResultStructure GravaDocumentoTesouraria(string DocumentoTesouraria, string ContaBancaria, string MovimentoBancario,
            string Rubrica, double Valor, DateTime Data, string FicheiroBMEPS, string RefATM)
        {
            string strSerieDocTes, strErro = "", strAvisos = "", strSQL = "";
            double dblValor;

            PrimaveraResultStructure result;

            GcpBEDocumentoTesouraria documentoTesouraria = new GcpBEDocumentoTesouraria();
            StdBELista lstLista;

            strSerieDocTes = _erpBs.Comercial.Series.DaSerieDefeito("B", DocumentoTesouraria);

            // VERIFICA SE AS TAXAS DO FICHEIRO JÁ FORAM INTEGRADAS
            strSQL = "SELECT TIPODOC, SERIE, NUMDOC, CONTAORIGEM FROM CabecTesouraria WHERE CDU_FicheiroBMEPS = '" + FicheiroBMEPS + "'";
            lstLista = _erpBs.Consulta(strSQL);

            //SE JÁ INTEGROU FICHEIRO, ENTÃO NÃO VOLTA A INTEGRAR
            if (lstLista.NumLinhas() > 0)
            {
                lstLista.Inicio();
                return new PrimaveraResultStructure
                {
                    codigo = 3,
                    descricao = "Aviso: Doc. Tesouraria com Taxas já existia (Conta: " + lstLista.Valor("CONTAORIGEM") + ") - " +
                        lstLista.Valor("TipoDoc") + " " + lstLista.Valor("SERIE") + "/" + lstLista.Valor("NUMDOC") + ""
                };
            }

            //SE NÃO EXISTE DOCUMENTO, ENTÃO CRIA NOVO DOCUMENTO

            documentoTesouraria.set_EmModoEdicao(false);
            documentoTesouraria.set_ModuloOrigem("B");
            documentoTesouraria.set_Filial("000");
            documentoTesouraria.set_TipoLancamento("000");
            documentoTesouraria.set_Tipodoc(DocumentoTesouraria);
            documentoTesouraria.set_Serie(strSerieDocTes);
            documentoTesouraria.set_Data(Data);

            documentoTesouraria.set_Moeda(_erpBs.Contexto.MoedaBase);
            documentoTesouraria.set_Cambio(_erpBs.Contexto.MBaseCambioCompra);
            documentoTesouraria.set_CambioMBase(_erpBs.Contexto.MBaseCambioCompra);
            documentoTesouraria.set_CambioMAlt(_erpBs.Contexto.MAltCambioCompra);
            documentoTesouraria.set_AgrupaMovimentos(false);
            documentoTesouraria.set_Observacoes("Criado na integração do Ficheiro BMEPS. " + Environment.NewLine + "Taxas do ficheiro '" + FicheiroBMEPS + "'.");
            documentoTesouraria.get_CamposUtil().get_Item("CDU_FicheiroBMEPS").Valor = FicheiroBMEPS;

            _erpBs.Comercial.Tesouraria.AdicionaLinha(documentoTesouraria, MovimentoBancario, ContaBancaria, documentoTesouraria.get_Moeda(), Valor, "", "", Rubrica);

            try
            {
                _erpBs.Comercial.Tesouraria.Actualiza(documentoTesouraria, strAvisos);

                return result = new PrimaveraResultStructure()
                {
                    codigo = 0,
                    descricao = string.Format("- Doc. Tesouraria criado com Sucesso (Taxas BMEPS) - {0} {1}/{2} {3}",
                        documentoTesouraria.get_Tipodoc(), documentoTesouraria.get_NumDoc(), documentoTesouraria.get_Serie(),
                        strAvisos == "" ? strAvisos : "-Aviso: " + strAvisos)
                };
            }
            catch (Exception ex)
            {
                return result = new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = string.Format("- Erro ao gravar documento tesouraria: {0}", ex.Message)
                };
            }



        }

        public PrimaveraResultStructure CriaDocumentoAdiantamento(string TipoEntidade, string Entidade, string TipoDoc, DateTime Data, double Valor, string ReferenciaMB, string IdTransBMEPS, string FicheiroBMEPS)
        {
            GCPBELinhaPendente objLinhaPendente;
            string strAvisos = "", strErros = "", strCondPag = "";

            DocAdiantamento = new GcpBEPendente();
            objLinhaPendente = new GCPBELinhaPendente();

            DocAdiantamento.set_Tipodoc(TipoDoc);
            DocAdiantamento.set_Serie(_erpBs.Comercial.Series.DaSerieDefeito("M", TipoDoc, Data));
            DocAdiantamento.set_TipoEntidade(TipoEntidade);
            DocAdiantamento.set_Entidade(Entidade);
            DocAdiantamento.set_Moeda(_erpBs.Contexto.MoedaBase);
            DocAdiantamento.set_Utilizador(_erpBs.Contexto.UtilizadorActual);

            DocAdiantamento.set_DataIntroducao(DateTime.Now);
            //DocAdiantamento.set_IDHistorico();

            _erpBs.Comercial.Pendentes.PreencheDadosRelacionados(DocAdiantamento);

            DocAdiantamento.set_DataDoc(Data);

            objLinhaPendente.set_Descricao("Ref. Banc.: " + ReferenciaMB);
            objLinhaPendente.set_Incidencia(Valor);
            objLinhaPendente.set_Total(objLinhaPendente.get_Incidencia());
            objLinhaPendente.set_PercIvaDedutivel(100);

            DocAdiantamento.set_ValorTotal(objLinhaPendente.get_Incidencia());
            DocAdiantamento.set_TotalIva(objLinhaPendente.get_ValorIva());
            DocAdiantamento.set_ValorPendente(objLinhaPendente.get_Incidencia());

            DocAdiantamento.get_CamposUtil().get_Item("CDU_Referencia").Valor = ReferenciaMB;
            DocAdiantamento.get_CamposUtil().get_Item("CDU_IDTransBMEPS").Valor = IdTransBMEPS;
            DocAdiantamento.get_CamposUtil().get_Item("CDU_FicheiroBMEPS").Valor = FicheiroBMEPS;

            try
            {
                _erpBs.Comercial.Pendentes.Actualiza(DocAdiantamento, strAvisos);
                return new PrimaveraResultStructure()
                {
                    codigo = 0,
                    descricao = string.Format("- Doc. Adiantamento Criado Com Sucesso (Entidade: {0} ) - {1} {2}/{3} {4}",
                    DocAdiantamento.get_Entidade(), DocAdiantamento.get_Tipodoc(), DocAdiantamento.get_NumDoc(), DocAdiantamento.get_Serie(),
                    strAvisos == "" ? strAvisos : "-Aviso: " + strAvisos)
                };

            }
            catch (Exception ex)
            {
                return new PrimaveraResultStructure()
                {
                    codigo = 3,
                    descricao = string.Format("Erro ao gravar documento Liquidação (Entidade {0}) devido ao erro {1} ", DocAdiantamento.get_Entidade(), ex.Message)
                };
            }


        }

        #endregion
    }
}
