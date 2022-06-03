using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models.ViewModel
{
    public class ListTicketByTrainCarriages
    {
        public int TrainCarriagesId { get; set; }
        public string TrainCarriagesName { get; set; }
        public List<Ticket> Tickets { get; set; }

        public ListTicketByTrainCarriages()
        {

        }
    }
}