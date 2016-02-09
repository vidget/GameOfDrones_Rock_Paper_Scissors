using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameOfDrones_Rock_Paper_Scissors.Context;
using GameOfDrones_Rock_Paper_Scissors.Models;

namespace GameOfDrones_Rock_Paper_Scissors.Controllers
{
    public class GamesController : Controller
    {
        private GameContext db = new GameContext();
               
        // GET: Returns the current game data, there is no link in the game, must enter directly in the browser 
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Returns the current round history
        public ActionResult RoundHistoryList() 
        {
            return View(db.RoundHistorys.ToList());
        }

        // GET: Returns the current game data, there is no link in the game, must enter directly to view 
        public ActionResult GameHistoryList() 
        {
            return View(db.GameHistorys.ToList());
        }
      
        //GET: Retrieves the record to reset the game.
        public ActionResult StartOver()
        {
            Game game = db.Games.Find(1);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        //POST: Games/StartOver
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult StartOver([Bind(Include="RoundNumber,PlayerOneName,PlayerTwoName,PlayerOneSelection,PlayerTwoSelection,PlayerOneScore,PlayerTwoScore,RoundCount,RoundWinner,GameWinner")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.PlayerOneName = "";
                game.PlayerTwoName = "";
                game.PlayerOneSelection = "";
                game.PlayerTwoSelection = "";
                game.PlayerOneScore = 0;
                game.PlayerTwoScore = 0;
                game.RoundCount = 0;
                game.RoundWinner = "";
                game.GameWinner = "";
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [RoundHistories]");//deletes all the records in the Round History Table
                db.SaveChanges();

                return RedirectToAction("Play");
            }
            return View(game);
        }


        //GET: Games/GameWin
        public ActionResult Winner() 
        {
            
            Game game = db.Games.Find(1);//Number 1 is the first line of a database created at intialization. 
            if (game == null)
            {
                return HttpNotFound();
            }

            ViewBag.Message = game.RoundWinner.ToString() + " is the new EMPEROR!";       
            return View(game);
        }
         

        //POST: Games/GameWin
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Game Win RESETS all the values in the 1 position of the database once a user press the button to start again
        public ActionResult Winner([Bind(Include = "RoundNumber,PlayerOneName,PlayerTwoName,PlayerOneSelection,PlayerTwoSelection,PlayerOneScore,PlayerTwoScore,RoundCount,RoundWinner,GameWinner")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.PlayerOneName = "";
                game.PlayerTwoName = "";
                game.PlayerOneSelection = "";
                game.PlayerTwoSelection = "";
                game.PlayerOneScore = 0;
                game.PlayerTwoScore = 0;
                game.RoundCount = 0;
                game.RoundWinner = "";
                game.GameWinner = "";

                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                //Erases the ROUND HISTORY TABLE to Start a new game series.....WARNING USE CAREFULLY! 
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [RoundHistories]");
                db.SaveChanges();

                return RedirectToAction("Play");

            }
            return View(game);
        }

        /////////////Play() is the first view after the start game button on the front page is pressed
        //GET: Games/Play
        public ActionResult Play()
        {
            //it loads the record from games that holds round play
            Game game = db.Games.Find(1);
            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }


        //POST: Games/Play
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Play([Bind(Include = "RoundNumber,PlayerOneName,PlayerTwoName,PlayerOneSelection,PlayerTwoSelection,PlayerOneScore,PlayerTwoScore,RoundCount,RoundWinner,GameWinner")] Game game)
        {
            if (ModelState.IsValid)
            {

                if(game.PlayerOneName==null){
                    game.PlayerOneName = "Player One";
                }

                if (game.PlayerTwoName == null)
                {
                    game.PlayerTwoName = "Player Two";
                }
                game.PlayerOneSelection = "";
                game.PlayerTwoSelection = "";
                game.PlayerOneScore = 0;
                game.PlayerTwoScore = 0;
                game.RoundCount = 1;
                game.RoundWinner = "";
                game.GameWinner = "";

                db.Entry(game).State = EntityState.Modified;
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [RoundHistories]");     //Erases the ROUND HISTORY TABLE to Start a new game series
                db.SaveChanges();
                    
                return RedirectToAction("PlayerOne");
            }
            return View(game);
        }

        //////////////////////PLAYER ONE TURN////////////////////////////
        //GET: Games/PlayerOne
        public ActionResult PlayerOne()
        {
            Game game = db.Games.Find(1);
            if (game == null)
            {
            return HttpNotFound();
            }

            displayScore(game);
            ViewBag.Message2 = game.PlayerOneName.ToString() + " enter your selection!";

            return View(game);
        }

        //POST: Games/PlayerOne
        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public ActionResult PlayerOne([Bind(Include = "RoundNumber,PlayerOneName,PlayerTwoName,PlayerOneSelection,PlayerTwoSelection,PlayerOneScore,PlayerTwoScore,RoundCount,RoundWinner,GameWinner")] Game game)
        
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PlayerTwo");
            }
            return View(game);
        }

        //////////////////////PLAYER TWO TURN////////////////////////////
        //GET: Games/PlayerTwo
        public ActionResult PlayerTwo()
        {
            Game game = db.Games.Find(1);
            if (game == null)
            {
                return HttpNotFound();
            }

            displayScore(game);
            ViewBag.Message2 = game.PlayerTwoName.ToString() + " enter your selection!";

            return View(game);
        }

        //POST: Games/PlayerTwo
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult PlayerTwo([Bind(Include = "RoundNumber,PlayerOneName,PlayerTwoName,PlayerOneSelection,PlayerTwoSelection,PlayerOneScore,PlayerTwoScore,RoundCount,RoundWinner,GameWinner")] Game game)
        
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("RoundWin");
            }
            return View(game);
        }


        //////////////////////RoundWin////////////////////////////
        //GET: Games/RoundWin
        public ActionResult RoundWin()
        {  
            Game game = db.Games.Find(1);
            //RoundHistory roundhistory = db.RoundHistorys.Find(1);

            if (game == null)
            {
                return HttpNotFound();
            }

            ///Calls the method to get the name of the winner before it's written to the database
            string winner = CheckWin();


            ViewBag.Message = game.PlayerOneName.ToString() + " selected " + game.PlayerOneSelection.ToString() + ", " + game.PlayerTwoName.ToString() +" selected "+ game.PlayerTwoSelection;

            ViewBag.Message2 = winner +" won!";

            return View(game);
        }

        //POST: Games/RoundWin
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult RoundWin([Bind(Include = "RoundNumber,PlayerOneName,PlayerTwoName,PlayerOneSelection,PlayerTwoSelection,PlayerOneScore,PlayerTwoScore,RoundCount,RoundWinner,GameWinner,Round,PlayerOne,PlayerTwo,PlayerOneSelection,PlayerTwoSelection,Winner,PlayerOne,PlayerTwo,GameNumber,GameWinner,NumberOfRounds")] Game game, RoundHistory round, GameHistory gameHistory)
        {

            if (ModelState.IsValid)
            {
                //SCORE LOGIC HERE 

                string player1 = game.PlayerOneSelection;
                string player2 = game.PlayerTwoSelection;

                if (player1 == "Paper" && player2 == "Rock" || player1 == "Rock" && player2 == "Scissors" || player1 == "Scissors" && player2 == "Paper")
                {
                    game.PlayerOneScore = game.PlayerOneScore + 1;
                    game.RoundWinner = game.PlayerOneName.ToString();

                    round.Round = game.RoundCount;
                    round.PlayerOne = game.PlayerOneName;
                    round.PlayerTwo = game.PlayerTwoName;
                    round.PlayerOneSelection = game.PlayerOneSelection;
                    round.PlayerTwoSelection = game.PlayerTwoSelection;
                    round.Winner = game.RoundWinner;

                }
                else if (player1 == player2)
                {
                    //TIE Does nothing
                    round.Round = game.RoundCount;
                }
                else
                {
                    // If above conditions aren't met Player Two has won //
                    game.PlayerTwoScore = game.PlayerTwoScore + 1;
                    game.RoundWinner = game.PlayerTwoName.ToString();

                    round.Round = game.RoundCount;
                    round.PlayerOne = game.PlayerOneName;
                    round.PlayerTwo = game.PlayerTwoName;
                    round.PlayerOneSelection = game.PlayerOneSelection;
                    round.PlayerTwoSelection = game.PlayerTwoSelection;
                    round.Winner = game.RoundWinner;
                }

                game.RoundCount = game.RoundCount + 1;

                db.Entry(game).State = EntityState.Modified;
                db.Entry(round).State = EntityState.Added;

                db.SaveChanges();


                ////Check here to see if Player has won the Series and route to Final Win Page
                if (game.PlayerOneScore >= 3)
                {
                    gameHistory.GameWinner = game.PlayerOneName;
                    gameHistory.PlayerOne = game.PlayerOneName;
                    gameHistory.PlayerTwo = game.PlayerTwoName;
                    gameHistory.NumberOfRounds = game.RoundCount-1;

                    db.Entry(gameHistory).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Winner");
                    
                   
                }
                else if( game.PlayerTwoScore >= 3)
                {
                    //Records the winner to the Game History Table
                    gameHistory.PlayerOne = game.PlayerOneName;
                    gameHistory.PlayerTwo = game.PlayerTwoName;
                    gameHistory.GameWinner = game.PlayerTwoName;
                    gameHistory.NumberOfRounds = game.RoundCount-1;

                    db.Entry(gameHistory).State = EntityState.Added;                
                    db.SaveChanges();
                    return RedirectToAction("Winner");

                }else
                {
                    return RedirectToAction("PlayerOne");
                }
               
            }
            return View(game);
        }

        ////////displayScore is a method that used to create repeating ViewBag Messages to display score and round
         
        public void displayScore(Game game)
        {
            ///ViewBag.Message for the current SCORE
            ViewBag.Message = game.PlayerOneName.ToString() + " = " + game.PlayerOneScore.ToString() + " &nbsp;  &nbsp; &nbsp; vs  &nbsp;  &nbsp; &nbsp;"
                + game.PlayerTwoName.ToString() + " = " + game.PlayerTwoScore.ToString();

            ///ViewBag Message2 for ROUND COUNT.
            ViewBag.Message3 = "\nRound: " + game.RoundCount.ToString();
        }

        //CheckWin() method returns the string name of the winner
        public string CheckWin()

        {
            //win string holds the name of the winner. 
            string win = "";

            //sets the game object to the correct field from the TABLE. I used the 1st entry in the DB. The 
            //The first entry was seed at database intialization with default empty values
            Game game = db.Games.Find(1);

            string player1 = game.PlayerOneSelection;
            string player2 = game.PlayerTwoSelection;

            if(player1 == "Paper" && player2=="Rock")
            {
                win = game.PlayerOneName.ToString();
            }
            else if (player1 == "Rock" && player2 == "Scissors")
            {
                win = game.PlayerOneName.ToString();
            }
            else if (player1 == "Scissors" && player2 == "Paper")
            {
                win = game.PlayerOneName.ToString();
            }
            else if (player1 == player2)
            {
                win = "Nobody ";
            }
            else
            {
                win = game.PlayerTwoName.ToString();
            }
                return win ;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
