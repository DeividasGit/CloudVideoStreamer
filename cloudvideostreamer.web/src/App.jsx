import { useReducer, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  //const [count, setCount] = useState(0);
  const [count, incrementCount] = useReducer((count) => count + 1, 0);

  return (
    <div className="container">
      <button className="btn" onClick={incrementCount}>
        { count }
      </button>
    </div>
  );
}

export default App
