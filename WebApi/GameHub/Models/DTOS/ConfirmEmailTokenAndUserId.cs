using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.DTOS
{
    // ConfirmEmail and Token DTO
    [BindProperties]
    public class ConfirmEmailTokenAndUserId
    {
        public string token { get; set; }
        public string userId { get; set; }

    }
}
