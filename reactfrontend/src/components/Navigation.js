import { Link } from 'react-router-dom';
import '../styles/Navigation.css';

export default function Navigation() {
    return (
        <nav>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/managers">Managers</Link>
                </li>
            </ul>
        </nav>
    );
}
