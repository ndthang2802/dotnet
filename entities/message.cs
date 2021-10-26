using System;

namespace Application.entities
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public User sender { get; set; }
        public string Text { get; set; }
    }
}