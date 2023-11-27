﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodAcademy.Areas.Admin.ViewModels
{
    public class CreateDoorVM
    {
        public int Id { get; set; }


        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
