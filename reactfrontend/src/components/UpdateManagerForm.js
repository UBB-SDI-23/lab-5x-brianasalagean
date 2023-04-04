import React, { useEffect, useState } from 'react';
import axios from 'axios';

function UpdateManagerForm(props) {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [location, setLocation] = useState('');
    const [age, setAge] = useState('');
    const [yearsExp, setYearsExp] = useState('');

    useEffect(() => {
        axios.get(`https://localhost:7075/api/Manager/${props.managerId}`).then((response) => {
            setFirstName(response.data.manager.FirstName);
            setLastName(response.data.manager.LastName);
            setLocation(response.data.manager.Location);
            setAge(response.data.manager.Age);
            setYearsExp(response.data.manager.YearsExp);
        });
    }, [])

    function handleSubmit(event) {
        event.preventDefault();
        const id = props.managerId;
        const updatedManager = {
            id,
            firstName,
            lastName,
            location,
            age,
            yearsExp,
        };
        props.onUpdateManager(updatedManager);
    }

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>
                    First Name:
                    <input type="text" value={firstName} onChange={(event) => setFirstName(event.target.value)} />
                </label>
            </div>
            <div>
                <label>
                    Last Name:
                    <input type="text" value={lastName} onChange={(event) => setLastName(event.target.value)} />
                </label>
            </div>
            <div>
                <label>
                    Location:
                    <input type="text" value={location} onChange={(event) => setLocation(event.target.value)} />
                </label>
            </div>
            <div>
                <label>
                    Age:
                    <input type="number" value={age} onChange={(event) => setAge(event.target.value)} />
                </label>
            </div>
            <div>
                <label>
                    Years of Experience:
                    <input type="number" value={yearsExp} onChange={(event) => setYearsExp(event.target.value)} />
                </label>
            </div>
            <div>
                <button className="update__button2" type="submit">Update</button>
            </div>
        </form>
    );
}

export default UpdateManagerForm;
