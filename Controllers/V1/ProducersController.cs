using desafio_catalogo_jogos.Exceptions;
using desafio_catalogo_jogos.Models;
using desafio_catalogo_jogos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;
        private readonly IGameService _gameService;

        public ProducersController(IProducerService producerService, IGameService gameService)
        {
            _producerService = producerService;
            _gameService = gameService;
        }

        /// <summary>
        /// Busca uma lista de produtores de forma paginada
        /// </summary>
        /// <param name="page">Número da página</param>
        /// <param name="qnt">Quantidade de elementos por página</param>
        /// <response code="200">Retorna a lista de produtores</response>
        /// <response code="204">Caso não haja produtores na página</response>
        [HttpGet]
        public async Task<ActionResult<List<ProducerViewModel>>> Get(
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int qnt = 5)
        {
            var producers = await _producerService.Get(page, qnt);
            return producers.Count == 0 ? NoContent() : Ok(producers);
        }

        /// <summary>
        /// Obtem um produtor por ID
        /// </summary>
        /// <param name="producerId">ID do produtor</param>
        /// <response code="200">Retorna os dados do produtor</response>
        /// <response code="404">Caso não seja encontrado um produtor</response>
        [HttpGet("{producerId:int}")]
        public async Task<ActionResult<ProducerViewModel>> Get([FromRoute] int producerId)
        {
            try
            {
                var producer = await _producerService.Get(producerId);
                return Ok(producer);
            }
            catch (ProducerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca uma lista de produtores que contém um fragmento de forma paginada
        /// </summary>
        /// <param name="fragment">Fragmento buscado</param>
        /// <param name="page">Número da página</param>
        /// <param name="qnt">Quantidade de elementos por página</param>
        /// <response code="200">Retorna a lista de produtores com tal fragmento</response>
        /// <response code="204">Caso não haja produtores na página</response>
        [HttpGet("{fragment}")]
        public async Task<ActionResult<List<ProducerViewModel>>> Find(
            [FromRoute] string fragment,
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int qnt = 5)
        {
            var producers = await _producerService.Find(fragment, page, qnt);
            return producers.Count == 0 ? NoContent() : Ok(producers);
        }

        /// <summary>
        /// Adiciona um produtor no banco de dados
        /// </summary>
        /// <param name="producerInputModel">Dados do produtor</param>
        /// <response code="200">Produtor inserido com sucesso</response>
        /// <response code="422">Caso o produtor já esteja cadastrado</response>
        [HttpPost]
        public async Task<ActionResult<ProducerViewModel>> InsertProducer([FromBody] ProducerInputModel producerInputModel)
        {
            try
            {
                var producer = await _producerService.Insert(producerInputModel);
                return Ok(producer);
            }
            catch (ProducerAlreadyExistsException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um produtor no banco de dados
        /// </summary>
        /// <param name="producerId">ID do produtor</param>
        /// <param name="producerInputModel">Novos dados do produtor</param>
        /// <response code="200">Produtor atualizado com sucesso</response>
        /// <response code="404">Caso o produtor não seja encontrado</response>
        /// <response code="422">Caso já exista um produtor de mesmo nome cadastrado</response>
        [HttpPut("{producerId:int}")]
        public async Task<ActionResult> UpdateProducer([FromRoute] int producerId, [FromBody] ProducerInputModel producerInputModel)
        {
            try
            {
                await _producerService.Update(producerId, producerInputModel);
                return Ok();
            }
            catch (ProducerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProducerAlreadyExistsException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        /// <summary>
        /// Remove um produtor do banco de dados, excluindo ou não seus jogos do sistema
        /// </summary>
        /// <param name="producerId">ID do produtor</param>
        /// <param name="forceDelete">Força a deleção de tanto o produtor e seus jogos</param>
        /// <response code="200">Dados removidos com sucesso</response>
        /// <response code="404">Caso o produtor não seja encontrado</response>
        /// <response code="422">Caso o produtor ainda possua jogos com seu ID</response>
        [HttpDelete("{producerId:int}/{forceDelete:bool}")]
        public async Task<ActionResult> DeleteProducer([FromRoute] int producerId, [FromRoute] bool forceDelete = false)
        {
            try
            {
                if (forceDelete) await _gameService.DeleteAllWithProducerId(producerId);
                await _producerService.Delete(producerId);
                return Ok();
            }
            catch (ProducerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ExistingGamesWithProducerException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}