using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Mappers
{
    public static class ChapterMapper
    {
        public static ChapterDto ToChapterDto(this Chapter chapter)
        {
            return new ChapterDto
            {
                Id = chapter.Id,
                Title = chapter.Title,
                TimestampSeconds = chapter.TimestampSeconds
            };
        }
    }
}