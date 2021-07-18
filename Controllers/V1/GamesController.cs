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
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Busca uma lista de jogos de forma paginada
        /// </summary>
        /// <param name="page">Número da página</param>
        /// <param name="qnt">Quantidade de elementos por página</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos na página</response>
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Get(
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int qnt = 5)
        {
            var games = await _gameService.Get(page, qnt);
            return games.Count == 0 ? NoContent() : Ok(games);
        }

        /// <summary>
        /// Busca uma lista de jogos de um produtor de forma paginada
        /// </summary>
        /// <param name="producerId">ID do produtor</param>
        /// <param name="page">Número da página</param>
        /// <param name="qnt">Quantidade de elementos por página</param>
        /// <response code="200">Retorna a lista de jogos do produtor</response>
        /// <response code="204">Caso não haja jogos</response>
        /// <response code="404">Caso o produtor seja inválido</response>
        [HttpGet("{producerId:int}")]
        public async Task<ActionResult<List<GameViewModel>>> Get(
            [FromRoute] int producerId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int qnt = 5)
        {
            try
            {
                var games = await _gameService.GetByProducerId(producerId, page, qnt);
                return games.Count == 0 ? NoContent() : Ok(games);
            }
            catch (ProducerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Busca uma lista de jogos que contém um fragmento de forma paginada
        /// </summary>
        /// <param name="fragment">Fragmento buscado</param>
        /// <param name="page">Número da página</param>
        /// <param name="qnt">Quantidade de elementos por página</param>
        /// <response code="200">Retorna a lista de jogos com tal fragmento</response>
        /// <response code="204">Caso não haja jogos na página</response>
        [HttpGet("{fragment}")]
        public async Task<ActionResult<List<GameViewModel>>> Find(
            [FromRoute] string fragment,
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery, Range(1, 50)] int qnt = 5)
        {
            var games = await _gameService.Find(fragment, page, qnt);
            return games.Count == 0 ? NoContent() : Ok(games);
        }

        /// <summary>
        /// Adiciona um jogo no banco de dados
        /// </summary>
        /// <param name="gameInputModel">Dados do jogo</param>
        /// <response code="200">Jogo inserido com sucesso</response>
        /// <response code="404">Caso o produtor seja inválido</response>
        /// <response code="422">Caso o jogo já esteja cadastrado</response>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            }
            catch (ProducerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (GameAlreadyExistsException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um jogo no banco de dados
        /// </summary>
        /// <param name="gameId">ID do jogo a ser atualizado</param>
        /// <param name="gameInputModel">Novos dados do jogo</param>
        /// <response code="200">Jogo atualizado com sucesso</response>
        /// <response code="404">Caso o jogo ou produtor não foram encontrados</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome e produtor cadastrados</response>
        [HttpPut("{gameId:int}")]
        public async Task<ActionResult> UpdateGame([FromRoute] int gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(gameId, gameInputModel);
                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProducerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (GameAlreadyExistsException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o preço de um jogo no banco de dados
        /// </summary>
        /// <param name="gameId">ID do jogo</param>
        /// <param name="price">Novo preço do jogo</param>
        /// <response code="200">Preço atualizado com sucesso</response>
        /// <response code="404">Caso o jogo não seja encontrado</response>
        [HttpPatch("{gameId:int}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] int gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(gameId, price);
                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Remove um jogo do banco de dados
        /// </summary>
        /// <param name="gameId">ID do jogo</param>
        /// <response code="200">Jogo removido com sucesso</response>
        /// <response code="404">Caso o jogo não seja encontrado</response>
        [HttpDelete("{gameId:int}")]
        public async Task<ActionResult> DeleteGame([FromRoute] int gameId)
        {
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}