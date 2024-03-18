using AutoMapper;
using b3.api.DTO.Model;
using b3.api.UnityOfWork.Interfaces;
using b3_domain.Model;
using b3_Service.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace b3.api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TarefasController : Controller
    {
        private readonly IUnityOfWork _uof;
        private readonly ILogger<TarefasController> _logger;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        private readonly IRmqService _rabbit;

        public TarefasController(IUnityOfWork uof,
                                 ILogger<TarefasController> logger,
                                 IMapper mapper,
                                 IConfiguration config,
                                 IRmqService rabbit)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _rabbit = rabbit;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> Get()
        {
            try
            {
                var clientes = await _uof.TarefasRepository.Get().ToListAsync();

                var clientesDto = _mapper.Map<List<TarefaDto>>(clientes);

                if (clientes is null) return NotFound();

                return clientesDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpGet("{id:int}", Name = "obter tarefa")]
        public async Task<ActionResult<TarefaDto>> GetById(int id)
        {
            try
            {

                var tarefa = await _uof.TarefasRepository.GetById(x => x.id == id);

                var tarefaDto = _mapper.Map<TarefaDto>(tarefa);

                if (tarefaDto is null) return NotFound("Produto não encontrado");



                _logger.LogInformation($"Sucesso ao buscar tarefa id: {id}");
                return tarefaDto;

            }
            catch (SqlException e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpGet("{desc}")]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetByDescricao(string desc)
        {
            try
            {
                var tarefa = await _uof.TarefasRepository.GetTarefasDescricao(desc);

                var tarefaDto = _mapper.Map<List<TarefaDto>>(tarefa);

                if (tarefaDto is null) return NotFound("Produto não encontrado");

                return tarefaDto;

            }
            catch (SqlException e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(TarefaDto tarefa)
        {

            try
            {
                var TarefaExistente = await _uof.TarefasRepository.GetTarefasDescricao(tarefa.Descricao);
                var retorno = TarefaExistente.Where(x => x.descricao == tarefa.Descricao).FirstOrDefault();

                if (null == retorno)
                {
                    var task = _mapper.Map<Tarefa>(tarefa);

                    _uof.TarefasRepository.Add(task);

                    await _uof.Commit();

                    //Enviar RabbitMq 
                    if (_config.GetSection("SendRbQueue").Value.ToLower() == "ligado")
                    {
                        var result = _rabbit.SendMessageRb(task);
                        _logger.LogInformation($"Mensagem enviada: {result}");
                    }


                    return Ok(tarefa);
                }
                else
                {
                    return BadRequest($"Tarefa Já Existente na base de dados: {retorno.descricao}");
                }
            }
            catch (SqlException e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}, ");
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, TarefaDto cli)
        {
            try
            {
                var tarefa = _mapper.Map<Tarefa>(cli);
                _uof.TarefasRepository.Update(tarefa);
                await _uof.Commit();

                return Ok(tarefa);

            }
            catch (SqlException e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpPatch]
        public async Task<ActionResult> Patch(TarefaDto cli)
        {

            try
            {
                var cliente = _mapper.Map<Tarefa>(cli);

                _uof.TarefasRepository.Update(cliente);
                await _uof.Commit();

                return Ok(cliente);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var tarefa = await _uof.TarefasRepository.GetById(x => x.id == id);

                if (tarefa is null) return NotFound();

                _uof.TarefasRepository.Delete(tarefa);
                await _uof.Commit();

                return Ok(tarefa);

            }
            catch (SqlException e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }




    }
}
