using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Create
        
        [HttpPost]
        public GameResult AddGameResult([FromBody] GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            _context.SaveChanges();
            return gameResult;
        }

        #endregion

        #region Read
        
        [HttpGet]
        public List<GameResult> GetGameResults()
        {
            List<GameResult> results= _context.GameResults.OrderByDescending(item=>item.Score).ToList();
            return results;
        }
        
        [HttpGet("{id}")]  //이 부분은 요청할때 api/Ranking/1 이런식으로 요청해야함
        public GameResult GetGameResult(int id)
        {
            GameResult result= _context.GameResults.Where(item=>item.Id==id).FirstOrDefault();
            return result;
        }

        #endregion

        #region Update

        [HttpPut]
        public bool UpdateGameResult([FromBody] GameResult gameResult)
        {
            var findResult = _context.GameResults.Where(item => item.Id == gameResult.Id).FirstOrDefault();
            
            if (findResult == null)
                return false;
            
            findResult.Username = gameResult.Username;
            findResult.Score = gameResult.Score;
            _context.SaveChanges();

            return true;
        }

        #endregion
        
        #region Delete

        [HttpDelete("{id}")]
        public bool DeleteGameResult(int id)
        {
            var findResult = _context.GameResults.Where(item => item.Id == id).FirstOrDefault();
            
            if (findResult == null)
                return false;
            
            _context.GameResults.Remove(findResult);
            _context.SaveChanges();

            return true;
        }
        
        #endregion
    }
}
