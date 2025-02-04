using System;

namespace HTTP_Projeto
{
    internal class Tarefa
    {
        public int userId;
        public int id;
        public string title;
        public bool completed;

        public void Exibir()
        {
            Console.WriteLine("");
            Console.WriteLine($"ID do usuário: {userId}");
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Título: {title}");
            Console.WriteLine($"Completado?: {completed}");
            Console.WriteLine("");
            Console.WriteLine("====================================");
        }
    }
}
