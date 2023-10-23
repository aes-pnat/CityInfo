﻿using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class CityForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name for the city!")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

    }
}