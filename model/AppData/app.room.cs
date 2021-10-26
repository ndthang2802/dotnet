using Application.model.Authentication;
namespace Application.model.AppData
{
    public class RoomModel 
    {
        public long Id { get; set; }
        public string name {get;set;}
        public UserModel creator {get;set;}
    }
}