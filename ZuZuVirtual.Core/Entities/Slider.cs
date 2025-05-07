using System.ComponentModel.DataAnnotations;
using ZuZuVirtual.Core.Entities;

namespace ZuZuVirtual.core.Entities
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Image")]
        public string? Image { get; set; }
        [Display(Name = "Link")]
        public string? Link { get; set; }

    }
}
//Used to store, authenticate and manage slider information