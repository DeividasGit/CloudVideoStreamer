import { useReducer, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  //const [count, setCount] = useState(0);
  const [count, incrementCount] = useReducer((count) => count + 1, 0);

  return (
    <button onClick={incrementCount}>
      { count }
    </button>
  );
}

export default App
