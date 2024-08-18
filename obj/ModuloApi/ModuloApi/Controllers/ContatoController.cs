using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ModuloApi.Context;
using ModuloApi.Models;

namespace ModuloApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {

        private AgendaContext _context;
        public ContatoController(AgendaContext contex)
        {
            _context = contex;
        }

        [HttpPost]
        public ActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }

        [HttpGet("{id}")]
        public ActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);

        }

        [HttpGet("ObterPorNome")]
        public ActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Name.Contains(nome));
            return Ok(contatos);


        }

        [HttpPut("{id}")]
        public ActionResult Atualizar (int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
            {
                return NotFound();
            }

            contatoBanco.Name = contato.Name;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            var contatoBanco = _context.Contatos.Find(id);

            if (contatoBanco == null)
            {
                return NotFound();
            }
            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return NoContent();

        }

       


    }
}
