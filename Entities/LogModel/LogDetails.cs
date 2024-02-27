using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.LogModel
{
	public class LogDetails
	{
        public object? ModelName{ get; set; }
        public object? Controller{ get; set; }
        public object? Action{ get; set; }
        public object? Id { get; set; }
        public object? CreatedAt { get; set; }
        public LogDetails()
        {
            CreatedAt = DateTime.Now;
        }
        public override string ToString() => JsonSerializer.Serialize(this);//bu classi serialize et
	}
}
