using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameOfDrones_Rock_Paper_Scissors.Models;
using System.ComponentModel.DataAnnotations;

namespace GameOfDrones_Rock_Paper_Scissors.Models
{
    public class RoundHistory
    {
        [Key]
        public int HistoryID { get; set; }
        public int Round { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string PlayerOneSelection { get; set; }
        public string PlayerTwoSelection { get; set; }
        public string Winner { get; set; }
    }
} 