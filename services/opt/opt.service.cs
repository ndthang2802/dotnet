using System;
using System.Linq;
using Application.model.Authentication;
using Application.helpers;
using BC = BCrypt.Net.BCrypt;
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