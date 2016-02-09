using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameOfDrones_Rock_Paper_Scissors.Models;
using System.ComponentModel.DataAnnotations;

namespace GameOfDrones_Rock_Paper_Scissors.Models
{
    public class GameHistory
    {
         [Key]
         public int GameNumber { get; set; }
         public string PlayerOne { get; set; }
         public string PlayerTwo { get; set; } 
         public string GameWinner { get; set; }
         public int NumberOfRounds { get; set; }
    }
} 