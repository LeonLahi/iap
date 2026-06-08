using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Models;

namespace iap.API.Dtos
{
    public class PlaylistDeleteImpactDto
    {
        public bool HasChildren { get; set; }
        public int ChildCount { get; set; }
        public string? WarningMessage { get; set; }

    }
}