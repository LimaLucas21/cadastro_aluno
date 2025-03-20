using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;
using System.Data.SqlClient;



namespace EX01
{

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-2R2HF9V;Database=aluno_bd;Integrated Security=true;"; 

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                connection.Open();

                List<Aluno> alunos = new List<Aluno>();
                int escolha = 0;
                while (escolha != 4)
                {
                    Console.Clear();
                    Console.WriteLine("------Listagem de Alunos------");
                    Console.WriteLine("Escolha o que fazer a seguir:\n1 - Cadastrar: \n2 - Pesquisar:\n3 - Deletar: ");
                    escolha = Convert.ToInt32(Console.ReadLine());

                    switch (escolha)
                    {
                        case 1:

                            Console.WriteLine("Digite o nome do aluno: ");
                            string nome = Console.ReadLine();
                            Console.WriteLine("Digite a idade: ");
                            int idade = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("E a nota: ");
                            double media = Convert.ToDouble(Console.ReadLine());
                            Aluno novoAluno = new Aluno(nome, idade, media);
                            alunos.Add(novoAluno);
                            Console.WriteLine("Aluno cadastrado com sucesso");

                         
                            string sql = "INSERT INTO dbo.alunos (nome, idade, nota) VALUES (@nome, @idade, @nota)";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@nome", nome);
                                command.Parameters.AddWithValue("@idade", idade);
                                command.Parameters.AddWithValue("@nota", media);

                                int rowsAffected = command.ExecuteNonQuery();
                                Console.WriteLine($"Linhas afetadas: {rowsAffected}");
                            }
                            Console.ReadKey();

                            break;

                        case 2:
                            Console.WriteLine("Qual aluno deseja pesquisar?: ");
                            nome = Console.ReadLine();

                            ///////////////////////////////////////////////
                            sql = "SELECT nome, idade FROM dbo.alunos WHERE nome = @nome";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@nome", nome);
                               
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string nomeRetornado = reader["nome"].ToString();
                                        string idadeRetornada = reader["idade"].ToString();
                                        Console.WriteLine($"Aluno: {nomeRetornado} Idade: {idadeRetornada}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Aluno não encontrado");
                                    }
                                }
                            }

                            Console.ReadKey();
                            break;



                        case 3:
                            Console.WriteLine("Qual aluno deseja excluir?");
                            string nomeAlunoExcluir = Console.ReadLine();

                            sql = "DELETE FROM dbo.alunos WHERE nome = @nome";

                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@nome", nomeAlunoExcluir);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine($"Aluno {nomeAlunoExcluir} excluído com sucesso do banco de dados.");
                                }
                                else
                                {
                                    Console.WriteLine($"Aluno {nomeAlunoExcluir} não encontrado no banco de dados.");
                                }
                            }

                            Console.ReadKey();


                            break;
                    }
                }
            }
        }
    }
}
