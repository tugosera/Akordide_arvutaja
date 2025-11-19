using System;
using System.Collections.Generic;
using System.Linq;

namespace Akordide_arvutaja.Models;

/// <summary>
/// Kolmkõla (triad) — põhitoon, suur terts (+4), kvint (+7).
/// Suudab teisendada MIDI-numbreid <-> nimed (alates 60).
/// Konstruktor: Kolmkola(int root) või Kolmkola(string rootName).
/// </summary>
public class Kolmkola
{
    public int Pohitoon { get; protected set; }

    private static readonly string[] NamesFrom60 = new[]
    {
        "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "B", "H"
    };
    // NamesFrom60[0] == "C" corresponds to MIDI 60

    public Kolmkola(int root)
    {
        Pohitoon = root;
    }

    public Kolmkola(string name)
    {
        // name could be like "C", "C#", "EB", "H" etc.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name required", nameof(name));
        Pohitoon = NameToNote(name.Trim());
    }

    public virtual int[] MangitavadNoodid()
    {
        return new[] { Pohitoon, Pohitoon + 4, Pohitoon + 7 };
    }

    public virtual string[] MangitavadNimed()
    {
        return MangitavadNoodid().Select(NoteToName).ToArray();
    }

    public static string NoteToName(int midi)
    {
        // Normalize relative to 60
        int offset = midi - 60;
        // in case outside, wrap around modulo 12
        int idx = ((offset % 12) + 12) % 12;
        return NamesFrom60[idx];
    }

    public static int NameToNote(string name)
    {
        var n = name.Trim().ToUpperInvariant();
        // Accept variations: Eb or D#
        if (n == "D#") n = "Eb";
        if (n == "BB") n = "A#";
        // Find in NamesFrom60
        for (int i = 0; i < NamesFrom60.Length; i++)
        {
            if (NamesFrom60[i].ToUpperInvariant() == n) return 60 + i;
        }

        // Allow 'B' meaning English B -> in our mapping NamesFrom60 includes "B" as idx 10, "H" idx 11.
        // If still not found, try single-letter mapping (E#, Fb etc are not supported).
        throw new ArgumentException($"Unknown note name '{name}'");
    }
}
