using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class TvShowsController : Controller
    {
        private readonly TvShowsContext _context;

        public TvShowsController(TvShowsContext context)
        {
            _context = context;
        }

        // GET: TvShows
        // GET: TvShows
        public async Task<IActionResult> Index(string TvShowsGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.TvShows
                                            orderby m.Genre
                                            select m.Genre;

            var tvShows = from m in _context.TvShows
                        select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                tvShows = tvShows.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(TvShowsGenre))
            {
                tvShows = tvShows.Where(x => x.Genre == TvShowsGenre);
            }

            var TvShowsGenreVM = new TvShowsGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                tvShows = await tvShows.ToListAsync()
            };

            return View(TvShowsGenreVM);
        }

        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShows = await _context.TvShows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShows == null)
            {
                return NotFound();
            }

            return View(tvShows);
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] TvShows tvShows)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShows);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShows);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShows = await _context.TvShows.FindAsync(id);
            if (tvShows == null)
            {
                return NotFound();
            }
            return View(tvShows);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] TvShows tvShows)
        {
            if (id != tvShows.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvShows);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowsExists(tvShows.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tvShows);
        }

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShows = await _context.TvShows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShows == null)
            {
                return NotFound();
            }

            return View(tvShows);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tvShows = await _context.TvShows.FindAsync(id);
            _context.TvShows.Remove(tvShows);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvShowsExists(int id)
        {
            return _context.TvShows.Any(e => e.Id == id);
        }
    }
}
