import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ManagersList from './components/ManagersList';
import Home from './components/Home';
import Navigation from './components/Navigation';

function App() {
    return (
        <div>
            <Navigation />
            <Routes>
                <Route exact path="/" element={<Home />} />
                <Route exact path="/managers" element={<ManagersList />} />
            </Routes>
            
        </div>
    );
}

export default App;
