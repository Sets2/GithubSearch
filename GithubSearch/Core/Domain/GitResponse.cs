﻿using System.ComponentModel.DataAnnotations;

namespace GithubSearch.Core.Domain
{
    public class GitResponse
    {
        [Key]
        public long Id { get; set; }
        public string SearchString { get; set; } = null!;
        public string SearchResult { get; set; } = null!;

     }
}
