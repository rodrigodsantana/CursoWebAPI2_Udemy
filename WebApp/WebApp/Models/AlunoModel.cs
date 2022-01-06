﻿using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class AlunoModel
    {
        public List<AlunoDTO> ListarAlunos(int? id = null)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                return alunoBD.ListarAlunosDB(id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao listar Alunos: Erro => {ex.Message}");
            }
        }

        //public bool RescreverArquivo(List<AlunoDTO> listaAlunos)
        //{
        //    var caminhoArquivo = HostingEnvironment.MapPath(@"/App_Data/Base.json");
        //    var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
        //    File.WriteAllText(caminhoArquivo, json);
        //    return true;
        //}

        public void Inserir(AlunoDTO aluno)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                alunoBD.InserirAlunoDB(aluno);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao inserir Aluno: Erro => {ex.Message}");
            }
        }

        public void Atualizar(AlunoDTO aluno)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                alunoBD.AtualizarAlunoDB(aluno);

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar Aluno: Erro => {ex.Message}");
            }
        }

        public void Deletar(int id)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                alunoBD.DeletarAlunoDB(id);

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar Aluno: Erro => {ex.Message}");
            }
        }
    }
}