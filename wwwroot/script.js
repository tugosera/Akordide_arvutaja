import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [result, setResult] = useState([]);
    const [chords, setChords] = useState("C F G C");

    useEffect(() => {
        fetch(`https://localhost:4444/Chord?chords=${encodeURIComponent(chords)}&format=names`)
            .then(res => res.json())
            .then(json => setResult(json.result));
    }, [chords]); // вызывается каждый раз, когда меняем аккорды

    return (
        <div className="App">
            <h2>Akordide Arvutaja</h2>

            <input
                type="text"
                value={chords}
                onChange={e => setChords(e.target.value)}
                placeholder="Например: C F G C"
            />

            <h3>Результат:</h3>
            <div>
                {result.join(", ")}
            </div>
        </div>
    );
}

export default App;
