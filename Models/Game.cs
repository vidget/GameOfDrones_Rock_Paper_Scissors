using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameOfDrones_Rock_Paper_Scissors.Models;
using System.ComponentModel.DataAnnotations;

namespace GameOfDrones_Rock_Paper_Scissors.Models
{
    public class Game
    {
        [Key]
        public int RoundNumber { get; set; }
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public string PlayerOneSelection { get; set; }
        public string PlayerTwoSelection { get; set; }
        public int PlayerOneScore { get; set; }
        public int PlayerTwoScore { get; set; }
        public int RoundCount { get; set; }
        public string RoundWinner { get; set; }
        public string GameWinner { get; set; }
    }
}