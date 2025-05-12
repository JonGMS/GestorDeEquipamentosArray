using System;
using System.Linq;
using System.Security.Cryptography;

namespace gestaoDeEquipamentos
{
    internal class Program
    {

        static void Main(string[] args)
        {

            int opcao = 0, contadorDeCadastrosEquipamentos = 0, contadorDeCadastrosChamados = 0;
            string[] nomesEsquipamentos = new string[100]; string[] nomesFabricantes = new string[100]; string[] tituloChamado = new string[100]; string[] descricaoChamado = new string[100];
            decimal[] precosEquipamentos = new decimal[100]; int[] numerosDeSerieEquipamentos = new int[100];
            string[] datasDeFabricacaoEquipamentos = new string[100]; DateTime[] datasDeChamados = new DateTime[100];
            int[] chamado = new int[100];

            while (true)
            {
                opcao = ApresentarMenu(opcao);

                if (opcao == 1)
                {
                    while (true)
                    {
                        opcao = ApresentarSubMenuCRUD(opcao);
                        Console.Clear();
                        if (opcao != 0)
                        {

                            if (opcao == 1)
                            {

                                int numeroDeSerie = contadorDeCadastrosEquipamentos + 1;
                                string nome = " ", fabricante = " ", dataDeFabricacao = " ", preco;
                                decimal precoConvertido = 0;

                                Console.WriteLine("INSERINDO novo equipamento\n\n");
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("Digite o nome do equipamento (Mínimo de 6 caracteres): ");
                                    nome = Console.ReadLine();
                                    if (nome.Length < 6)
                                    {
                                        ApresentarMsg("Nome Inválido [6 Caracteres], ", ConsoleColor.DarkYellow, false);
                                        i--;
                                    }
                                } // NOME
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("Digite o preço de aquisição (Número Inteiro):");
                                    preco = Console.ReadLine();
                                    if (NaoEhNumero(preco))
                                    {
                                        ApresentarMsg("Os caracteres digitados não são Números, ", ConsoleColor.DarkYellow, false);
                                        i--;
                                        continue;
                                    }
                                    precoConvertido = Convert.ToDecimal(preco);
                                } // PREÇO
                                Console.Write("Número de série: " + numeroDeSerie);
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("\nDigite a Data de Fabricação (dd/MM/yyyy): ");
                                    dataDeFabricacao = Console.ReadLine();

                                    if (!DateTime.TryParse(dataDeFabricacao, out DateTime parseDate))
                                    {
                                        ApresentarMsg("Data inválida (dd/MM/yyyy), ", ConsoleColor.DarkYellow, false);
                                        i--;
                                    }
                                } // DATA DE FABRICAÇÃO
                                Console.Write("Digite o frabricante: ");
                                fabricante = Console.ReadLine();

                                nomesEsquipamentos[contadorDeCadastrosEquipamentos] = nome;
                                precosEquipamentos[contadorDeCadastrosEquipamentos] = precoConvertido;
                                numerosDeSerieEquipamentos[contadorDeCadastrosEquipamentos] = numeroDeSerie;
                                datasDeFabricacaoEquipamentos[contadorDeCadastrosEquipamentos] = dataDeFabricacao;
                                nomesFabricantes[contadorDeCadastrosEquipamentos] = fabricante;

                                contadorDeCadastrosEquipamentos++;
                                Console.Clear();
                            } //CADASTRO
                            else if (opcao == 2)
                            {
                                opcao = ApresentarSubMenuVisualizar();

                                if (opcao == 1)
                                {
                                    for (int i = 0; i < contadorDeCadastrosEquipamentos; i++)
                                    {
                                        if (numerosDeSerieEquipamentos[i] == -1)
                                        {
                                            continue;
                                        }
                                        Console.Write("\n--------------------------------------");
                                        Console.Write("\nNÚMERO DE SÉRIE (ID): " + numerosDeSerieEquipamentos[i]);
                                        Console.Write("\nNOME equipamento: " + nomesEsquipamentos[i]);
                                        Console.Write("\nNome FABRICANTE: " + nomesFabricantes[i]);
                                    }
                                    ApresentarMsg("\n\nEquipamentos visualizados com sucesso", ConsoleColor.DarkBlue, true);
                                }
                                else if (opcao == 2)
                                {
                                    int numeroDeSerieSolicitado = 0;
                                    for (int i = 0; i < contadorDeCadastrosEquipamentos; i++)
                                    {
                                        Console.Write("Digie o número de série do equipamento ou [0] para sair: ");
                                        numeroDeSerieSolicitado = Convert.ToInt16(Console.ReadLine());
                                        numeroDeSerieSolicitado--;
                                        if (numeroDeSerieSolicitado > numerosDeSerieEquipamentos.Length)
                                        {
                                            ApresentarMsg("Número de série inválido ou não cadastrado, ", ConsoleColor.DarkBlue, true);
                                            i--;
                                        }
                                        else if (numerosDeSerieEquipamentos[numeroDeSerieSolicitado] == -1)
                                        {
                                            ApresentarMsg("Esse equipamento foi excluído, ", ConsoleColor.DarkYellow, false);
                                            continue;
                                        }
                                    }
                                    Console.Write("NÚMERO DE SÉRIE (ID): " + numerosDeSerieEquipamentos[numeroDeSerieSolicitado]);
                                    Console.Write("\nNOME equipamento: " + nomesEsquipamentos[numeroDeSerieSolicitado]);
                                    Console.Write("\nNome FABRICANTE: " + nomesFabricantes[numeroDeSerieSolicitado]);
                                }
                                else
                                {
                                    break;
                                }
                            } //VISUALIZAR
                            else if (opcao == 3)
                            {
                                int numeroDeSerieSelecionadoInt = 0;
                                string numeroDeSerieSelecionado = "";
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("EDITANDO equipamento\n\nDigite o número de série do equipamento que deseja EDITAR: ");
                                    numeroDeSerieSelecionado = Console.ReadLine();
                                    numeroDeSerieSelecionadoInt = Convert.ToInt16(numeroDeSerieSelecionado);
                                    numeroDeSerieSelecionadoInt--;
                                    if (NaoEhNumero(numeroDeSerieSelecionado) || numeroDeSerieSelecionadoInt > numerosDeSerieEquipamentos.Length)
                                    {
                                        ApresentarMsg("\nNúmero de série inválido ou não cadastrado, ", ConsoleColor.DarkBlue, true);
                                        i--;
                                        continue;
                                    }
                                }//Validação de Nº de Serie selecionado

                                string nome = "", dataDeFabricacao = "", fabricante;
                                decimal precoConvertido = 0;
                                Console.Write("Número de série: " + numeroDeSerieSelecionado);
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.WriteLine("\nNome(Antigo nome: " + nomesEsquipamentos[numeroDeSerieSelecionadoInt] + "): ");
                                    nome = Console.ReadLine();
                                    if (nome.Length < 6)
                                    {
                                        ApresentarMsg("Nome Inválido [6 Caracteres], ", ConsoleColor.DarkYellow, false);
                                        i--;
                                    }
                                }
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("\nPreço (Antigo preço: " + precosEquipamentos[numeroDeSerieSelecionadoInt] + "): ");
                                    string preco = Console.ReadLine();
                                    if (NaoEhNumero(preco))
                                    {
                                        ApresentarMsg("\nOs caracteres digitados não são Números, ", ConsoleColor.DarkYellow, false);
                                        i--;
                                        continue;
                                    }
                                    precoConvertido = Convert.ToDecimal(preco);
                                }
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("\nData de Fabricação (Antiga data: " + datasDeFabricacaoEquipamentos[numeroDeSerieSelecionadoInt] + "): ");
                                    dataDeFabricacao = Console.ReadLine();

                                    if (!DateTime.TryParse(dataDeFabricacao, out DateTime parseDate))
                                    {
                                        ApresentarMsg("Data inválida (dd/MM/yyyy), ", ConsoleColor.DarkYellow, false);
                                        i--;

                                    }
                                } // DATA DE FABRICAÇÃO

                                Console.Write("\nFabricante (Nome antigo: " + nomesFabricantes[numeroDeSerieSelecionadoInt] + "): ");
                                fabricante = Console.ReadLine();

                                nomesEsquipamentos[numeroDeSerieSelecionadoInt] = nome;
                                precosEquipamentos[numeroDeSerieSelecionadoInt] = precoConvertido;
                                datasDeFabricacaoEquipamentos[numeroDeSerieSelecionadoInt] = dataDeFabricacao;
                                nomesFabricantes[numeroDeSerieSelecionadoInt] = fabricante;


                            } //EDITAR
                            else if (opcao == 4)
                            {
                                int numeroDeSerieSelecionadoInt = 0;
                                string numeroDeSerieSelecionado = "";
                                for (int i = 0; i < 1; i++)
                                {
                                    Console.Write("EXCLUINDO equipamento\n\n Digite o número de série do equipamento que deseja EXCLUIR: ");
                                    numeroDeSerieSelecionado = Console.ReadLine();
                                    numeroDeSerieSelecionadoInt = Convert.ToInt16(numeroDeSerieSelecionado);
                                    numeroDeSerieSelecionadoInt--;
                                    if (NaoEhNumero(numeroDeSerieSelecionado) || numeroDeSerieSelecionadoInt > numerosDeSerieEquipamentos.Length)
                                    {
                                        ApresentarMsg("Número de série inválido ou não cadastrado, ", ConsoleColor.DarkBlue, true);
                                        i--;
                                        continue;
                                    }
                                }//Validação de Nº de Serie selecionado

                                Console.WriteLine("=========================================\nNº de Série: " + numeroDeSerieSelecionado + "\nNome: " + nomesEsquipamentos[numeroDeSerieSelecionadoInt] + "\nPreço: " + precosEquipamentos[numeroDeSerieSelecionadoInt] + "\nData de Fabricação: " + datasDeFabricacaoEquipamentos[numeroDeSerieSelecionadoInt] + "\nFabricante: " + nomesFabricantes[numeroDeSerieSelecionadoInt] + "\n=========================================");
                                Console.Write("Deseja realmente excluir o equipamento?");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("\n digite [1] para SIM |");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("| digite [2] para NÃO: ");
                                Console.ResetColor();
                                for (int i = 0; i < 1; i++)
                                {
                                    opcao = Convert.ToInt16(Console.ReadLine());
                                    if (opcao != 1 && opcao != 2)
                                    {
                                        ApresentarMsg("Opção inválida, ", ConsoleColor.Red, false);
                                        i--;
                                        continue;
                                    }
                                }
                                if (opcao == 1)
                                {
                                    numerosDeSerieEquipamentos[numeroDeSerieSelecionadoInt] = -1;
                                    nomesEsquipamentos[numeroDeSerieSelecionadoInt] = null;
                                    precosEquipamentos[numeroDeSerieSelecionadoInt] = 0;
                                    datasDeFabricacaoEquipamentos[numeroDeSerieSelecionadoInt] = null;
                                    nomesFabricantes[numeroDeSerieSelecionadoInt] = null;
                                }
                            } //EXCLUIR -- falta validar, caso o equipamento esteja vinculado a um chamado.
                        }

                        else
                        {
                            break;
                        } // SAIR
                    }

                }//ApresentarSubMenuCRUD
                else if (opcao == 2)
                {
                    opcao = ApresentarSubMenuChamados(opcao);
                    if (opcao == 1)
                    {
                        int numeroDeSerie = 0;
                        Console.WriteLine("Registro de chamado\n");
                        Console.Write("Digite o título do chamado: ");
                        string nome = Console.ReadLine();

                        Console.Write("Descrição: ");
                        string descricao = Console.ReadLine();
                        for (int i = 0; i < 1; i++)
                        {
                            Console.Write("Digite o nº de série do equipamento: ");
                            string numeroDeSerieStr = Console.ReadLine();
                            if (NaoEhNumero(numeroDeSerieStr))
                            {
                                ApresentarMsg("\nOs caracteres digitados não são Números, ", ConsoleColor.Red, false);
                                i--;
                                continue;
                            }
                            numeroDeSerie = Convert.ToInt16(numeroDeSerieStr);
                            numeroDeSerie--;

                            if (numerosDeSerieEquipamentos[numeroDeSerie] == 0)
                            {
                                ApresentarMsg("O equipamento não existe,", ConsoleColor.DarkRed, true);
                                continue;
                            }
                            else if (numerosDeSerieEquipamentos[numeroDeSerie] == -1)
                            {
                                ApresentarMsg("Esse equipamento foi Excluido, ", ConsoleColor.DarkYellow, true);
                                continue;
                            }

                        }
                        DateTime dataChamada = DateTime.Now;
                        Console.WriteLine("Data do chamado: " + dataChamada);
                        Console.ReadLine();

                       // string dataChamadaStr = Convert.ToString(dataChamada);

                        tituloChamado[contadorDeCadastrosChamados] = nome;
                        descricaoChamado[contadorDeCadastrosChamados] = descricao;
                        chamado[contadorDeCadastrosChamados] = numeroDeSerie;
                        datasDeChamados[contadorDeCadastrosChamados] = dataChamada;

                        contadorDeCadastrosChamados++;
                        Console.ReadLine();
                    }
                    else if (opcao == 2)
                    {
                        Console.WriteLine("Visualizar Chamados");

                        for (int i = 0; i < contadorDeCadastrosChamados; i++)
                        {
                            TimeSpan diferencaDeTempo = datasDeChamados[i] - DateTime.Now;
                            int diasPassados = diferencaDeTempo.Days;

                            int posicaoChamado = chamado[i];
                            Console.Write("\n_______________________________");
                            Console.Write("ID: " + contadorDeCadastrosChamados + "\nTítulo: " + tituloChamado[i] + "\nDescrição: " + descricaoChamado[i] + "\nEquipamento: " + nomesEsquipamentos[posicaoChamado] + "\nData de criação: " + datasDeChamados[i] + " " + diasPassados );

                        }
                        Console.ReadLine();
                    }
                    else if (opcao == 3)
                    {
                        int id = 0;
                        Console.Write("Edição de chamado\n\n");
                        for (int i = 0; i < 1; i++)
                        {
                            Console.Write("Digie o ID do chamado que deseja EDITAR: ");
                            string idStr = Console.ReadLine();
                            if (NaoEhNumero(idStr))
                            {
                                ApresentarMsg("\nOs caracteres digitados não são Números, ", ConsoleColor.Red, false);
                                i--;
                                continue;
                            }
                            id = Convert.ToInt16(idStr);
                        }

                        int novoChamado = 0;
                        Console.WriteLine("ID: " + id);
                        id--;
                        Console.Write("Título (" + tituloChamado[id] + "): ");
                        string nome = Console.ReadLine();
                        Console.Write("Descrição: ");
                        string descricao = Console.ReadLine();
                        int nomeEquipamento = chamado[id];
                        for (int i = 0; i < 1; i++)
                        {
                            Console.Write("Equipamento (" +  nomesEsquipamentos[nomeEquipamento] + "): ");
                            string novoChamadoStr = Console.ReadLine();
                            if (NaoEhNumero(novoChamadoStr))
                            {
                                ApresentarMsg("\nOs caracteres digitados não são Números, ", ConsoleColor.Red, false);
                                i--;
                                continue;
                            }
                            novoChamado = Convert.ToInt16(novoChamadoStr); //erro
                            novoChamado--;
                            if (numerosDeSerieEquipamentos[novoChamado] == 0)
                            {
                                ApresentarMsg("O equipamento não existe,", ConsoleColor.DarkRed, true);
                                i--;
                                continue;                               
                            }
                            else if (numerosDeSerieEquipamentos[novoChamado] == -1)
                            {
                                ApresentarMsg("Esse equipamento foi Excluido, ", ConsoleColor.DarkYellow, true);
                                i--;
                                continue;
                            }
                        }
                        Console.Write("Data de criação: " + datasDeChamados[id]);

                        tituloChamado[id] = nome;
                        descricaoChamado[id] = descricao;
                        chamado[id] = novoChamado;
                    }

                }//ApresentarSubMenuChamados
                else if (opcao == 0)
                {
                    break;
                }
            }

        }
        #region MÉTODOS
        private static int ApresentarSubMenuChamados(int opcao)
        {

            while (true)
            {
                Console.WriteLine("Gestão de equipamentos\n\nDigite [1] para CRIAR um chamado\nDigite [2] para VISUALIZAR os chamados\nDigite [3] para EDITAR um chamado\nDigite [4] para EXCLUIR um chamado\nDigite [0] para VOLTAR");
                string opcaoStr = Console.ReadLine();
                if (opcaoStr != "1" && opcaoStr != "2" && opcaoStr != "3" && opcaoStr != "4" && opcaoStr != "0")
                {
                    ApresentarMsg("Opção inválida,", ConsoleColor.Red, true);
                    continue;
                }
                opcao = Convert.ToInt16(opcaoStr);
                break;
            }
            Console.Clear();
            return opcao;
        }

        private static int ApresentarSubMenuVisualizar()
        {
            int opcao;
            Console.WriteLine("Vizualizando Esquipamentos\nDigite [1] para VISUALIZAR todos os equipamentos\nDigite [2] para VISUALIZAR apenas um equipamento\nDigite [0] para VOLTAR");
            opcao = Convert.ToInt16(Console.ReadLine());
            if (opcao != 1 && opcao != 2 && opcao != 0)
            {
                ApresentarMsg("Opção inválida, ", ConsoleColor.Red, true);
            }
            Console.Clear();
            return opcao;
        }

        private static bool NaoEhNumero(string preco)
        {
            bool ehNumero = !preco.All(char.IsDigit);
            return ehNumero;
        }

        static int ApresentarSubMenuCRUD(int opcao)
        {
            while (true)
            {
                Console.WriteLine("Gestão de Equipamentos\n\nDigite [1] para INSERIR novo equipamento\nDigite [2] para VISUALIZAR equipamentos\nDigite [3] para EDITAR um equipamento\nDigite [4] para EXCLUIR um equipamento\nDigite [0] para SAIR");//SUBMENU CRUD
                string opcaoStr = Console.ReadLine();
                if (opcaoStr != "1" && opcaoStr != "2" && opcaoStr != "3" && opcaoStr != "4" && opcaoStr != "0")
                {
                    ApresentarMsg("Opção inválida,", ConsoleColor.Red, true);
                    continue;
                }
                opcao = Convert.ToInt16(opcaoStr);

                Console.Clear();
                return opcao;
            }

        }


        static int ApresentarMenu(int opcao)
        {
            while (true)
            {
                Console.WriteLine("Gestão de Equipamentos\n\nDigite [1] para o Cadastro de Equipamentos\nDigite [2] para o Controle de Chamados\nDigite [0] para Sair");//Menu
                string opcaoStr = Console.ReadLine();

                if (opcaoStr != "1" && opcaoStr != "2" && opcaoStr != "0")
                {
                    ApresentarMsg("Opção inválida,", ConsoleColor.Red, true);
                    continue;
                }//ValidaOpcao

                opcao = Convert.ToInt16(opcaoStr);
                break;
            }
            Console.Clear();
            return opcao;
        }

        static void ApresentarMsg(string mensagem, ConsoleColor cor, bool limparTela)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem + " precione [ENTER] para tentar novamente.");
            Console.ReadLine();
            if (limparTela)
            {
                Console.Clear();
            }
            Console.ResetColor();
        }
        #endregion

    }
}
