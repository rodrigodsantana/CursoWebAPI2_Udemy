using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        [HttpGet]
        [Route("RetornaAlunos")]
        [Authorize(Roles = Funcao.Professor)]
        public IHttpActionResult RetornaAlunos()
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("RetornaAlunos/{id:int}/{nome?}/{sobrenome?}")]
        public IHttpActionResult RecuperarPorId(int id, string nome = null, string sobrenome = null)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                return Ok(aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route(@"RetornaPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult Retorna(string data, string nome)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                IEnumerable<AlunoDTO> alunos = aluno.ListarAlunos().Where(x => x.data == data || x.nome == nome);

                if (!alunos.Any())
                    NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // POST: api/Aluno
        [HttpPost]
        public IHttpActionResult Post(AlunoDTO aluno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.Inserir(aluno);
                return Ok(_aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Aluno/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]AlunoDTO aluno)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();
                aluno.id = id;
                _aluno.Atualizar(aluno);
                return Ok(_aluno.ListarAlunos(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Aluno/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.Deletar(id);
                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
