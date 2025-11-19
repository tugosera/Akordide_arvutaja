using Microsoft.AspNetCore.Mvc;
using Akordide_arvutaja.Models;

namespace Akordide_arvutaja.Controllers;

[ApiController]
[Route("[controller]")]
public class ChordController : ControllerBase
{
    // GET /chord?chords=C F G C&format=names
    [HttpGet]
    public IActionResult Get(string chords = "", string format = "numbers")
    {
        if (string.IsNullOrWhiteSpace(chords))
            return BadRequest("Query parameter 'chords' is required. Example: ?chords=C F G C&format=names");

        // Split by whitespace or comma
        var parts = chords.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

        var lugu = new Lugu();
        foreach (var p in parts)
        {
            // create specific subclass when matches or generic Kolmkola from name
            Kolmkola? k = p.Trim().ToUpperInvariant() switch
            {
                "C" => new CKolmkola(),
                "F" => new FKolmkola(),
                "G" => new GKolmkola(),
                _ => new Kolmkola(p.Trim()) // uses name constructor
            };

            lugu.LisaTakt(k);
        }

        if (format.Equals("names", StringComparison.OrdinalIgnoreCase))
        {
            var outNames = lugu.MangitavadNoodidNimetustega();
            return Ok(new { format = "names", chords = parts, result = outNames });
        }
        else
        {
            var outNums = lugu.MangitavadNoodid();
            return Ok(new { format = "numbers", chords = parts, result = outNums });
        }
    }
}
