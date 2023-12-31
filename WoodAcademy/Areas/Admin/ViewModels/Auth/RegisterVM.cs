﻿using System.ComponentModel.DataAnnotations;

namespace WoodAcademy.Areas.Admin.ViewModels.Auth
{
    public class RegisterVM
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required, MaxLength(50)]
        public string FullName { get; set; }
    }
}
