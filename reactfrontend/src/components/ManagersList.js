import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../styles/ManagersList.css'
import AddManagerForm from './AddManagerForm';
import UpdateManagerForm from './UpdateManagerForm';

export default function ManagersList() {
    const [managers, setManagers] = useState([]);
    const [showAddForm, setShowAddForm] = useState(false);
    const [yearsExp, setYearsExp] = useState(0);
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [selectedManager, setSelectedManager] = useState(null);

    useEffect(() => {
        axios.get('https://localhost:7075/api/Manager')
            .then(response => setManagers(response.data));
    }, []);

    const toggleAddForm = () => {
        setShowAddForm(!showAddForm);
    };

    const addManager = (manager) => {
        axios.post('https://localhost:7075/api/Manager', manager)
            .then(response => setManagers(response.data))
            .catch(error => {
                console.log(error);
            });
    };

    const handleFilterByYearsExp = () => {
        axios.get(`https://localhost:7075/api/Manager/YearsExp/${yearsExp}`)
            .then(response => setManagers(response.data));
    };

    const handleUpdateClick = (managerId) => {
        setSelectedManager(managerId);
        setShowUpdateForm(!showUpdateForm);
    };

    const handleDeleteClick = (managerId) => {
        axios.delete(`https://localhost:7075/api/Manager/${managerId}`)
            .then(() => {
                const updatedManagers = managers.filter(manager => manager.id !== managerId);
                setManagers(updatedManagers);
            });
    };

    const handleUpdateManager = (updatedManager) => {
        axios.put(`https://localhost:7075/api/Manager/${selectedManager}`, updatedManager)
            .then(response => {
                setManagers(response.data);
                setShowUpdateForm(false);
                setSelectedManager(null);
            })
            .catch(error => {
                console.log(error);
            });
    };

    const sortByYearsExp = () => {
        managers.sort((m1, m2) => (m1.yearsExp < m2.yearsExp) ? 1 : -1);
        setManagers([...managers]);
    }

    return (
        <div>
            <h1>Managers</h1>
            <button className="add__button" onClick={toggleAddForm}>Add Manager</button>
            {showAddForm && <AddManagerForm onAddManager={addManager} />}
            <input
                type="number"
                id="yearsExp"
                value={yearsExp}
                onChange={(e) => setYearsExp(e.target.value)}
            />
            <button className="filter__button" onClick={handleFilterByYearsExp}>Filter</button>
            <button className="sort__button" onClick={sortByYearsExp}>Sort</button>
            <table className="managers__table">
                <thead className="managers__table__header">
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Location</th>
                        <th>Age</th>
                        <th>Years of Experience</th>
                    </tr>
                </thead>
                <tbody className="managers__table__body">
                    {managers.map(manager => (
                        <tr key={manager.id}>
                            <td>{manager.firstName}</td>
                            <td>{manager.lastName}</td>
                            <td>{manager.location}</td>
                            <td>{manager.age}</td>
                            <td>{manager.yearsExp}</td>
                            <td><button className="update__button" onClick={() => handleUpdateClick(manager.id)}>Update</button></td>
                            <td><button className="delete__button" onClick={() => handleDeleteClick(manager.id)}>Delete</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {showUpdateForm && selectedManager && <UpdateManagerForm managerId={selectedManager} onUpdateManager={handleUpdateManager} />}
        </div>
    );
}
