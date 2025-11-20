async function getChord() {
    const chord = document.getElementById("chordSelect").value;

    // Здесь правильный URL для твоего контроллера
    const url = `/Chord?chords=${encodeURIComponent(chord)}&format=names`;

    const response = await fetch(url);
    const data = await response.json();

    document.getElementById("result").innerText =
        "Результат: " + data.result.join(", ");
}
