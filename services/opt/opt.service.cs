using System;
namespace Application.services.opt
{
    public class OptService
    {
        
        private string GenerateOpt()
        {
            Random r = new Random();
            string OPT = r.Next(100000,999999).ToString();
            return OPT;
        }

    } 

}