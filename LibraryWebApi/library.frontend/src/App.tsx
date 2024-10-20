import React, { ChangeEvent, useState, MouseEvent, useEffect } from "react";
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import BookList from './Components/BookList/BookList'
import Search from './Components/Search/Search'

function App() {
    const [search, setSearch] = useState<string>("");

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
        console.log(e);
    };

    const onClick = (e: MouseEvent<HTMLButtonElement, MouseEvent>) => {
        console.log(e);
    };

    useEffect(() => {
        fetch('https://localhost:7076/book/getall')
            .then(response => response.json())
            .then(data => { console.log(data); })
    }, [])

    return (
        <div className="App">
            <Search onClick={onClick} search={search} handleChange={handleChange} />
            <BookList />
        </div>
    );
}
export default App