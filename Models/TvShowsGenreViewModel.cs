using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class TvShowsGenreViewModel
    {
        public List<TvShows> tvShows { get; set; }
        public SelectList Genres { get; set; }
        public string TvShowsGenre { get; set; }
        public string SearchString { get; set; }
    }
}