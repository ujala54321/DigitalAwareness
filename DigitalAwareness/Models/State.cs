﻿using System.ComponentModel.DataAnnotations;

namespace DigitalAwareness.Models
{
    public class State
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<City> Cities { get; set; } = new List<City>();
    }
}