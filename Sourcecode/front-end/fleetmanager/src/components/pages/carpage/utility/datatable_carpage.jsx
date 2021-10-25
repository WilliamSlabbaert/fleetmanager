import { Link } from "react-router-dom";
import { Button } from 'react-bootstrap';

export const columnsCarpage = [
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
    },
    {
        name: 'Build date',
        selector: row => {
            return row.buildDate.split("T")[0]
        }
    }
];

export const columnsRequests = [
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
        selector: row => row.id
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
        name: 'Chaffeur ID',
        selector: row => row.chaffeurId
    }
];

export const columnsChaffeurs = [
    {
        name: '',
        cell: row => {
            const temp = '/Chaffeurpage/' + row.chaffeurId;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'Chaffeur ID',
        selector: row => {
            return row.chaffeurId;
        },
    },
    {
        name: 'Status',
        selector: row => {
            if (row.isActive)
                return "Active"
            else
                return "Not active";
        }
    }
];
export const columnsPlates = [
    {
        name: '',
        cell: row => {
            const temp = '/Licenseplatespage/' + row.chaffeurId;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'ID',
        selector: row => {
            return row.id;
        },
    },
    {
        name: 'Plate',
        selector: row => {
            return row.plate;
        },
    },
    {
        name: 'Status',
        selector: row => {
            if (row.isActive)
                return "Active"
            else
                return "Not active";
        }
    }
];
export const columnsKilometers = [
    {
        name: '',
        cell: row => {
            const temp = '/KilometerHistory/' + row.chaffeurId;
            return (
                <Link to={temp}>
                    <Button variant="dark" height="30px" width="30px">EDIT</Button>
                </Link>)
        }
    },
    {
        name: 'ID',
        selector: row => {
            return row.id;
        },
    },
    {
        name: 'Kilometers',
        selector: row => {
            return row.kilometers;
        },
    },
    {
        name: 'Date',
        selector: row => {
            return row.date.split("T")[0]
        }
    }
];