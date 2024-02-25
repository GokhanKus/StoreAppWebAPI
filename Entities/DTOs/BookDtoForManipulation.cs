using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
	public abstract record BookDtoForManipulation
	{
        [Required(ErrorMessage = "This field is a required field")]
        [MinLength(2,ErrorMessage = "This field must consist of at least 2 characters")]
        [MaxLength(100,ErrorMessage = "This field must consist of max 100 characters")]
        public string Title { get; init; }

        [Required(ErrorMessage = "This field is a required field")]
        [Range(10,5000)]//fiyat araligi 10 ile 5000 arasinda olmali
        public decimal Price { get; init; }
    }
}
/*
Dto'larin gorevi veri tasima veya istedigimiz alanlardaki verileri okuma gibi islemleri yerine getirmesi gerekirken,
biz burada extradan dogrulama islemi yapiyoruz solid'in s'sine aykiri(single responsibility)
 */
