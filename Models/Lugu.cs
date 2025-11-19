using System.Collections.Generic;
using System.Linq;

namespace Akordide_arvutaja.Models;

/// <summary>
/// Lugu - iga takti jaoks saab lisada kolmkõla.
/// Võimaldab välja anda kõik mängitavad noodinumbrid või -nimed ühe listina (järjekorras).
/// </summary>
public class Lugu
{
    private readonly List<Kolmkola> taktid = new();

    public void LisaTakt(Kolmkola k)
    {
        taktid.Add(k);
    }

    public int[] MangitavadNoodid()
    {
        // tagastame järjest kõikide taktide noodid (iga takt 3 nooti)
        return taktid.SelectMany(t => t.MangitavadNoodid()).ToArray();
    }

    public string[] MangitavadNoodidNimetustega()
    {
        return taktid.SelectMany(t => t.MangitavadNimed()).ToArray();
    }
}
