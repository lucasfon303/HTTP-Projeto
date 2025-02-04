using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net;

namespace HTTP_Projeto
{
    internal class Program
    {
        enum Menu { Unico = 1, Lista, Sair }
        public static bool escolheuSair = false;
        public static string erro = "Opção inválida\nPressione ENTER para retornar ao menu inicial.";
        public static string retornar = "Pressione ENTER para retornar ao menu inicial.";
        static void Main(string[] args)
        {
            while (escolheuSair == false)
            {
                Console.Clear();
                Console.WriteLine("-REQUISITOR DE TAREFAS-");
                Console.WriteLine("Informe qual opção você deseja:\n1 - Requisitar uma tarefa específica\n2 - Requisitar a lista completa de tarefas\n3 - Sair");
                string opcaoStr = Console.ReadLine();
                bool opcaoBool = int.TryParse(opcaoStr, out int opcaoInt);
                if (opcaoBool == true)
                {
                    Console.Clear();
                    Menu opcaoMenu = (Menu)opcaoInt;

                    switch (opcaoMenu)
                    {
                        case Menu.Unico:
                            ReqOne();
                            break;
                        case Menu.Lista:
                            ReqList();
                            break;
                        case Menu.Sair:
                            Sair();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(erro);
                    Console.ReadLine();
                }
            }
        }
        static void ReqList()
        {
            Console.WriteLine("-LISTAGEM DE TAREFAS-");
            Console.WriteLine("");
            Console.WriteLine("==========================");
            Console.WriteLine("");
            var requisição = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/");
            requisição.Method = "GET";
            var resposta = requisição.GetResponse();
            using (resposta)
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                List<Tarefa> tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(dados.ToString());
                foreach (Tarefa tarefa in tarefas)
                {
                    tarefa.Exibir();
                }

                stream.Close();
                resposta.Close();
            }
            Console.ReadLine();
        }
        static void ReqOne()
        {
            Console.WriteLine("-LISTAGEM DE TAREFA ÚNICA-");
            Console.WriteLine("Informe o ID da tarefa que você deseja requisitar:");
            string idStr = Console.ReadLine();
            bool idBool = int.TryParse(idStr, out int idInt);
            if (idBool == true)
            {
                Console.WriteLine("======================================");
                var requisição = WebRequest.Create($"https://jsonplaceholder.typicode.com/todos/{idInt}");
                requisição.Method = "GET";
                var resposta = requisição.GetResponse();
                using (resposta)
                {
                    var stream = resposta.GetResponseStream();
                    StreamReader leitor = new StreamReader(stream);
                    object dados = leitor.ReadToEnd();

                    var tarefa = JsonConvert.DeserializeObject<Tarefa>(dados.ToString());

                    tarefa.Exibir();

                    stream.Close();
                    resposta.Close();
                }
            }
            else 
            {
                Console.WriteLine(erro);
            }
            Console.ReadLine();
        }
        static void Sair()
        {
            Console.WriteLine("-SAIR-");
            Console.WriteLine("Você tem certeza de que deseja sair?\n1 - Sim\n2 - Não");
            string respostaStr = Console.ReadLine();
            bool respostaBool = int.TryParse(respostaStr, out int respostaInt);
            if (respostaBool == true)
            {
                if (respostaInt == 1)
                {
                    escolheuSair = true;
                }
                else if (respostaInt == 2)
                {
                    Console.WriteLine(retornar);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(erro);
                    Console.ReadLine();
                }
            }
        }
    }
}
