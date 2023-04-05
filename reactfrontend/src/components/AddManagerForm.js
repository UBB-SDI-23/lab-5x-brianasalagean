import React, { useState } from 'react';
import axios from 'axios';
import '../styles/AddManagerForm.css'

export default function AddManagerForm(props) {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [location, setLocation] = useState('');
    const [age, setAge] = useState('');
    const [yearsExp, setYearsExp] = useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        const newManager = { firstName, lastName, location, age, yearsExp };
        props.onAddManager(newManager);
    };

    return (
        <form className="add__manager__form" onSubmit={handleSubmit}>
            <div>
                <label htmlFor="firstName">First Name:</label>
                <input type="text" id="firstName" value={firstName} onChange={(event) => setFirstName(event.target.value)} />
            </div>
            <div>
                <label htmlFor="lastName">Last Name:</label>
                <input type="text" id="lastName" value={lastName} onChange={(event) => setLastName(event.target.value)} />
            </div>
            <div>
                <label htmlFor="location">Location:</label>
                <input type="text" id="location" value={location} onChange={(event) => setLocation(event.target.value)} />
            </div>
            <div>
                <label htmlFor="age">Age:</label>
                <input type="text" id="age" value={age} onChange={(event) => setAge(event.target.value)} />
            </div>
            <div>
                <label htmlFor="yearsExp">Years of Experience:</label>
                <input type="text" id="yearsExp" value={yearsExp} onChange={(event) => setYearsExp(event.target.value)} />
            </div>
            <button className="add__button" type="submit">Add Manager</button>
        </form>
    );
}