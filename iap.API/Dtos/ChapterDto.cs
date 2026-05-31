using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class ChapterDto
    {
       public int Id { get; set; } 
       public string Title { get; set; } = String.Empty;  
       public int TimestampSeconds { get; set; }  
    }
}