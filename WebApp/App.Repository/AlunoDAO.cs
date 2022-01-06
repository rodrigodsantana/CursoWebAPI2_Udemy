using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App.Repository
{
    public class AlunoDAO
    {
        //string stringConexao = ConfigurationManager.AppSettings["ConnectionString"]; //ConfigurationManager.ConnectionStrings[""];
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private IDbConnection conexao;
        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }
        public List<AlunoDTO> ListarAlunosDB(int? id)
        {
            try
            {
                var listaAlunos = new List<AlunoDTO>();
                IDbCommand select = conexao.CreateCommand();
                if (id == null)
                {
                    select.CommandText = "SELECT * FROM ALUNOS";
                }
                else
                {
                    select.CommandText = $"SELECT * FROM ALUNOS WHERE ID={id}";
                }


                IDataReader resultado = select.ExecuteReader();

                while (resultado.Read())
                {
                    var aluno = new AlunoDTO
                    {
                        id = Convert.ToInt32(resultado["id"]),
                        nome = Convert.ToString(resultado["nome"]),
                        sobrenome = Convert.ToString(resultado["sobrenome"]),
                        telefone = Convert.ToString(resultado["telefone"]),
                        ra = Convert.ToInt32(resultado["ra"])
                    };
                    listaAlunos.Add(aluno);
                }
                conexao.Close();
                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand InsertCmd = conexao.CreateCommand();
                InsertCmd.CommandText = "INSERT INTO ALUNOS(NOME, SOBRENOME, TELEFONE, RA) VALUES(@pNome, @pSobrenome, @pTelefone, @pRa)";

                IDbDataParameter pNome = new SqlParameter("pNome", aluno.nome);
                IDbDataParameter pSobrenome = new SqlParameter("pSobrenome", aluno.sobrenome);
                IDbDataParameter pTelefone = new SqlParameter("pTelefone", aluno.telefone);
                IDbDataParameter pRa = new SqlParameter("pRa", aluno.ra);

                InsertCmd.Parameters.Add(pNome);
                InsertCmd.Parameters.Add(pSobrenome);
                InsertCmd.Parameters.Add(pTelefone);
                InsertCmd.Parameters.Add(pRa);

                InsertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        public void AtualizarAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand UpdateCmd = conexao.CreateCommand();
                UpdateCmd.CommandText = "UPDATE ALUNOS SET NOME=@pNome, SOBRENOME=@pSobrenome, TELEFONE=@pTelefone, RA=@pRa WHERE ID=@pId";

                IDbDataParameter pId = new SqlParameter("pId", aluno.id);
                IDbDataParameter pNome = new SqlParameter("pNome", aluno.nome);
                IDbDataParameter pSobrenome = new SqlParameter("pSobrenome", aluno.sobrenome);
                IDbDataParameter pTelefone = new SqlParameter("pTelefone", aluno.telefone);
                IDbDataParameter pRa = new SqlParameter("pRa", aluno.ra);

                UpdateCmd.Parameters.Add(pId);
                UpdateCmd.Parameters.Add(pNome);
                UpdateCmd.Parameters.Add(pSobrenome);
                UpdateCmd.Parameters.Add(pTelefone);
                UpdateCmd.Parameters.Add(pRa);

                UpdateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand DeleteCmd = conexao.CreateCommand();
                DeleteCmd.CommandText = "DELETE FROM ALUNOS WHERE ID=@pId";

                IDbDataParameter pId = new SqlParameter("pId", id);
                DeleteCmd.Parameters.Add(pId);

                DeleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}