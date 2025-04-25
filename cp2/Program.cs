using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace cp2
{
    internal class Program
    {
        private static Dictionary<Guid, Paciente> pacientes = new();
        private static Dictionary<Guid, Medico> medicos = new();
        private static List<Consulta> consultas = new();

        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1 - Cadastrar Paciente");
                Console.WriteLine("2 - Cadastrar Médico");
                Console.WriteLine("3 - Agendar Consulta");
                Console.WriteLine("4 - Listar Consultas");
                Console.WriteLine("5 - Alterar Consultas (Remover)");
                Console.WriteLine("6 - Relatório Diário");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    // Cadastrar Paciente
                    case "1":
                        Console.Write("Nome: ");
                        string nomePaciente = Console.ReadLine();
                        Console.Write("CPF (apenas números): ");
                        string cpf = Console.ReadLine();
                        if (!ValidarCPF(cpf))
                        {
                            Console.WriteLine("CPF inválido.");
                            break;
                        }
                        Console.Write("Data de nascimento (aaaa-mm-dd): ");
                        DateOnly nascimento = DateOnly.Parse(Console.ReadLine());

                        var paciente = new Paciente(nomePaciente, cpf, nascimento);
                        pacientes[paciente.GetId()] = paciente;
                        Console.WriteLine("Paciente cadastrado com sucesso!");
                        break;

                    // Cadastrar Médico
                    case "2":
                        Console.Write("Nome: ");
                        string nomeMedico = Console.ReadLine();
                        Console.Write("CRM: ");
                        string crm = Console.ReadLine();
                        if (!ValidarCRM(crm))
                        {
                            Console.WriteLine("CRM inválido.");
                            break;
                        }
                        Console.Write("Especialidade: ");
                        string esp = Console.ReadLine();

                        var medico = new Medico(nomeMedico, crm, esp);
                        medicos[medico.GetId()] = medico;
                        Console.WriteLine("Médico cadastrado com sucesso!");
                        break;

                    // Agendar Consulta
                    case "3":
                        Console.WriteLine("Pacientes:");
                        var listaPacientes = pacientes.Values.ToList();
                        for (int i = 0; i < listaPacientes.Count; i++)
                            Console.WriteLine($"{i} - {listaPacientes[i].GetNome()}");

                        Console.Write("Número do paciente: ");
                        var idxPaciente = int.Parse(Console.ReadLine());
                        var pacienteSelecionado = listaPacientes[idxPaciente];

                        Console.WriteLine("Médicos:");
                        var listaMedicos = medicos.Values.ToList();
                        for (int i = 0; i < listaMedicos.Count; i++)
                            Console.WriteLine($"{i} - {listaMedicos[i].GetNome()}");

                        Console.Write("Número do médico: ");
                        var idxMedico = int.Parse(Console.ReadLine());
                        var medicoSelecionado = listaMedicos[idxMedico];

                        Console.Write("Data (aaaa-mm-dd): ");
                        var data = DateOnly.Parse(Console.ReadLine());
                        Console.Write("Hora (hh:mm): ");
                        var hora = TimeOnly.Parse(Console.ReadLine());

                        var consulta = new Consulta(data, hora);
                        consultas.Add(consulta);
                        medicoSelecionado.AdicionarConsulta(consulta);
                        Console.WriteLine("Consulta agendada.");
                        break;

                    // Listar Consultas
                    case "4":
                        if (consultas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma consulta agendada.");
                        }
                        else
                        {
                            foreach (var con in consultas)
                                Console.WriteLine($"{con.GetId()} - {con.GetData()} {con.GetHora()}");
                        }
                        break;

                    // Alterar Consultas (Remover)
                    case "5":
                        if (consultas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma consulta agendada.");
                        }
                        else
                        {
                            Console.WriteLine("Consultas:");
                            for (int i = 0; i < consultas.Count; i++)
                                Console.WriteLine($"{i} - {consultas[i].GetData()} {consultas[i].GetHora()} ({consultas[i].GetId()})");

                            Console.Write("Número da consulta para remover: ");
                            var idxRemover = int.Parse(Console.ReadLine());
                            var consultaRemovida = consultas[idxRemover];

                            consultas.RemoveAt(idxRemover);
                            foreach (var m in medicos.Values)
                                m.RemoverConsulta(consultaRemovida.GetId());

                            Console.WriteLine("Consulta removida.");
                        }
                        break;

                    // Relatório Diário
                    case "6":
                        Console.Write("Data (aaaa-mm-dd): ");
                        var dataRelatorio = DateOnly.Parse(Console.ReadLine());
                        var consultasDoDia = consultas
                            .Where(c => c.GetData() == dataRelatorio)
                            .OrderBy(c => c.GetHora())
                            .ToList();

                        if (consultasDoDia.Count == 0)
                        {
                            Console.WriteLine("Nenhuma consulta nesse dia.");
                        }
                        else
                        {
                            foreach (var con in consultasDoDia)
                                Console.WriteLine($"{con.GetHora()} - {con.GetId()}");

                            if (consultasDoDia.Count > 1)
                            {
                                double totalMinutos = 0;
                                for (int i = 1; i < consultasDoDia.Count; i++)
                                    totalMinutos += (consultasDoDia[i].GetHora().ToTimeSpan() - consultasDoDia[i - 1].GetHora().ToTimeSpan()).TotalMinutes;

                                Console.WriteLine($"Intervalo médio: {totalMinutos / (consultasDoDia.Count - 1):N1} minutos");
                            }
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        // Método genérico para validação de dados
        private static bool ValidarCPF(string cpf)
        {
            return Regex.IsMatch(cpf, @"^\d{11}$");
        }

        private static bool ValidarCRM(string crm)
        {
            return Regex.IsMatch(crm, @"^\d{4,6}$");
        }
    }
}
