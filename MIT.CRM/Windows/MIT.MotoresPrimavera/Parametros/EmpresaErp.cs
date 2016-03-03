using Interop.AdmBE900;
using Interop.AdmBS900;
using Interop.ErpBS900;
using Interop.StdBE900;
using Microsoft.Win32;
using MIT.Data.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MIT.MotoresPrimavera.Parametros
{
    public class EmpresaErp
    {

        public ErpBS _erpBs;
        private int tipoPlataforma { get; set; }

        private string codUsuario { get; set; }

        private string codEmpresa { get; set; }

        private string password { get; set; }

        public EmpresaErp()
        {
            // Resolução das Assemblies
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

        /// <summary>
        /// Metodo para inicializar o motor do primavera
        /// </summary>
        /// <param name="tipoPlataforma"> 0 - Executiva, 1- Profissional</param>
        /// <param name="codEmpresa"></param>
        /// <param name="codUsuario"></param>
        /// <param name="password"></param>
        /// <remarks></remarks>
        public PrimaveraResultStructure AbreEmpresaPrimavera(int tipoPlataforma, string codEmpresa, string codUsuario, string password)
        {
            PrimaveraResultStructure result = new PrimaveraResultStructure();

            try
            {
                this.tipoPlataforma = tipoPlataforma;
                this.codUsuario = codUsuario;
                this.codEmpresa = codEmpresa;
                this.password = password;

                if (_erpBs == null)
                {
                    _erpBs = new ErpBS();
                }
                else
                {
                    _erpBs.FechaEmpresaTrabalho();
                }

                _erpBs.AbreEmpresaTrabalho(tipoPlataforma == 0 ? EnumTipoPlataforma.tpEmpresarial : EnumTipoPlataforma.tpProfissional,
                    codEmpresa, codUsuario, password, null, "DEFAULT", true);

                result.codigo = 0;
                result.descricao = string.Format("Empresa {0} - {1} Aberta Com Sucesso", _erpBs.Contexto.CodEmp, _erpBs.Contexto.IDNome);
                Console.WriteLine(String.Format("[{0}] Empresa {1} - {2} Aberta Com Sucesso", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), _erpBs.Contexto.CodEmp, _erpBs.Contexto.IDNome));

                return result;
            }
            catch (Exception ex)
            {
                result.codigo = 3;
                result.descricao = ex.Message;
                Console.WriteLine(String.Format("[{0}] Erro a abrir a Empresa {1} - {2} devido a: {3}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), _erpBs.Contexto.CodEmp, _erpBs.Contexto.IDNome, ex.Message));

                return result;
            }

        }

        /// <summary>
        /// Valida se empresa primavera se encontra aberta
        /// </summary>
        /// <returns></returns>
        public bool EmpresaPrimaveraAberta()
        {
            if (_erpBs == null)
            {
                return false;
            }
            else
            {
                return _erpBs.Contexto.EmpresaAberta;
            }
        }

        /// <summary>
        /// Inicialização das Transações no Erp
        /// </summary>
        public void IniciaTransacao()
        {
            _erpBs.IniciaTransaccao();
            Console.WriteLine(String.Format("[{0}] Inicia a Transação da Empresa {1} - {2}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), _erpBs.Contexto.CodEmp, _erpBs.Contexto.IDNome));

        }

        /// <summary>
        /// Finialização das Transações no Erp
        /// </summary>
        public void TerminaTransacao()
        {
            _erpBs.TerminaTransaccao();
            Console.WriteLine(String.Format("[{0}] Termina a Transação da Empresa {1} - {2}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), _erpBs.Contexto.CodEmp, _erpBs.Contexto.IDNome));

        }

        /// <summary>
        /// Roolback das transações
        /// </summary>
        public void DesfazTransacao()
        {
            try
            {
                _erpBs.DesfazTransaccao();

            }
            catch { }


        }

        /// <summary>
        /// Executa Query na empresa primavera
        /// </summary>
        /// <param name="str_query"></param>
        public string Executa_Query_Insert_Update(string str_query)
        {
            try
            {
                object a;
                //_erpBs.DSO.BDAPL.Execute(str_query, out a, -1);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }

    public class AdministradorErp
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Instance { get; set; }
        public string Type { get; set; }
        public string Backupsdir { get; set; }

        private AdmBS _admBs { get; set; }

        public AdministradorErp(string user, string password, string instance = "DEFAULT", string type = "Executive", string backupsdir = null)
        {
            User = user;
            Password = password;
            Instance = instance;
            Type = type;
            if (backupsdir != null) Backupsdir = backupsdir;

            // Resolução das Assemblies
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

        public List<Empresa> listaEmpresas(string categoriaEmpresa="")
        {
            init();
            string categoria = "";
            List<Empresa> lstempresa = new List<Empresa>();
            var empresasPri = _admBs.Empresas.ListaEmpresas(true);
            var listaCategoria = _admBs.Consulta( String.Format( "select * from CategoriasEmpresas where Descricao = '{0}'",categoriaEmpresa));

            if (listaCategoria.Vazia () == false && categoriaEmpresa == "")
            {
                categoria = listaCategoria.Valor("Categoria");

            }
            

            foreach (AdmBEEmpresa temp in empresasPri)
            {
                if (categoria == "")
                {
                    lstempresa.Add(new Empresa()
                    {
                        codigo = temp.get_Identificador(),
                        codEmpresaPri = temp.get_Identificador(),
                        nome = temp.get_IDNome(),
                        nomeEmpresa = temp.get_IDNome(),
                        nuit = temp.get_IFNIF(),
                        nuitEmpresa =temp.get_IFNIF(),
                        categoria = temp.get_Categoria(),
                        empresaPrimavera = true
                    });
                }
                else
                {
                    if (temp.get_Categoria() == categoria)
                        lstempresa.Add(new Empresa()
                        {
                            codigo = temp.get_Identificador(),
                            codEmpresaPri = temp.get_Identificador(),
                            nome = temp.get_IDNome(),
                            nomeEmpresa = temp.get_IDNome(),
                            nuit = temp.get_IFNIF(),
                            nuitEmpresa = temp.get_IFNIF(),
                            categoria = temp.get_Categoria(),
                            empresaPrimavera = true
                        });

                }

                
            }
            return lstempresa;
        }
        
        public void init()
        {
            if (_admBs == null)
            {
                _admBs = new AdmBS();
                StdBETransaccao objtrans = new StdBETransaccao();

                EnumTipoPlataforma tp = EnumTipoPlataforma.tpProfissional;
                if (Type.Equals("Executive"))
                    tp = EnumTipoPlataforma.tpEmpresarial;

                _admBs.AbrePRIEMPRE(ref tp, User, Password, ref objtrans, Instance);
            }
        }

        public void end()
        {
            _admBs.FechaPRIEMPRE();
            
        }

        public void cria_copia_seguranca(string database)
        {
            init();

            string bkpname = database + " backup";
            string bkpdescription = "Full backup for " + database;
            string dir = "";
            if (Backupsdir != null) dir = Backupsdir;

            DateTime now = DateTime.Now;
            string nowString = now.ToString("yyyyMMddhhmmss");
           
            string f = "Full_Backup_" + database + "_" + nowString;
             Console.WriteLine("Backup file: " + f);

            _admBs.BasesDados.CopiaSeguranca(ref database, ref bkpname, ref bkpdescription, ref dir, ref f);
            return;
        }
        public void reposicao_copia_seguranca(string database, string file)
        {
            init();
             Console.WriteLine("Reposicao de copia seguranca na base dados: " + database + "; com ficheiro: " + file);

            _admBs.BasesDados.ReposicaoCompletaCopiaSeguranca(ref database, ref file);
            return;
        }
        public void lista_backups()
        {
            init();
            string backupsdir = Backupsdir;
            if (backupsdir == null) backupsdir = _admBs.SQLServer.DirectoriaBackup();

            // Console.WriteLine( "backupsdir: " + backupsdir );

            DirectoryInfo dirInfo = new DirectoryInfo(backupsdir);
            FileInfo[] filenames = dirInfo.GetFiles("*.*");

            // sort file names
            Array.Sort(filenames, (a, b) => DateTime.Compare(b.LastWriteTime, a.LastWriteTime));
            foreach (FileInfo fi in filenames)
            {
                 Console.WriteLine("{0};{1};{2};{3};{4}", fi.Name, fi.CreationTime, fi.LastWriteTime, fi.Length, fi.FullName);
                // TODO read backup file for get more info
                //read_backupfile(fi);
            }
            return;
        }
        public void lista_basesdados()
        {
            init();
            AdmBEBasesDados abds = _admBs.BasesDados.ListaBasesDados();
            foreach (AdmBEBaseDados bd in abds)
            {
                 Console.WriteLine("name: " + bd.get_Nome());
            }
            return;
        }
        public void config_backups()
        {
            init();
            string bkpdir = _admBs.SQLServer.DirectoriaBackup();

             Console.WriteLine("DirectoriaBackup: " + bkpdir);
            return;
        }
        public void lista_planos_copiaseguranca()
        {
            init();
            AdmBEPlanosCopiasSeg lista = _admBs.PlanosCopiasSeguranca.ListaPlanos();
            foreach (AdmBEPlanoCopiasSeg pl in lista)
            {
                string id = pl.get_Id();
                 Console.WriteLine("PlanoCopiasSeg_id: " + id);

                string xmlPlano = pl.get_Plano();
                // Console.WriteLine(" xml: " + xmlPlano);

                XmlReader xmlreader = XmlReader.Create(new StringReader(xmlPlano));

                //xmlreader.Read();
                xmlreader.ReadToFollowing("backupPlan");
                 Console.WriteLine(" id: " + xmlreader.GetAttribute("id"));
                 Console.WriteLine(" name: " + xmlreader.GetAttribute("name"));
                 Console.WriteLine(" verify: " + xmlreader.GetAttribute("verify"));
                 Console.WriteLine(" incremental: " + xmlreader.GetAttribute("incremental"));
                 Console.WriteLine(" overwrite: " + xmlreader.GetAttribute("overwrite"));
                 Console.WriteLine(" destination: " + xmlreader.GetAttribute("destination"));
                // Console.WriteLine(" schedule: " + xmlreader.GetAttribute("schedule"));
                 Console.WriteLine(" date: " + xmlreader.GetAttribute("date"));
                 Console.WriteLine(" lastExecution: " + xmlreader.GetAttribute("lastExecution"));
                 Console.WriteLine(" nextExecution: " + xmlreader.GetAttribute("nextExecution"));

                string schedule_id = xmlreader.GetAttribute("schedule");
                 Console.WriteLine(" schedule id: " + schedule_id);
                AdmBECalendario pcal = _admBs.Calendario.Edita(schedule_id);
                // Console.WriteLine(" schedule_id: " + pcal.Id );
                 Console.WriteLine("  schedule_periodo: " + pcal.get_Periodo().ToString());

                xmlreader.ReadToFollowing("companies");
                while (xmlreader.ReadToFollowing("company"))
                {
                    xmlreader.ReadToFollowing("properties");
                     Console.WriteLine(" company_key: " + xmlreader.GetAttribute("key"));
                     Console.WriteLine(" company_name: " + xmlreader.GetAttribute("name"));
                }
            }
        }
        public void insere_plano_copiaseguranca(string name, string verify, string incremental, string overwrite, string companiesByComma, string periodo)
        {
            init();
            string newid = System.Guid.NewGuid().ToString();

            AdmBEPlanoCopiasSeg newPC = new AdmBEPlanoCopiasSeg();
            AdmBECalendario objCal = new AdmBECalendario();

            newPC.set_Id(newid);
            objCal.Id = newid;

            if (periodo.Equals("mensal"))
                objCal.set_Periodo(EnumPeriodoExecucao.prMensal);
            else if (periodo.Equals("semanal"))
                objCal.set_Periodo(EnumPeriodoExecucao.prSemanal);
            else
                objCal.set_Periodo(EnumPeriodoExecucao.prDiario);

            // Exec 23h (TODO change this by arg)
            objCal.set_FreqUnicaHora(new DateTime(1900, 1, 1, 23, 0, 0));

            _admBs.Calendario.Actualiza(objCal);

            StringWriter stringwriter = new StringWriter();

            XmlWriterSettings xmlsettings = new XmlWriterSettings();
            xmlsettings.OmitXmlDeclaration = true;
            xmlsettings.Indent = false;
            XmlWriter xmlwriter = XmlWriter.Create(stringwriter, xmlsettings);

            xmlwriter.WriteStartElement("backupPlan");
            xmlwriter.WriteAttributeString("id", "{" + newPC.get_Id() + "}");

            xmlwriter.WriteAttributeString("name", name);
            xmlwriter.WriteAttributeString("verify", verify);
            xmlwriter.WriteAttributeString("incremental", incremental);
            xmlwriter.WriteAttributeString("overwrite", overwrite);

            string backupsdir = Backupsdir;
            if (backupsdir == null) backupsdir = _admBs.SQLServer.DirectoriaBackup();

            xmlwriter.WriteAttributeString("destination", backupsdir);
            xmlwriter.WriteAttributeString("schedule", "{" + objCal.Id + "}");

            // Console.WriteLine(" date: " + xmlreader.GetAttribute("date"));
            DateTime datenow = DateTime.Now;
            xmlwriter.WriteAttributeString("date", datenow.ToString("dd-MM-yyyy HH:mm:ss"));

            // Console.WriteLine(" lastExecution: " + xmlreader.GetAttribute("lastExecution"));
            DateTime lastdate = objCal.UltimaOcorrencia;
            xmlwriter.WriteAttributeString("lastExecution", lastdate.ToString("dd-MM-yyyy HH:mm:ss"));

            // Console.WriteLine(" nextExecution: " + xmlreader.GetAttribute("nextExecution"));
            //DateTime nextdate = new DateTime(datenow.Year,datenow.Month,datenow.Day);
            DateTime nextdate = objCal.ProximaOcorrencia;
            xmlwriter.WriteAttributeString("nextExecution", nextdate.ToString("dd-MM-yyyy HH:mm:ss"));

            // companies
            xmlwriter.WriteStartElement("companies");

            //string companiesByComma = "DEMO,PRIDEMO;DEMOX,PRIDEMOX";
            string[] companies = companiesByComma.Split(new char[] { ';' });

            foreach (string company in companies)
            {
                string[] cfields = company.Split(new char[] { ',' });
                if (cfields.Length == 2)
                {
                    xmlwriter.WriteStartElement("company");

                    xmlwriter.WriteStartElement("properties");
                    xmlwriter.WriteAttributeString("key", cfields[0]);
                    xmlwriter.WriteAttributeString("name", cfields[1]);
                    xmlwriter.WriteEndElement(); // properties

                    xmlwriter.WriteEndElement(); // company
                }
            }

            xmlwriter.WriteEndElement(); // companies

            xmlwriter.WriteEndElement(); // backupPlan

            xmlwriter.Flush();

            // Console.WriteLine("xml string: " + stringwriter.ToString());

            //string strBackupPlan = "<backupPlan id=\"" + newpc_id + "\" name=\"teste all\" verify=\"False\" incremental=\"False\" overwrite=\"False\" destination=\"C:\\PROGRAM FILES\\MICROSOFT SQL SERVER\\MSSQL10.PRIMAVERA\\MSSQL\\BACKUP\\\" schedule=\"" + newpc_id + "\" date=\"" + DateTime.Now.ToString() + "\" lastExecution=\"undefined\" nextExecution=\"" + DateTime.Now.ToString("dd-MM-yyyy") + " 23:00:00\"><companies><company><properties key=\"OBIADM\" name=\"BIADM\"/></company><company><properties key=\"EDEMO\" name=\"PRIDEMO\"/></company><company><properties key=\"EDEMOX\" name=\"PRIDEMOX\"/></company><company><properties key=\"OPRIEMPRE\" name=\"PRIEMPRE\"/></company></companies></backupPlan>";
            newPC.set_Plano(stringwriter.ToString());

            _admBs.PlanosCopiasSeguranca.Actualiza(newPC);
            _admBs.PlanosCopiasSeguranca.ListaPlanos().Insere(newPC);

             Console.WriteLine(" Plano de Copia Seguranca inserido com id: " + newPC.get_Id());
        }
        public void remove_plano_copiaseguranca(string id)
        {
            init();
            _admBs.Calendario.Remove(id);
            _admBs.PlanosCopiasSeguranca.Remove(id);

             Console.WriteLine(" Plano de Copia Seguranca com id: " + id + " removido.");
        }
        public void lista_empresas()
        {
            init();
            AdmBEEmpresas empresas = _admBs.Empresas.ListaEmpresas(true);
            foreach (AdmBEEmpresa e in empresas)
            {
                 Console.WriteLine("name: " + e.get_Identificador() + " description: " + e.get_IDNome());
            }
            return;
        }
        public void lista_utilizadores()
        {
            init();
            StdBELista uList = _admBs.Consulta("SELECT * FROM utilizadores");

            uList.Inicio();
            while (!uList.NoFim())
            {

                CultureInfo idioma = CultureInfo.GetCultureInfo(uList.Valor("Idioma"));

                 Console.WriteLine("Utilizador: " + uList.Valor("Codigo"));
                 Console.WriteLine(" Codigo: " + uList.Valor("Codigo"));
                 Console.WriteLine(" Nome: " + uList.Valor("Nome"));
                 Console.WriteLine(" Email: " + uList.Valor("Email"));
                 Console.WriteLine(" Activo: " + uList.Valor("Activo"));
                 Console.WriteLine(" Administrador: " + uList.Valor("Administrador"));
                 Console.WriteLine(" PerfilSugerido: " + uList.Valor("PerfilSugerido"));
                 Console.WriteLine(" NaoPodeAlterarPwd: " + uList.Valor("NaoPodeAlterarPwd"));
                 Console.WriteLine(" Idioma: " + idioma);
                 Console.WriteLine(" LoginWindows: " + uList.Valor("LoginWindows"));
                 Console.WriteLine(" Telemovel: " + uList.Valor("Telemovel"));
                 Console.WriteLine(" Bloqueado: " + uList.Valor("Bloqueado"));
                 Console.WriteLine(" TentativasFalhadas: " + uList.Valor("TentativasFalhadas"));
                 Console.WriteLine(" AutenticacaoPersonalizada: " + uList.Valor("AutenticacaoPersonalizada"));
                 Console.WriteLine(" SuperAdministrador: " + uList.Valor("SuperAdministrador"));
                 Console.WriteLine(" Tecnico: " + uList.Valor("Tecnico"));

                uList.Seguinte();
            }
            return;
        }
        public void lista_perfis()
        {
            init();
            StdBELista pList = _admBs.Consulta("SELECT * FROM perfis");

            pList.Inicio();
            while (!pList.NoFim())
            {
                 Console.WriteLine("Perfil: " + pList.Valor("Codigo"));
                 Console.WriteLine(" Codigo: " + pList.Valor("Codigo"));
                 Console.WriteLine(" Nome: " + pList.Valor("Nome"));

                pList.Seguinte();
            }
            return;
        }
        public void lista_aplicacoes()
        {
            init();

            RegistryKey rk_LM = Registry.LocalMachine;

            string s_basepath = "SOFTWARE\\PRIMAVERA\\SGE800";
            if (Type.Equals("Executive"))
            {
                s_basepath = "SOFTWARE\\PRIMAVERA\\SGE800";
            }
            else
            {
                s_basepath = "SOFTWARE\\PRIMAVERA\\SGP800";
            }
            RegistryKey rk_PrimaveraDefault = rk_LM.OpenSubKey(s_basepath + "\\DEFAULT");
            string[] subkeys = rk_PrimaveraDefault.GetSubKeyNames();
            foreach (string key in subkeys)
            {
                RegistryKey rk_App = rk_PrimaveraDefault.OpenSubKey(key);
                if (rk_App != null)
                {
                    string nome = (string)rk_App.GetValue("NOME");
                    if ((nome != null) && (key.Length == 3))
                    {
                        string versao = (string)rk_App.GetValue("VERSAO");
                         Console.WriteLine("Aplicacao: " + key);
                         Console.WriteLine(" Codigo: " + key);
                         Console.WriteLine(" Nome: " + nome);
                         Console.WriteLine(" Versao: " + versao);
                    }
                }
            }
            return;
        }
        public void lista_utilizador_aplicacoes(string user)
        {
            init();
            StdBELista uaList = _admBs.Consulta("SELECT * FROM UtilizadoresAplicacoes WHERE Utilizador='" + user + "'");

            uaList.Inicio();
            while (!uaList.NoFim())
            {
                 Console.WriteLine("Aplicacao: " + uaList.Valor("Apl"));
                uaList.Seguinte();
            }
            return;
        }
        public void insere_utilizador_aplicacao(string user, string apl)
        {
            init();
            string sqlInsereUtilizadorAplicacao = "INSERT [UtilizadoresAplicacoes] ([Utilizador], [Apl]) VALUES (N'" + user + "',N'" + apl + "')";
            _admBs.SQLServer.ExecutaComando(sqlInsereUtilizadorAplicacao, "PRIEMPRE", false);
             Console.WriteLine("Insert utilizador '" + user + "' applicacao '" + apl + "' ok.");
            return;
        }
        public void remove_utilizador_aplicacao(string user, string apl)
        {
            init();
            string sqlRemoveUtilizadorAplicacao = "DELETE [UtilizadoresAplicacoes] WHERE [Utilizador] = '" + user + "' AND [Apl] = '" + apl + "'";
            _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizadorAplicacao, "PRIEMPRE", false);
             Console.WriteLine("Delete utilizador '" + user + "' applicacao '" + apl + "' ok.");
            return;
        }
        public bool actualiza_utilizador_aplicacoes(string user, string[] aplicacoes)
        {
            init();

            _admBs.IniciaTransaccao();
            try
            {
                string sqlRemoveUtilizadorAplicacoes = "DELETE [UtilizadoresAplicacoes] WHERE [Utilizador] = '" + user + "'";
                _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizadorAplicacoes, "PRIEMPRE", false);

                foreach (string apl in aplicacoes)
                {
                    string sqlInsereUtilizadorAplicacao = "INSERT [UtilizadoresAplicacoes] ([Utilizador], [Apl]) VALUES (N'" + user + "',N'" + apl + "')";
                    _admBs.SQLServer.ExecutaComando(sqlInsereUtilizadorAplicacao, "PRIEMPRE", false);
                }
            }
            catch (Exception e)
            {
                _admBs.DesfazTransaccao();
                 Console.WriteLine("Actualiza applicacoes do utilizador '" + user + "' falhou: {0} Exception caught.", e);
                return false;
            }
            _admBs.TerminaTransaccao();

             Console.WriteLine("Actualiza applicacoes do utilizador '" + user + "' ok.");
            return true;
        }
        public void lista_utilizador_permissoes(string user)
        {
            init();
            StdBELista upList = _admBs.Consulta("SELECT * FROM Permissoes WHERE Utilizador='" + user + "'");

            upList.Inicio();
            while (!upList.NoFim())
            {
                 Console.WriteLine("Permissao: ");
                 Console.WriteLine(" Perfil: " + upList.Valor("Perfil"));
                 Console.WriteLine(" Empresa: " + upList.Valor("Empresa"));
                upList.Seguinte();
            }
            return;
        }
        public void insere_utilizador_permissao(string user, string perfil, string empresa)
        {
            init();
            string sqlInsereUtilizadorPermissao = "INSERT [Permissoes] ([Utilizador], [Perfil], [Empresa]) VALUES (N'" + user + "',N'" + perfil + "',N'" + empresa + "')";
            _admBs.SQLServer.ExecutaComando(sqlInsereUtilizadorPermissao, "PRIEMPRE", false);
             Console.WriteLine("Insert utilizador '" + user + "' permissao do perfil '" + perfil + "' empresa '" + empresa + "' ok.");
            return;
        }
        public void remove_utilizador_permissao(string user, string perfil, string empresa)
        {
            init();
            string sqlRemoveUtilizadorPermissao = "DELETE [Permissoes] WHERE [Utilizador] = '" + user + "' AND [Perfil] = '" + perfil + "' AND [Empresa] = '" + empresa + "'";
            _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizadorPermissao, "PRIEMPRE", false);
             Console.WriteLine("Delete utilizador '" + user + "' permissao do perfil '" + perfil + "' empresa '" + empresa + "' ok.");
            return;
        }
        public bool actualiza_utilizador_permissoes(string user, string[][] permissoes)
        {
            init();

            _admBs.IniciaTransaccao();
            try
            {
                string sqlRemoveUtilizadorPermissoes = "DELETE [Permissoes] WHERE [Utilizador] = '" + user + "'";
                _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizadorPermissoes, "PRIEMPRE", false);


                for (int i = 0; i < permissoes.Length; i++)
                {
                    if (permissoes[i].Length == 2)
                    {
                        string perfil = permissoes[i][0];
                        string empresa = permissoes[i][1];
                        string sqlInsereUtilizadorPermissao = "INSERT [Permissoes] ([Utilizador], [Perfil], [Empresa]) VALUES (N'" + user + "',N'" + perfil + "',N'" + empresa + "')";
                        _admBs.SQLServer.ExecutaComando(sqlInsereUtilizadorPermissao, "PRIEMPRE", false);
                    }
                }
            }
            catch (Exception e)
            {
                _admBs.DesfazTransaccao();
                 Console.WriteLine("Actualiza permissoes do utilizador '" + user + "' falhou: {0} Exception caught.", e);
                return false;
            }
            _admBs.TerminaTransaccao();

             Console.WriteLine("Actualiza permissoes do utilizador '" + user + "' ok.");
            return true;
        }
        public void insere_utilizador(string codigo, string nome, string email, string password, string activo, string administrador, string perfilSugerido, string naoPodeAlterarPwd, string idioma, string loginWindows, string telemovel, string bloqueado, string tentativasFalhadas, string autenticacaoPersonalizada, string superAdministrador, string tecnico)
        {
            init();
            string sqlInsereUtilizador = "INSERT [Utilizadores] ([Codigo], [Nome], [Email], [Password], [Activo], [Administrador], [PerfilSugerido], [NaoPodeAlterarPwd], [Idioma], [LoginWindows], [Telemovel], [Bloqueado], [TentativasFalhadas], [AutenticacaoPersonalizada], [SuperAdministrador], [Tecnico]) VALUES (N'" + codigo + "',N'" + nome + "',N'" + email + "',N'" + password + "'," + activo + "," + administrador + ",N'" + perfilSugerido + "'," + naoPodeAlterarPwd + "," + idioma + ",N'" + loginWindows + "',N'" + telemovel + "'," + bloqueado + "," + tentativasFalhadas + "," + autenticacaoPersonalizada + "," + superAdministrador + "," + tecnico + ")";
            _admBs.SQLServer.ExecutaComando(sqlInsereUtilizador, "PRIEMPRE", false);
             Console.WriteLine("Insert user '" + codigo + "' ok.");
        }
        public void actualiza_utilizador(string codigo, string nome, string email, string password, string activo, string administrador, string perfilSugerido, string naoPodeAlterarPwd, string idioma, string loginWindows, string telemovel, string bloqueado, string tentativasFalhadas, string autenticacaoPersonalizada, string superAdministrador, string tecnico)
        {
            init();
            string sqlActualizaUtilizador = "UPDATE [Utilizadores] SET [Nome] = '" + nome + "', [Email] = '" + email + "', [Activo] = " + activo + ", [Administrador] = " + administrador + ", [PerfilSugerido] = '" + perfilSugerido + "', [NaoPodeAlterarPwd] = " + naoPodeAlterarPwd + ", [Idioma] = " + idioma + ", [LoginWindows] = '" + loginWindows + "', [Telemovel] = '" + telemovel + "', [Bloqueado] = " + bloqueado + ", [TentativasFalhadas] = " + tentativasFalhadas + ", [AutenticacaoPersonalizada] = " + autenticacaoPersonalizada + ", [SuperAdministrador] = " + superAdministrador + ", [Tecnico] = " + tecnico;
            if (password.Length > 0)
            {
                sqlActualizaUtilizador = sqlActualizaUtilizador + ", [Password] = '" + password + "'";
            }
            sqlActualizaUtilizador = sqlActualizaUtilizador + " WHERE [Codigo] = '" + codigo + "'";

            // Console.WriteLine(sqlActualizaUtilizador);

            _admBs.SQLServer.ExecutaComando(sqlActualizaUtilizador, "PRIEMPRE", false);
             Console.WriteLine("Update user '" + codigo + "' ok.");
        }
        public bool remove_utilizador(string codigo)
        {
            init();

            _admBs.IniciaTransaccao();
            try
            {
                string sqlRemoveUtilizadorAplicacoes = "DELETE [UtilizadoresAplicacoes] WHERE [Utilizador] = '" + codigo + "'";
                _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizadorAplicacoes, "PRIEMPRE", false);

                string sqlRemoveUtilizadorPermissao = "DELETE [Permissoes] WHERE [Utilizador] = '" + codigo + "'";
                _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizadorPermissao, "PRIEMPRE", false);

                string sqlRemoveUtilizador = "DELETE [Utilizadores] WHERE [Codigo] = '" + codigo + "'";
                _admBs.SQLServer.ExecutaComando(sqlRemoveUtilizador, "PRIEMPRE", false);
            }
            catch (Exception e)
            {
                _admBs.DesfazTransaccao();
                 Console.WriteLine("Actualiza applicacoes do utilizador '" + codigo + "' falhou: {0} Exception caught.", e);
                return false;
            }
            _admBs.TerminaTransaccao();
             Console.WriteLine("Delete user '" + codigo + "' ok.");

            return true;
        }
        public void info()
        {
            init();
            ErpBS motor = new ErpBS();

            bool _false = false;

             Console.WriteLine("License: " + !motor.Licenca.VersaoDemo);
             Console.WriteLine("Language: " + _admBs.Params.get_Idioma());
             Console.WriteLine("Seguranca Activa: " + _admBs.Params.get_SegurancaActiva());
             Console.WriteLine("Seguranca Pro Emp Activa: " + _admBs.Params.get_SegurancaPorEmpActiva());
             Console.WriteLine("Modo Seguranca: " + _admBs.Params.get_SegurancaActiva());
             //Console.WriteLine("N Postos: " + adm.Utilizadores.listPostos.ListaPostos(ref _false).NumItens);

            string backupsdir = Backupsdir;
            if (backupsdir == null) backupsdir = _admBs.SQLServer.DirectoriaBackup();

             Console.WriteLine("DirectoriaBackup: " + backupsdir);

            StdBELista uList = _admBs.Consulta("SELECT * FROM utilizadores");
             Console.WriteLine("N Utilizadores: " + uList.NumLinhas());

            uList.Inicio();
            while (!uList.NoFim())
            {
                 Console.WriteLine(" Utilizador: " + uList.Valor("Codigo") + ", " + uList.Valor("Nome"));
                uList.Seguinte();
            }

            StdBELista eList = _admBs.Consulta("SELECT * FROM empresas");
             Console.WriteLine("N Empresas: " + eList.NumLinhas());

            eList.Inicio();
            while (!eList.NoFim())
            {
                 Console.WriteLine(" Empresa: " + eList.Valor("Codigo") + ", " + eList.Valor("IDNome"));
                eList.Seguinte();
            }
            return;
        }
    }


    /// <summary>
    /// Classe Instancia para a gestão das versões do primavera e as suas respectivas assemblies
    /// </summary>
    public class Instancia
    {

        public int instancia = 1;
        public string empresa;
        public string usuario;
        public string password;
        public string instanciaSql;
        public string empresaSql;
        public string usuarioSql;
        public string passwordSql;
        public static string versaoErp = "V900";
        public const string pastaConfigV800 = "PRIMAVERA\\SG800";

        public const string pastaConfigV900 = "PRIMAVERA\\SG900";
        public object daConnectionString()
        {
            return "Data Source=" + instanciaSql + ";Initial Catalog= " + empresaSql + ";User Id=" + usuarioSql + ";Password=" + passwordSql;
        }

        public static string daPastaConfig()
        {
            if (versaoErp == "V800")
            {
                return pastaConfigV800;
            }
            else
            {
                return pastaConfigV900;
            }
        }

    }
}    