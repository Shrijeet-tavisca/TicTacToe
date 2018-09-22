
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TicTacToeGame
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        public static int currentPlayer= DataOfPlayer.GetCurrentUserID();
        public static bool winner;
        // GET: api/Identity
        [HttpGet("{id}")]
        [Logger]
        public string Get(int id)
        {
            string Token = DataOfPlayer.GetCurrentUserToken(id);
            return Token;
        }

        [HttpPost]
        [Logger]
        public ActionResult Post([FromBody]Player player)
        {
            bool isSuccessful = DataOfPlayer.Add(player);
            if (isSuccessful)
                return Ok("Player Added successfully Your Name is "+player.Name+"& Your ID is"+ player.Id +"");
            return BadRequest("Could not add Player");
        }
        
        // PUT: api/Identity/5
        [HttpPut("{id}")]
        [Logger]
        public ActionResult Put(int id, [FromBody]string value)
        {
            //editing in case of Inputs 
            if(id==currentPlayer)
            {
                return BadRequest("Player cannot play two times consecutively");
            }
            else
            {
                currentPlayer = id;
                //BoardData.StoreMove(value);
                
                if (winner == false)
                {
                    if (Convert.ToInt32(value) <= 9)
                    {
                        BoardData.StoreMove(value,id);
                        winner = BoardData.CheckForWinner(id);
                        return Ok("Valid move made");
                    }
                    else
                    {
                        return BadRequest("Position enrtered is inValid . Please enter value less than 9");
                    }
                }
                else
                {
                    return Ok("Result declared : "+id + " is Winner");
                    // I need  to stop this 
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

