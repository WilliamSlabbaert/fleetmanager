import { Link } from "react-router-dom";
import { Button } from 'react-bootstrap';

export const columnsRequestpage = [
    {
        name: '',
        cell: row => {
            const temp = '/Requestpage/' + row.id;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'ID',
        selector: row => row.id,
    },
    {
        name: 'Start date',
        selector: row => {
            const date = row.startDate.split(":");
            return date[0].replace("T", " ") + ":" + date[1]
        },
    },
    {
        name: 'End date',
        selector: row => {
            const date = row.endDate.split(":");
            return date[0].replace("T", " ") + ":" + date[1]
        }
    },
    {
        name: 'Status',
        selector: row => row.status
    },
    {
        name: 'Type',
        selector: row => row.type
    },
    {
        name: 'Vehicle ID',
        selector: row => row.vehicleId
    },
    {
        name: 'Chaffeur ID',
        selector: row => row.chaffeurId
    }
];

export const columnsVehicles = [
    {
        name: '',
        cell: row => {
            const temp = '/Carpage/' + row.id;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'ID',
        selector: row => row.id
    },
    {
        name: 'Chassis',
        selector: row => row.chassis
    },
    {
        name: 'Brand',
        selector: row => row.brand
    },
    {
        name: 'Model',
        selector: row => row.model
    },
    {
        name: 'Build date',
        selector: row => {
            return row.buildDate.split('T')[0]
        }
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

export const columnsChaffeurs = [
    {
        name: '',
        cell: row => {
            const temp = '/Chaffeurpage/' + row.id;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'Chaffeur ID',
        selector: row => {
            return row.id;
        },
    },
    {
        name: 'Name',
        selector: row => {
            return row.lastName + " " + row.firstName;
        },
    },
    {
        name: 'Address',
        selector: row => {
            return row.city + " " + row.street + " " + row.houseNumber;
        },
    },
    {
        name: 'Birth',
        selector: row => {
            const date = row.dateOfBirth.split("T")
            return date[0];
        },
    },
    {
        name: 'National insurence nr.',
        selector: row => {
            return row.nationalInsurenceNumber;
        },
    },
    {
        name: 'Working',
        selector: row => {
            if (row.isActive)
                return "Active"
            else
                return "Not active";
        }
    }
];