using GameOfDrones_Rock_Paper_Scissors.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameOfDrones_Rock_Paper_Scissors.Models
{
    public class GameDbIntializer : System.Data.Entity.DropCreateDatabaseAlways<GameContext>
    {
        protected override void Seed(GameContext context)
        {
            context.Games.Add(new Game 
                                { 
                                RoundNumber = 0,
                                PlayerOneName ="",
                                PlayerTwoName ="",
                                PlayerOneSelection = "",
                                PlayerTwoSelection = "",
                                PlayerOneScore = 0,
                                PlayerTwoScore = 0,
                                RoundCount = 1,
                                RoundWinner = "",
                                GameWinner =""
                                });

            context.RoundHistorys.Add(new RoundHistory
                                {
                                    HistoryID = 0,
                                    Round = 0,
                                    PlayerOne = "",
                                    PlayerTwo = "",
                                    PlayerOneSelection = "",
                                    PlayerTwoSelection = "",
                                    Winner = ""
                                });
            //If Im planning to seed Game History data uncomment. 
            //context.GameHistorys.Add(new GameHistory
            //                    {
                                   
            //                        GameNumber = 0,
            //                        GameWinner = "",
            //                        NumberOfRounds = 0
            //                    });

            base.Seed(context);
        }




    }
}