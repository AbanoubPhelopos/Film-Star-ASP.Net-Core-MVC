using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Film_Star.Models
{
	public class Film
	{
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "You have to provide a valid Film Name.")]
        public string Name { get; set; }

        [DisplayName("Film Genres")]
        [Required(ErrorMessage = "You have to provide a valid Film Genres.")]
        public string Genres { get; set; }

        [DisplayName("Film Description")]
        [Required(ErrorMessage = "You have to provide a valid Film Description.")]
        public string Description { get; set; }

        [DisplayName("Film Duration")]
        [Required(ErrorMessage = "You have to provide a valid Film Duration.")]
        public int Duration { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImageURL { get; set; }

        public string Link { get; set; }


    }
}
