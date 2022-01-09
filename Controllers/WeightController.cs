#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeightRaceAPI.Data;
using WeightRaceAPI.Models;

namespace WeightRaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly WeightRaceContext _context;

        public WeightController(WeightRaceContext context)
        {
            _context = context;
        }

        // GET: api/Weight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weight>>> GetWeights()
        {
            return await _context.Weights.ToListAsync();
        }

        // GET: api/Weight/5
        [HttpGet("GetUserWeights/{userId}")]
        public async Task<ActionResult<IEnumerable<Weight>>> GetUserWeights(int userId)
        {
            var weight = await _context.Weights.Where(x => x.UserId == userId).OrderBy(d => d.LogDate).ToListAsync();

            if (weight == null)
            {
                return NotFound();
            }

            return weight;
        }

        // GET: api/Weight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weight>> GetWeight(int id)
        {
            var weight = await _context.Weights.FindAsync(id);

            if (weight == null)
            {
                return NotFound();
            }

            return weight;
        }

        // PUT: api/Weight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeight(int id, Weight weight)
        {
            if (id != weight.WeightId)
            {
                return BadRequest();
            }

            _context.Entry(weight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Weight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weight>> PostWeight(Weight weight)
        {
            _context.Weights.Add(weight);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeight), new { id = weight.WeightId }, weight);
        }

        // DELETE: api/Weight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeight(int id)
        {
            var weight = await _context.Weights.FindAsync(id);
            if (weight == null)
            {
                return NotFound();
            }

            _context.Weights.Remove(weight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeightExists(int id)
        {
            return _context.Weights.Any(e => e.WeightId == id);
        }
    }
}
