using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TicTacToeGame
{
    public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        static int playerNumber = 0;
        static string player1Token = "";
        static string player2Token = "";
        public void OnActionExecuted(ActionExecutedContext context)
        {

          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new UnauthorizedAccessException("Empty Input");
            }
            else
            {
                bool tokenPresentInDB = DataOfPlayer.CheckIfTokenPresentInDB(apiKey);
                if (!tokenPresentInDB)
                {
                    throw new UnauthorizedAccessException("Something wrong with key");
                }
                else if (tokenPresentInDB)
                {
                    if (playerNumber <= 2)
                    {
                        if (playerNumber == 1)
                        {
                            player1Token = apiKey;
                            playerNumber++;
                        }
                        else if (!apiKey.Equals(player1Token))
                        {
                            player2Token = apiKey;
                        }                       
                    }
                    else if (!apiKey.Equals(player1Token) && !apiKey.Equals(player2Token))
                    {
                        throw new UnauthorizedAccessException("Only Two Players Can Play");
                    }

                   
                }
            }
        }
    }
}
