import { Link } from "react-router-dom";
import { Button } from 'react-bootstrap';

export const columnsChaffeurpage = [
    {
        name: '',
        cell: row => {
            const temp = '/Chauffeurpage/' + row.id;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'Name',
        selector: row => row.id,
    },
    {
        name: 'Chassis',
        selector: row => row.chassis,
    },
    {
        name: 'Model',
        selector: row => row.model
    },
    {
        name: 'Brand',
        selector: row => row.brand
    },
    {
        name: 'Type',
        selector: row => row.type
    },
    {
        name: 'Fueltype',
        selector: row => row.fuelType
    }
];