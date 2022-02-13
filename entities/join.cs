using System;

namespace Application.entities
{
    public class Join
    {
        public string UserId { get; set; }
        public string ConservationId { get; set; }
        
        public Join(string userId,string conservationId){
            UserId = userId;
            ConservationId = conservationId;
        }
        
    }
}