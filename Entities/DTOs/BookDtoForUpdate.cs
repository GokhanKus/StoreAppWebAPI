using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
	public record BookDtoForUpdate : BookDtoForManipulation
	{
		[Required]
        public int Id { get; init; }
        public BookDtoForUpdate(int id, string title, decimal price)
        {
			Id = id;
			Title = title;
			Price = price;
		}
    }
	//public record BookDtoForUpdate(int Id, string Title, decimal Price);
	//public record BookDtoForUpdate (bu 2 kullanim ayni, ikiside init;)
	//{
	//	public int Id { get; init; }
	//	public string Title { get; init; }
	//	public decimal Price { get; init; }
	//}
}
/*
Biz bir DTO(data transfer object)'dan bahsediyorsak bu 
readonly (immutable) olmalı

record ifadesi;
LINQ destegi vardir yani uzerinde sorgular yazılabilir
Referans tiptir, class ile ayni ozellikleri tasir
Ctor tanimliyor gibi DTO tanimlamamiza izin verir?

//set; yerine init(initialize); yaparak immutable(readonly) ozelligi kazandirmis oluyoruz yani
tanimlandigi yerde degeri verilebilir sadece o zaman set edilebilir, o asamadan sonra o propun degisme ihtimali yoktur
 */